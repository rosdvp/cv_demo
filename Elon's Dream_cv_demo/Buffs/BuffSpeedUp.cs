using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    [System.Serializable]
    public class BuffSpeedUp : ABuff
    {
        [SerializeField]
        float _moveSpeedK;


        protected override void OnBuffStart()
            => Target.MoveParams.MoveSpeed *= _moveSpeedK;

        protected override void OnBuffEnd()
            => Target.MoveParams.MoveSpeed /= _moveSpeedK;

        public override ABuff Clone()
            => new BuffSpeedUp()
            {
                _id = _id,
                _duration = _duration,
                _effectId = _effectId,
                _moveSpeedK = _moveSpeedK
            };
    }
}