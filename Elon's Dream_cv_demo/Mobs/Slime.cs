using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mobs.Components;
using Timer = Stuff.Timer;
using CharDamagerBase = Char.CharDamagerBase;
using CharAnimator = Char.CharAnimator;

namespace Mobs
{
    public class Slime : MonoBehaviour
    {
        [Header("Params")]
        [Tooltip("Время между рывками")]
        [SerializeField]
        float _shiftInteval;
        [Tooltip("Расстояние рывка к игроку (остальные рывки - 1 клетка)")]
        [SerializeField]
        float _shiftDistance;

        [Header("Components")]
        [SerializeField]
        MobPlayerDetector _detector;
        [SerializeField]
        MobMover _mover;
        [SerializeField]
        CharDamagerBase _damager;
        [SerializeField]
        MobHealther _healther;
        [SerializeField]
        CharAnimator _charAnim;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        private void Start()
        {
            _mover.EvMoveStart += (targetPos) =>
            {
                _charAnim.SetLookAt(targetPos);
                _charAnim.SetLegsMove();

                _damager.Enable();
            };
            _mover.EvMoveEnd += delegate
            {
                _charAnim.SetLegsIdle();

                _damager.Disable();
                ShiftIntervalTimer.Set(_shiftInteval);
            };

            _healther.Base.EvKilled += () => Destroy(gameObject);
        }


        Timer ShiftIntervalTimer { get; set; } = new Timer();

        private void FixedUpdate()
        {
            if (_healther.Base.IsStunned)
                return;

            _mover.OnUpdate();

            if (_mover.State == EState.Ready && ShiftIntervalTimer.IsDone)
            {
                if (_detector.IsPlayerDetected)
                {
                    if (_detector.DistToPlayer <= _shiftDistance)
                        _mover.Begin(_detector.PlayerPos);
                    else
                        _mover.Begin(Stuff.PathUtil.GetNextPosToTarget(transform.position, _detector.PlayerPos));
                }
                else
                    _mover.Begin(Stuff.PathUtil.GetNextPosToRandom(transform.position));
            }
        }
    }
}