using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Instances.Configs
{
    public static class EntryWindowsConfig
    {
        private static ExeConfigurationFileMap configMap = new ExeConfigurationFileMap
        {
            ExeConfigFilename = @"C:\Users\egork\source\repos\Chat\Chat\ResourcesDictionaries\Configs\EntryWindows.config"
        };

        private static Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);


        //Views parameters
        public static double ContentWidth => Convert.ToDouble(config.AppSettings.Settings[nameof(ContentWidth)].Value);
        public static double LogotypeHeight => Convert.ToDouble(config.AppSettings.Settings[nameof(LogotypeHeight)].Value);
    }
}
