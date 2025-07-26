
namespace GDK
{
    public interface IStorage
    {


        // 更多关于存储的隔离策略和清理策略说明可以查看这里 https://developers.weixin.qq.com/minigame/dev/guide/base-ability/storage.html

        /*
         * @description 同步设置int型数据存储，
         * @param key 键名
         * @param value 数值
         */
        public void SetInt(string key, int value);

        /*
         * @description 同步获取之前设置过的int型数据，
         * @param key 键名
         * @param defaultValue 默认值
         * @returns 异常的和空时候会返回默认值
         */
        public int GetInt(string key, int defaultValue);

        /*
         * @description 同步设置string型数据存储，
         * @param key 键名
         * @param value 数值
         */
        public void SetString(string key, string value = "");

        /*
         * @description 同步获取之前设置过的string型数据，
         * @param key 键名
         * @param defaultValue 默认值
         * @returns 异常的和空时候会返回默认值
         */
        public string GetString(string key, string defaultValue);

        /*
         * @description 同步设置float型数据存储，
         * @param key 键名
         * @param value 数值
         */
        public void SetFloat(string key, float value);

        /*
         * @description 同步获取之前设置过的float型数据，
         * @param key 键名
         * @param defaultValue 默认值
         * @returns 异常的和空时候会返回默认值
         */
        public float GetFloat(string key, float defaultValue);

        /*
         * @description 同步删除所有数据
         */
        public void DeleteAll();

        /*
         * @description 同步删除对应一个key的数据
         * @param key 键名
         */
        public void DeleteKey(string key);

        /*
         * @description 判断一个key是否有值
         * @param key 键名
         * @returns true：有，false：没有
         */
        public bool HasKey(string key);

    }

}
