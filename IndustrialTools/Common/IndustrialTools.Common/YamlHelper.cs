using System.Text;
using YamlDotNet.Serialization;

namespace IndustrialTools.Common
{
    public static class YamlHelper
    {
        private static ReaderWriterLockSlim _LockSlim = new ReaderWriterLockSlim();
        private static ISerializer _serializer;
        private static IDeserializer _deserializer;
        static YamlHelper()
        {
            _serializer = new SerializerBuilder().Build();
            _deserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        }
        public static string Serialize(object target)
        {
            return _serializer.Serialize(target);
        }
        public static void SerializeToFile(object target, string filePath)
        {
            var content = Serialize(target);
            try
            {
                _LockSlim.EnterWriteLock();
                File.WriteAllText(filePath, content, Encoding.Unicode);
            }
            finally
            {
                _LockSlim.ExitWriteLock();
            }
        }
        public static T Deserialize<T>(string yaml)
        {
            return _deserializer.Deserialize<T>(yaml);
        }
        public static T DeserializeFromFile<T>(string filePath)
        {
            string yaml = "";
            try
            {
                _LockSlim.EnterReadLock();
                yaml = File.ReadAllText(filePath, Encoding.Unicode);
            }
            finally
            {
                _LockSlim.ExitReadLock();
            }
            return Deserialize<T>(yaml);
        }
    }

    public static class DealWithYaml
    {
        public static bool WriteYaml(SystemConfigOption buildConfigFile)
        {
            try
            {
                var ymlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SystemConfig.yml");
                if (!File.Exists(ymlFile))
                {
                    File.Create(ymlFile).Dispose();
                }
                YamlHelper.SerializeToFile(buildConfigFile, ymlFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static SystemConfigOption ReadYaml()
        {
            var ymlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SystemConfig.yml");
            if (!File.Exists(ymlFile))
            {
                File.Create(ymlFile).Dispose();
            }
            return YamlHelper.DeserializeFromFile<SystemConfigOption>(ymlFile);
        }

        public static string ConvertirYamlAJson(TextReader yml)
        {
            var deserializer = new DeserializerBuilder().Build();
            var yamlObject = deserializer.Deserialize(yml);
            var serializer = new SerializerBuilder().JsonCompatible().Build();
            string json = serializer.Serialize(yamlObject);
            return json;
        }
    }

}
