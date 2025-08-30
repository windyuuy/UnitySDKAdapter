using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GDK
{
	public class OnShareAppMessageParam
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

	public class OnShareAppMessageParas
	{
		public Action<OnShareAppMessageParam> call;

		public string webViewUrl;
		public string channel;
	}

	public class ShareAppMessageParas
	{
		public Action<OnShareAppMessageParam> call;

		/// 否	转发标题，不传则默认使用当前小游戏的昵称。
		public string title;

		public string desc;

		/// 否	转发显示图片的链接，可以是网络图片路径或本地图片文件路径或相对代码包根目录的图片文件路径。显示图片长宽比是 5:4
		public string imageUrl;

		/// 否	查询字符串，从这条转发消息进入后，可通过 wx.getLaunchOptionsSync() 或 wx.onShow() 获取启动参数中的 query。必须是 key1=val1&key2=val2 的格式。
		public string query;

		/// 否	审核通过的图片编号，详见 使用审核通过的转发图片	2.4.3
		public string imageUrlId;

		public string templateId;

		/// true	否	是否转发到当前群。该参数只对从群工具栏打开的场景下生效，默认转发到当前群，填入false时可转发到其他会话。	2.12.2
		public bool toCurrentGroup;

		/// 否	独立分包路径。详见 小游戏独立分包指南	2.12.2
		public string path;
	}

	public class ShowShareMenuParas
	{
		public bool withShareTicket;
		public string[] menus;
	}

	public class ShowShareMenuResult: BaseResponse
	{
	}

	public interface IShare : IModule
	{
		public void OnShareAppMessage(OnShareAppMessageParam defaultParam,
			Func<OnShareAppMessageParas, OnShareAppMessageParam> action = null);

		public void OnShareTimeline(Action<Action<OnShareTimelineListenerResult>> callback);
		public void OnAddToFavorites(Action<Action<OnAddToFavoritesListenerResult>> callback);

		public void ShareAppMessage(ShareAppMessageParas paras);
		public Task<ShowShareMenuResult> ShowShareMenu(ShowShareMenuParas paras);
	}
}