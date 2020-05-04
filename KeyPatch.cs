using HarmonyLib;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace MBAutoRun
{
    [HarmonyPatch(typeof(Key), "IsDown")]
    public class KeyPatch
    {

        private static int KEY_FORWARD  = 0;  // keyboard W
        private static int KEY_BACKWARD = 1; // keyboard S

        public static void Postfix(Key __instance, ref bool __result)
        {
            if (!AutoRunHotKey.IsKeyActive || !__instance.IsKeyboardInput) return;

            // If "S" is pressed we want to ignore auto-run so the player can walk backwards
            GameKey backward = _getKey(KEY_BACKWARD);
            if (_isKeyDown(backward)) return;

            GameKey forward = _getKey(KEY_FORWARD);

            if ( (__instance.IsControllerInput && __instance.InputKey == forward.ControllerKey.InputKey) ||
                 __instance.InputKey == forward.PrimaryKey.InputKey)
                __result = true;
        }

        private static GameKey _getKey(int id)
        {
            return GenericGameKeyContext.Current.GameKeys.Find((GameKey g) => g.Id == id);
        }

        private static bool _isKeyDown(GameKey key)
        {
            return key.PrimaryKey.InputKey.IsDown() || key.ControllerKey.InputKey.IsDown();
        }
    }
}
