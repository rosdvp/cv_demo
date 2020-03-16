using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Char;

namespace Mobs.Components
{
    public class MobAttackerMelee : MonoBehaviour
    {
        [Header("Params")]
        [Tooltip("Дистанция с которой атака считается возможной")]
        [SerializeField]
        float _attackDistance;

        [Header("Components")]
        [SerializeField]
        CharAttackerBase _attacker;
        [SerializeField]
        CharDamagerHook _damager;
        [SerializeField]
        MobPlayerDetector _detector;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public CharAttackerBase Base => _attacker;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public bool IsPossible
            => _detector.IsPlayerDetected && _detector.DistToPlayer <= _attackDistance;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void Awake()
        {
            _attacker.EvPrepareStart += delegate
            {
                var direction = (_detector.PlayerPos - transform.position).normalized;
                _damager.TurnTo(transform.position, direction);
            };
            _attacker.EvAttackStart += _damager.Enable;
            _attacker.EvAttackEnd += _damager.Disable;
        }
    }
}