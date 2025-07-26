using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDK
{
	public class StorageBase : IStorage
	{
		public virtual void SetInt(string key, int value)
		{
			UnityEngine.PlayerPrefs.SetInt(key, value);
		}

		public virtual int GetInt(string key, int defaultValue)
		{
			return UnityEngine.PlayerPrefs.GetInt(key, defaultValue);
		}

		public virtual void SetString(string key, string value = "")
		{
			UnityEngine.PlayerPrefs.SetString(key, value);
		}

		public virtual string GetString(string key, string defaultValue)
		{
			return UnityEngine.PlayerPrefs.GetString(key, defaultValue);
		}

		public virtual void SetFloat(string key, float value)
		{
			UnityEngine.PlayerPrefs.SetFloat(key, value);
		}

		public virtual float GetFloat(string key, float defaultValue)
		{
			return UnityEngine.PlayerPrefs.GetFloat(key, defaultValue);
		}

		public virtual void DeleteAll()
		{
			UnityEngine.PlayerPrefs.DeleteAll();
		}

		public virtual void DeleteKey(string key)
		{
			UnityEngine.PlayerPrefs.DeleteKey(key);
		}

		public virtual bool HasKey(string key)
		{
			return UnityEngine.PlayerPrefs.HasKey(key);
		}
	}
}