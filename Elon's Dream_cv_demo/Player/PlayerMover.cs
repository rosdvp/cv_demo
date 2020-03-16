using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharAnimator = Char.CharAnimator;
using ControlScreen = Screens.ControlScreen;

namespace Player
{
    public class PlayerMover : MonoBehaviour, Buffs.IMoveParams
    {
        [Header("Params")]
        [Tooltip("Базовая скорость передвижения игрока")]
        [SerializeField]
        float _moveSpeedBase;

        [Header("Dependencies")]
        [SerializeField]
        CharAnimator _playerAnimator;
        [SerializeField]
        ControlScreen _controlScreen;
        [SerializeField]
        PlayerLooker _looker;

        [Header("Components")]
        [SerializeField]
        Rigidbody2D _playerRIG;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public float MoveSpeed { get => _moveSpeedBase; set => _moveSpeedBase = value; }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public void FixedUpdate()
        {
            var direction = _controlScreen.JoystickUI.Direction;
            if (direction == Vector3.zero)
                _playerAnimator.SetLegsIdle();
            else
            {
                _playerAnimator.SetLegsMove();
                _looker.MoveDirection = direction;
            }

            _playerRIG.velocity = direction * MoveSpeed;
        }
    }
}
