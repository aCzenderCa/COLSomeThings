using BepInEx;
using COLSomeThings.patcher;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace COLSomeThings
{
    [BepInPlugin("zender.COLSomeThingsModMainRuntime", "COLSomeThingsMod", "0.0.2")]
    public class COLSomeThingsModMainRuntime : BaseUnityPlugin
    {
        public static readonly Harmony HarmonyInstance = new("zender.COLSomeThingsModMainRuntime");
        public static readonly AssetBundle DataAssetBundle;
        public static readonly TMP_FontAsset ScFontAsset;

        static COLSomeThingsModMainRuntime()
        {
            HarmonyInstance.PatchAll(typeof(Font2Dynamic));
            DataAssetBundle = AssetBundle.LoadFromStream(AllData.CoLFont);
            ScFontAsset = DataAssetBundle.LoadAsset<TMP_FontAsset>("SourceHanSerifSC-Light SDF");
        }
    }
}