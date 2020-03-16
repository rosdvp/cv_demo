using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class EffectBlockMissiles : AEffect
    {
        protected override void OnEffectStart()
        {
            if (!Target._blockMissilesAnim.gameObject.activeInHierarchy)
                Target._blockMissilesAnim.gameObject.SetActive(true);
            Target._blockMissilesAnim.SetBool("Appear", true);
        }

        protected override void OnEffectEnd()
            => Target._blockMissilesAnim.SetBool("Appear", false);

        public override AEffect Clone()
            => new EffectBlockMissiles()
            {
                _id = _id,
                _duration = _duration
            };
    }
}