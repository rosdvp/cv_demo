using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace Char
{
    public class CharHealtherBase : MonoBehaviour, IAttackable, IStunnable, Buffs.IHealthParams
    {
        [Tooltip("Кол-во хп на старте")]
        [SerializeField]
        int _health;


        TargetEffects TargetEffects => transform.parent.GetComponent<TargetEffects>();

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public event System.Action<GameObject> EvHitted = delegate { };
        public event System.Action EvDamaged = delegate { };
        public event System.Action EvKilled = delegate { };

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public bool IsStunned { get; set; }
        public int ImmuneCounter { get; set; }
        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public int Health { get => _health; set => _health = value; }

        public bool TakeDamage(GameObject sender)
        {
            EvHitted(sender);

            if (ImmuneCounter > 0)
                return false;

            Health -= 1;
            EvDamaged();
            if (Health > 0)
                Effecter.PlayEffect(TargetEffects, EffectId.Damaged);
            else
            {
                CreateDeathEffect(sender.transform.position);
                EvKilled();
            }

            return true;
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        void CreateDeathEffect(Vector3 fromPos)
        {
            Vector3 direction = (transform.position - fromPos).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            var deathGO = Effecter.GetEffectDeathInstance();
            deathGO.transform.position = transform.position;
            deathGO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}