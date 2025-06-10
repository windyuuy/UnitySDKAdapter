
using System;
using GDK;
using UnityEngine;

namespace DevelopGDK
{

	public class SystemAPI : GDK.SystemAPIBase
	{
		public override void init()
		{
			base.init();
		}

		public override void GetFPS(int fps)
		{
			Application.targetFrameRate = fps;
		}

		public override void OnMemoryWarning(Action<GDK.IOnMemoryWarningResult> call)
		{
		}

		public override IClipboard Clipboard { get; set; } = new Clipboard();
	}
}
