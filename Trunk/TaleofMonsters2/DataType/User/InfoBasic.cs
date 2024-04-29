using TaleofMonsters.Core;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Achieves;
using TaleofMonsters.MainItem;
using System.IO;

namespace TaleofMonsters.DataType.User
{
    internal class InfoBasic : INlSerlizable
    {
        public int pid;
        public string name;
        public int face;
        public int sex;
        public int constellation;//星座
        public int job;
        public int atk;
        public int def;
        public int mag;
        public int skl;
        public int spd;
        public int luk;
        public int vit;
        public int adp;
        public int level;
        public int exp;
        public int mapId;
        public int skillPoint;
        public int attrPoint;
        public int lastLoginTime;
        public int ap;
        public int digCount;

        public int[] GetSkillValue()
        {
            int[] skillValue = new int[8];
            skillValue[0] = atk;
            skillValue[1] = def;
            skillValue[2] = mag;
            skillValue[3] = skl;
            skillValue[4] = spd;
            skillValue[5] = luk;
            skillValue[6] = vit;
            skillValue[7] = adp;
            return skillValue;
        }

        public void AddSkillValueById(int index, int value)
        {
            switch (index)
            {
                case 1: atk += value; break;
                case 2: def += value; break;
                case 3: mag += value; break;
                case 4: skl += value; break;
                case 5: spd += value; break;
                case 6: luk += value; break;
                case 7: vit += value; break;
                case 8: adp += value; break;
            }
        }

        public void AddExp(int ex)
        {
            if (level >= ExpTree.MaxLevel)
            {
                return;
            }

            exp += ex;
            MainForm.Instance.AddTip(string.Format("|获得|Cyan|{0}||点经验值", ex), "White");
            AchieveBook.CheckByCheckType("exp");

            if (exp >= ExpTree.GetNextRequired(level))
            {
                PanelManager.ShowLevelUp();
            }
        }

        public bool CheckNewLevel()
        {
            int expNeed = ExpTree.GetNextRequired(level);
            if (exp >= expNeed)
            {
                exp -= expNeed;
                level++;
                return true;
            }
            return false;
        }

        #region INlSerlizable 成员

        public void Write(BinaryWriter bw)
        {
            bw.Write(pid);
            bw.Write(name);
            bw.Write(face);
            bw.Write(sex);
            bw.Write(job);
            bw.Write(atk);
            bw.Write(def);
            bw.Write(mag);
            bw.Write(skl);
            bw.Write(spd);
            bw.Write(luk);
            bw.Write(vit);
            bw.Write(adp);
            bw.Write(level);
            bw.Write(exp);
            bw.Write(mapId);
            bw.Write(skillPoint);
            bw.Write(attrPoint);
            bw.Write(lastLoginTime);
            bw.Write(ap);
            bw.Write(digCount);
        }

        public void Read(BinaryReader br)
        {
            pid = br.ReadInt32();
            name = br.ReadString();
            face = br.ReadInt32();
            sex = br.ReadInt32();
            job = br.ReadInt32();
            atk = br.ReadInt32();
            def = br.ReadInt32();
            mag = br.ReadInt32();
            skl = br.ReadInt32();
            spd = br.ReadInt32();
            luk = br.ReadInt32();
            vit = br.ReadInt32();
            adp = br.ReadInt32();
            level = br.ReadInt32();
            exp = br.ReadInt32();
            mapId = br.ReadInt32();
            skillPoint = br.ReadInt32();
            attrPoint = br.ReadInt32();
            lastLoginTime = br.ReadInt32();
            ap = br.ReadInt32();
            digCount = br.ReadInt32();
        }

        #endregion
    }
}
