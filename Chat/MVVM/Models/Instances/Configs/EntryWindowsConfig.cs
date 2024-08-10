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


        //Side column's style parameters
        public static double SideColumnHorizontalScreenValue => Convert.ToDouble(config.AppSettings.Settings[nameof(SideColumnHorizontalScreenValue)].Value);
        public static double SideColumnSquareScreenValue => Convert.ToDouble(config.AppSettings.Settings[nameof(SideColumnSquareScreenValue)].Value);
        public static double SideColumnVerticalScreenValue => Convert.ToDouble(config.AppSettings.Settings[nameof(SideColumnVerticalScreenValue)].Value);


        //Main column's style parameters
        public static double MainColumnHorizontalScreenValue => Convert.ToDouble(config.AppSettings.Settings[nameof(MainColumnHorizontalScreenValue)].Value);
        public static double MainColumnSquareScreenValue => Convert.ToDouble(config.AppSettings.Settings[nameof(MainColumnSquareScreenValue)].Value);
        public static double MainColumnVerticalScreenValue => Convert.ToDouble(config.AppSettings.Settings[nameof(MainColumnVerticalScreenValue)].Value);
    }
}
