using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Upgrades
{
    public abstract class AUpgrade
    {
        public abstract void InitIfExists(PlayerController player);
    }
}