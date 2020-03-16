using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    [System.Serializable]
    public class EffectSpeedUp : AEffect
    {
        [Header("Params")]
        [Tooltip("Время жизни копии (секунды)")]
        [SerializeField]
        float _copyLifetime;
        [Tooltip("Время между появлением копий (секунды)")]
        [SerializeField]
        float _delayBetweenCopies;

        [Header("Assets")]
        [SerializeField]
        SpriteRenderer _prefCopy;

        [Header("Components")]
        [SerializeField]
        Transform _copiesHolder;


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        protected override void OnEffectStart()
            => Consts.CourutineHolder.StartCoroutine(CrEffect());

        protected override void OnEffectEnd() { }

        IEnumerator CrEffect()
        {
            while (IsPlaying)
            {
                Consts.CourutineHolder.StartCoroutine(CrCreateCopy());
                yield return new WaitForSeconds(_delayBetweenCopies);
            }
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        IEnumerator CrCreateCopy()
        {
            var copy = GetFreeCopy();
            copy.sprite = Target._renderer.sprite;
            copy.transform.position = Target.transform.position;
            copy.gameObject.SetActive(true);

            yield return new WaitForSeconds(_copyLifetime);

            copy.gameObject.SetActive(false);
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        List<SpriteRenderer> Copies { get; set; } = new List<SpriteRenderer>();

        SpriteRenderer GetFreeCopy()
        {
            var copy = Copies.Find(c => !c.gameObject.activeInHierarchy);
            if (copy == null)
            {
                copy = GameObject.Instantiate(_prefCopy, _copiesHolder);
                Copies.Add(copy);
            }
            return copy;
        }


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public override AEffect Clone()
            => new EffectSpeedUp
            {
                _id = _id,
                _duration = _duration,
                _delayBetweenCopies = _delayBetweenCopies,
                _copyLifetime = _copyLifetime,
                _copiesHolder = _copiesHolder,
                _prefCopy = _prefCopy
            };
    }
}