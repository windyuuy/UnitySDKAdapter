#if UNITY_EDITOR
using System;
using System.Threading.Tasks;
using GDK;

namespace DevelopGDK
{
	public class KeyBoard : GDK.IKeyBoard
	{
		public Task ShowKeyboard(ShowKeyboardOptions options)
		{
			DevLog.Instance.Info(nameof(ShowKeyboard));
			return Task.CompletedTask;
		}

		public Task UpdateKeyboard(UpdateKeyboardOptions options)
		{
			DevLog.Instance.Info(nameof(UpdateKeyboard));
			return Task.CompletedTask;
		}

		public Task HideKeyboard()
		{
			DevLog.Instance.Info(nameof(HideKeyboard));
			return Task.CompletedTask;
		}

		public void OnKeyboardInputEvent(Action<OnKeyboardInputResult> callback)
		{
			DevLog.Instance.Info(nameof(OnKeyboardInputEvent));
		}

		public void OffKeyboardInputEvent(Action<OnKeyboardInputResult> callback)
		{
			DevLog.Instance.Info(nameof(OffKeyboardInputEvent));
		}

		public void OnKeyboardConfirm(Action<OnKeyboardConfirmResult> callback)
		{
			DevLog.Instance.Info(nameof(OnKeyboardConfirm));
		}

		public void OffKeyboardConfirm(Action<OnKeyboardConfirmResult> callback)
		{
			DevLog.Instance.Info(nameof(OffKeyboardConfirm));
		}

		public void OnKeyboardComplete(Action<OnKeyboardCompleteResult> callback)
		{
			DevLog.Instance.Info(nameof(OnKeyboardComplete));
		}

		public void OffKeyboardComplete(Action<OnKeyboardCompleteResult> callback)
		{
			DevLog.Instance.Info(nameof(OffKeyboardComplete));
		}
	}

	public class Widgets : GDK.WidgetsBase
	{
		public override GDK.IKeyBoard keyboard { get; } = new KeyBoard();

		public override Task ShowLoading(GDK.ShowLoadingParams obj)
		{
			DevLog.Instance.Info("showLoading");
			return Task.CompletedTask;
		}

		public override Task HideLoading()
		{
			DevLog.Instance.Info("hideLoading");
			return Task.CompletedTask;
		}

		public override Task<ShowWidgetResult> ShowToast(ShowToastOptions obj)
		{
			DevLog.Instance.Info("showToast");
			return Task.FromResult(new ShowWidgetResult()
			{
				IsOk = true,
			});
		}

		public override Task hideToast()
		{
			DevLog.Instance.Info("hideToast");
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
#endif
