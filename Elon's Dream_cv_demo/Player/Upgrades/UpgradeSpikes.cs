using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeSpikes : AUpgrade
    {
        [Header("Params")]
        [Tooltip("Как далеко наносится ответный урон (не в клетках, на глаз, 1 клетка = 1.35)")]
        [SerializeField]
        float _distance;


        public override void InitIfExists(PlayerController player)
        {
            if (Global.Modeler.Session.Upgrades.Contains(UpgradeId.SPIKES))
                player.Healther.Base.EvHitted += (mob) =>
                {
                    if (mob && Vector3.Distance(mob.transform.position, player.transform.position) <= _distance)
                        player.Attacker.Damager.DamageManually(mob.gameObject);
                    Effecter.PlayEffect(player.TargetBuffs.TargetEffects, EffectId.Spikes);
                };
        }
    }
}