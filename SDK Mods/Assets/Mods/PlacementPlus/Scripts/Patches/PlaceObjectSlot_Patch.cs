﻿using HarmonyLib;
using UnityEngine;

namespace PlacementPlus
{
    [HarmonyPatch]
    public static class PlaceObjectSlot_Patch
    {
      /*  [HarmonyPatch(typeof(PlaceObjectSlot), "PlaceItem")]
        [HarmonyPrefix]
        public static bool OnPlace(PlaceObjectSlot __instance)
        {
            PlacementHandler.SetAllowPlacingAnywhere(false);

            PlayerController pc = __instance.slotOwner;
            ObjectDataCD item = pc.GetHeldObject();
            Vector3Int pos = __instance.placementHandler.bestPositionToPlaceAt;

            if (BrushExtension.size == 0 ||
                BrushExtension.mode == BrushMode.NONE)
            {
                if (PugDatabase.HasComponent<TileCD>(item) && BrushExtension.replaceTiles)
                {
                    bool used1 = false, used2 = false; 
                    return BrushExtension.HandleReplaceLogic(__instance, pos.ToInt2(), true, ref used1, ref used2);
                }
            
                return true;
            }

            ObjectInfo itemInfo = __instance.placementHandler.GetInfoAboutObjectToPlace_Public();
            if (!BrushExtension.IsItemValid(itemInfo)) return true;

            BrushExtension.PlayEffects(__instance, pos, itemInfo);
            BrushExtension.PlaceGrid(__instance, pos, item, itemInfo);

            return false;
        }*/

       /* [HarmonyPatch(typeof(PlaceObjectSlot), "Rotate")]
        [HarmonyPostfix]
        public static void OnRotate(PlaceObjectSlot __instance)
        {
            BrushExtension.TryRotate(__instance);
        }

        [HarmonyPatch(typeof(PaintToolSlot), "PlaceItem")]
        [HarmonyPrefix]
        public static bool OnPaint(PaintToolSlot __instance)
        {
            PlacementHandler.SetAllowPlacingAnywhere(false);
            if (BrushExtension.size == 0 ||
                BrushExtension.mode == BrushMode.NONE) return true;

            PlacementHandlerPainting handler = __instance.placementHandler as PlacementHandlerPainting;

            ObjectDataCD item = __instance.objectData;
            if (item.objectID <= 0) return true;
            if (!PugDatabase.HasComponent<PaintToolCD>(item)) return true;

            BrushExtension.PaintGrid(__instance, handler);

            return false;
        }*/
    }
}