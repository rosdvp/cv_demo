using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effecter = Effects.Effecter;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeBlockMissiles : AUpgrade
    {
        [Header("Components")]
        [SerializeField]
        GameObject _blockMissilesGO;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public override void InitIfExists(PlayerController player)
        {
            if (!Global.Modeler.Session.Upgrades.Contains(UpgradeId.BLOCK_MISSILES))
                return;

            player.Attacker.Base.EvAttackStart += delegate
            {
                IEnumerator CrCircleAttack()
                {
                    Effecter.SetEffect(player.TargetBuffs.TargetEffects, EffectId.BlockMissiles, true);
                    _blockMissilesGO.SetActive(true);
                    yield return new WaitForSeconds(player.Attacker.Base.AttackDuration);
                    _blockMissilesGO.SetActive(false);
                    Effecter.SetEffect(player.TargetBuffs.TargetEffects, EffectId.BlockMissiles, false);
                }
                Consts.CourutineHolder.StartCoroutine(CrCircleAttack());
            };
        }
    }
}