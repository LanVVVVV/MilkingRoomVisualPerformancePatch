//using DG.Tweening;
//using HarmonyLib;
//using MBMScripts;
//using System.Collections.Generic;
//using UnityEngine;

//namespace MilkingRoomVisualPerformancePatch.Abandoned;

//[HarmonyPatch(typeof(UpdaterTransformMoter), "Display")]
//public class UpdaterTransformMoterPatch
//{
//    [HarmonyPrefix]
//    static bool DisplayPrefix(UpdaterTransformMoter __instance,
//        List<Reference> ___m_ReferenceArray,
//        float ___m_FillTime,
//        float ___m_DisplayMilkStorageCapacityPercent
//        //float ___m_CurrentFillTime
//        //float ___m_MilkStorageCapacityPercent
//        )
//    {
//        foreach (Reference reference in ___m_ReferenceArray)
//        {
//            if (reference.ReferenceType == EReferenceType.Float)
//            {
//                var milkStorageCapacityPercent = reference.GetFloat();

//                float targetY = Mathf.Lerp(-0.92f, 0f, milkStorageCapacityPercent);

//                if (milkStorageCapacityPercent == ___m_DisplayMilkStorageCapacityPercent)
//                {
//                    Vector3 localPosition = __instance.transform.localPosition;
//                    localPosition.y = targetY;
//                    __instance.transform.localPosition = localPosition;
//                    break;
//                }

//                __instance.transform.DOPause();
//                __instance.transform.DOKill();
//                __instance.transform.DOLocalMoveY(targetY, ___m_FillTime).SetEase(Ease.OutSine);
//                break;
//            }
//        }
//        return false;
//    }
//}