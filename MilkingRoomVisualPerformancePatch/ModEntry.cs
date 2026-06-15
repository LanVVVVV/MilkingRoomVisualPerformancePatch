using MilkingRoomVisualPerformancePatch.AssetBundles;
using MilkingRoomVisualPerformancePatch.Features;
using MilkingRoomVisualPerformancePatch.Patches;
using UnityEngine;

namespace MilkingRoomVisualPerformancePatch;

public static class ModEntry
{
    internal const string ModName = "MilkingRoomVisualPerformancePatch";

    public static void Load()
    {
        ABLoader.Load();

        SeqObjectPoolManagerPatch.AfterGameInitialized += MilkingRoomLiquid.InjectShader;

        Log("MilkingRoomVisualPerformancePatch Mod loaded!");
    }

    internal static void Log(string msg) => Debug.Log($"[MRVP] {msg}");

    internal static void LogError(string msg) => Debug.LogError($"[MRVP] {msg}");
}