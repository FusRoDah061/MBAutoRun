using HarmonyLib;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace MBAutoRun
{
    [HarmonyPatch(typeof(Key), "IsDown")]
    public class KeyPatch
    {

        public static void Postfix(Key __instance, ref bool __result)
        {
            if (!AutoRunHotKey.IsKeyActive || !__instance.IsKeyboardInput) return;

            // If "S" is pressed we want to ignore auto-run so the player can walk backwards
            GameKey sKey = GenericGameKeyContext.Current.GameKeys.Find((GameKey g) => g.PrimaryKey.InputKey == InputKey.S);
            if (sKey.PrimaryKey.InputKey.IsDown()) return;

            if (__instance.InputKey == InputKey.W)
                __result = true;
        }

    }
}
