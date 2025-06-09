
using System;
using System.Threading.Tasks;

namespace GDK
{
    // public class LoginError extends ReqError { }

    /** 登录请求结果 */
    public class LoginResult
    {
        public string openId;
        public string code;
        public object extra;
    }

    /** 登录请求参数 */
    public class LoginParams : ReqParams
    {
        // oppo 包名 pkgName
        public string pkgName { get; }
        /**
		 * 是否禁止游客登陆
		 */
        public bool disableVisitor { get; } = false;
        /**
		 * 是否静默登陆
		 */
        public bool silent { get; } = false;

        /**
		 * 是否需要实名制
		 */
        public bool realName { get; }

        /**
		 * 是否允许自动登陆
		 * * 如果当前未绑定任何第三方账号，则执行游客登陆
		 * * 否则，执行第三方账号的自动登陆
		 */
        public bool autoLogin { get; } = true;
        /**
		* gamepind 登录token
		*/
        public string token { get; }

    }

    public class ShowRealNameDialogResult
    {
        public bool isVerified;
        public double age;
        public string name;
        public string idCard;
        public string birthday;
    }

    public class BindUserResult
    {
        public bool success;
        public object data;
    }

    public class GetFriendCloudStorageReq
    {

        public string[] keyList;
        /**
         * - 玩一玩和浏览器必须
         * - 格式形如（null开头）：
         * 	- [null, 'goldRank', 'seedRank', 'unlockRank', 'sceneRank',]
         **/
        public string[] typeIndex;
    }

    public class SetFriendCloudStorageReq
    {

        public string[] keyList;
        /**
         * - 玩一玩和浏览器必须
         * - 格式形如（null开头）：
         * 	- [null, 'goldRank', 'seedRank', 'unlockRank', 'sceneRank',]
         **/
        public string[] typeIndex;
    }

    public class GetFriendCloudStorageResult
    {
        public UserGameData[] data;
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

    // 自动生成
    /**
	 * 用户接口
	 * @usage 包括登录、用户存档管理等
	 */
    public interface IUser : IModule
    {
        /** 登录 */
        public Task<LoginResult> login(LoginParams paras);
        /** 绑定回调 */
        public void setBindCallback(Action<bool, object> callback);

        /** 绑定回调 */
        public void setRebootCallback(Action callback);
        /**
		 * 显示用户中心
		 * * APP平台支持
		 */
        public Task showUserCenter();

        /**
		 * 判断是否为本地实名制系统
		 */
        public bool isNativeRealNameSystem { get; }

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
        public Task<ShowRealNameDialogResult> showRealNameDialog(long userID, bool force);

        /**
		 * 显示账号绑定
		 * * APP平台支持
		 */
        public Task showBindDialog();

        public Task<BindUserResult> bindUser();

        /** 检查登录态是否过期 */
        public Task checkSession(ReqParams paras);

        /** 更新用户数据 */
        /// <summary>
        /// Updates the user data asynchronously and returns the result of the operation.
        /// </summary>
        /// <returns>A task that represents the asynchronous update operation. The task result contains the <see cref="UserDataUpdateResult"/>.</returns>
        public Task<UserDataUpdateResult> update();

        /**
		 * 获取用户云端数据
		 * - oppo未处理
		 */
        public Task<GetFriendCloudStorageResult> getFriendCloudStorage(GetFriendCloudStorageReq obj);
        /**
         * 提交用户云端数据
         * - oppo未处理
         */
        public Task setUserCloudStorage(SetFriendCloudStorageReq obj);
        /**
         * 判断userId对应的用户是否绑定过社交账号
         * @param userId 登录时服务器返回的userId
         */
        public bool checkIsUserBind(long userId);

        public void setLoginSupport(LoginSupportOptions loginSupport);

        public void setAccountChangeListener(Action f);
    }
}
