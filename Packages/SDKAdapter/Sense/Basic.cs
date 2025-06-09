namespace GDK
{
    /** 请求参数 */
    public class ReqParams
    {
        /** 超时时间(s) */
        public double timeout;
        /** 平台 */
        public string platform;
    }
    public class BaseResponse
    {
        // public string CallbackId;

        public bool IsOk;
        public int ErrCode;
        public string ErrMsg;
    }

}
