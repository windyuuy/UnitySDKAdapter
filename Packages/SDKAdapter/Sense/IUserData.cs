namespace GDK
{
    /** 登录请求结果 */
    public class UserDataUpdateResult
    {
    }

    //https://developers.weixin.qq.com/minigame/dev/document/open-api/data/KVData.html
    public class KVData
    {
        public string key { get; }
        public string value { get; }
    }

    //https://developers.weixin.qq.com/minigame/dev/document/open-api/data/UserGameData.html
    public class UserGameData
    {
        //用户的微信头像 url
        public string avatarUrl { get; }

        //用户的微信昵称
        public string nickname { get; }

        //用户的 openid
        public string openid { get; }

        //用户的托管 KV 数据列表
        public KVData[] KVDataList { get; }
    }

    /**
	 * 用户数据
	 */
    public interface IUserData : IModule
    {
        public string openId { get; }
        public string openKey { get; }
        /** 密码 */
        public string password { get; }
        /** 昵称 */
        public string nickName { get; }
        /** 用户ID */
        public long userId { get; }
        /** 是否新用户 */
        public bool isNewUser { get; }
        /** 用户头像 */
        public string avatarUrl { get; }
        /** 上传存档时间(秒) */
        public double backupTime { get; }
        /** 是否已关注公众号
		 * - 0 未关注
		 * - 1 已关注
		 **/
        public int followGzh { get; }
        /** 渠道id */
        public double channelId { get; }
        /** 创建时间 */
        public double createTime { get; }
        /**
		 * 性别
		 * - 0 未知
		 * - 1 男
		 * - 2 女
		 **/
        public double sex { get; }

        /**
		 * 是否为该游戏管理账号用户
		 * - 1 是
		 * - 0 否
		 **/
        public double isWhiteUser { get; }
        /**
		 * 是否房主，1房主，0参加者
		 **/
        public double isMaster { get; }
        /**
		 * 房间号
		 **/
        public double roomId { get; }
    }
}
