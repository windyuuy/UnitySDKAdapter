#if UNITY_EDITOR || true
using System;
using System.Threading.Tasks;
using GDK;
using MonoExtLib.AsyncExt;
using UnityEngine;

namespace DevelopGDK
{
	public class SystemAPI : GDK.SystemAPIBase
	{
		public override void Init()
		{
			base.Init();
		}

		public override void SetFPS(int fps)
		{
			Application.targetFrameRate = fps;
		}

		public override void OnMemoryWarning(Action<GDK.IOnMemoryWarningResult> call)
		{
		}

		public override IClipboard Clipboard { get; set; } = new Clipboard();

		public override async Task<RestartMiniProgramResult> RestartMiniProgram(RestartMiniProgramOptions options)
		{
			await UniAsyncUtils.JoinMainThread();

		#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
			// UnityEditor.EditorApplication.EnterPlaymode();
		#else
			Application.Quit();
		#endif
			return new RestartMiniProgramResult();
		}

		#if UNITY_EDITOR
		[UnityEditor.MenuItem("Tools/Test/LaunchOptionsSync/Scene/正常启动")]
		private static void TestLaunchFromScene正常启动()
		{
			UnityEngine.PlayerPrefs.SetString(Key_UserAPI_GetLaunchOptionsSync_Scene, "");
		}
		[UnityEditor.MenuItem("Tools/Test/LaunchOptionsSync/Scene/从小程序启动1023")]
		private static void TestLaunchFromScene1023()
		{
			UnityEngine.PlayerPrefs.SetString(Key_UserAPI_GetLaunchOptionsSync_Scene, "1023");
		}
		[UnityEditor.MenuItem("Tools/Test/LaunchOptionsSync/Scene/从桌面启动1104")]
		private static void TestLaunchFromScene1104()
		{
			UnityEngine.PlayerPrefs.SetString(Key_UserAPI_GetLaunchOptionsSync_Scene, "1104");
		}
		#endif
		internal const string Key_UserAPI_GetLaunchOptionsSync_Scene = "UserAPI::GetLaunchOptionsSync::scene";
		public override LaunchOptions GetLaunchOptionsSync()
		{
			var launchOptionsSync = base.GetLaunchOptionsSync();
			var scene = UnityEngine.PlayerPrefs.GetString(Key_UserAPI_GetLaunchOptionsSync_Scene, "");
			if (!string.IsNullOrEmpty(scene))
			{
				launchOptionsSync.Scene = scene;
			}
			return launchOptionsSync;
		}
	}
}
#endif
