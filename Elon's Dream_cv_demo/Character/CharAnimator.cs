using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Char
{
    public class CharAnimator : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        Animator _animator;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void SetLookAt(Vector3 targetPos)
            => SetLookDirection(Vector3.Normalize(targetPos - transform.position));

        public void SetLookDirection(Vector2 direction)
        {
            var angle = Vector2.SignedAngle(Vector2.left, direction);
            if (angle < 0)
                angle *= -1;
            else
                angle = 360 - angle;
            var dir = Mathf.RoundToInt(angle / 45.0f);
            _animator.SetInteger("Direction", dir > 7 ? 0 : dir);

            SetAnim();
        }

        public void SetLegsIdle()
        {
            IsWalking = false;
            SetAnim();
        }

        public void SetLegsMove()
        {
            IsWalking = true;
            SetAnim();
        }

        public void SetArmsIdle()
        {
            ArmState = 0;
            SetAnim();
        }

        public void SetArmsPrepare()
        {
            ArmState = 1;
            SetAnim();
        }

        public void SetArmsAttack()
        {
            ArmState = 2;
            SetAnim();
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        int ArmState { get; set; }
        bool IsWalking { get; set; }

        void SetAnim()
        {
            if (ArmState == 0)
            {
                _animator.speed = 1;
                _animator.SetInteger("State", IsWalking ? 1 : 0);
            }
            else
            {
                _animator.speed = IsWalking ? 1 : 0;
                _animator.SetInteger("State", ArmState + 1);
            }
        }
    }
}