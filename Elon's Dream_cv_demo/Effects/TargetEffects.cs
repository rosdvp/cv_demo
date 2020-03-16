using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class TargetEffects : MonoBehaviour
    {
        [SerializeField]
        public SpriteRenderer _renderer;
        [SerializeField]
        public ParticleSystem _ragePS;
        [SerializeField]
        public ParticleSystem _spikesPS;
        [SerializeField]
        public Animator _blockMissilesAnim;
    }
}