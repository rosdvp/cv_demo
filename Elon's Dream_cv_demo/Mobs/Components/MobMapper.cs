using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs.Components
{
    public class MobMapper : MonoBehaviour
    {
        Room.RoomMap RoomMap { get; set; }

        Vector2Int CurrCord { get; set; } = new Vector2Int(-1, -1);

        const string MARK_MOB = "MOB";

        public void SetCurrCordToPos(Vector2 pos)
        {
            if (RoomMap.IsCordValid(CurrCord) && RoomMap.Get(CurrCord) == MARK_MOB)
                RoomMap.Set(CurrCord, "");


            CurrCord = RoomMap.PosToCord(pos);
            if (RoomMap.IsCordValid(CurrCord))
                RoomMap.Set(CurrCord, MARK_MOB);
        }

        public void ClearCord()
        {
            if (RoomMap.IsCordValid(CurrCord) && RoomMap.Get(CurrCord) == MARK_MOB)
                RoomMap.Set(CurrCord, "");
        }

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        private void Awake()
            => RoomMap = GameObject.Find("RoomMap").GetComponent<Room.RoomMap>();
    }
}