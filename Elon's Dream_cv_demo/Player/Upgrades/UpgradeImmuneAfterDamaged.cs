using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buffs;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeImmuneAfterDamaged : AUpgrade
    {
        public override void InitIfExists(PlayerController player)
        {
            var isLong = Global.Modeler.Session.Upgrades.Contains(UpgradeId.IMMUNE_AFTER_DAMAGED);
            player.Healther.Base.EvDamaged += delegate
            {
                Buffer.PlayBuff(player.TargetBuffs, isLong ? BuffId.ImmuneLong : BuffId.ImmuneBase);
            };
        }
    }
}