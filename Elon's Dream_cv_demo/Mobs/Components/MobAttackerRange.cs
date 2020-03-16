using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Char;

namespace Mobs.Components
{
    public class MobAttackerRange : MonoBehaviour
    {
        [Header("Params")]
        [Tooltip("Дистанция с которой атака считается возможной")]
        [SerializeField]
        float _attackDistance;
        [Tooltip("Толщина снаряда (для проверки есть ли прямой коридор для выстрела)")]
        [SerializeField]
        float _missileWidth;

        [Header("Components")]
        [SerializeField]
        MobPlayerDetector _detector;
        [SerializeField]
        CharAttackerBase _attacker;
        [SerializeField]
        CharDamagerMissile _damager;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public CharAttackerBase Base => _attacker;
        public CharDamagerMissile Damager => _damager;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public bool IsPossible
            => _detector.IsPlayerDetected 
            && _detector.DistToPlayer <= _attackDistance
            && Stuff.DirectLineUtil.IsDirectShootable(transform.position, _detector.PlayerPos, _missileWidth);

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void Awake()
        {
            _attacker.EvAttackStart += () => _damager.ShootTo(_detector.PlayerPos);
            _attacker.EvAttackEnd += _damager.Disable;
        }
    }
}
