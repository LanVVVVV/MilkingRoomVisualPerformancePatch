using UnityEngine;
using System.Reflection;

namespace MilkingRoomVisualPerformancePatch.AssetBundles;

static internal class ABLoader
{
    static internal Shader ShaderMoterLiquid = null!;

    static internal Shader ShaderTankLiquid = null!;

    static internal Sprite SpriteMoterLiquid = null!;

    static internal Sprite SpriteTankLiquid = null!;

    static internal Sprite SpriteSeamlessWave = null!;

    static internal void Load()
    {
        var asm = Assembly.GetExecutingAssembly();
        using (var stream = asm.GetManifestResourceStream("MilkingRoomVisualPerformancePatch.AssetBundles.mrvp"))
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            AssetBundle ab = AssetBundle.LoadFromMemory(data);
            
            var matMoter = ab.LoadAsset<Material>("MoterMatertial");
            ShaderMoterLiquid = matMoter.shader;
            
            var matTank = ab.LoadAsset<Material>("TankMaterial");
            ShaderTankLiquid = matTank.shader;

            SpriteMoterLiquid = ab.LoadAsset<Sprite>("milking_room_milk_gauge_gauge");

            SpriteTankLiquid = ab.LoadAsset<Sprite>("milking_room_milk_tank_gauge");

            SpriteSeamlessWave = ab.LoadAsset<Sprite>("seamless_milking_room_milk_tank_wave");

            ab.Unload(false);
        }
    }
}
