using BepInEx;
using HarmonyLib;
using System.Reflection;
using BepInEx.Logging;
using System.Linq;

namespace OcbBiggerSchools
{

    [BepInPlugin("ch.ocbnet.bsf", "OcbBiggerSchools", "0.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private void Awake()
        {
            Log = base.Logger;
            // Unity.Burst.BurstCompiler.Options.EnableBurstCompilation = false;
            var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "ch.ocbnet.bsf");
            var patchedMethods = harmony.GetPatchedMethods().ToArray();
            log.info("Plugin OcbBiggerSchools is loaded! Patched {0} methods", patchedMethods.Length);
            foreach (var patchedMethod in patchedMethods)
            {
                log.info("Patched method: {0}:{1}", patchedMethod.Module.Name, patchedMethod.Name);
            }
        }
    }

    // Little logging helper
    public static class log
    {
        public static void fatal(string msg) =>
            Plugin.Log.Log(LogLevel.Fatal, msg);
        public static void error(string msg) =>
            Plugin.Log.Log(LogLevel.Error, msg);
        public static void warn(string msg) =>
            Plugin.Log.Log(LogLevel.Warning, msg);
        public static void message(string msg) =>
            Plugin.Log.Log(LogLevel.Message, msg);
        public static void info(string msg) =>
            Plugin.Log.Log(LogLevel.Info, msg);
        public static void debug(string msg) =>
            Plugin.Log.Log(LogLevel.Debug, msg);
        public static void fatal(string fmt, params object[] args) =>
            Plugin.Log.Log(LogLevel.Fatal, string.Format(fmt, args));
        public static void error(string fmt, params object[] args) =>
            Plugin.Log.Log(LogLevel.Error, string.Format(fmt, args));
        public static void warn(string fmt, params object[] args) =>
            Plugin.Log.Log(LogLevel.Warning, string.Format(fmt, args));
        public static void message(string fmt, params object[] args) =>
            Plugin.Log.Log(LogLevel.Message, string.Format(fmt, args));
        public static void info(string fmt, params object[] args) =>
            Plugin.Log.Log(LogLevel.Info, string.Format(fmt, args));
        public static void debug(string fmt, params object[] args) =>
            Plugin.Log.Log(LogLevel.Debug, string.Format(fmt, args));
    }

    // Harmony patch to increase school sizes (more students)
    [HarmonyPatch(typeof(Game.Prefabs.School), "Initialize")]
    static class School_Patch
    {
        static void Prefix(Game.Prefabs.School __instance)
        {
            if (__instance.m_Level == Game.Prefabs.SchoolLevel.Elementary)
                __instance.m_StudentCapacity = (int)(3.0f * __instance.m_StudentCapacity);
            if (__instance.m_Level == Game.Prefabs.SchoolLevel.HighSchool)
                __instance.m_StudentCapacity = (int)(2.0f * __instance.m_StudentCapacity);
        }
    }

}
