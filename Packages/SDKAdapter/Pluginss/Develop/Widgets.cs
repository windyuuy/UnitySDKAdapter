using System;
using System.Threading.Tasks;
using GDK;

namespace DevelopGDK
{
	public class KeyBoard : GDK.IKeyBoard
	{
		public Task ShowKeyboard(ShowKeyboardOptions options)
		{
			WidgetsBase.Devlog.Info(nameof(ShowKeyboard));
			return Task.CompletedTask;
		}

		public Task UpdateKeyboard(UpdateKeyboardOptions options)
		{
			WidgetsBase.Devlog.Info(nameof(UpdateKeyboard));
			return Task.CompletedTask;
		}

		public Task HideKeyboard()
		{
			WidgetsBase.Devlog.Info(nameof(HideKeyboard));
			return Task.CompletedTask;
		}

		public void OnKeyboardInputEvent(Action<OnKeyboardInputResult> callback)
		{
			WidgetsBase.Devlog.Info(nameof(OnKeyboardInputEvent));
		}

		public void OffKeyboardInputEvent(Action<OnKeyboardInputResult> callback)
		{
			WidgetsBase.Devlog.Info(nameof(OffKeyboardInputEvent));
		}

		public void OnKeyboardConfirm(Action<OnKeyboardConfirmResult> callback)
		{
			WidgetsBase.Devlog.Info(nameof(OnKeyboardConfirm));
		}

		public void OffKeyboardConfirm(Action<OnKeyboardConfirmResult> callback)
		{
			WidgetsBase.Devlog.Info(nameof(OffKeyboardConfirm));
		}

		public void OnKeyboardComplete(Action<OnKeyboardCompleteResult> callback)
		{
			WidgetsBase.Devlog.Info(nameof(OnKeyboardComplete));
		}

		public void OffKeyboardComplete(Action<OnKeyboardCompleteResult> callback)
		{
			WidgetsBase.Devlog.Info(nameof(OffKeyboardComplete));
		}
	}

	public class Widgets : GDK.WidgetsBase
	{
		public override GDK.IKeyBoard keyboard { get; } = new KeyBoard();

		public override Task ShowLoading(GDK.ShowLoadingParams obj)
		{
			Devlog.Info("showLoading");
			return Task.CompletedTask;
		}

		public override Task HideLoading()
		{
			Devlog.Info("hideLoading");
			return Task.CompletedTask;
		}

		public override Task showToast(GDK.ShowToastOptions obj)
		{
			Devlog.Info("showToast");
			return Task.CompletedTask;
		}

		public override Task hideToast()
		{
			Devlog.Info("hideToast");
			return Task.CompletedTask;
		}

		public override Task<GDK.ShowModalResult> ShowModal(GDK.ShowModalOptions obj)
		{
			var ts = new TaskCompletionSource<GDK.ShowModalResult>();
			var result = obj.title + ";" + obj.content;
			var r = new GDK.ShowModalResult();
			r.confirm = true;
			r.cancel = false;
			r.result = result;
			ts.SetResult(r);
			return ts.Task;
		}

		public override Task hideLaunchingView()
		{
			return Task.CompletedTask;
		}
	}
}