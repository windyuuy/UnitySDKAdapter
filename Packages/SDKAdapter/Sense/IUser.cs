
using System;
using System.Threading.Tasks;

namespace GDK
{
    // public class LoginError extends ReqError { }

    /** 登录请求结果 */
    [Serializable]
    public class LoginResult
    {
        public bool IsOk;
        public string OpenId;
        public string Code;
        public string Unionid;
        public string ErrMsg;
        public object Extra;
    }

    /** 登录请求参数 */
    [Serializable]
    public class LoginParams : ReqParams
    {
        // oppo 包名 pkgName
        public string PkgName { get; }
        /**
		 * 是否禁止游客登陆
		 */
        public bool DisableVisitor { get; } = false;
        /**
		 * 是否静默登陆
		 */
        public bool Silent { get; } = false;

        /**
		 * 是否需要实名制
		 */
        public bool RealName { get; }

        /**
		 * 是否允许自动登陆
		 * * 如果当前未绑定任何第三方账号，则执行游客登陆
		 * * 否则，执行第三方账号的自动登陆
		 */
        public bool AutoLogin { get; } = true;
        /**
		* gamepind 登录token
		*/
        public string Token { get; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }

    public class ShowRealNameDialogResult
    {
        public bool IsVerified;
        public double Age;
        public string Name;
        public string IdCard;
        public string Birthday;
    }

    public class BindUserResult
    {
        public bool Success;
        public object Data;
    }

    public class GetFriendCloudStorageReq
    {

        public string[] KeyList;
        /**
         * - 玩一玩和浏览器必须
         * - 格式形如（null开头）：
         * 	- [null, 'goldRank', 'seedRank', 'unlockRank', 'sceneRank',]
         **/
        public string[] TypeIndex;
    }

    public class SetFriendCloudStorageReq
    {

        public string[] KeyList;
        /**
         * - 玩一玩和浏览器必须
         * - 格式形如（null开头）：
         * 	- [null, 'goldRank', 'seedRank', 'unlockRank', 'sceneRank',]
         **/
        public string[] TypeIndex;
    }

    public class GetFriendCloudStorageResult
    {
        public UserGameData[] Data;
    }

    public class LoginSupportOptions
    {

        public bool google;
        public bool visitor;
        public bool facebook;
        public bool wechat;
        public bool gamecenter;
        public bool account;
    }

    public class UserInfo
    {
	    /// <summary>
	    /// 用户头像图片的 URL。URL 最后一个数值代表正方形头像大小（有 0、46、64、96、132 数值可选，0 代表 640x640 的正方形头像，46 表示 46x46 的正方形头像，剩余数值以此类推。默认132），用户没有头像时该项为空。若用户更换头像，原有头像 URL 将失效。
	    /// </summary>
	    public string avatarUrl;
	    /// <summary>
	    /// 用户所在城市。不再返回，参考 [相关公告](https://developers.weixin.qq.com/community/develop/doc/00028edbe3c58081e7cc834705b801)
	    /// </summary>
	    public string city;
	    /// <summary>
	    /// 用户所在国家。不再返回，参考 [相关公告](https://developers.weixin.qq.com/community/develop/doc/00028edbe3c58081e7cc834705b801)
	    /// </summary>
	    public string country;
	    /// <summary>
	    /// 用户性别。不再返回，参考 [相关公告](https://developers.weixin.qq.com/community/develop/doc/00028edbe3c58081e7cc834705b801)
	    /// 可选值：
	    /// - 0: 未知;
	    /// - 1: 男性;
	    /// - 2: 女性;
	    /// </summary>
	    public double gender;
	    /// <summary>
	    /// 显示 country，province，city 所用的语言。强制返回 “zh_CN”，参考 [相关公告](https://developers.weixin.qq.com/community/develop/doc/00028edbe3c58081e7cc834705b801)
	    /// 可选值：
	    /// - 'en': 英文;
	    /// - 'zh_CN': 简体中文;
	    /// - 'zh_TW': 繁体中文;
	    /// </summary>
	    public string language;
	    /// <summary>用户昵称</summary>
	    public string nickName;
	    /// <summary>
	    /// 用户所在省份。不再返回，参考 [相关公告](https://developers.weixin.qq.com/community/develop/doc/00028edbe3c58081e7cc834705b801)
	    /// </summary>
	    public string province;
    }

    public class GetUserInfoOptions
    {
	    
