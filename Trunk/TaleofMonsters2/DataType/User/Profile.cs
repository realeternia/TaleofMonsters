using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using NarlonLib.Core;
using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.World;
using TaleofMonsters.Core;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.User.Mem;
using TaleofMonsters.DataType.Cards;
using NarlonLib.Math;
using ConfigDatas;

namespace TaleofMonsters.DataType.User
{
    internal class Profile : ISqliteData
    {
        public int pid;
        public string name;
        public InfoBasic InfoBasic;
        public InfoBag InfoBag;
        public InfoCard InfoCard;
        public InfoSkill InfoSkill;
        public InfoTask InfoTask;
        public InfoRival InfoRival;
        public InfoFarm InfoFarm;
        public InfoMaze InfoMaze;
        public InfoMinigame InfoMinigame;
        public InfoRecord InfoRecord;
        public InfoAchieve InfoAchieve;
        public InfoWorld InfoWorld;

        public Profile(int pid)
        {
            this.pid = pid;
            InfoBasic = new InfoBasic();
            InfoBag = new InfoBag();
            InfoCard = new InfoCard();
            InfoTask = new InfoTask();
            InfoSkill = new InfoSkill();
            InfoRival=new InfoRival();
            InfoFarm=new InfoFarm();
            InfoMaze=new InfoMaze();
            InfoMinigame=new InfoMinigame();
            InfoRecord=new InfoRecord();
            InfoAchieve=new InfoAchieve();
            InfoWorld = new InfoWorld();
            InfoTask.InfoBag = InfoBag;
        }

