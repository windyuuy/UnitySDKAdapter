#if SUPPORT_BYTEDANCE
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using GDK;
	using TTSDK;

	namespace BytedanceGDK
	{
		public class Storage : IStorage
		{
			public IModuleMap Api { get; set; }

			protected HashSet<string> Keys = new HashSet<string>();
			protected List<string> keys = new List<string>();

			public void Init()
			{
			}

			public Task InitWithConfig(GDKConfigV2 info)
			{
				return Task.CompletedTask;
			}

			public void SetInt(string key, int value)
			{
				TT.PlayerPrefs.SetInt(key, value);
			}

			public int GetInt(string key, int defaultValue)
			{
				return TT.PlayerPrefs.GetInt(key, defaultValue);
			}

			public void SetString(string key, string value = "")
			{
				TT.PlayerPrefs.SetString(key, value);
			}

			public string GetString(string key, string defaultValue)
			{
				return TT.PlayerPrefs.GetString(key, defaultValue);
			}

			public void SetFloat(string key, float value)
			{
				TT.PlayerPrefs.SetFloat(key, value);
			}

			public float GetFloat(string key, float defaultValue)
			{
				return TT.PlayerPrefs.GetFloat(key, defaultValue);
			}

			public void DeleteAll()
			{
				TT.PlayerPrefs.DeleteAll();
			}

			public void DeleteKey(string key)
			{
				TT.PlayerPrefs.DeleteKey(key);
			}

			public bool HasKey(string key)
			{
				return TT.PlayerPrefs.HasKey(key);
			}

			public void Save()
			{
				
			}
		}
	}
#endif