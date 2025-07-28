using System.Threading.Tasks;
using GDK;
using WeChatWASM;

namespace WechatGDK
{
	public class Storage : IStorage
	{
		public IModuleMap Api { get; set; }

		public void Init()
		{
		}

		public Task InitWithConfig(GDKConfigV2 info)
		{
			return Task.CompletedTask;
		}

		public void SetInt(string key, int value)
		{
			WX.StorageSetIntSync(key, value);
		}

		public int GetInt(string key, int defaultValue)
		{
			return WX.StorageGetIntSync(key, defaultValue);
		}

		public void SetString(string key, string value = "")
		{
			WX.StorageSetStringSync(key, value);
		}

		public string GetString(string key, string defaultValue)
		{
			return WX.StorageGetStringSync(key, defaultValue);
		}

		public void SetFloat(string key, float value)
		{
			WX.StorageSetFloatSync(key, value);
		}

		public float GetFloat(string key, float defaultValue)
		{
			return WX.StorageGetFloatSync(key, defaultValue);
		}

		public void DeleteAll()
		{
			WX.StorageDeleteAllSync();
		}

		public void DeleteKey(string key)
		{
			WX.StorageDeleteKeySync(key);
		}

		public bool HasKey(string key)
		{
			return WX.StorageHasKeySync(key);
		}
	}
}