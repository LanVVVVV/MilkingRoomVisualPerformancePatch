using HarmonyLib;
using MBMScripts;
using MilkingRoomVisualPerformancePatch.AssetBundles;
using MilkingRoomVisualPerformancePatch.UpdaterShader;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace MilkingRoomVisualPerformancePatch.Patches;

[HarmonyPatch(typeof(SeqObjectPoolManager), nameof(SeqObjectPoolManager.Initialize))]
public class SeqObjectPoolManagerPatch
{
    [HarmonyPostfix]
    public static void InitializePostfix()
    {
        var instance = SeqObjectPoolManager.Instance;
        Dictionary<string, List<PooledObject>> PooledObjectDictionary =
            Traverse.Create(instance).Field<Dictionary<string, List<PooledObject>>>("m_PooledObjectDictionary").Value;
        PooledObjectDictionary.TryGetValue("MilkingRoom".ToLower(), out List<PooledObject> list0);
        var milkingRoom = list0[0];

        var tankMask = milkingRoom.transform.Find("MilkTank/Mask");
        var moterMask0 = milkingRoom.transform.Find("Seat (0)/Moter/Mask");
        var moterMask1 = milkingRoom.transform.Find("Seat (1)/Moter/Mask");

        SpriteRenderer[] spriteRendererList;
        foreach(var mask in new[] { tankMask, moterMask0, moterMask1 })
        {
            Object.Destroy(mask.GetComponent<SpriteMask>());
            spriteRendererList = mask.GetComponentsInChildren<SpriteRenderer>(true);
            foreach(var spriteRenderer in spriteRendererList)
            {
                spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
            }
        }

        var tankUpdater = tankMask.GetChild(0).gameObject;
        Object.Destroy(tankUpdater.GetComponent<UpdaterTransformMilkTank>());
        Object.Destroy(tankUpdater.transform.GetChild(0).gameObject);
        tankUpdater.transform.localPosition = Vector3.zero;
        tankUpdater.GetComponent<SpriteRenderer>().sprite = ABLoader.SpriteTankLiquid;
        tankUpdater.AddComponent<UpdaterShaderTank>().Init(ABLoader.ShaderTankLiquid,ABLoader.SpriteSeamlessWave);

        foreach (var motermask in new[] { moterMask0, moterMask1 })
        {
            var moterUpdater = motermask.GetChild(0).gameObject;
            Object.Destroy(moterUpdater.GetComponent<UpdaterTransformMoter>());
            moterUpdater.transform.localPosition = Vector3.zero;
            moterUpdater.GetComponent<SpriteRenderer>().sprite = ABLoader.SpriteMoterLiquid;
            moterUpdater.AddComponent<UpdaterShaderMoter>().Init(ABLoader.ShaderMoterLiquid);
        }
    }
}