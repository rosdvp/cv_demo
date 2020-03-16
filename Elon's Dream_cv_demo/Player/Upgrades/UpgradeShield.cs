using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharShield = Char.CharShield;
using SafeZone = Room.Objects.SafeZone;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeShield : AUpgrade
    {
        [Header("Dependencies")]
        [SerializeField]
        CharShield _shield;

        [Header("Params")]
        [Tooltip("Кол-во секунд, которое щит существует после исчезновения безопасной зоны")]
        [SerializeField]
        float _lifeSeconds;
        [Tooltip("Кол-во секунд после исчезновения безопасной зоны, через которое щит начинает моргать")]
        [SerializeField]
        float _blinkStartSeconds;
        [Tooltip("Задержка между морганиями щита")]
        [SerializeField]
        float _blinkDelaySeconds;

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public override void InitIfExists(PlayerController player)
        {
            if (!Global.Modeler.Session.Upgrades.Contains(UpgradeId.SHIELD))
                return;

            _shield.gameObject.SetActive(true);

            GameObject.Find("SafeZone").GetComponent<SafeZone>().EvClear += 
                () => _shield.StartCoroutine(CrShieldHiding());
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        IEnumerator CrShieldHiding()
        {
            var rend = _shield.GetComponent<SpriteRenderer>();
            var count = Mathf.FloorToInt((_lifeSeconds - _blinkStartSeconds) / _blinkDelaySeconds);

            yield return new WaitForSeconds(_blinkStartSeconds);
            
            for (var i = 0; i < count; i++)
            {
                rend.enabled = !rend.enabled;
                yield return new WaitForSeconds(_blinkDelaySeconds);
            }

            if (_shield.gameObject.activeInHierarchy)
                _shield.Disable();
        }
    }
}
