//using HarmonyLib;
//using MBMScripts;
//using System.Collections.Generic;
//using UnityEngine;

//namespace MilkingRoomVisualPerformancePatch.Abandoned;

//[HarmonyPatch(typeof(UpdaterTransformMilkTankWave))]
//public class UpdaterTransformMilkTankWavePatch
//{
//    //private static readonly Dictionary<UpdaterTransformMilkTankWave, SpriteRenderer>
//    //    RendererCache = new Dictionary<UpdaterTransformMilkTankWave, SpriteRenderer>();

//    [HarmonyPatch("Display")]
//    [HarmonyPrefix]
//    static bool DisplayPrefix(
//        UpdaterTransformMilkTankWave __instance,
//        List<Reference> ___m_ReferenceArray,
//        //float ___m_FillTime,
//        float ___m_WaveSpeed,
//        float ___m_BreakerSpeed,
//        //float ___m_OffsetX,
//        //float ___m_DisplayMilkTankCapacityPercent,
//        ref float ___m_CurrentFillTime,
//        ref float ___m_MilkTankCapacityPercent,
//        ref float ___m_CurrentWaveSpeed
//        )
//    {
//        foreach (Reference reference in ___m_ReferenceArray)
//        {
//            if (reference.ReferenceType == EReferenceType.Float)
//            {
//                ___m_MilkTankCapacityPercent = reference.GetFloat();
//                float allNum = (GameManager.ConfigData.MilkTankCapacity / GameManager.ConfigData.MilkStorageCapacity);

//                float currentY = __instance.transform.parent.transform.localPosition.y;
//                float DisplayMilkTankCapacityPercent = (currentY + 3f) / 3f;

//                float num = Mathf.Abs(DisplayMilkTankCapacityPercent - ___m_MilkTankCapacityPercent) * allNum;
//                ___m_CurrentWaveSpeed = Mathf.Lerp(___m_WaveSpeed, ___m_BreakerSpeed, num);
//                ___m_CurrentFillTime = 0;
//                break;
//            }
//        }
//        return false;
//    }

//    [HarmonyPatch("Update")]
//    [HarmonyPrefix]
//    static bool UpdatePrefix(UpdaterTransformMilkTankWave __instance,
//        List<Reference> ___m_ReferenceArray,
//        float ___m_FillTime,
//        float ___m_WaveSpeed,
//        //float ___m_BreakerSpeed,
//        float ___m_OffsetX,
//        //float ___m_DisplayMilkTankCapacityPercent,
//        ref float ___m_CurrentFillTime,
//        //float ___m_MilkTankCapacityPercent,
//        float ___m_CurrentWaveSpeed
//        )
//    {
//        float waveOffset = 0f;

//        if (___m_CurrentFillTime <= ___m_FillTime)
//        {
//            waveOffset = Mathf.Lerp(___m_CurrentWaveSpeed, ___m_WaveSpeed, Mathf.Clamp01(___m_CurrentFillTime / ___m_FillTime)) * Time.deltaTime;
//            ___m_CurrentFillTime += Time.deltaTime;
//        }
//        else
//        {
//            waveOffset = ___m_WaveSpeed * Time.deltaTime;
//        }

//        Vector3 localPosition = __instance.transform.localPosition;
//        localPosition.x += waveOffset;
//        if (localPosition.x > ___m_OffsetX)
//        {
//            localPosition.x = 0f;
//        }
//        __instance.transform.localPosition = localPosition;

//        return false;
//    }
//}