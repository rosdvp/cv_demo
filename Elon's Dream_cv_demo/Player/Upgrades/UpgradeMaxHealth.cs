using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Upgrades
{
    [System.Serializable]
    public class UpgradeMaxHealth : AUpgradeBasic
    {
        [Header("Params")]
        [SerializeField]
        int _maxHealthDelta;

        [Header("Dependencies")]
        [SerializeField]
        Screens.Control.HealthUI _healthUI;


        public override void InitIfExists(PlayerController player)
        {
            var count = Consts.Player.HEALTH_MAX_BASE +
                Global.Modeler.Session.UpgradeCount(UpgradeId.MAX_HEALTH);

            while (Global.Modeler.Session.MaxHealth < count)
            {
                Global.Modeler.Session.MaxHealth += _maxHealthDelta;
                Global.Modeler.Session.CurrHealth += _maxHealthDelta;
            }
            _healthUI.SynchWithModel();
        }
    }
}