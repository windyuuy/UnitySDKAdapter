#if SUPPORT_BYTEDANCE
	using System;
	using System.Threading.Tasks;
	using GDK;
	using Lang.Encoding;
	using TTSDK;
	using UnityEngine;
	using RmdirParam = TTSDK.RmdirParam;

	namespace BytedanceGDK
	{
		public class FileSystemManager : GDK.IFileSystemManager
		{
			private readonly TTFileSystemManager _fs;
			public IModuleMap Api { get; }

			public FileSystemManager(IModuleMap api)
			{
				Api = api;
				_fs = TT.GetFileSystemManager();
			}

			// public void Access(AccessParam param)
			// {
			// 	_fs.Access(param);
			// }

			public AccessSyncResult AccessSync(string path)
			{
				return new AccessSyncResult
				{
					Exist = _fs.AccessSync(path),
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
				return _fs.ReadDirSync(dirPath);
			}

			public void ReadFileBytes(GDK.ReadFileBytesParam option)
			{
				_fs.Open(new()
				{
					success = (resp) =>
					{
						var fd = resp.fd;
						long readLen;
						if (option.length == null)
						{
							readLen = _fs.FstatSync(new()
							{
								fd = fd,
							}).size;
						}
						else
						{
							readLen = option.length.Value;
						}

						var buffer = new byte[readLen];
						try
						{
							_fs.Read(new()
							{
								success = (resp) =>
								{
									if (fd != null)
									{
										_fs.CloseSync(new CloseSyncParam
										{
											fd = fd,
										});
										fd = null;
									}

									option.success(new ReadFileResult()
									{
										binData = resp.arrayBuffer,
										arrayBufferLength = resp.bytesRead,
										errMsg = resp.errMsg,
										errCode = 0,
									});
								},
								fail = (resp) =>
								{
									if (fd != null)
									{
										_fs.CloseSync(new CloseSyncParam
										{
											fd = fd,
										});
										fd = null;
									}

									option.fail(new ReadFileResult()
									{
										errMsg = resp.errMsg,
										errCode = resp.errCode,
									});
								},
								fd = fd,
								arrayBuffer = buffer,
								offset = 0,
								length = (int)readLen,
								position = (int?)option.position,
							});
						}
						catch (Exception exception)
						{
							Debug.LogError("_fs.Read-exception:");
							Debug.LogException(exception);
							if (fd != null)
							{
								_fs.CloseSync(new CloseSyncParam
								{
									fd = fd,
								});
								fd = null;
							}
						}
					},
					fail = (resp) =>
					{
						option.fail(new ReadFileResult()
						{
							errMsg = resp.errMsg,
							errCode = resp.errCode,
						});
					},
					filePath = option.filePath,
					flag = "r",
				});
			}

			public void ReadFileAllText(ReadFileAllTextParam option)
			{
				_fs.ReadFile(new TTSDK.ReadFileParam
				{
					success = (resp) =>
					{
						option.success(new ReadFileResult()
						{
							stringData = resp.stringData,
							binData = resp.binData,
							errMsg = resp.errMsg,
							errCode = resp.errCode,
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
					filePath = option.filePath,
					encoding = option.encoding,
				});
			}

			public void ReadFileAllBytes(ReadFileAllBytesParam option)
			{
				_fs.ReadFile(new TTSDK.ReadFileParam
				{
					success = (resp) =>
					{
						option.success(new ReadFileResult()
						{
							stringData = resp.stringData,
							binData = resp.binData,
							errMsg = resp.errMsg,
							errCode = resp.errCode,
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
					filePath = option.filePath,
					encoding = option.encoding,
				});
			}

			public byte[] ReadFileBytesSync(string filePath, long? position = null, long? length = null)
			{
				var fd = _fs.OpenSync(new()
				{
					filePath = filePath,
					flag = "r",
				});
				long readLen;
				if (length == null)
				{
					readLen = _fs.FstatSync(new()
					{
						fd = fd,
					}).size;
				}
				else
				{
					readLen = length.Value;
				}

				var buffer = new byte[readLen];
				try
				{
					var result = _fs.ReadSync(new()
					{
						fd = fd,
						arrayBuffer = buffer,
						offset = 0,
						length = (int)readLen,
						position = (int?)position,
					});
					return result.arrayBuffer;
				}
				finally
				{
					_fs.CloseSync(new CloseSyncParam()
					{
						fd = fd,
					});
				}
			}

			// public void Rename()
			// {
			// 	throw new System.NotImplementedException();
			// }

			public void RenameSync(string oldPath, string newPath)
			{
				_fs.RenameFileSync(oldPath, newPath);
			}

			public void Rmdir(GDK.RmdirParam param)
			{
				_fs.Rmdir(new()
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
				_fs.WriteFile(new TTSDK.WriteFileParam()
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
				_fs.WriteFile(new TTSDK.WriteFileStringParam()
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
				var ok = result == "";
				if (!ok)
				{
				}

				return ok;
			}

			public bool WriteFileBytesSync(string filePath, byte[] data)
			{
				var result = _fs.WriteFileSync(filePath, data);
				var ok = result == "";
				if (!ok)
				{
				}

				return ok;
			}

			// public void GetLocalCachedPathForUrl()
			// {
			// 	throw new System.NotImplementedException();
			// }

			// public void IsUrlCached()
			// {
			// 	throw new System.NotImplementedException();
			// }

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

			public void WriteSync(WriteSyncOption paras)
			{
				_fs.WriteSync(new WriteBinSyncParam
				{
					fd = paras.fd,
					data = paras.data,
					offset = (int)paras.offset,
					length = (int?)paras.length,
					encoding = paras.encoding,
				});
			}

			public byte[] ReadCompressedFileSync(ReadCompressedFileSyncOption options)
			{
				return _fs.ReadCompressedFileSync(new()
				{
					compressionAlgorithm = options.compressionAlgorithm,
					filePath = options.filePath,
				});
			}

			public string ReadCompressedFileTextSync(ReadCompressedFileSyncOption options)
			{
				var text = EncodingExt.UTF8WithoutBom.GetString(ReadCompressedFileSync(options));
				return text;
			}

			public void CleanAllFileCache(Action<bool> callback)
			{
				var rootDir = Api.GameInfo.UserDataPath;
				_fs.Rmdir(new RmdirParam()
				{
					dirPath = rootDir,
					recursive = true,
					success = (resp) => { callback?.Invoke(true); },
					fail = (resp) =>
					{
						DevLog.Instance.Error($"CleanAllFileCache fail: {resp.errCode}, {resp.errMsg}");
						callback?.Invoke(false);
					},
				});
			}
		}

		public class FileSystem : IFileSystem
		{
			public IModuleMap Api { get; set; }

			public void Init()
			{
			}

			public Task InitWithConfig(GDKConfigV2 info)
			{
				FileSystemManager = new FileSystemManager(Api);
				return Task.CompletedTask;
			}

			private IFileSystemManager FileSystemManager;

			public IFileSystemManager GetFileSystemManager()
			{
				return FileSystemManager;
			}
		}
	}
#endif