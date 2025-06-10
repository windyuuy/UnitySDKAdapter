
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

		public override void setFPS(int fps)
		{
			Application.targetFrameRate = fps;
		}

		public override void onMemoryWarning(Action<GDK.IOnMemoryWarningResult> call)
		{
		}

		public override IClipboard clipboard { get; set; } = new Clipboard();
	}
}
