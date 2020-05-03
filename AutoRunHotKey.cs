using BannerLib.Input;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace MBAutoRun
{
    public class AutoRunHotKey : HotKeyBase
    {

        public static bool IsKeyActive { get; set; } = false;

        public AutoRunHotKey() : base(nameof(AutoRunHotKey))
        {
            DisplayName = "Auto-run";
            Description = "Toggle auto-run. Once activated, will make the player character run until presses again.";
            DefaultKey = InputKey.LeftControl;
            Category = BannerLib.Input.HotKeyManager.Categories[HotKeyCategory.Action];
            Predicate = new Func<bool>(this._isKeyAllowed);
        }

        private bool _isKeyAllowed()
        {
            bool ret;
            if (Campaign.Current != null)
                ret = (
                    Mission.Current != null 
                    && Mission.Current.MainAgent != null 
                    && !Campaign.Current.ConversationManager.ConversationIsInProgress);
            else
                ret = (
                    Mission.Current != null 
                    && Mission.Current.MainAgent != null);

            if (!ret) IsKeyActive = false;

            return ret;
        }

        protected override void OnReleased()
        {
            string log = "Auto-run is ";

            if (IsKeyActive)
            {
                IsKeyActive = false;
                log += "off.";
            }
            else
            {
                IsKeyActive = true;
                log += "on.";
            }

            InformationManager.DisplayMessage(new InformationMessage(log));
        }
    }
}
