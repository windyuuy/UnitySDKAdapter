using System;
using System.IO;
using System.Threading.Tasks;
using MonoExtLib.AsyncExt;
using UnityEngine;
using UnityEngine.Networking;

#if UNITY_WEBGL && SUPPORT_WECHATGAME
using WeChatWASM;
#endif

namespace MpegMedia.Vedio
{
    public interface IFullScreenVideoPlayer
    {
        public Task<bool> PreDownload();

        /// <summary>
        /// 清理本地视频文件缓存
        /// </summary>
        /// <returns></returns>
        public Task<bool> RemoveCache();

        public Task Play();
    }

    // http://127.0.0.1:8080/aa.mp4
    public class FullScreenVideoPlayer : IFullScreenVideoPlayer
    {
        public string Uri;

        public FullScreenVideoPlayer(string uri)
        {
            Uri = uri;
        }

        protected bool IsDownloading = false;
        protected bool IsDownloaded = false;
        protected Task<bool> DownloadTask;

        public Task<bool> PreDownload()
        {
            if (!IsDownloaded && DownloadTask == null)
            {
                DownloadTask = _preDownload();
            }

            Debug.Log($"Application.platform: {Application.platform}");

            return DownloadTask;
        }

        private async Task<bool> _preDownload()
        {
#if UNITY_WEBGL && SUPPORT_WECHATGAME
            var wxfs = WX.GetFileSystemManager();

            var path = GetFilePath();
            if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer)
            {
                if (File.Exists(path))
                {
                    return true;
                }
            }
            else
            {
                if (wxfs.AccessSync(path) == "access:ok")
                {
                    return true;
                }
            }

            IsDownloading = true;
            var uwr = UnityWebRequest.Get(Uri);
            uwr.timeout = 45;
            await uwr.SendWebRequest().GetTask();
            if (uwr.isDone && uwr.result == UnityWebRequest.Result.Success)
            {
                if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    var downloadHandler = uwr.downloadHandler as DownloadHandlerBuffer;
                    try
                    {
                        File.WriteAllBytes(path, downloadHandler!.data);
                        IsDownloaded = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
                else
                {
                    var ts = new TaskCompletionSource<bool>();
                    var downloadHandler = uwr.downloadHandler as DownloadHandlerBuffer;
                    var bytes = downloadHandler!.data;
                    WX.CleanFileCache((int)uwr.downloadedBytes, (res) =>
                    {
                        wxfs.WriteFile(new WriteFileParam()
                        {
                            filePath = GetFilePath(),
                            data = bytes,
                            fail = (err) =>
                            {
                                Debug.LogError(err.errMsg);
                                ts.SetResult(false);
                            },
                            success = (res) =>
                            {
                                // success
                                ts.SetResult(true);
                            },
                        });
                    });
                    var saveResult = await ts.Task;
                    IsDownloaded = saveResult;
                }

                IsDownloading = false;

                return IsDownloaded;
            }
            return false;
#endif
            return false;
        }

        private string GetFilePath()
        {
            var fileName = Path.GetFileName(Uri);
            if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer)
            {
                var path = $"{Application.persistentDataPath}/{fileName}";
                return path;
            }
            else
            {
#if UNITY_WEBGL && SUPPORT_WECHATGAME
                var path = $"{WX.env.USER_DATA_PATH}/__GAME_FILE_CACHE/{fileName}";
                return path;
#endif
                return "";
            }
        }

        /// <summary>
        /// 清理本地视频文件缓存
        /// </summary>
        /// <returns></returns>
        public Task<bool> RemoveCache()
        {
            if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer)
            {
                try
                {
                    var path = GetFilePath();
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    return Task.FromResult(true);
                }
                catch (Exception ex)
                {
                    return Task.FromResult(false);
                }
            }
            else
            {
#if UNITY_WEBGL && SUPPORT_WECHATGAME
                var ts = new TaskCompletionSource<bool>();
                WX.RemoveFile(GetFilePath(), (ok) => { ts.SetResult(ok); });
                return ts.Task;
#endif
                return Task.FromResult(false);
            }
        }

        public async Task Play()
        {
            Debug.Log($"play video: {Uri}");

#if UNITY_WEBGL && SUPPORT_WECHATGAME
            // var isDownloaded = await PreDownload();
            if (DownloadTask != null)
            {
                await DownloadTask;
            }

            string uri;
            if (IsDownloaded)
            {
                uri = GetFilePath();
            }
            else
            {
                uri = Uri;
            }

            Debug.Log($"play video source: {uri}");

            if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer)
            {
                return;
            }

            Debug.Log("WX.GetWindowInfo();");
            var size = WX.GetWindowInfo();
            Debug.Log("WX.CreateVideo();");
            var video = WX.CreateVideo(new()
            {
                src = uri,
                objectFit = "cover",
                controls = false,
                showCenterPlayBtn = false,
                //   obeyMuteSwitch=true,
                width = Mathf.CeilToInt((float)size.windowWidth),
                height = Mathf.CeilToInt((float)size.windowHeight),
                autoplay = true,
            });
            Debug.Log("set video.OnEnded0();");
            video.OnEnded(() =>
            {
                Debug.Log("video.OnEnded();");
                video.Destroy();
                Debug.Log("video.Destroy();");
            });
            Debug.Log("set video.OnEnded1();");
#endif
        }
    }

    public class VideoPlayerManager
    {
        public static IFullScreenVideoPlayer CreateFullScreenVideoPlayer(string uri)
        {
            return new FullScreenVideoPlayer(uri);
        }
    }
}