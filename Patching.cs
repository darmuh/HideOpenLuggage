using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace HideOpenLuggage
{
    [HarmonyPatch(typeof(Luggage), "OpenLuggageRPC")]
    public class OpenLuggagePatch
    {
        internal static WaitForSeconds timeToDestroy = new(ModConfig.TimeToHide.Value);

        public static void Postfix(Luggage __instance)
        {
            Plugin.Spam("OpenLuggageRPC");
            __instance.meshRenderers.DoIf(x => x.gameObject != null, x => Plugin.instance.StartCoroutine(DelayDestroy(x.gameObject)));
        }

        private static IEnumerator DelayDestroy(GameObject gameObj)
        {
            yield return null;
            yield return timeToDestroy;
            Object.Destroy(gameObj);
            Plugin.Log.LogDebug($"{gameObj.name} destroyed after timer - {ModConfig.TimeToHide.Value}!");
        }
    }
}
