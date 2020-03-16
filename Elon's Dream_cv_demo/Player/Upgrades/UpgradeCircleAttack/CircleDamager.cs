using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Upgrades.CircleAttack
{
    public class CircleDamager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        Char.CharDamagerHook _damager;

        [Header("Components")]
        [SerializeField]
        Animator _anim;


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void ActivateTo(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Consts.CourutineHolder.StartCoroutine(
                _anim.CrCustomAnimation(
                    stateName: "State_appear",
                    withGOactivation: true,
                    withGOdisactivation: true));
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.CompareTag("Mob"))
                return;

            _damager.DamageManually(collider.gameObject);
        }
    }
}