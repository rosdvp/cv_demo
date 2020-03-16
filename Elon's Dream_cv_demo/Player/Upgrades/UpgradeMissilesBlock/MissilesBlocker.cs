using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharDamagerMissile = Char.CharDamagerMissile;

namespace Player.Upgrades.BlockMissiles
{
    public class MissilesBlocker : MonoBehaviour
    {
        List<GameObject> InversedMissiles { get; set; } = new List<GameObject>();

        private void OnTriggerEnter2D(Collider2D collided)
        {
            if (collided.CompareTag("Missile") && !InversedMissiles.Contains(collided.gameObject))
            {
                collided.GetComponent<CharDamagerMissile>().Inverse("Mob", 180);
                InversedMissiles.Add(collided.gameObject);
            }
        }
    }
}
