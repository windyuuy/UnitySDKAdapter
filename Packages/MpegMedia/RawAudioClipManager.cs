using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

#if !UNITY_WEBGL || UNITY_EDITOR
using AudioMedia = MpegMedia.MpegReader;
#else
public class AudioMedia:IDisposable
{
    public void Dispose()
    {
        
    }
}
#endif

namespace MpegMedia.Audio
{
    public class RawAudioClipLoader : IDisposable
    {
        public AudioClip AudioClip;
        public AudioMedia MpegFile;
        public AudioSource AudioSource;
        public Task<AudioClip> LoadAudioClipTask;

        public Task<AudioClip> LoadMpegFile(string path, Func<string, Task<byte[]>> loader, AudioSource bgmSource)
        {
            if (LoadAudioClipTask == null)
            {
#if !UNITY_WEBGL || UNITY_EDITOR
                LoadAudioClipTask = _loadMpegFileOnce(path, loader, bgmSource);
#endif
            }

            return LoadAudioClipTask;
        }

        protected bool NeedReset = false;
        protected bool Inited = false;
        protected bool NeedBreak = false;

#if !UNITY_WEBGL || UNITY_EDITOR
        private async Task<AudioClip> _loadMpegFileOnce(string path, Func<string, Task<byte[]>> loader,
            AudioSource bgmSource)
        {
            AudioSource = bgmSource;
            if (AudioClip == null)
            {
                var filename = Path.GetFileNameWithoutExtension(path) + ".mp3";

                var bytes = await loader(path);
                Debug.Log($"loaded: {path}");
                var memoryStream = new MemoryStream(bytes);
                MpegFile = new AudioMedia(memoryStream);
                Debug.Log($"decode MpegFile: {path}");
                if (MpegFile.Channels != 1)
                {
                    throw new NotImplementedException($"请使用单声道音频: {path}");
                }

                var supportStream = true;
#if UNITY_WEBGL
                supportStream = false;
#endif
                AudioClip = AudioClip.Create(filename,
                    (int)MpegFile.Length/sizeof(float),
                    MpegFile.Channels,
                    MpegFile.SampleRate,
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    supportStream,
                    (data) =>
                    {
                        MpegFile.ReadSamples(data, 0, data.Length);
                        ;
                    },
                    (position) =>
                    {
                        // MpegFile.SamplePosition = position;
                        ;
                        MpegFile = new AudioMedia(memoryStream);
                    }
                );
            }

            return AudioClip;
        }
#endif
        
        //
        // private async Task<AudioClip> _loadMpegFilePartially(string path, Func<string, Task<byte[]>> loader,
        //     AudioSource bgmSource)
        // {
        //     AudioSource = bgmSource;
        //     if (AudioClip == null)
        //     {
        //         var filename = Path.GetFileNameWithoutExtension(path) + ".mp3";
        //
        //         var bytes = await loader(path);
        //         Debug.Log($"loaded: {path}");
        //         MpegFile = new AudioMedia(new MemoryStream(bytes));
        //         Debug.Log($"decode MpegFile: {path}");
        //         if (MpegFile.Channels != 1)
        //         {
        //             throw new NotImplementedException($"请使用单声道音频: {path}");
        //         }
        //
        //         // if (Application.platform == RuntimePlatform.OSXPlayer)
        //         {
        //             var setOnceOnly = MpegFile.TotalTime.TotalSeconds <= 6;
        //             if (setOnceOnly)
        //             {
        //                 AudioClip = AudioClip.Create(filename,
        //                     (int)MpegFile.TotalSamples,
        //                     MpegFile.Channels,
        //                     MpegFile.SampleRate,
        //                     false,
        //                     (data) =>
        //                     {
        //                         MpegFile.ReadSamples(data, 0, data.Length);
        //                         ;
        //                     },
        //                     (position) =>
        //                     {
        //                         MpegFile.SamplePosition = position;
        //                         ;
        //                     }
        //                 );
        //                 return AudioClip;
        //             }
        //
        //             int clipLen0 = MpegFile.SampleRate;
        //             int addLen0 = 20000;
        //             AudioClip = AudioClip.Create(filename,
        //                 clipLen0 + addLen0,
        //                 MpegFile.Channels,
        //                 MpegFile.SampleRate,
        //                 false,
        //                 null,
        //                 null
        //             );
        //             var buff = new float[clipLen0 + addLen0];
        //
        //             async Task Load()
        //             {
        //                 while (true)
        //                 {
        //                     if (!Inited
        //                         || (Inited && NeedReset)
        //                         || (
        //                             AudioSource != null
        //                             && AudioSource.timeSamples >= clipLen0
        //                             && AudioSource.clip == AudioClip
        //                             && AudioSource.isPlaying
        //                         ))
        //                     {
        //                         NeedReset = false;
        //                         var t1 = Date.Now();
        //                         var readLen = MpegFile.ReadSamples(buff, 0, buff.Length);
        //                         if (readLen < buff.Length)
        //                         {
        //                             // 基于音频长度大于 clipLen0
        //                             MpegFile.SamplePosition = 0;
        //                             MpegFile.ReadSamples(buff, readLen, buff.Length - readLen);
        //                         }
        //
        //                         MpegFile.SamplePosition =
        //                             (MpegFile.SamplePosition - addLen0 + MpegFile.TotalSamples) % MpegFile.TotalSamples;
        //
        //                         AudioClip.SetData(buff, 0);
        //                         var t2 = Date.Now();
        //                         // Debug.Log($"ReadSamples-TimeCost: {t2 - t1}");
        //                         var timeSamples = AudioSource.timeSamples - clipLen0;
        //                         if (timeSamples < 0)
        //                         {
        //                             timeSamples = 0;
        //                         }
        //
        //                         AudioSource.timeSamples = timeSamples;
        //                     }
        //
        //                     await AsyncUtils.WaitForFrames();
        //                     Inited = true;
        //                     if (NeedBreak)
        //                     {
        //                         break;
        //                     }
        //                 }
        //             }
        //
        //             Load();
        //
        //             return AudioClip;
        //         }
        //     }
        //
        //     return AudioClip;
        // }

