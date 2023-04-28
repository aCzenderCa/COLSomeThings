using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HarmonyLib;
using TMPro;
using Debug = UnityEngine.Debug;

namespace COLSomeThings.patcher
{
    public static class Font2Dynamic
    {
        [HarmonyPrefix, HarmonyPatch(typeof(TMP_FontAsset), "Awake")]
        public static void MakeItDynamic(TMP_FontAsset __instance)
        {
            if (!__instance.name.StartsWith("Chinese")) return;
            __instance.fallbackFontAssetTable ??= new List<TMP_FontAsset>();
            __instance.fallbackFontAssetTable.Add(COLSomeThingsModMainRuntime.ScFontAsset);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(DataManager), nameof(DataManager.RemoveUnlockedFollowerSkin))]
        public static bool DontRemoveSkin()
        {
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(DataManager), nameof(DataManager.GetFollowerSkinUnlocked))]
        public static bool AllIsUnlocked(out bool __result)
        {
            __result = true;
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(DataManager), nameof(DataManager.GetRandomUnlockedSkin))]
        public static bool ReturnAllSkinsAsUnlockedSkins(out string __result)
        {
            __result = DataManager.GetRandomSkin();
            return false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(GameManager), "Start")]
        public static void AutoUnlockAllSkins()
        {
            try
            {
                DataManager.Instance.FollowerSkinsUnlocked.Concat(DataManager.Instance.DLCSkins)
                    .Concat(DataManager.Instance.SpecialEventSkins).Concat(DataManager.Instance.FollowerSkinsBlacklist).ToHashSet()
                    .Do(DataManager.SetFollowerSkinUnlocked);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}