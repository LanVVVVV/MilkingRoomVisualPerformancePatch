using MBM.ModLoader.Core;
using MBM.ModLoader.Settings;
using MilkingRoomVisualPerformancePatch.AssetBundles;
using System.Collections.Generic;
using UnityEngine;

namespace MilkingRoomVisualPerformancePatch;

public static class ModEntry
{
    internal const string ModName = "MilkingRoomVisualPerformancePatch";

    public static void Load()
    {
        ABLoader.Load();

        TimeScaleOptimizerCompatibility();

        Log("MilkingRoomVisualPerformancePatch Mod loaded!");
    }

    public static void TimeScaleOptimizerCompatibility()
    {
        ModSettings.OnChanged("TimeScaleOptimizer", "Hose Masks", v =>
        {
            if ((bool)v) ModSettings.Set("TimeScaleOptimizer", "Hose Masks", false);
            else {
                ModSettings.RegisterBool("TimeScaleOptimizer", "Hose Masks", false, "Hose Masks", null!, "MRVP");
                ModSettings.SetVisibleWhen("TimeScaleOptimizer", "Hose Masks",
                    new Dictionary<string, string[]>
                    {
                        { "false", new[] { "MRVP" } }
                    }
                    );
            };
        });
    }

    internal static void Log(string msg) => Debug.Log($"[MRVP] {msg}");

    internal static void LogError(string msg) => Debug.LogError($"[MRVP] {msg}");
}