using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class EffectRage : AEffect
    {
        [Header("Params")]
        [Tooltip("Изменение цвета")]
        [SerializeField]
        Color _color;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        protected override void OnEffectStart()
        {
            Target._renderer.color -= Color.white - _color;
            Target._ragePS.gameObject.SetActive(true);
        }

        protected override void OnEffectEnd()
        {
            Target._renderer.color += Color.white - _color;
            Target._ragePS.gameObject.SetActive(false);
        }

        public override AEffect Clone()
            => new EffectRage
            {
                _id = _id,
                _duration = _duration,
                _color = _color
            };
    }
}