	    /// <summary>
	    /// 显示用户信息的语言
	    /// 可选值：
	    /// - 'en': 英文;
	    /// - 'zh_CN': 简体中文;
	    /// - 'zh_TW': 繁体中文;
	    /// </summary>
	    public string lang;
	    /// <summary>
	    /// 是否带上登录态信息。当 withCredentials 为 true 时，要求此前有调用过 wx.login 且登录态尚未过期，此时返回的数据会包含 encryptedData, iv 等敏感信息；当 withCredentials 为 false 时，不要求有登录态，返回的数据不包含 encryptedData, iv 等敏感信息。
	    /// </summary>
	    public bool? withCredentials;
    }
    public class GetUserInfoResult: BaseResponse
    {
	    /// <summary>
	    /// 需要基础库： `2.7.0`
	    /// 敏感数据对应的云 ID，开通[云开发](https://developers.weixin.qq.com/minigame/dev/wxcloud/basis/getting-started.html)的小程序才会返回，可通过云调用直接获取开放数据，详细见[云调用直接获取开放数据](https://developers.weixin.qq.com/minigame/dev/guide/open-ability/signature.html#method-cloud)
	    /// </summary>
	    public string cloudID;
	    /// <summary>
	    /// 包括敏感数据在内的完整用户信息的加密数据，详见 [用户数据的签名验证和加解密](https://developers.weixin.qq.com/minigame/dev/guide/open-ability/signature.html#加密数据解密算法)
	    /// </summary>
	    public string encryptedData;
	    /// <summary>
	    /// 加密算法的初始向量，详见 [用户数据的签名验证和加解密](https://developers.weixin.qq.com/minigame/dev/guide/open-ability/signature.html#加密数据解密算法)
	    /// </summary>
	    public string iv;
	    /// <summary>不包括敏感信息的原始数据字符串，用于计算签名</summary>
	    public string rawData;
	    /// <summary>
	    /// 使用 sha1( rawData + sessionkey ) 得到字符串，用于校验用户信息，详见 [用户数据的签名验证和加解密](https://developers.weixin.qq.com/minigame/dev/guide/open-ability/signature.html)
	    /// </summary>
	    public string signature;
	    /// <summary>
	    /// [UserInfo](https://developers.weixin.qq.com/minigame/dev/api/open-api/user-info/UserInfo.html)
	    /// 用户信息对象，不包含 openid 等敏感信息
	    /// </summary>
	    public UserInfo userInfo;
    }

    // 自动生成
    /**
	 * 用户接口
	 * @usage 包括登录、用户存档管理等
	 */
    public interface IUser : IModule
    {
        /** 登录 */
        public Task<LoginResult> Login(LoginParams paras);
        /** 绑定回调 */
        public void SetBindCallback(Action<bool, object> callback);

        /** 绑定回调 */
        public void SetRebootCallback(Action callback);
        /**
		 * 显示用户中心
		 * * APP平台支持
		 */
        public Task showUserCenter();

        /**
		 * 判断是否为本地实名制系统
		 */
        public bool IsNativeRealNameSystem { get; }

        /**
		 * 显示未成年人游戏描述信息
		 * * APP平台支持
		 */
        public Task showMinorInfo(string info);

        /**
		 * 显示实名制弹框，进入实名制流程
		 * * APP平台支持
		 * @param force 是否强制
		 */
        public Task<ShowRealNameDialogResult> ShowRealNameDialog(long userID, bool force);

        /**
		 * 显示账号绑定
		 * * APP平台支持
		 */
        public Task ShowBindDialog();

        public Task<BindUserResult> BindUser();

        /** 检查登录态是否过期 */
        public Task CheckSession(ReqParams paras);

        /** 更新用户数据 */
        /// <summary>
        /// Updates the user data asynchronously and returns the result of the operation.
        /// </summary>
        /// <returns>A task that represents the asynchronous update operation. The task result contains the <see cref="UserDataUpdateResult"/>.</returns>
        public Task<UserDataUpdateResult> Update();

        /**
		 * 获取用户云端数据
		 * - oppo未处理
		 */
        public Task<GetFriendCloudStorageResult> GetFriendCloudStorage(GetFriendCloudStorageReq obj);
        /**
         * 提交用户云端数据
         * - oppo未处理
         */
        public Task SetUserCloudStorage(SetFriendCloudStorageReq obj);
        /**
         * 判断userId对应的用户是否绑定过社交账号
         * @param userId 登录时服务器返回的userId
         */
        public bool CheckIsUserBind(long userId);

        public void SetLoginSupport(LoginSupportOptions loginSupport);

        public void SetAccountChangeListener(Action f);

        public Task<GetUserInfoResult> GetUserInfo(GetUserInfoOptions options);
    }
}
