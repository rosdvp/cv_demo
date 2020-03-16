using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeMoveSpeed : AUpgradeBasic
    {
        [Header("Params")]
        [SerializeField]
        float _speedDeltaPercents;


        public override void InitIfExists(PlayerController player)
        {
            player.Mover.MoveSpeed *= 1 + 
                Global.Modeler.Session.UpgradeCount(UpgradeId.MOVE_SPEED) * _speedDeltaPercents;
        }
    }
}