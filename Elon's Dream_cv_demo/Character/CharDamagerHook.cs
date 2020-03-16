using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Char
{
    public class CharDamagerHook : CharDamagerBase
    {
        [Tooltip("Базовая длина атаки")]
        [SerializeField]
        float _attackRangeBase;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public float AttackRange { get => _attackRangeBase; set => _attackRangeBase = value; }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void TurnTo(Vector3 sourcePos, Vector3 direction)
        {
            transform.position = sourcePos + direction * AttackRange;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}