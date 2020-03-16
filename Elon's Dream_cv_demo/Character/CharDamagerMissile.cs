using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Char
{
    public class CharDamagerMissile : CharDamagerBase
    {
        [Tooltip("Скорость снаряда")]
        [SerializeField]
        float _missileSpeed;
        [Tooltip("Насколько далеко появляется снаряд от центра моба (чтобы не уничтожать об самого себя)")]
        [SerializeField]
        float _missileSpawnOffset;


        public float MissileSpeed { get => _missileSpeed; set => _missileSpeed = value; }
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void ShootTo(Vector3 targetPos)
        {
            var missileGO = Instantiate(gameObject, transform.parent.parent); //to go to mob and then to room
            var missileT = missileGO.transform;

            var direction = Vector3.Normalize(targetPos - transform.position);
            missileT.position = transform.position + direction * _missileSpawnOffset;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            missileT.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            missileGO.GetComponent<CharDamagerMissile>().Enable();
            missileGO.GetComponent<Rigidbody2D>().velocity = missileGO.transform.right * _missileSpeed;
        }

        public void ShootTo(float angle)
        {
            var missileGO = Instantiate(gameObject, transform.parent.parent); //to go to mob and then to room
            var missileT = missileGO.transform;

            missileT.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            missileT.position = transform.position + missileT.right * _missileSpawnOffset;

            missileGO.GetComponent<CharDamagerMissile>().Enable();
            missileGO.GetComponent<Rigidbody2D>().velocity = missileGO.transform.right * _missileSpeed;
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public new void OnTriggerEnter2D(Collider2D collided)
        {
            if (collided.CompareTag(_attackableTag))
            {
                DamageManually(collided.gameObject);
                Destroy(gameObject);
            }
            else if (collided.gameObject.layer == LayerMask.NameToLayer("ObstacleFull"))
                Destroy(gameObject);
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void Inverse(string attackableTag, float turnAngle)
        {
            var rig = GetComponent<Rigidbody2D>();

            rig.velocity = Quaternion.Euler(0, 0, turnAngle) * rig.velocity;
            _attackableTag = attackableTag;
        }
    }
}