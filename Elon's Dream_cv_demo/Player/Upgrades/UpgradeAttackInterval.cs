using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeAttackInterval : AUpgradeBasic
    {
        [Header("Params")]
        [SerializeField]
        float _intervalDelta;


        public override void InitIfExists(PlayerController player)
        {
            player.Attacker.Base.AttackInterval += 
                Global.Modeler.Session.UpgradeCount(UpgradeId.ATTACK_INTERVAL) * _intervalDelta;
        }
    }
}