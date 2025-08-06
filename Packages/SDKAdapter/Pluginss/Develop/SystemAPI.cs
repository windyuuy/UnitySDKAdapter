#if UNITY_EDITOR
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

		public override void GetFPS(int fps)
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
	}
}
#endif
