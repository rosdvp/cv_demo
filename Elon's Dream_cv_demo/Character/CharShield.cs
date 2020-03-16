using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Char
{
    public class CharShield : MonoBehaviour, IAttackable
    {
        [Header("Params")]
        [SerializeField]
        bool _withRepair;
        [SerializeField]
        float _repairDuration;

        [Header("Components")]
        [SerializeField]
        CharHealtherBase _healther;


        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void OnEnable()
        {
            _healther.ImmuneCounter += 1;
            _healther.gameObject.SetActive(false);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            _healther.gameObject.SetActive(true);
            _healther.StartCoroutine(CrDelayedImmuneDisabler());
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public bool TakeDamage(GameObject sender)
        {
            Disable();

            if (_withRepair)
            {
                IEnumerator CrRepair()
                {
                    yield return new WaitForSeconds(_repairDuration);
                    gameObject.SetActive(true);
                }
                _healther.StartCoroutine(CrRepair());
            }

            return false;
        }

        IEnumerator CrDelayedImmuneDisabler()
        {
            yield return new WaitForSeconds(0.3f);
            _healther.ImmuneCounter -= 1;
        }
    }
}