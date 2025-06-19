using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace HideOpenLuggage
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, (PluginInfo.PLUGIN_VERSION))]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin instance = null!;
        public static class PluginInfo
        {
            public const string PLUGIN_GUID = "com.github.darmuh.HideOpenLuggage";
            public const string PLUGIN_NAME = "HideOpenLuggage";
            public const string PLUGIN_VERSION = "0.1.0";
        }

        internal static ManualLogSource Log = null!;

        private void Awake()
        {
            instance = this;
            Log = base.Logger;
            Log.LogInfo($"{PluginInfo.PLUGIN_NAME} is loading with version {PluginInfo.PLUGIN_VERSION}!");
            ModConfig.Init();
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Log.LogInfo($"{PluginInfo.PLUGIN_NAME} load complete!");
        }

        internal static void Spam(string message)
        {
            if (ModConfig.DeveloperLogging.Value)
                Log.LogDebug(message);
            else
                return;
        }

        internal static void ERROR(string message)
        {
            Log.LogError(message);
        }

        internal static void WARNING(string message)
        {
            Log.LogWarning(message);
        }
    }
}
