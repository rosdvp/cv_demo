using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buffs;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeMoveSpeedAfterKill : AUpgrade
    {
        public override void InitIfExists(PlayerController player)
        {
            if (!Global.Modeler.Session.Upgrades.Contains(UpgradeId.MOVE_SPEED_AFTER_KILL))
                return;

            player.Attacker.Damager.EvKill += () => Buffs.Buffer.PlayBuff(player.TargetBuffs, BuffId.KillSpeedUp);
        }
    }
}