        public void ResetAudioClip()
        {
            if (!Inited)
            {
                return;
            }

            NeedReset = true;
#if USE_OGG_BGM
            MpegFile.SamplePosition = 0;
#endif
            AudioSource.timeSamples = 0;
        }

        protected void ReleaseTempMemory()
        {
            if (MpegFile != null)
            {
                MpegFile.Dispose();
                MpegFile = null;
            }
        }

        public void Dispose()
        {
            if (MpegFile != null)
            {
                MpegFile.Dispose();
                MpegFile = null;
            }

            if (AudioClip != null)
            {
                GameObject.Destroy(AudioClip);
                AudioClip = null;
            }

            AudioSource = null;
            LoadAudioClipTask = null;

            NeedBreak = true;
        }

        public Task PlayAudio(string path, Func<string, Task<byte[]>> loader, AudioSource bgmSource)
        {
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
            return PlayNativeAudio(path, loader, bgmSource);
#else
            return PlayUnityAudio(path, loader, bgmSource);
#endif
        }

        public Task StopAudio()
        {
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
            return StopNativeAudio();
#else
            return StopUnityAudio();
#endif
        }

        public void MuteAudio(bool mute)
        {
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
            _ = MuteNativeAudio(mute);
#else
            _ = MuteUnityAudio(mute);
#endif
        }
        public void SetVolume(float vol)
        {
#if UNITY_WEBGL && SUPPORT_WECHATGAME && !UNITY_EDITOR
            _ = SetVolumeNative(vol);
#else
            _ = SetVolumeUnity(vol);
#endif
        }

        private Task SetVolumeUnity(float vol)
        {
            if (AudioSource != null)
            {
                AudioSource.volume = vol;
            }

            return Task.CompletedTask;
        }

        public async Task PlayUnityAudio(string bgmPath, Func<string, Task<byte[]>> loader, AudioSource bgmSource)
        {
            AudioClip clip;
            clip = await RawAudioClipManager.LoadAudioClip(bgmPath, loader, bgmSource);

            if (clip == null) return;
            if (bgmSource.clip == clip)
            {
                return;
            }

            RawAudioClipManager.ResetAudioClip(bgmPath);

            bgmSource.clip = clip;
            bgmSource.Play();
        }

        public async Task StopUnityAudio()
        {
            if (AudioSource != null)
            {
                AudioSource.Stop();
            }
        }

        public Task MuteUnityAudio(bool mute)
        {
            if (AudioSource != null)
            {
                AudioSource.mute = mute;
            }

            return Task.CompletedTask;
        }

#if UNITY_WEBGL && SUPPORT_WECHATGAME
        protected Task<WeChatWASM.WXInnerAudioContext> AudioContextSource;
        protected static WeChatWASM.WXInnerAudioContext SharedAudioContext;
        protected string AudioUri;

