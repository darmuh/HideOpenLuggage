using BepInEx.Configuration;
using System;
using System.Linq;

namespace HideOpenLuggage
{
    public class ModConfig
    {
        public static ConfigEntry<bool> DeveloperLogging { get; internal set; }
        public static ConfigEntry<float> TimeToHide { get; internal set; }


        public static void Init()
        {
            Plugin.Log.LogInfo("Initializing config");
            DeveloperLogging = MakeBool(Plugin.instance.Config, "Debug", "DeveloperLogging", false, "Enable or Disable developer logging for this mod. (this will fill your log file FAST)");
            TimeToHide = MakeClampedFloat(Plugin.instance.Config, "Settings", "TimeToHide", 0f, "Use this config item to set how long after opening the luggage until it should be hidden (destroyed).\nThe default, 0, will hide the luggage bag immediately after opening", 0f, 60f);
            Plugin.Log.LogInfo("Config has been initialized");
        }

        public static ConfigEntry<bool> MakeBool(ConfigFile ModConfig, string section, string configItemName, bool defaultValue, string configDescription)
        {
            section = BepinFriendlyString(section);
            configItemName = BepinFriendlyString(configItemName);

            return ModConfig.Bind<bool>(section, configItemName, defaultValue, configDescription);
        }

        public static ConfigEntry<float> MakeClampedFloat(ConfigFile ModConfig, string section, string configItemName, float defaultValue, string configDescription, float minValue, float maxValue)
        {
            section = BepinFriendlyString(section);
            configItemName = BepinFriendlyString(configItemName);

            return ModConfig.Bind(section, configItemName, defaultValue, new ConfigDescription(configDescription, new AcceptableValueRange<float>(minValue, maxValue)));
        }

        public static string BepinFriendlyString(string input)
        {
            char[] invalidChars = ['\'', '\n', '\t', '\\', '"', '[', ']'];
            string result = "";

            input = input.Trim();

            foreach (char c in input)
            {
                if (!invalidChars.Contains(c))
                    result += c;
                else
                    continue;
            }

            return result;
        }
    }
}
