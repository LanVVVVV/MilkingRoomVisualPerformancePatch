using HarmonyLib;
using MBMScripts;
using System;

namespace MilkingRoomVisualPerformancePatch.Patches;

[HarmonyPatch(typeof(SeqObjectPoolManager), nameof(SeqObjectPoolManager.Initialize))]
public class SeqObjectPoolManagerPatch
{
    public static event Action? AfterGameInitialized;

    [HarmonyPostfix]
    public static void InitializePostfix()
    {
        AfterGameInitialized?.Invoke();
    }
}