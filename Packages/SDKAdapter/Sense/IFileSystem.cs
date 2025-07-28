using System;

namespace GDK
{
	public struct AccessSyncResult
	{
		public bool Exist;
	}

	public struct ReadFileParam
	{
		/// <summary>要读取的文件的路径 (本地路径)</summary>
		public string filePath;

		/// <summary>指定读取文件的字符编码，如果不传 encoding，则以 ArrayBuffer 格式读取文件的二进制内容</summary>
		public string encoding;

		/// <summary>
		/// 从文件指定位置开始读，如果不指定，则从文件头开始读。读取的范围应该是左闭右开区间 [position, position+length)。有效范围：[0, fileLength - 1]。单位：byte
		/// </summary>
		public long? position;

		/// <summary>指定文件的长度，如果不指定，则读到文件末尾。有效范围：[1, fileLength]。单位：byte</summary>
		public long? length;

		public Action<ReadFileResult> success;
		public Action<ReadFileResult> fail;
	}

	public struct ReadFileResult
	{
		/// <summary>如果返回二进制，则数据在这个字段</summary>
		public byte[] binData;

		public int? arrayBufferLength;

		/// <summary>如果返回的是字符串，则数据在这个字段</summary>
		public string stringData;

		public string errMsg;
		public double errCode;
	}

	public class RmdirParam
	{
		public string dirPath;
		public bool recursive;
		public Action<BaseResponse> success;
		public Action<BaseResponse> fail;
	}

	public class WriteFileParam
	{
		/// <summary>要写入的文件路径 (本地路径)</summary>
		public string filePath;
		/// <summary>要写入的二进制数据</summary>
		public byte[] data;
		/// <summary>指定写入文件的字符编码</summary>
		public string encoding;
		public Action<BaseResponse> success;
		public Action<BaseResponse> fail;
	}

	public class WriteFileStringParam
	{
		/// <summary>要写入的文件路径 (本地路径)</summary>
		public string filePath;
		/// <summary>要写入的二进制数据</summary>
		public string data;
		/// <summary>指定写入文件的字符编码</summary>
		public string encoding;
		public Action<BaseResponse> success;
		public Action<BaseResponse> fail;
	}

	public interface IFileSystemManager
	{
		/// 判断文件/目录是否存在
		// public void Access();
		/// 同步判断文件/目录是否存在
		public AccessSyncResult AccessSync(string path);

		/// 获取已保存的本地缓存文件列表
		// public void GetSavedFileList();
		/// 复制文件
		// public void CopyFile();
		/// 同步复制文件
		// public void CopyFileSync();
		/// 创建目录
		// public void Mkdir();
		/// 同步创建目录
		public string MkdirSync(string dirPath, bool recursive);

		/// 读取本地文件内容
		public void ReadFile(ReadFileParam option);

		public byte[] ReadFileSync(string filePath, long? position = null, long? length = null);

		/// 重命名文件，可以把文件从 oldPath 移动到 newPath
		// public void Rename();
		/// 同步重命名文件，可以把文件从 oldPath 移动到 newPath
		public void RenameSync(string oldPath, string newPath);

		/// 删除目录
		public void Rmdir(RmdirParam param);

		/// 同步删除目录
		// public void RmdirSync();
		/// 获取文件 Stats 对象
		// public void Stat();
		/// 同步获取文件 Stats 对象
		// public void StatSync();
		/// 删除文件
		// public void Unlink();
		/// 同步删除文件
		public bool UnlinkSync(string filePath);

		/// 写文件
		public void WriteFile(WriteFileParam param);

		public void WriteFile(WriteFileStringParam param);

		/// 同步写文件
		public bool WriteFileSync(string filePath, string data, string encoding = "utf8");

		public bool WriteFileSync(string filePath, byte[] data, string encoding = "utf8");
		/// 根据 url 链接获取本地缓存文件路径（仅 WebGL 平台可用）
		// public void GetLocalCachedPathForUrl();
		/// 判断该 url 是否有本地缓存文件（仅 WebGL 平台可用）
		// public void IsUrlCached();
	}

	public interface IFileSystem : IModule
	{
		public IFileSystemManager GetFileSystemManager();
	}
}