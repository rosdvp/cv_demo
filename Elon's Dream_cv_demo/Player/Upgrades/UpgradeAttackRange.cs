using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeAttackRange : AUpgrade
    {
        [Header("Params")]
        [SerializeField]
        float _rangeK;

        [Header("Assets")]
        [SerializeField]
        Sprite _hookSprite;


        public override void InitIfExists(PlayerController player)
        {
            if (Global.Modeler.Session.Upgrades.Contains(UpgradeId.ATTACK_RANGE))
            {
                player.Attacker.Damager.AttackRange *= _rangeK;
                player.Attacker.Damager.GetComponent<SpriteRenderer>().sprite = _hookSprite;
            }
        }
    }
}