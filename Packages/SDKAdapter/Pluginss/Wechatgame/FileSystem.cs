using System.Threading.Tasks;
using GDK;
using WeChatWASM;
using ReadFileParam = WeChatWASM.ReadFileParam;
using WriteFileParam = WeChatWASM.WriteFileParam;
using WriteFileStringParam = WeChatWASM.WriteFileStringParam;

namespace WechatGDK
{
	public class FileSystemManager : GDK.IFileSystemManager
	{
		private WXFileSystemManager _fs;

		public FileSystemManager()
		{
			_fs = WX.GetFileSystemManager();
		}

		// public void Access(AccessParam param)
		// {
		// 	_fs.Access(param);
		// }

		public AccessSyncResult AccessSync(string path)
		{
			return new AccessSyncResult
			{
				Exist = _fs.AccessSync(path) == "access:ok",
			};
		}

		// public void GetSavedFileList()
		// {
		// 	throw new System.NotImplementedException();
		// }

		// public void CopyFile()
		// {
		// 	throw new System.NotImplementedException();
		// }

		// public void CopyFileSync()
		// {
		// 	throw new System.NotImplementedException();
		// }

		// public void Mkdir()
		// {
		// 	throw new System.NotImplementedException();
		// }

		public string MkdirSync(string dirPath, bool recursive)
		{
			return _fs.MkdirSync(dirPath, recursive);
		}

		public void ReadFile(GDK.ReadFileParam option)
		{
			_fs.ReadFile(new ReadFileParam
			{
				filePath = option.filePath,
				encoding = option.encoding,
				position = option.position,
				length = option.length,
				success = (resp) =>
				{
					option.success(new ReadFileResult()
					{
						binData = resp.binData,
						arrayBufferLength = resp.arrayBufferLength,
						stringData = resp.stringData,
						errMsg = resp.errMsg,
						errCode = 0,
					});
				},
				fail = (resp) =>
				{
					option.fail(new ReadFileResult()
					{
						errMsg = resp.errMsg,
						errCode = resp.errCode,
					});
				},
			});
		}

		public string ReadFileSync(string filePath, string encoding, long? position = null, long? length = null)
		{
			return _fs.ReadFileSync(filePath, encoding, position, length);
		}

		public byte[] ReadFileSync(string filePath, long? position = null, long? length = null)
		{
			return _fs.ReadFileSync(filePath, position, length);
		}

		// public void Rename()
		// {
		// 	throw new System.NotImplementedException();
		// }

		public void RenameSync(string oldPath, string newPath)
		{
			_fs.RenameSync(oldPath, newPath);
		}

		public void Rmdir(GDK.RmdirParam param)
		{
			_fs.Rmdir(new WeChatWASM.RmdirParam
			{
				success = (resp) =>
				{
					param.success(new BaseResponse()
					{
						ErrCode = 0,
						ErrMsg = "ok",
						IsOk = true,
					});
				},
				fail = (resp) =>
				{
					param.fail(new BaseResponse()
					{
						ErrCode = resp.errCode,
						ErrMsg = resp.errMsg,
						IsOk = false,
					});
				},
				dirPath = param.dirPath,
				recursive = param.recursive,
			});
		}

		// public void RmdirSync()
		// {
		// 	throw new System.NotImplementedException();
		// }

		// public void Stat()
		// {
		// 	throw new System.NotImplementedException();
		// }

		// public void StatSync()
		// {
		// 	throw new System.NotImplementedException();
		// }

		// public void Unlink()
		// {
		// 	throw new System.NotImplementedException();
		// }

		public bool UnlinkSync(string filePath)
		{
			return _fs.UnlinkSync(filePath) == "unlink:ok";
		}

		/// 写文件
		public void WriteFile(GDK.WriteFileParam param)
		{
			_fs.WriteFile(new WriteFileParam
			{
				success = (resp) =>
				{
					param.success(new BaseResponse
					{
						IsOk = true,
						ErrCode = resp.errCode,
						ErrMsg = resp.errMsg,
					});
				},
				fail = (resp) =>
				{
					param.fail(new BaseResponse
					{
						IsOk = false,
						ErrCode = resp.errCode,
						ErrMsg = resp.errMsg,
					});
				},
				filePath = param.filePath,
				data = param.data,
				encoding = param.encoding
			});
		}

		public void WriteFile(GDK.WriteFileStringParam param)
		{
			_fs.WriteFile(new WriteFileStringParam
			{
				success = (resp) =>
				{
					param.success(new BaseResponse
					{
						IsOk = true,
						ErrCode = resp.errCode,
						ErrMsg = resp.errMsg,
					});
				},
				fail = (resp) =>
				{
					param.fail(new BaseResponse
					{
						IsOk = false,
						ErrCode = resp.errCode,
						ErrMsg = resp.errMsg,
					});
				},
				data = param.data,
				filePath = param.filePath,
				encoding = param.encoding
			});
		}

		/// 同步写文件
		public bool WriteFileSync(string filePath, string data, string encoding = "utf8")
		{
			return _fs.WriteFileSync(filePath, data, encoding) == "ok";
		}

		public bool WriteFileSync(string filePath, byte[] data, string encoding = "utf8")
		{
			return _fs.WriteFileSync(filePath, data, encoding) == "ok";
		}

		// public void GetLocalCachedPathForUrl()
		// {
		// 	throw new System.NotImplementedException();
		// }

		// public void IsUrlCached()
		// {
		// 	throw new System.NotImplementedException();
		// }
	}

	public class FileSystem : IFileSystem
	{
		public IModuleMap Api { get; set; }

		public void Init()
		{
		}

		public Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		private readonly IFileSystemManager FileSystemManager = new FileSystemManager();

		public IFileSystemManager GetFileSystemManager()
		{
			return FileSystemManager;
		}
	}
}