using System.Threading.Tasks;
using GDK;

namespace DevelopGDK
{
	public class KeyBoard : GDK.IKeyBoard
	{
		public async Task hideKeyboard()
		{
			WidgetsBase.Devlog.Info("hideKeyboard");
		}
	}

	public class Widgets : GDK.WidgetsBase
	{
		public override GDK.IKeyBoard keyboard { get; } = new KeyBoard();

		public override Task showLoading(GDK.ShowLoadingParams obj)
		{
			Devlog.Info("showLoading");
			return Task.CompletedTask;
		}

		public override Task hideLoading()
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

		public override Task<GDK.ShowConfirmResult> showConfirm(GDK.ShowConfirmOptions obj)
		{
			var ts = new TaskCompletionSource<GDK.ShowConfirmResult>();
			var r = new GDK.ShowConfirmResult();
			r.confirm = true;
			r.cancel = false;
			ts.SetResult(r);
			return ts.Task;
		}

		public override Task<GDK.ShowAlertResult> showAlert(GDK.ShowAlertOptions obj)
		{
			var ts = new TaskCompletionSource<GDK.ShowAlertResult>();
			var r = new GDK.ShowAlertResult();
			ts.SetResult(r);
			return ts.Task;
		}

		public override Task<GDK.ShowPromptResult> showPrompt(GDK.ShowPromptOptions obj)
		{
			var ts = new TaskCompletionSource<GDK.ShowPromptResult>();
			var result = obj.title + ";" + obj.content;
			var r = new GDK.ShowPromptResult();
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