        public async Task PlayNativeAudio(string path, Func<string, Task<byte[]>> loader, AudioSource bgmSource)
        {
            AudioSource = bgmSource;
            if (AudioContextSource == null)
            {
                async Task<WeChatWASM.WXInnerAudioContext> CreateAudioSource()
                {
                    WeChatWASM.WXInnerAudioContext audio = null;
                    var bytes = await loader(path);
                    Debug.Log($"loaded-native: {path}");

                    var filename = Path.GetFileNameWithoutExtension(path) + ".mp3";
                    var filePath = $"{WeChatWASM.WX.env.USER_DATA_PATH}/{filename}";

                    var ts = new TaskCompletionSource<bool>();
                    var wxfs = WeChatWASM.WX.GetFileSystemManager();
                    Debug.Log($"writefile: {path}");
                    AudioUri = filePath;
                    wxfs.WriteFile(new WeChatWASM.WriteFileParam
                    {
                        filePath = filePath,
                        data = bytes,
                        success = (res) =>
                        {
                            Debug.Log($"WriteFile-ok: {path}");
                            ts.SetResult(true);
                        },
                        fail = (err) =>
                        {
                            Debug.LogError($"播放音频失败: {err.errCode}, {err.errMsg}");
                            ts.SetResult(false);
                        },
                    });

                    await ts.Task;

                    return audio;
                }

                AudioContextSource = CreateAudioSource();
            }
            if (!AudioContextSource.IsCompleted)
            {
                await AudioContextSource;
            }
            if (SharedAudioContext == null)
            {
                SharedAudioContext = WeChatWASM.WX.CreateInnerAudioContext(new()
                {
                    // src = filePath,
                    loop = false,
                    startTime = 0,
                    autoplay = false,
                    volume = AudioSource.volume,
                    playbackRate = 1,
                    needDownload = false,
                });
            }

            SharedAudioContext.src = AudioUri;
            // Debug.Log($"playaudio: {path}");
            SharedAudioContext.Play();
        }

        public async Task StopNativeAudio()
        {
            if (AudioContextSource != null)
            {
                var audioContext = await AudioContextSource;
                audioContext.Stop();
            }
        }

        public async Task MuteNativeAudio(bool mute)
        {
            if (AudioContextSource != null)
            {
                var audioContext = await AudioContextSource;
                audioContext.mute = mute;
            }
        }
        
        private async Task SetVolumeNative(float vol)
        {
            // Debug.Log($"SetVolume3: {vol}");
            if (AudioContextSource != null)
            {
                var audioContext = await AudioContextSource;
                Debug.Log($"SetVolumeNative: {vol}");
                audioContext.volume = vol;
            }
        }

#endif
    }

    public class RawAudioClipManager : IDisposable
    {
        protected static Dictionary<string, RawAudioClipLoader> AudioClipLoaders = new();

        public static Task<AudioClip> LoadAudioClip(string path, Func<string, Task<byte[]>> loader,
            AudioSource bgmSource)
        {
            if (!AudioClipLoaders.TryGetValue(path, out var audioClipLoader))
            {
                audioClipLoader = new RawAudioClipLoader();
                AudioClipLoaders.Add(path, audioClipLoader);
            }

            return audioClipLoader.LoadMpegFile(path, loader, bgmSource);
        }

        public void Dispose()
        {
            foreach (var rawAudioClipLoader in AudioClipLoaders)
            {
                rawAudioClipLoader.Value.Dispose();
            }

            AudioClipLoaders.Clear();
        }

        public static void ResetAudioClip(string path)
        {
            if (AudioClipLoaders.TryGetValue(path, out var audioClipLoader))
            {
                audioClipLoader.ResetAudioClip();
            }
        }

        protected static RawAudioClipLoader CurRawAudioClipLoader;

        public static RawAudioClipLoader LoadAudio(string path)
        {
            if (!AudioClipLoaders.TryGetValue(path, out var audioClipLoader))
            {
                audioClipLoader = new RawAudioClipLoader();
                AudioClipLoaders.Add(path, audioClipLoader);
            }

            CurRawAudioClipLoader = audioClipLoader;
            return CurRawAudioClipLoader;
        }

        public static async Task<RawAudioClipLoader> PlayAudio(string path, Func<string, Task<byte[]>> loader, AudioSource bgmSource)
        {
            var curClip = CurRawAudioClipLoader;
            var audio  = RawAudioClipManager.LoadAudio(path);
            // foreach (var rawAudioClipLoader in AudioClipLoaders)
            // {
            //     if (
            //         rawAudioClipLoader.Value.AudioSource==bgmSource && 
            //         rawAudioClipLoader.Value != audio)
            //     {
            //         rawAudioClipLoader.Value.StopAudio();
            //     }
            // }

            if (curClip != audio || curClip?.AudioSource != bgmSource)
            {
                await audio.PlayAudio(path,loader,bgmSource);
            }
            return audio;
        }

        public static void StopAudio()
        {
            if (CurRawAudioClipLoader != null)
            {
                CurRawAudioClipLoader.StopAudio();
            }
        }
        
        public static void MuteAudio(bool mute)
        {
            if (CurRawAudioClipLoader != null)
            {
                CurRawAudioClipLoader.MuteAudio(mute);
            }
        }

        public static void SetVolume(float vol)
        {
            // Debug.Log($"SetVolume4: {vol}");
            if (CurRawAudioClipLoader != null)
            {
                CurRawAudioClipLoader.SetVolume(vol);
            }
        }
    }
}