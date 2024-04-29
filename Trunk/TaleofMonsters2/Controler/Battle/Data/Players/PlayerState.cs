using System;
using ConfigDatas;
using NarlonLib.Core;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Others;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.Controler.Battle.Data.Players
{
    internal class PlayerState
    {
        public string name;
        public int job;
        public int sex;
        public int level;
        private int atk;
        private int def;
        private int mag;
        private int skl;
        private int spd;
        private int luk;
        private int vit;
        private int adp;
        private int atkp;
        private int defp;
        private int magp;
        private int sklp;
        private int spdp;
        private int lukp;
        private int vitp;
        private int adpp;

        private int saatk;
        private int sadef;
        private int samagic;
        private int sahit;
        private int sadhit;
        private int saspd;
        private int sahp;

        public int[] equips;
        public int[] skillCommons;
        public int[] skillSpecials;

        public AutoDictionary<int, int> monsterskills;
        public AutoDictionary<int, int> weaponskills;
        public AutoDictionary<int, int> masterskills;
        private AutoDictionary<int, int> monsterCounts;
        private AutoDictionary<int, int> monsterIdCounts;

        public PlayerState()
        {
            Profile profile = UserProfile.Profile;
            atk = profile.InfoBasic.atk;
            def = profile.InfoBasic.def;
            mag = profile.InfoBasic.mag;
            skl = profile.InfoBasic.skl;
            spd = profile.InfoBasic.spd;
            luk = profile.InfoBasic.luk;
            vit = profile.InfoBasic.vit;
            adp = profile.InfoBasic.adp;
            profile.InfoSkill.CheckSkillAttrSkill(this);
            name = profile.name;
            job = profile.InfoBasic.job;
            sex = profile.InfoBasic.sex;
            level = profile.InfoBasic.level;
            equips = new int[9];
            profile.InfoBag.equipon.CopyTo(equips, 0);
            skillCommons = new int[profile.InfoSkill.skillCommons.Count];
            skillCommons.CopyTo(skillCommons, 0);
            skillSpecials = new int[profile.InfoSkill.skillSpecials.Count];
            skillSpecials.CopyTo(skillSpecials, 0);
        }

        public PlayerState(int peopleId)
        {
            PeopleConfig peopleConfig = ConfigData.GetPeopleConfig(peopleId);
            int[] attrs = JobBook.GetJobLevelAttr(peopleConfig.Job, peopleConfig.Level);
            atk = attrs[0];
            def = attrs[1];
            mag = attrs[2];
            skl = attrs[3];
            spd = attrs[4];
            luk = attrs[5];
            vit = attrs[6];
            adp = attrs[7];
            name = peopleConfig.Name;
            job = peopleConfig.Job;
            sex = peopleConfig.Sex;
            level = peopleConfig.Level;
            equips = new int[9];
            Array.Clear(equips, 0, equips.Length);
        }

        public void AddSkillAttr(int saatk1, int sadef1, int samagic1, int sahit1, int sadhit1, int saspd1, int sahp1)
        {
            saatk += saatk1;
            sadef += sadef1;
            samagic += samagic1;
            sahit += sahit1;
            sadhit += sadhit1;
            saspd += saspd1;
            sahp += sahp1;
        }

        #region 属性
        public int Saatk
        {
            get { return saatk; }
        }

        public int Sadef
        {
            get { return sadef; }
        }

        public int Samagic
        {
            get { return samagic; }
        }

        public int Sahit
        {
            get { return sahit; }
        }

        public int Sadhit
        {
            get { return sadhit; }
        }

        public int Saspd
        {
            get { return saspd; }
        }

        public int Sahp
        {
            get { return sahp; }
        }
#endregion

        public int GetAttr(PlayerAttrs attr, bool isPlus)
        {
            if (isPlus)
            {
                switch (attr)
                {
                    case PlayerAttrs.Atk: return atkp;
                    case PlayerAttrs.Def: return defp;
                    case PlayerAttrs.Mag: return magp;
                    case PlayerAttrs.Skl: return sklp;
                    case PlayerAttrs.Spd: return spdp;
                    case PlayerAttrs.Luk: return lukp;
                    case PlayerAttrs.Vit: return vitp;
                    case PlayerAttrs.Adp: return adpp;
                }
            }
            else
            {
                switch (attr)
                {
                    case PlayerAttrs.Atk: return atk;
                    case PlayerAttrs.Def: return def;
                    case PlayerAttrs.Mag: return mag;
                    case PlayerAttrs.Skl: return skl;
                    case PlayerAttrs.Spd: return spd;
                    case PlayerAttrs.Luk: return luk;
                    case PlayerAttrs.Vit: return vit;
                    case PlayerAttrs.Adp: return adp;
                }
            }

            return 0;
        }

        public int GetTotalAttr(PlayerAttrs attr)
        {
            switch (attr)
            {
                case PlayerAttrs.Atk: return atk + atkp;
                case PlayerAttrs.Def: return def + defp;
                case PlayerAttrs.Mag: return mag + magp;
                case PlayerAttrs.Skl: return skl + sklp;
                case PlayerAttrs.Spd: return spd + spdp;
                case PlayerAttrs.Luk: return luk + lukp;
                case PlayerAttrs.Vit: return vit + vitp;
                case PlayerAttrs.Adp: return adp + adpp;
            }
            return 0;
        }

        public void AddAttrs(PlayerAttrs attr, int value)
        {
            switch (attr)
            {
                case PlayerAttrs.Atk: atkp += value; break;
                case PlayerAttrs.Def: defp += value; break;
                case PlayerAttrs.Mag: magp += value; break;
                case PlayerAttrs.Skl: sklp += value; break;
                case PlayerAttrs.Spd: spdp += value; break;
                case PlayerAttrs.Luk: lukp += value; break;
                case PlayerAttrs.Vit: vitp += value; break;
                case PlayerAttrs.Adp: adpp += value; break;
            }
        }

        public void UpdateSkills(int[] sid, int[] svalue)
        {
            for (int i = 0; i < sid.Length; i ++)
            {
                switch (ConfigData.GetEquipAddonConfig(sid[i]).Type)
                {
                    case "mon": monsterskills[sid[i]] = monsterskills[sid[i]] + svalue[i]; break;
                    case "master": masterskills[sid[i]] = masterskills[sid[i]] + svalue[i]; break;
                    case "weapon": weaponskills[sid[i]] = weaponskills[sid[i]] + svalue[i]; break;
                }
            }
        }

        public void CheckMonsterEvent(bool isAdd, Monster mon)
        {
            if (isAdd)
            {
                monsterCounts[(int) MonsterCountTypes.Total]++;
                monsterCounts[mon.MonsterConfig.Type + 10]++;
                monsterIdCounts[mon.Id]++;
            }
            else
            {
                monsterCounts[(int)MonsterCountTypes.Total]--;
                monsterCounts[mon.MonsterConfig.Type + 10]--;
                monsterIdCounts[mon.Id]--;
            }
        }

        public int GetMonsterCountByType(MonsterCountTypes type)
        {
            return monsterCounts[(int)type];
        }

        public int GetMonsterCountById(int mid)
        {
            return monsterIdCounts[mid];
        }

        public void Init()
        {
            monsterskills = new AutoDictionary<int, int>();
            masterskills = new AutoDictionary<int, int>();
            weaponskills = new AutoDictionary<int, int>();
            monsterCounts = new AutoDictionary<int, int>();
            monsterIdCounts = new AutoDictionary<int, int>();
        }
    }
}
