using System;
using System.Collections.Generic;

namespace GDK
{
	public class ShareAppMessageParam
	{
		public string title;
		public string imageUrl;
		public string query;
		public string imageUrlId;
		public bool toCurrentGroup;
		public string path;
	}

	public class OnShareTimelineListenerResult
	{
		/// <summary>
		/// 转发显示图片的链接，可以是网络图片路径或本地图片文件路径或相对代码包根目录的图片文件路径。（该图片用于分享到朋友圈的卡片以及从朋友圈转发到会话消息的卡片展示）
		/// </summary>
		public string imageUrl;

		/// <summary>
		/// 需要基础库： `2.14.3`
		/// 朋友圈预览图链接，不传则默认使用当前游戏画面截图
		/// </summary>
		public string imagePreviewUrl;

		/// <summary>
		/// 需要基础库： `2.14.3`
		/// 审核通过的朋友圈预览图图片编号，详见 [使用审核通过的转发图片](https://developers.weixin.qq.com/minigame/dev/guide/open-ability/share/share.html#使用审核通过的转发图片)
		/// </summary>
		public string imagePreviewUrlId;

		/// <summary>
		/// 审核通过的图片编号，详见 [使用审核通过的转发图片](https://developers.weixin.qq.com/minigame/dev/guide/open-ability/share/share.html#使用审核通过的转发图片)
		/// </summary>
		public string imageUrlId;

		/// <summary>
		/// 需要基础库： `2.12.2`
		/// 独立分包路径。详见 [小游戏独立分包指南](https://developers.weixin.qq.com/minigame/dev/guide/base-ability/independent-sub-packages.html)
		/// </summary>
		public string path;

		public string query;

		/// <summary>转发标题，不传则默认使用当前小游戏的昵称。</summary>
		public string title;
	}

	public class OnAddToFavoritesListenerResult
	{
		/// <summary>禁止收藏后长按转发，默认 false</summary>
		public bool disableForward;

		/// <summary>
		/// 转发显示图片的链接，可以是网络图片路径或本地图片文件路径或相对代码包根目录的图片文件路径。显示图片长宽比是 5:4
		/// </summary>
		public string imageUrl;

		public string query;

		/// <summary>收藏标题，不传则默认使用当前小游戏的昵称。</summary>
		public string title;
	}

	public class ShareResult
	{
		/**
		 * 分享结果
		 * * 0 成功
		 * * 1 失败
		 * * 2 取消
		 */
		public int result;

		/**
		 * 返回信息，如果失败可以弹出对话框让玩家确定
		 */
		public string message;

		/**
		 * 是否是群或讨论组
		 */
		public bool isGroup = false;

		/**
		 * 原生返回数据
		 */
		public object extra;
	}

	public class ShareAppMessageParas
	{
		public Action<ShareAppMessageParam> call;
		
		public string webViewUrl;
		public string channel;
	}

	public interface IShare : IModule
	{
		public void OnShareAppMessage(ShareAppMessageParam defaultParam,
			Func<ShareAppMessageParas,ShareAppMessageParam> action = null);

		public void OnShareTimeline(Action<Action<OnShareTimelineListenerResult>> callback);
		public void OnAddToFavorites(Action<Action<OnAddToFavoritesListenerResult>> callback);
	}
}