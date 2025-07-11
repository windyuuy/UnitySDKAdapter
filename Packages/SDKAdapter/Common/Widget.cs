
using System.Threading.Tasks;
using Lang.Loggers;

namespace GDK
{

	public abstract class WidgetsBase : GDK.IWidgets
	{
		// const devlog = new lang.libs.Log({ tags: ["DEVELOP"] })
		public static Logger Devlog = new Logger();
		/**
		 * 显示启动画面
		 */
		public void showLaunchingView()
		{
			Devlog.Info("showLaunchingView simulation not implemented.");
		}

		/** 系统键盘对象 */
		public abstract IKeyBoard keyboard { get; }

		/** 显示 loading 提示框。需主动调用 wx.hideLoading 才能关闭提示框 */
		public abstract Task showLoading(ShowLoadingParams obj);
		/** 隐藏 loading 提示框 */
		public abstract Task hideLoading();
		/** 显示消息提示框 */
		public abstract Task showToast(ShowToastOptions obj);
		/** 隐藏消息提示框 */
		public abstract Task hideToast();
		/**
		 * 显示模态对话框
		 * - 有`确定`和`取消`两个按钮
		 */
		public abstract Task<ShowConfirmResult> showConfirm(ShowConfirmOptions obj);

		/**
		 * 显示模态对话框
		 * - 有`确定`和`取消`两个按钮
		 */
		public abstract Task<ShowPromptResult> showPrompt(ShowPromptOptions obj);

		/**
		 * 显示模态对话框
		 * - 只有`确定`一个按钮
		 */
		public abstract Task<ShowAlertResult> showAlert(ShowAlertOptions obj);

		/**
		 * 隐藏启动画面
		 */
		public abstract Task hideLaunchingView();
	}
}
