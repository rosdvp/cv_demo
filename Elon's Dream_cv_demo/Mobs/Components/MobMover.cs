using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs.Components
{
    public class MobMover : MonoBehaviour, Buffs.IMoveParams
    {
        [Header("Params")]
        [Tooltip("Базовая скорость передвижения")]
        [SerializeField]
        float _moveSpeedBase;

        [Header("Components")]
        [SerializeField]
        MobMapper _mapper;
        [SerializeField]
        Rigidbody2D _rigidbody2D;


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public event System.Action<Vector3> EvMoveStart = delegate { };
        public event System.Action EvMoveEnd = delegate { };

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public EState State { get; private set; } = EState.Ready;

        float _moveSpeed;
        public float MoveSpeed
        {
            get => _moveSpeed;
            set
            {
                _moveSpeed = value;
                _rigidbody2D.velocity = Direction * value;
            }
        }
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void Awake()
        {
            MoveSpeed = _moveSpeedBase;

            EvMoveStart += (targetPos) =>
            {
                TargetPos = targetPos;
                _mapper.SetCurrCordToPos(targetPos);

                Direction = Vector3.Normalize(targetPos - transform.position);
                _rigidbody2D.velocity = Direction * MoveSpeed;
            };
            EvMoveEnd += () => _rigidbody2D.velocity = Vector3.zero;
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public Vector3 TargetPos { get; private set; }
        public Vector3 Direction { get; private set; }

        public void Begin(Vector3 targetPos)
        {
            if (Vector2.Distance(transform.position, targetPos) < 0.1f)
                return;

            State = EState.Doing;
            EvMoveStart(targetPos);
        }

        public void OnUpdate()
        {
            if (State == EState.Doing)
            {
                if (Vector2.Distance(transform.position, TargetPos) < 0.1f)
                {
                    State = EState.Ready;
                    EvMoveEnd();
                }
            }
        }

        public void ForceStop()
        {
            EvMoveEnd();
            _mapper.SetCurrCordToPos(_rigidbody2D.position);
            State = EState.Ready;
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void OnDestroy()
            => _mapper.ClearCord();
    }
}
