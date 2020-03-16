using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Char
{
    public class CharDamagerBase : MonoBehaviour
    {
        [Tooltip("Тэг существа для нанесения урона")]
        [SerializeField]
        protected string _attackableTag;

        [Tooltip("Самый главный GO персонажа")]
        [SerializeField]
        GameObject _rootGO;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public event System.Action EvHit = delegate { };
        public event System.Action EvKill = delegate { };
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void Enable()
            => gameObject.SetActive(true);

        public void Disable()
            => gameObject.SetActive(false);

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void OnTriggerEnter2D(Collider2D collided)
        {
            if (collided.CompareTag(_attackableTag))
                DamageManually(collided.gameObject);
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void DamageManually(GameObject target)
        {
            var attackable = target.GetComponentInChildren<IAttackable>();
            var isKilled = attackable.TakeDamage(_rootGO.gameObject);

            EvHit();
            if (isKilled)
                EvKill();
        }

        public void TrigHitManually()
            => EvHit();

        public void TrigKillManually()
            => EvKill();
    }
}