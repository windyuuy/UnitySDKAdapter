#if SUPPORT_WECHATGAME
using System.Threading.Tasks;
using GDK;
using Lang.Encoding;
using Lang.Loggers;
using WeChatWASM;
using ReadCompressedFileSyncOption = WeChatWASM.ReadCompressedFileSyncOption;
using ReadFileParam = WeChatWASM.ReadFileParam;
using WriteFileParam = WeChatWASM.WriteFileParam;
using WriteFileStringParam = WeChatWASM.WriteFileStringParam;
using WriteSyncOption = WeChatWASM.WriteSyncOption;

namespace WechatGDK
{
	public class FileSystemManager : GDK.IFileSystemManager
	{
		public Logger devlog = new Logger();
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

		public string[] ReaddirSync(string dirPath)
		{
			return _fs.ReaddirSync(dirPath);
		}

		public void ReadFileBytes(GDK.ReadFileBytesParam option)
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

		public void ReadFileAllText(ReadFileAllTextParam option)
		{
			ReadFileBytes(new()
			{
				filePath = option.filePath,
				encoding = option.encoding,
				success = option.success,
				fail = option.fail
			});
		}

		public void ReadFileAllBytes(ReadFileAllBytesParam option)
		{
			ReadFileBytes(new()
			{
				filePath = option.filePath,
				encoding = option.encoding,
				success = option.success,
				fail = option.fail
			});
		}

		public string ReadFileStringSync(string filePath, string encoding, long? position = null, long? length = null)
		{
			return _fs.ReadFileSync(filePath, encoding, position, length);
		}

		public byte[] ReadFileBytesSync(string filePath, long? position = null, long? length = null)
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
		public void WriteFileBytes(GDK.WriteFileParam param)
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
			});
		}

		public void WriteFileText(GDK.WriteFileTextParam param)
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
		public bool WriteFileTextSync(string filePath, string data, string encoding = "utf8")
		{
			var result = _fs.WriteFileSync(filePath, data, encoding);
			var ok = result == "ok";
			if (!ok)
			{
				devlog.Error(result);
			}

			return ok;
		}

		public bool WriteFileBytesSync(string filePath, byte[] data)
		{
			var result = _fs.WriteFileSync(filePath, data);
			var ok = result == "ok";
			if (!ok)
			{
				devlog.Error(result);
			}

			return ok;
		}

		public void CloseSync(CloseSyncOptions paras)
		{
			_fs.CloseSync(new()
			{
				fd = paras.fd,
			});
		}

		public string OpenSync(OpenSyncOptions paras)
		{
			return _fs.OpenSync(new()
			{
				filePath = paras.filePath,
				flag = paras.flag,
			});
		}

		public void WriteSync(GDK.WriteSyncOption paras)
		{
			_fs.WriteSync(new WriteSyncOption
			{
				fd = paras.fd,
				encoding = paras.encoding,
				data = paras.data,
				length = paras.length,
				offset = paras.offset,
				position = paras.position,
			});
		}

		public byte[] ReadCompressedFileSync(GDK.ReadCompressedFileSyncOption options)
		{
			return _fs.ReadCompressedFileSync(new ReadCompressedFileSyncOption
			{
				compressionAlgorithm = options.compressionAlgorithm,
				filePath = options.filePath,
			});
		}

		public string ReadCompressedFileTextSync(GDK.ReadCompressedFileSyncOption options)
		{
			var text = EncodingExt.UTF8WithoutBom.GetString(ReadCompressedFileSync(options));
			return text;
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
#endif
