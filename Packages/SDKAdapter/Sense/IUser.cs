
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
    }
}
