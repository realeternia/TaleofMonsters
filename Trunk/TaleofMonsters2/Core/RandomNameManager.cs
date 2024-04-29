using System.Collections.Generic;
using System.IO;
using System.Text;
using TaleofMonsters.Controler.Loader;
using NarlonLib.Math;

namespace TaleofMonsters.Core
{
    internal class RandomNameManager
    {
        private static List<string> mon1;
        private static List<string> mon2;
        private static List<string> wep1;
        private static List<string> wep2;
        private static List<string> spl1;
        private static List<string> spl2;

        public static string GetMonsterName()
        {
            if (mon1== null)
            {
                mon1 = new List<string>();
                ReadToList("monster1", mon1);
            }
            if (mon2 == null)
            {
                mon2 = new List<string>();
                ReadToList("monster2", mon2);
            }
            return string.Format("{0}{1}", mon1[MathTool.GetRandom(mon1.Count)], mon1[MathTool.GetRandom(mon2.Count)]);
        }

        public static string GetWeaponName()
        {
            if (wep1 == null)
            {
                wep1 = new List<string>();
                ReadToList("weapon1", wep1);
            }
            if (wep2 == null)
            {
                wep2 = new List<string>();
                ReadToList("weapon2", wep2);
            }
            return string.Format("{0}{1}", wep1[MathTool.GetRandom(wep1.Count)], wep2[MathTool.GetRandom(wep2.Count)]);
        }

        public static string GetSpellName()
        {
            if (spl1 == null)
            {
                spl1 = new List<string>();
                ReadToList("spell1", spl1);
            }
            if (spl2 == null)
            {
                spl2 = new List<string>();
                ReadToList("spell2", spl2);
            }
            return string.Format("{0}{1}", spl1[MathTool.GetRandom(spl1.Count)], spl2[MathTool.GetRandom(spl2.Count)]);
        }

        private static void ReadToList(string fileName, List<string> list)
        {
            StreamReader sr = new StreamReader(DataLoader.Read("Name", string.Format("{0}.txt", fileName)), Encoding.Default);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                if (s == "")
                    continue;

                if (!list.Contains(s))
                {
                    list.Add(s);
                }
            }
            sr.Close();
        }
    }
}
