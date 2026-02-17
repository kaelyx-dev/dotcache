using static DotCache.Infrastructure.Configuration.ConfigManager;
using Tomlyn;
using Tomlyn.Model;


namespace DotCache.Infrastructure.Configuration
{
    public enum ConfigTable
    {
        Server,
        Application,
        Logging,
        Downstream,
        Dotcache
    }

    public static class ConfigTableExtensions
    {
        public static string GetSectionName(this ConfigTable configTable)
        {
            return configTable switch
            {
                ConfigTable.Server => "Server",
                ConfigTable.Application => "Application",
                ConfigTable.Logging => "Logging",
                ConfigTable.Downstream => "Downstream",
                ConfigTable.Dotcache => "DotCache",
                _ => throw new ArgumentOutOfRangeException(nameof(configTable), $"Not expected config table value: {configTable}")
            };
        }
    }

    public class ConfigManager
    {
        private readonly Lock _lock = new();
        private readonly string _configFilePath;
        private TomlTable _configData = [];

        public ConfigManager(string? configPath = null)
        {
            _configFilePath = configPath ?? Path.Combine(Directory.GetCurrentDirectory(), "config.toml");
            LoadConfig();
        }


        public void LoadConfig()
        {
            lock (_lock)
            {
                string configContent = File.ReadAllText(_configFilePath);
                if(!TestConfigFile(configContent))
                {
                    throw new Exception("Invalid Config File");
                }
                _configData = Toml.ToModel(configContent);
            }
        }

        public T? Get<T>(ConfigTable table, string key, T? defaultValue = default)
        {
            return Get(table.GetSectionName(), key, defaultValue);
        }
        public T? Get<T>(string section, string key, T? defaultValue = default)
        {
            lock (_lock)
            {
                if (!_configData.TryGetValue(section, out var sectionObj) || sectionObj is not TomlTable table)
                {
                    return defaultValue;
                }

                if (!table.TryGetValue(key, out var value))
                {
                    return defaultValue;
                }

                if (typeof(T).Equals(typeof(int)) && value is long longValue)
                {
                    return (T)(object)(int)longValue;
                }

                return value is T typedValue ? typedValue : defaultValue;
            }
        }

        public bool TestConfigFile(string fileContent)
        {
            return true; // TODO
        }

        public string GetServerUrl()
        {
            string host = Get<string>(ConfigTable.Server.GetSectionName(), "Host", "localhost") ?? "localhost";
            int port = Get<int>(ConfigTable.Server, "port", 8080);
            return $"http://{host}:{port}";
        }
    }
}
