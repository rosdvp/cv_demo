using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class EffectColor : AEffect
    {
        [Header("Params")]
        [Tooltip("На какой цвет меняется")]
        [SerializeField]
        Color _color;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        protected override void OnEffectStart()
            => Target._renderer.color -= Color.white - _color;

        protected override void OnEffectEnd()
            => Target._renderer.color += Color.white - _color;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public override AEffect Clone()
            => new EffectColor
            {
                _id = _id,
                _duration = _duration,
                _color = _color,
            };
    }
}