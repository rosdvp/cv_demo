using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Player.Upgrades;

namespace Player
{
    public class PlayerUpgrader : MonoBehaviour
    {
        [Header("Basics")]
        [SerializeField]
        UpgradeAttackInterval _attackInterval;
        [SerializeField]
        UpgradeMoveSpeed _moveSpeed;
        [SerializeField]
        UpgradeMaxHealth _maxHealth;

        [Header("Attack")]
        [SerializeField]
        UpgradeAttackRange _attackRange;
        [SerializeField]
        UpgradeCircleAttack _circleAttack;
        [SerializeField]
        UpgradeMoveSpeedAfterKill _moveSpeedAfterKill;
        [SerializeField]
        UpgradeAttackIntervalAfterKill _attackIntervalAfterKill;

        [Header("Deffence")]
        [SerializeField]
        UpgradeImmuneAfterDamaged _immuneAfterDamaged;
        [SerializeField]
        UpgradeShield _shield;
        [SerializeField]
        UpgradeSpikes _spikes;
        [SerializeField]
        UpgradeBlockMissiles _blockMissiles;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public AUpgrade[] Upgrades => new AUpgrade[]
        {
            _attackInterval, _moveSpeed, _maxHealth, 
            _attackRange, _circleAttack, _moveSpeedAfterKill, _attackIntervalAfterKill,
            _immuneAfterDamaged, _shield, _spikes, _blockMissiles
        };

        public AUpgrade GetUpgrade(UpgradeId upgradeId)
            => Upgrades[(int)upgradeId];

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void Init(PlayerController player)
        {
            foreach (var upgrade in Upgrades)
                upgrade.InitIfExists(player);
        }
    }
}


public enum UpgradeId
{
    ATTACK_INTERVAL,
    MOVE_SPEED,
    MAX_HEALTH,

    ATTACK_RANGE,
    CIRCLE_ATTACK,
    MOVE_SPEED_AFTER_KILL,
    ATTACK_INTERVAL_AFTER_KILL,

    IMMUNE_AFTER_DAMAGED,
    SHIELD,
    SPIKES,
    BLOCK_MISSILES
}