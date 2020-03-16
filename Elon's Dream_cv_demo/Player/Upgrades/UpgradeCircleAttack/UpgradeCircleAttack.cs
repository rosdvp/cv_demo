using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Upgrades.CircleAttack;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeCircleAttack : AUpgrade
    {
        [Header("Params")]
        [SerializeField]
        float _interval;

        [Header("Dependencies")]
        [SerializeField]
        CircleDamager _circleDamager;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        Stuff.Timer CircleAttackIntervalTimer { get; set; } = new Stuff.Timer();

        public override void InitIfExists(PlayerController player)
        {
            if (!Global.Modeler.Session.Upgrades.Contains(UpgradeId.CIRCLE_ATTACK))
                return;


            player.Attacker.Base.EvAttackStart += () =>
            {
                if (CircleAttackIntervalTimer.IsDone)
                {
                    player.Attacker.Damager.gameObject.SetActive(false);
                    _circleDamager.ActivateTo(player.Looker.CurrDirection);
                    CircleAttackIntervalTimer.Set(_interval);
                }
            };
        }
    }
}