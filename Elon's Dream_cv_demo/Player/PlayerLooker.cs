using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RoomMobsPlacer = Room.RoomMobsPlacer;
using CharAnimator = Char.CharAnimator;

namespace Player
{
    public class PlayerLooker : MonoBehaviour
    {
        [Header("Params")]
        [Tooltip("Дистанция, с которой игрок прицеливается на моба")]
        [SerializeField]
        float _aimDistance;
        [Tooltip("Раз в сколько кадров происходит перенацеливания игрока на врага")]
        [SerializeField]
        int _framesPerAim;

        [Header("Dependencies")]
        [SerializeField]
        RoomMobsPlacer _mobsPlacer;
        [SerializeField]
        CharAnimator _animator;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public Vector3 MoveDirection { private get; set; } = Vector3.left;
        Transform AimedMob { get; set; }
        int FramesPerAimCounter { get; set; }

        private void FixedUpdate()
        {
            if (FramesPerAimCounter-- < 0)
            {
                CheckAim();
                FramesPerAimCounter = _framesPerAim;
            }

            _animator.SetLookDirection(CurrDirection);
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public Vector3 CurrDirection
            => AimedMob ? Vector3.Normalize(AimedMob.position - transform.position) : MoveDirection;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        void CheckAim()
        {
            AimedMob = null;

            var closestDist = -1.0f;
            Transform closestEnemyT = null;
            foreach (var enemy in _mobsPlacer.MobsInRoom)
            {
                if (enemy == null)
                    continue;

                var dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist < closestDist || closestDist == -1)
                {
                    closestEnemyT = enemy;
                    closestDist = dist;
                }
            }

            if (closestEnemyT && closestDist < _aimDistance)
                AimedMob = closestEnemyT;
        }
    }
}
