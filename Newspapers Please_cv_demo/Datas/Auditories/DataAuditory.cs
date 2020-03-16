using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.Auditories
{
    [CreateAssetMenu(menuName = "Datas/Auditory")]
    public class DataAuditory : ScriptableObject
    {
        public AuditoryType Type 
        { 
            get 
            {
                System.Enum.TryParse(name, out AuditoryType type);
                return type;
            }
        }
        public int[] opinionsNewsTypes;
        [HideInInspector]
        public int[] opinionsNewsParams;
    }
}
