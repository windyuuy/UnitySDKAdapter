using System;
using System.Collections.Generic;
using WeChatWASM;

namespace GDK
{
	public struct AccessSyncResult
	{
		public bool Exist;
	}

	public struct ReadFileBytesParam
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

	public struct ReadFileAllTextParam
	{
		/// <summary>要读取的文件的路径 (本地路径)</summary>
		public string filePath;

		/// <summary>指定读取文件的字符编码，如果不传 encoding，则以 ArrayBuffer 格式读取文件的二进制内容</summary>
		public string encoding;

		public Action<ReadFileResult> success;
		public Action<ReadFileResult> fail;
	}

	public struct ReadFileAllBytesParam
	{
		/// <summary>要读取的文件的路径 (本地路径)</summary>
		public string filePath;

		/// <summary>指定读取文件的字符编码，如果不传 encoding，则以 ArrayBuffer 格式读取文件的二进制内容</summary>
		public string encoding;

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
		public Action<BaseResponse> success;

		public Action<BaseResponse> fail;
	}

	public class WriteFileTextParam
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

	public class CloseSyncOptions
	{
		/// <summary>
		/// 文件标识符
		/// </summary>
		public string fd;
	}

	public class OpenSyncOptions
	{
		/// <summary>
		/// 文件路径
		/// </summary>
		public string filePath;

		/// <summary>
		/// 文件系统标志
		/// </summary>
		public string flag;
	}

	public class WriteSyncOption
	{
		/// <summary>写入的内容，类型为 String 或 ArrayBuffer</summary>
		public byte[] data;

		/// <summary>
		/// 文件描述符。fd 通过 [FileSystemManager.open](https://developers.weixin.qq.com/minigame/dev/api/file/FileSystemManager.open.html) 或 [FileSystemManager.openSync](https://developers.weixin.qq.com/minigame/dev/api/file/FileSystemManager.openSync.html) 接口获得
		/// </summary>
		public string fd;

		/// <summary>
		/// 只在 data 类型是 String 时有效，指定写入文件的字符编码，默认为 utf8
		/// 可选值：
		/// - 'ascii': ;
		/// - 'base64': ;
		/// - 'binary': ;
		/// - 'hex': ;
		/// - 'ucs2': 以小端序读取;
		/// - 'ucs-2': 以小端序读取;
		/// - 'utf16le': 以小端序读取;
		/// - 'utf-16le': 以小端序读取;
		/// - 'utf-8': ;
		/// - 'utf8': ;
		/// - 'latin1': ;
		/// </summary>
		public string encoding;

		/// <summary>
		/// 只在 data 类型是 ArrayBuffer 时有效，指定要写入的字节数，默认为 arrayBuffer 从0开始偏移 offset 个字节后剩余的字节数
		/// </summary>
		public double? length;

		/// <summary>
		/// 只在 data 类型是 ArrayBuffer 时有效，决定 arrayBuffe 中要被写入的部位，即 arrayBuffer 中的索引，默认0
		/// </summary>
		public double? offset;

		/// <summary>
		/// 指定文件开头的偏移量，即数据要被写入的位置。当 position 不传或者传入非 Number 类型的值时，数据会被写入当前指针所在位置。
		/// </summary>
		public double? position;
	}

	public class ReadCompressedFileSyncOption
	{
		/// <summary>
		/// 文件压缩类型，目前仅支持 'br'。
		/// 可选值：
		/// - 'br': brotli压缩文件;
		/// </summary>
		public string compressionAlgorithm = "br";

		/// <summary>要读取的文件的路径 (本地用户文件或代码包文件)</summary>
		public string filePath;
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

		public string[] ReaddirSync(string dirPath);

		/// 读取本地文件内容
		public void ReadFileBytes(ReadFileBytesParam option);

		/// 读取本地文件内容
		public void ReadFileAllText(ReadFileAllTextParam option);

		/// 读取本地文件内容
		public void ReadFileAllBytes(ReadFileAllBytesParam option);

		public byte[] ReadFileBytesSync(string filePath, long? position = null, long? length = null);

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
		public void WriteFileBytes(WriteFileParam param);

		public void WriteFileText(WriteFileTextParam param);

		/// 同步写文件
		public bool WriteFileTextSync(string filePath, string data, string encoding = "utf8");

		public bool WriteFileBytesSync(string filePath, byte[] data);

		/// 根据 url 链接获取本地缓存文件路径（仅 WebGL 平台可用）
		// public void GetLocalCachedPathForUrl();
		/// 判断该 url 是否有本地缓存文件（仅 WebGL 平台可用）
		// public void IsUrlCached();
		public void CloseSync(CloseSyncOptions paras);

		public string OpenSync(OpenSyncOptions paras);
		public void WriteSync(GDK.WriteSyncOption paras);
		public byte[] ReadCompressedFileSync(ReadCompressedFileSyncOption options);
		public string ReadCompressedFileTextSync(ReadCompressedFileSyncOption options);
	}

	public interface IFileSystem : IModule
	{
		public IFileSystemManager GetFileSystemManager();
	}
}