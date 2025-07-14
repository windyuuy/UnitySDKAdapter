
using System.Threading.Tasks;

namespace GDK
{

	public class ShowConfirmResult
	{
		/**
		 * 点击了确定按钮
		 */
		public bool confirm;
		/**
		 * 点击了取消按钮
		 */
		public bool cancel;

		/**
		 * 原始数据
		 */
		public object extra;
	}
	public class ShowPromptResult
	{
		/**
		 * 点击了确定按钮
		 */
		public bool confirm;
		/**
		 * 点击了取消按钮
		 */
		public bool cancel;

		/**
		 * 输入结果
		 */
		public string result;

		/**
		 * 原始数据
		 */
		public object extra;
	}

	public class ShowAlertResult
	{
		/**
		 * 原始数据
		 */
		public object extra;
	}

	public class ShowLoadingParams
	{
		/** 提示的内容 */
		public string title;
		public bool? mask;
	}
	public interface ShowToastOptions
	{

		/**
		 * 提示的内容
		 */
		public string title { get; }

		/**
		 * 提示的延迟时间，单位毫秒，默认：1500, 最大为10000
		 */
		public double duration { get; }
	}
	public interface ShowModalResult
	{

		/**
		 * confirm==1时，表示用户点击确定按钮
		 */
		public double confirm { get; }
	}
	public interface ShowConfirmOptions
	{

		/**
		 * 提示的标题
		 */
		public string title { get; }

		/**
		 * 提示的内容
		 */
		public string content { get; }

		/**
		 * 确认按钮文字
		 */
		public string okLabel { get; }

		/**
		 * 取消按钮文字
		 */
		public string cancelLabel { get; }
	}

	public interface ShowPromptOptions
	{

		/**
		 * 提示的标题
		 */
		public string title { get; }

		/**
		 * 提示的内容
		 */
		public string content { get; }

		/**
		 * 确认按钮文字
		 */
		public string okLabel { get; }

		/**
		 * 取消按钮文字
		 */
		public string cancelLabel { get; }

		/**
		 * 默认值
		 */
		public string defaultValue { get; }
	}

	public interface ShowAlertOptions
	{

		/**
		 * 提示的标题
		 */
		public string title { get; }

		/**
		 * 提示的内容
		 */
		public string content { get; }

		/**
		 * 确认按钮文字
		 */
		public string okLabel { get; }
	}

	public interface IKeyBoard
	{
		/** 隐藏键盘 */
		public Task hideKeyboard();
	}

	/**
	 * 各种UI小部件调用
	 */
	public interface IWidgets
	{
		/** 系统键盘对象 */
		public IKeyBoard keyboard { get; }

		/** 显示 loading 提示框。需主动调用 wx.hideLoading 才能关闭提示框 */
		public Task showLoading(ShowLoadingParams obj);
		/** 隐藏 loading 提示框 */
		public Task hideLoading();
		/** 显示消息提示框 */
		public Task showToast(ShowToastOptions obj);
		/** 隐藏消息提示框 */
		public Task hideToast();
		/**
		 * 显示模态对话框
		 * - 有`确定`和`取消`两个按钮
		 */
		public Task<ShowConfirmResult> showConfirm(ShowConfirmOptions obj);

		/**
		 * 显示模态对话框
		 * - 有`确定`和`取消`两个按钮
		 */
		public Task<ShowPromptResult> showPrompt(ShowPromptOptions obj);

		/**
		 * 显示模态对话框
		 * - 只有`确定`一个按钮
		 */
		public Task<ShowAlertResult> showAlert(ShowAlertOptions obj);

		/**
		 * 隐藏启动画面
		 */
		public Task hideLaunchingView();

		/**
		 * 显示启动画面
		 */
		public void showLaunchingView();
	}
}
