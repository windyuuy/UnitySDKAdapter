/// <reference path="ErrorMap.ts" />

namespace GDK
{
	/** 基本请求错误码 */
	public enum GDKErrorCode
	{
		// /** -----------Normal Error----------- */
		/** 请求成功 */
		SUCCESS = 0,

		/** 未知错误 */
		UNKNOWN = 100,

		/** 请求超时 */
		TIMEOUT = 101,

		/** 网络错误 */
		NETWORK_ERROR = 102,

		/** api不可用 */
		API_INVALID = 203,

		// /** -----------GameHttpClient Error----------- */
		/** 无效的OPENID */
		INVALID_OPENID = 2001,

		// /** -----------API Error----------- */

		/** API 登录 */
		API_LOGIN_SUCCESS = 30001,

		/** API 失败 */
		API_LOGIN_FAILED = 30002,

		// /** API 支付 */
		/** API 支付成功 */
		API_PAY_SUCCESS = 30011,

		/** API 支付失败 */
		API_PAY_FAILED = 30012,

		/** API 取消支付 */
		API_PAY_CANCEL = 30013,
		API_PAY_QUERYITEMINFO_FAILED = 30021,

		// /** API 更新用户数据 */
		/** 权鉴失败 */
		API_AUTHORIZE_FAILED = 30101,

		/** API 更新用户数据失败 */
		API_UPDATE_USERDATA_FAILED = 30102,

		// /** 排行数据 */
		/** 获取好友排行数据失败 */
		API_GET_FRIEND_CLOUD_STORAGE_FAILED = 30112,

		/** 上传用户数据失败 */
		API_SET_USER_CLOUD_STORAGE_FAILED = 30113,

		/** 打开客服反馈界面失败 */
		API_OPEN_FEEDBACK_FAILED = 30122,

		/** 显示 loading 失败 */
		API_SHOW_LOADING_FAILED = 30131,

		/** 隐藏 loading 失败 */
		API_HIDE_LOADING_FAILED = 30132,

		/** 显示 toast 失败 */
		API_SHOW_TOAST_FAILED = 3013,

		/** 隐藏 toast 失败 */
		API_HIDE_TOAST_FAILED = 30134,

		/** 显示 model 失败 */
		API_SHOW_MODAL_FAILED = 30135,

		/** 隐藏 键盘 失败 */
		API_HIDE_KEYBOARD_FAILED = 30141,

		/** 登录态过期 */
		API_LOGIN_SESSION_OUTDATE = 30151,

		/** 更新登录态失败 */
		API_UPDATE_LOGIN_SESSION_FAILED = 30152,

		/** 跳转小程序失败 */
		API_CALL_UP_MINI_PROGRAM_FAILED = 30161,

		/** 跳转原生app失败 */
		API_CALL_UP_NATIVE_APP_FAILED = 30161,


		/**
		 * 分享不被支持
		 */
		API_SHARE_UNSUPPORTED = 30201,

		/**
		 * 不支持的平台
		 */
		API_SHARE_UNSUPPORTED_PLATFORM = 30202,

		/**
		 * 请求打开并播放广告超时
		 */
		API_SHOW_ADVERT_TIMEOUT = 30304,

		/**
		 * 广告未加载就播放
		 */
		API_SHOW_ADVERT_WITHOUT_LOADED = 30305,
	}

	public partial class ResultTemplatesExtractor
	{
		/**
		 * 请求结果模板，用于生成错误结果
		 **/
		public static readonly ResultTemplatesExtractor GDKResultTemplates = new ResultTemplatesExtractor(new()
		{
			new () { errCode = GDKErrorCode.SUCCESS, message = "请求成功", reason = "请求成功",  },
			new() { errCode = GDKErrorCode.UNKNOWN, message = "请求失败", reason = "未知错误" },
			new() { errCode = GDKErrorCode.TIMEOUT, message = "请求超时", reason = "请求超时" },
			new() { errCode = GDKErrorCode.NETWORK_ERROR, message = "网络错误", reason = "网络错误" },

			new() { errCode = GDKErrorCode.API_INVALID, message = "api不可用", reason = "api不可用" },

			new() { errCode = GDKErrorCode.INVALID_OPENID, message = "登录失败", reason = "openId验证失败" },

			new() { errCode = GDKErrorCode.API_LOGIN_SUCCESS, message = "Api登录成功", reason = "Api登录成功" },
			new() { errCode = GDKErrorCode.API_LOGIN_FAILED, message = "Api登录失败", reason = "Api登录失败" },

			new() { errCode = GDKErrorCode.API_PAY_SUCCESS, message = "Api支付失败", reason = "Api支付失败" },
			new() { errCode = GDKErrorCode.API_PAY_FAILED, message = "Api支付失败", reason = "Api支付失败" },
			new() { errCode = GDKErrorCode.API_PAY_CANCEL, message = "Api支付取消", reason = "Api支付取消" },

			new() { errCode = GDKErrorCode.API_AUTHORIZE_FAILED, message = "Api用户权鉴失败", reason = "Api用户权鉴失败" },
			new()
			{
				errCode = GDKErrorCode.API_UPDATE_USERDATA_FAILED, message = "Api更新用户数据失败", reason = "Api更新用户数据失败"
			},

			new() { errCode = GDKErrorCode.API_GET_FRIEND_CLOUD_STORAGE_FAILED, message = "获取好友排行数据失败" },
			new() { errCode = GDKErrorCode.API_SET_USER_CLOUD_STORAGE_FAILED, message = "上传用户数据失败" },

			new() { errCode = GDKErrorCode.API_SHOW_LOADING_FAILED, message = "显示 loading 失败" },
			new() { errCode = GDKErrorCode.API_HIDE_LOADING_FAILED, message = "隐藏 loading 失败" },
			new() { errCode = GDKErrorCode.API_SHOW_TOAST_FAILED, message = "显示 toast 失败" },
			new() { errCode = GDKErrorCode.API_HIDE_TOAST_FAILED, message = "隐藏 toast 失败" },
			new() { errCode = GDKErrorCode.API_SHOW_MODAL_FAILED, message = "显示 modal 失败" },
			new() { errCode = GDKErrorCode.API_HIDE_KEYBOARD_FAILED, message = "登录态过期" },

			new() { errCode = GDKErrorCode.API_LOGIN_SESSION_OUTDATE, message = "登录态过期" },
			new() { errCode = GDKErrorCode.API_UPDATE_LOGIN_SESSION_FAILED, message = "更新登录态失败" },

			new() { errCode = GDKErrorCode.API_CALL_UP_MINI_PROGRAM_FAILED, message = "跳转小程序失败" },
			new() { errCode = GDKErrorCode.API_CALL_UP_NATIVE_APP_FAILED, message = "跳转原生app失败" },

			new() { errCode = GDKErrorCode.API_SHARE_UNSUPPORTED, message = "分享不被支持" },
			new() { errCode = GDKErrorCode.API_SHARE_UNSUPPORTED_PLATFORM, message = "不支持的平台" },

			new() { errCode = GDKErrorCode.API_SHOW_ADVERT_TIMEOUT, message = "打开并播放广告超时" },
			new() { errCode = GDKErrorCode.API_SHOW_ADVERT_WITHOUT_LOADED, message = "广告未加载就播放" },
		});
	}
}