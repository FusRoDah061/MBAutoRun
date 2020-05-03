using BannerLib.Input;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaleWorlds.MountAndBlade;

namespace MBAutoRun
{
    public class SubModule : MBSubModuleBase
    {

        private HotKeyManager _hotkeyManager;

        protected override void OnSubModuleLoad()
        {
            try
            {
                Harmony harmonyPatch = new Harmony("mbbannerlord.autorun");
                harmonyPatch.PatchAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed hooking fluid attacking code:\n" + ex.ToString());
            }
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            try
            {
                if (_hotkeyManager == null)
                {
                    _hotkeyManager = HotKeyManager.Create("mbbannerlord.autorun");
                    _hotkeyManager.Add<AutoRunHotKey>();
                    _hotkeyManager.Build();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed initializing auto-run hotkey:\n" + ex.ToString());
            }
        }
    }
}
