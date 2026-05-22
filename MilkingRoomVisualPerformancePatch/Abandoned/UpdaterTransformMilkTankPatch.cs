//using DG.Tweening;
//using HarmonyLib;
//using MBMScripts;
//using System.Collections.Generic;
//using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

//namespace MilkingRoomVisualPerformancePatch.Abandoned;

//[HarmonyPatch(typeof(UpdaterTransformMilkTank), "Display")]
//public class UpdaterTransformMilkTankPatch
//{
//    [HarmonyPrefix]
//    static bool DisplayPrefix(UpdaterTransformMoter __instance,
//        List<Reference> ___m_ReferenceArray,
//        float ___m_FillTime,
//        float ___m_DisplayMilkTankCapacityPercent
//        //float ___m_CurrentFillTime
//        //float ___m_MilkTankCapacityPercent
//        )
//    {
//        foreach (Reference reference in ___m_ReferenceArray)
//        {
//            if (reference.ReferenceType == EReferenceType.Float)
//            {
//                var milkTankCapacityPercent = reference.GetFloat();

//                float targetY = Mathf.Lerp(-3f, 0f, milkTankCapacityPercent);

//                if (milkTankCapacityPercent == ___m_DisplayMilkTankCapacityPercent)
//                {
//                    Vector3 localPosition = __instance.transform.localPosition;
//                    localPosition.y = targetY;
//                    __instance.transform.localPosition = localPosition;
//                    break;
//                }

//                targetY -= 0.2f;

//                __instance.transform.DOPause();
//                __instance.transform.DOKill();
//                __instance.transform.DOLocalMoveY(targetY, ___m_FillTime).SetEase(Ease.OutSine);
//                break;
//            }
//        }
//        return false;
//    }
//}