        public int GetAchieveState(int id)
        {
            if (InfoAchieve.GetAchieve(id))
            {
                return int.MaxValue;
            }

            AchieveConfig achieveConfig = ConfigData.GetAchieveConfig(id);
            switch (achieveConfig.Condition.Id)
            {
                case 1: return InfoBasic.level;
                case 2: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalWin);
                case 3: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalKill);
                case 4: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalSummon);
                case 5: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalWeapon);
                case 6: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalSpell);
                case 7: return InfoTask.GetTaskDoneCount();
                case 8: return InfoRival.GetRivalAvailCount();
                case 9: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalOnline);
                case 11: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalPick);
                case 12: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalDig);
                case 13: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalFish);
                case 14: return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.ContinueWin);
                case 21: return InfoBag.resource.Gold;
                case 22: return InfoBag.resource.Lumber;
                case 23: return InfoBag.resource.Stone;
                case 24: return InfoBag.resource.Mercury;
                case 25: return InfoBag.resource.Sulfur;
                case 26: return InfoBag.resource.Carbuncle;
                case 27: return InfoBag.resource.Gem;
                case 30: return InfoCard.GetCardCountByType(CardTypes.Null);
                case 31: return InfoCard.GetCardCountByType(CardTypes.Monster);
                case 32: return InfoCard.GetCardCountByType(CardTypes.Weapon);
                case 33: return InfoCard.GetCardCountByType(CardTypes.Spell);
                case 50: return BattleInfo.Instance.FastWin;
                case 51: return BattleInfo.Instance.Round;
                case 52: return BattleInfo.Instance.LeftMonsterAdd;
                case 53: return BattleInfo.Instance.LeftWeaponAdd;
                case 54: return BattleInfo.Instance.LeftSpellAdd;
                case 55: return BattleInfo.Instance.Items.Count;
                case 56: return BattleInfo.Instance.ZeroDie;
                case 57: return BattleInfo.Instance.OnlyMagic;
                case 58: return BattleInfo.Instance.OnlySummon;
                case 59: return BattleInfo.Instance.LeftHPRate;
                case 60: return BattleInfo.Instance.AlmostLost;
            }

            if (MathTool.ValueBetween(achieveConfig.Condition.Id, 100,108))
            {
                return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalKillByType + achieveConfig.Condition.Id - 100);
            }
            if (MathTool.ValueBetween(achieveConfig.Condition.Id, 110, 125))
            {
                return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalKillByRace + achieveConfig.Condition.Id - 110);
            }
            if (MathTool.ValueBetween(achieveConfig.Condition.Id, 131, 138))
            {
                return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalKillByLevel + achieveConfig.Condition.Id - 130);
            }
            if (MathTool.ValueBetween(achieveConfig.Condition.Id, 200, 216))
            {
                return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalSummonByType + achieveConfig.Condition.Id - 200);
            }
            if (MathTool.ValueBetween(achieveConfig.Condition.Id, 220, 235))
            {
                return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalSummonByRace + achieveConfig.Condition.Id - 220);
            }
            if (MathTool.ValueBetween(achieveConfig.Condition.Id, 241, 248))
            {
                return InfoRecord.GetRecordById((int)MemPlayerRecordTypes.TotalSummonByLevel + achieveConfig.Condition.Id - 240);
            }

            return 0;
        }

        public void FinishPick(int type)
        {
            if (type == 1)
            {
                InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalPick, 1);
            }
            else if (type == 2)
            {
                InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalFish, 1);
            }
            else if (type == 3)
            {
                InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalDig, 1);
            }
        }

        public void OnKillMonster(int tlevel, int trace, int ttype)
        {
            InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalKillByType + ttype, 1);
            InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalKillByRace + trace, 1);
            InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalKillByLevel + tlevel, 1);
            InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalKill, 1);
        }

        public void OnUseCard(int tlevel, int trace, int ttype)
        {
            InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalSummonByType + ttype, 1);
            if (ttype <= 8)
            {
                InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalSummonByRace + trace, 1);
                InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalSummonByLevel + tlevel, 1);
                InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalSummon, 1);
            }
            else if (ttype <= 12)
            {
                InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalWeapon, 1);
            }
            else
            {
                InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalSpell, 1);
            }
        }

        public void OnLogin()
        {
            if (TimeManager.IsDifferDay(InfoBasic.lastLoginTime, TimeTool.DateTimeToUnixTime(DateTime.Now)))
            {
                OnNewDay();                
            }
            InfoBasic.lastLoginTime = TimeTool.DateTimeToUnixTime(DateTime.Now);
        }

        public void OnLogout()
        {
            int inter = TimeTool.DateTimeToUnixTime(DateTime.Now) - InfoBasic.lastLoginTime;
            InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalOnline, inter / 60);
            InfoBasic.lastLoginTime = TimeTool.DateTimeToUnixTime(DateTime.Now);
        }

        public void OnNewDay()
        {
            UserProfile.InfoBasic.digCount = 0;
            UserProfile.Profile.InfoMinigame.miniGames = new List<int>();
            UserProfile.Profile.InfoBasic.ap = SysConstants.NewDayAP;

            int inter = TimeTool.DateTimeToUnixTime(DateTime.Now) - InfoBasic.lastLoginTime;
            InfoRecord.AddRecordById((int)MemPlayerRecordTypes.TotalOnline, inter / 60);
            InfoBasic.lastLoginTime = TimeTool.DateTimeToUnixTime(DateTime.Now);
        }

        public void CreatePlayer()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Profile/save.db"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText ="insert into mem_player(Id) Values(@Id)";
                cmd.Parameters.Add(new SQLiteParameter("Id", pid));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        #region ISqliteData 成员
        public void Load(SQLiteConnection conn)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from mem_player where Id=@Id";
            cmd.Parameters.Add(new SQLiteParameter("Id", pid));
            using (SQLiteDataReader dr = cmd.ExecuteReader())
            {
                name = dr["name"].ToString();

                SerlizableTool.Parse(InfoBasic, (byte[])dr["InfoBasic"]);
                SerlizableTool.Parse(InfoBag, (byte[])dr["InfoBag"]);
                SerlizableTool.Parse(InfoCard, (byte[])dr["InfoCard"]);
                SerlizableTool.Parse(InfoSkill, (byte[])dr["InfoSkill"]);
                SerlizableTool.Parse(InfoTask, (byte[])dr["InfoTask"]);
                SerlizableTool.Parse(InfoRival, (byte[])dr["InfoRival"]);
                SerlizableTool.Parse(InfoFarm, (byte[])dr["InfoFarm"]);
                SerlizableTool.Parse(InfoMaze, (byte[])dr["InfoMaze"]);
                SerlizableTool.Parse(InfoMinigame, (byte[])dr["InfoMinigame"]);
                SerlizableTool.Parse(InfoRecord, (byte[])dr["InfoRecord"]);
                SerlizableTool.Parse(InfoAchieve, (byte[])dr["InfoAchieve"]);
                SerlizableTool.Parse(InfoWorld, (byte[])dr["InfoWorld"]);
            }
          

            name = InfoBasic.name;

        }

        public void Save(SQLiteConnection conn)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update mem_player set Name=@Name,InfoBasic=@InfoBasic,InfoBag=@InfoBag,InfoCard=@InfoCard,InfoSkill=@InfoSkill,InfoTask=@InfoTask,InfoRival=@InfoRival,InfoFarm=@InfoFarm,InfoMaze=@InfoMaze,InfoMinigame=@InfoMinigame,InfoRecord=@InfoRecord,InfoAchieve=@InfoAchieve,InfoWorld=@InfoWorld where Id=@Id";
            cmd.Parameters.Add(new SQLiteParameter("Id", pid));
            cmd.Parameters.Add(new SQLiteParameter("Name", name));
            SQLiteParameter param = new SQLiteParameter("InfoBasic", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoBasic);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoBag", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoBag);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoCard", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoCard);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoSkill", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoSkill);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoTask", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoTask);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoRival", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoRival);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoFarm", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoFarm);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoMaze", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoMaze);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoMinigame", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoMinigame);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoRecord", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoRecord);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoAchieve", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoAchieve);
            cmd.Parameters.Add(param);
            param = new SQLiteParameter("InfoWorld", DbType.Binary);
            param.Value = SerlizableTool.ToBytes(InfoWorld);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
        }

        #endregion
    }
}
