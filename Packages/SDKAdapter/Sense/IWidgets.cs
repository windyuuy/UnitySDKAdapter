using System;
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

	public class ShowModalResult
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
		public string errMsg;

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

	[Serializable]
	public class ShowLoadingResult
	{
		/** 提示的内容 */
		public string errMsg;
	}

	[Serializable]
	public class ShowWidgetResult
	{
		public bool IsOk;
		/** 提示的内容 */
		public string errMsg;
	}

	[Serializable]
	public class ShowWidgetParas
	{
		/** 提示的内容 */
		public string errMsg;
	}

	[Serializable]
	public class ShowLoadingParams
	{
		/** 提示的内容 */
		public string title;

		public bool? mask;
	}

	[Serializable]
	public class ShowToastOptions
	{
		/**
		 * 提示的内容
		 */
		public string title;

		/**
		 * 提示的延迟时间，单位毫秒，默认：1500, 最大为10000
		 */
		public double duration;

		/// <summary>
		/// 图标，取值范围包括success, loading, none, fail(1.36.0版本支持)
		/// </summary>
		public string icon;

		public bool? mask;
		public string image;
	}

	[Serializable]
	public class ShowConfirmOptions
	{
		/**
		 * 提示的标题
		 */
		public string title;

		/**
		 * 提示的内容
		 */
		public string content;

		/**
		 * 确认按钮文字
		 */
		public string okLabel;

		/**
		 * 取消按钮文字
		 */
		public string cancelLabel { get; }
	}

	[Serializable]
	public class ShowModalOptions
	{
		/// <summary>取消按钮的文字颜色，必须是 16 进制格式的颜色字符串</summary>
		public string cancelColor;
		/// <summary>取消按钮的文字，最多 4 个字符</summary>
		public string cancelText;
		/// <summary>确认按钮的文字颜色，必须是 16 进制格式的颜色字符串</summary>
		public string confirmColor;
		/// <summary>确认按钮的文字，最多 4 个字符</summary>
		public string confirmText;
		/// <summary>提示的内容</summary>
		public string content;
		/// <summary>
		/// 需要基础库： `2.17.1`
		/// 是否显示输入框
		/// </summary>
		public bool? editable;
		/// <summary>
		/// 需要基础库： `2.17.1`
		/// 显示输入框时的提示文本
		/// </summary>
		public string placeholderText;
		/// <summary>是否显示取消按钮</summary>
		public bool? showCancel;
		/// <summary>提示的标题</summary>
		public string title;

		public Action<ShowModalResult> callback;
	}

	[Serializable]
	public class ShowAlertOptions
	{
		/**
		 * 提示的标题
		 */
		public string title;

		/**
		 * 提示的内容
		 */
		public string content;

		/**
		 * 确认按钮文字
		 */
		public string okLabel;
	}

	public class ShowKeyboardOptions
	{
		/// <summary>当点击完成时键盘是否保持显示</summary>
		public bool confirmHold;

		/// <summary>
		/// 键盘右下角 confirm 按钮的类型，只影响按钮的文本内容
		/// 可选值：
		/// - 'done': 完成;
		/// - 'next': 下一个;
		/// - 'search': 搜索;
		/// - 'go': 前往;
		/// - 'send': 发送;
		/// </summary>
		public string confirmType;

		/// <summary>键盘输入框显示的默认值</summary>
		public string defaultValue;

		/// <summary>键盘中文本的最大长度</summary>
		public double maxLength;

		/// <summary>是否为多行输入</summary>
		public bool multiple;
	}

	public class UpdateKeyboardOptions
	{
		public string value;
	}

	public class OnKeyboardInputResult
	{
		public string value;
	}

	public class OnKeyboardConfirmResult
	{
		public string value;
	}

	public class OnKeyboardCompleteResult
	{
		public string value;
	}

	public interface IKeyBoard
	{
		public Task ShowKeyboard(ShowKeyboardOptions options);

		public Task UpdateKeyboard(UpdateKeyboardOptions options);

		/** 隐藏键盘 */
		public Task HideKeyboard();

		public void OnKeyboardInputEvent(Action<OnKeyboardInputResult> callback);
		public void OffKeyboardInputEvent(Action<OnKeyboardInputResult> callback);
		public void OnKeyboardConfirm(Action<OnKeyboardConfirmResult> callback);
		public void OffKeyboardConfirm(Action<OnKeyboardConfirmResult> callback);
		public void OnKeyboardComplete(Action<OnKeyboardCompleteResult> callback);
		public void OffKeyboardComplete(Action<OnKeyboardCompleteResult> callback);
	}

	/**
	 * 各种UI小部件调用
	 */
	public interface IWidgets
	{
		/** 系统键盘对象 */
		public IKeyBoard keyboard { get; }

		/** 显示 loading 提示框。需主动调用 wx.hideLoading 才能关闭提示框 */
		public Task ShowLoading(ShowLoadingParams obj);

		/** 隐藏 loading 提示框 */
		public Task HideLoading();

		/** 显示消息提示框 */
		public Task<ShowWidgetResult> ShowToast(ShowToastOptions obj);

		/** 隐藏消息提示框 */
		public Task hideToast();

		/**
		 * 显示模态对话框
		 * - 有`确定`和`取消`两个按钮
		 */
		public Task<ShowModalResult> ShowModal(ShowModalOptions obj);

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