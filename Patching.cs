using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace HideOpenLuggage
{
    [HarmonyPatch(typeof(Luggage), "OpenLuggageRPC")]
    public class StartRoundAwake
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
            Plugin.Spam($"{gameObj.name} destroyed after timer - {ModConfig.TimeToHide.Value}!");
        }
    }
}
