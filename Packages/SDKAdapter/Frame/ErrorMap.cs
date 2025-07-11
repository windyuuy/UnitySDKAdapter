using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDK
{
	/** 请求错误扩展参数 */
	public class GDKErrorExtra
	{
		public GDKErrorCode errCode;
		public string message;
		public string reason;
		public object data;
		public GDKErrorExtra() { }
		public GDKErrorExtra(string errMsg)
		{
			this.message = errMsg;
		}
	}

	/** 请求错误结果 */
	public class GDKError : Exception
	{
		public GDKErrorCode errCode;
		public string reason;
		public object data;
		public string name = "";

		public GDKError(string message) : base(message)
		{
			this.name = "GDKError";
		}

		public override string ToString()
		{
			return $"{this.name}: {this.errCode} {this.Message} {this.reason}";
		}
	}


	/** 请求结果模板生成器 */
	public partial class ResultTemplatesExtractor
	{
		protected List<GDKErrorExtra> _temps = new();
		public List<GDKErrorExtra> temps => this._temps;

		public ResultTemplatesExtractor(List<GDKErrorExtra> temps)
		{
			this._temps = temps;
		}


		public GDKError make(GDKErrorCode errCode, Exception extra = null)
		{
			return make(errCode, new GDK.GDKErrorExtra(extra.ToString()));
		}
		public GDKError make(GDKErrorCode errCode, string extra = null)
		{
			return make(errCode, new GDK.GDKErrorExtra(extra));
		}

		/**
		 * 根据错误码和扩展参数构造请求结果
		 */
		public GDKError make<F>(GDKErrorCode errCode, F extra = null) where F : GDKErrorExtra
		{
			// 待优化
			var item = this._temps.FirstOrDefault((item) => item.errCode == errCode);
			var err = new GDKError((extra?.message != null) ? extra.message : item.message);
			err.errCode = extra?.errCode != null ? extra.errCode : item.errCode;
			err.reason = extra?.reason != null ? extra.reason : item.reason;
			err.data = extra?.data != null ? extra.data : item.data;
			return err;
		}
	}

}