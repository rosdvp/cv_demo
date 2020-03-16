using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SafeZone = Room.Objects.SafeZone;

namespace Mobs.Components
{
    public class MobPlayerDetector : MonoBehaviour
    {
        [Tooltip("Расстояние, с коротого моб видит игрока")]
        [Header("Params")]
        [SerializeField]
        float _detectDistance;


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public Transform PlayerT { get; private set; }
        public Vector3 PlayerPos => PlayerT.position;
        public float DistToPlayer => Vector3.Distance(PlayerPos, transform.position);
        public bool IsPlayerDetected => PlayerT.gameObject.activeInHierarchy
            && !IsSafeZoneEnabled
            && DistToPlayer < _detectDistance;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        bool IsSafeZoneEnabled { get; set; }
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        void Awake()
        {
            PlayerT = GameObject.Find("Player").GetComponent<Transform>();

            IsSafeZoneEnabled = true;
            GameObject.Find("SafeZone").GetComponent<SafeZone>().EvClear += () => IsSafeZoneEnabled = false;
        }
    }
}