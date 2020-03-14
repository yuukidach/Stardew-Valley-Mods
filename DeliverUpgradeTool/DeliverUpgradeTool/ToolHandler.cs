using System;
using StardewValley;
using StardewValley.Tools;

namespace DeliverUpgradeTool
{
    class ToolHandler
    {
        private Tool _toolBeingUpgrade;
        private String TRASH_SPELL;

        public ToolHandler(String TrashSpell) {
            TRASH_SPELL = TrashSpell;
        }

        public bool IsToolUpgradeFinished() {
            return Game1.player.hasReceivedToolUpgradeMessageYet;
        }


        public void ClearToolInClint() {
            _toolBeingUpgrade = null;
            Game1.player.hasReceivedToolUpgradeMessageYet = false;
            Game1.player.toolBeingUpgraded.Value = null;
        }


        private void _UpdateUpgradedToolInfo() {
            _toolBeingUpgrade = Game1.player.toolBeingUpgraded.Value;
        }


        public String GetName() {
            _UpdateUpgradedToolInfo();
            return _toolBeingUpgrade.Name;
        }


        public int GetUpgradeLevel() {
            _UpdateUpgradedToolInfo();
            return _toolBeingUpgrade.UpgradeLevel;
        }


        public void GetUpgradeTool() {
            // Avoid putting the trash can in user's packet.
            _UpdateUpgradedToolInfo();
            // TODO: this is a temporary solution, since the translation function cannot work
            // in my computer
            if (_toolBeingUpgrade.Name.Contains(TRASH_SPELL)
                || _toolBeingUpgrade.Name.Contains("垃圾")) {
                Game1.player.trashCanLevel += 1;
                return;
            }
            Game1.player.addItemByMenuIfNecessary(_toolBeingUpgrade);
        }
    }
}
