using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GDK
{
	public class StdFileSystemManager : GDK.IFileSystemManager
	{
		public AccessSyncResult AccessSync(string path)
		{
			return File.Exists(path) ? new AccessSyncResult { Exist = true } : new AccessSyncResult { Exist = false };
		}

		/// <summary>
		/// 根据path递规创建文件夹
		/// </summary>
		/// <param name="filePath">带文件名的全路径</param>
		/// <returns></returns>
		private static bool CreateDirectoryByPath(string filePath)
		{
			var isOk = false;
			var filePathDirectory = filePath.Substring(0, filePath.LastIndexOf(@"\"));
			filePathDirectory = filePathDirectory.Replace("//", @"\").Replace("/", @"\");
			var directory = filePathDirectory.Split('\\');
			var existsDir = string.Empty;
			foreach (var item in directory)
			{
				existsDir += item + @"\";
				if (!Directory.Exists(existsDir))
				{
					Directory.CreateDirectory(existsDir);
				}
			}

			isOk = true;
			return isOk;
		}

		public string MkdirSync(string dirPath, bool recursive)
		{
			if (recursive)
			{
				if (CreateDirectoryByPath(dirPath))
				{
					return dirPath;
				}
				else
				{
					return "";
				}
			}
			else
			{
				return Directory.CreateDirectory(dirPath).FullName;
			}
		}

		public async void ReadFileBytes(ReadFileBytesParam option)
		{
			try
			{
				if (option.encoding == "utf8" || option.encoding == "utf-8")
				{
					var text = await File.ReadAllTextAsync(option.filePath, Encoding.UTF8);
					option.success?.Invoke(new ReadFileResult
					{
						stringData = text,
						errMsg = "",
						errCode = 0
					});
				}
				else
				{
					var data = await File.ReadAllBytesAsync(option.filePath);
					option.success?.Invoke(new ReadFileResult
					{
						binData = data,
						errMsg = "",
						errCode = 0
					});
				}
			}
			catch (Exception exception)
			{
				option.fail?.Invoke(new ReadFileResult
				{
					errMsg = exception.ToString(),
					errCode = -1,
				});
			}
		}

		public async void ReadFileAllText(ReadFileAllTextParam option)
		{
			try
			{
				var text = await File.ReadAllTextAsync(option.filePath, UTF8WithoutBom);
				option.success(new ReadFileResult
				{
					stringData = text,
					errMsg = null,
					errCode = 0
				});
			}
			catch (Exception exception)
			{
				option.fail(new ReadFileResult
				{
					errMsg = exception.ToString(),
					errCode = -1,
				});
			}
		}

		public async void ReadFileAllBytes(ReadFileAllBytesParam option)
		{
			try
			{
				var data = await File.ReadAllBytesAsync(option.filePath);
				option.success(new ReadFileResult
				{
					binData = data,
					arrayBufferLength = data.Length,
					errMsg = null,
					errCode = 0
				});
			}
			catch (Exception exception)
			{
				option.fail(new ReadFileResult
				{
					errMsg = exception.ToString(),
					errCode = -1,
				});
			}
		}

		public byte[] ReadFileBytesSync(string filePath, long? position = null, long? length = null)
		{
			var file = File.OpenRead(filePath);

			int readPos;
			if (position == null)
			{
				readPos = 0;
			}
			else
			{
				readPos = (int)position.Value;
			}

			int readLen;
			if (length == null)
			{
				readLen = (int)file.Length - readPos;
			}
			else
			{
				readLen = (int)length.Value;
			}

			var buffer = new byte[readLen];
			file.Read(buffer, readPos, readLen);
			return buffer;
		}

		public void RenameSync(string oldPath, string newPath)
		{
			File.Move(oldPath, newPath);
		}

		public void Rmdir(RmdirParam param)
		{
			try
			{
				Directory.Delete(param.dirPath, param.recursive);
				param.success(new BaseResponse
				{
					IsOk = true,
					ErrCode = 0,
					ErrMsg = "",
				});
			}
			catch (Exception exception)
			{
				param.fail(new BaseResponse
				{
					IsOk = false,
					ErrCode = -1,
					ErrMsg = exception.ToString(),
				});
			}
		}

		public bool UnlinkSync(string filePath)
		{
			File.Delete(filePath);
			return true;
		}

		public async void WriteFileBytes(WriteFileParam param)
		{
			try
			{
				await File.WriteAllBytesAsync(param.filePath, param.data);
				param.success(new BaseResponse
				{
					IsOk = true,
					ErrCode = 0,
					ErrMsg = "",
				});
			}
			catch (Exception exception)
			{
				param.fail(new BaseResponse
				{
					IsOk = false,
					ErrCode = -1,
					ErrMsg = exception.ToString(),
				});
			}
		}

		private static readonly UTF8Encoding UTF8WithoutBom = new UTF8Encoding(false);

		public async void WriteFileString(WriteFileStringParam param)
		{
			try
			{
				await File.WriteAllTextAsync(param.filePath, param.data, UTF8WithoutBom);
				param.success(new BaseResponse
				{
					IsOk = true,
					ErrCode = 0,
					ErrMsg = "",
				});
			}
			catch (Exception exception)
			{
				param.fail(new BaseResponse
				{
					IsOk = false,
					ErrCode = -1,
					ErrMsg = exception.ToString(),
				});
			}
		}

		public bool WriteFileStringSync(string filePath, string data, string encoding = "utf8")
		{
			File.WriteAllText(filePath, data, UTF8WithoutBom);
			return true;
		}

		public bool WriteFileBytesSync(string filePath, byte[] data)
		{
			File.WriteAllBytes(filePath, data);
			return true;
		}

		public bool WriteFileBytesSync(string filePath, byte[] data, string encoding = "utf8")
		{
			File.WriteAllBytes(filePath, data);
			return true;
		}
	}

	public class FileSystemBase : IFileSystem
	{
		public virtual IModuleMap Api { get; set; }

		public virtual void Init()
		{
		}

		public virtual Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		private readonly StdFileSystemManager FileSystemManager = new StdFileSystemManager();

		public IFileSystemManager GetFileSystemManager()
		{
			return FileSystemManager;
		}
	}
}