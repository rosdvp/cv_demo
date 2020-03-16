using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buffs;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeAttackIntervalAfterKill : AUpgrade
    {   
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public override void InitIfExists(PlayerController player)
        {
            if (!Global.Modeler.Session.Upgrades.Contains(UpgradeId.ATTACK_INTERVAL_AFTER_KILL))
                return;

            player.Attacker.Damager.EvKill += () => Buffer.PlayBuff(player.TargetBuffs, BuffId.KillRage);
        }
    }
}