
using System;
using System.Threading.Tasks;

namespace GDK
{

		public abstract class KeyBoardBase<T1,T2,T3> : GDK.IKeyBoard
		{
			protected T1 OnKeyboardInputHandler;

			protected Action<OnKeyboardInputResult> OnKeyboardInputAction;

			public abstract Task ShowKeyboard(ShowKeyboardOptions options);
			public abstract Task UpdateKeyboard(UpdateKeyboardOptions options);
			public abstract Task HideKeyboard();
			public abstract void OnKeyboardInputEvent(Action<OnKeyboardInputResult> callback);

			public abstract void OffKeyboardInputEvent(Action<OnKeyboardInputResult> callback);

			protected T2 OnKeyboardConfirmHandler;

			protected Action<OnKeyboardConfirmResult> OnKeyboardConfirmAction;

			public abstract void OnKeyboardConfirm(Action<OnKeyboardConfirmResult> callback);

			public abstract void OffKeyboardConfirm(Action<OnKeyboardConfirmResult> callback);

			protected T3 OnKeyboardCompleteHandler;

			protected Action<OnKeyboardCompleteResult> OnKeyboardCompleteAction;

			public abstract void OnKeyboardComplete(Action<OnKeyboardCompleteResult> callback);

			public abstract void OffKeyboardComplete(Action<OnKeyboardCompleteResult> callback);
		}

	public abstract class WidgetsBase : GDK.IWidgets
	{
		/**
		 * 显示启动画面
		 */
		public void showLaunchingView()
		{
			DevLog.Instance.Info("showLaunchingView simulation not implemented.");
		}

		/** 系统键盘对象 */
		public abstract IKeyBoard keyboard { get; }

		/** 显示 loading 提示框。需主动调用 wx.hideLoading 才能关闭提示框 */
		public abstract Task ShowLoading(ShowLoadingParams obj);
		/** 隐藏 loading 提示框 */
		public abstract Task HideLoading();
		/** 显示消息提示框 */
		public abstract Task<ShowWidgetResult> ShowToast(ShowToastOptions obj);
		/** 隐藏消息提示框 */
		public abstract Task hideToast();
		/**
		 * 显示模态对话框
		 * - 有`确定`和`取消`两个按钮
		 */
		public abstract Task<ShowModalResult> ShowModal(ShowModalOptions obj);

		/**
		 * 隐藏启动画面
		 */
		public abstract Task hideLaunchingView();
	}
}
