using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Datas.News;

namespace Datas
{
    public class Newser : MonoBehaviour
    {
        [Header("Assets")]
        [SerializeField]
        List<DataNews> _news;

        /*---------------------------------------------*/
        /*---------------------------------------------*/
        static List<DataNews> News { get; set; }

        private void Awake() => News = _news;
        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public static DataNews GetNews(string name)
            => News.Find(n => n.name == name);

        public static List<string> GetWeeklyNewsNames()
            => News.Where(n => n.name.StartsWith(Global.Modeler.Week.weekNumber.ToString()))
            .Select(n => n.name).ToList();
    }
}