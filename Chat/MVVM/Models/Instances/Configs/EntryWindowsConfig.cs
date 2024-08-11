using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Instances.Configs
{
    public class EntryWindowsConfig : IConfig
    {
        //Config object
        private Configuration config;

        //Views parameters
        public double ContentWidth => Convert.ToDouble(config.AppSettings.Settings[nameof(ContentWidth)].Value);
        public double LogotypeHeight => Convert.ToDouble(config.AppSettings.Settings[nameof(LogotypeHeight)].Value);


        public EntryWindowsConfig()
        {
            var configMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = @"C:\Users\egork\source\repos\Chat\Chat\ResourcesDictionaries\Configs\EntryWindows.config"
            };

            config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
        }
    }
}
