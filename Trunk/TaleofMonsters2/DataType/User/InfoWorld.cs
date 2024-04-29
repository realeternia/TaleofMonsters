using System;
using System.Collections.Generic;
using System.IO;
using ConfigDatas;
using NarlonLib.Core;
using NarlonLib.Math;
using TaleofMonsters.Controler.World;
using TaleofMonsters.Core;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Cards.Spells;
using TaleofMonsters.DataType.Cards.Weapons;
using TaleofMonsters.DataType.Equips;
using TaleofMonsters.DataType.Peoples;
using TaleofMonsters.DataType.Items;
using TaleofMonsters.DataType.Shops;
using TaleofMonsters.DataType.Tournaments;
using TaleofMonsters.DataType.User.Mem;
using TaleofMonsters.Forms.TourGame;

namespace TaleofMonsters.DataType.User
{
    internal class InfoWorld :INlSerlizable
    {
        private List<MemChangeCardData> cards = new List<MemChangeCardData>();
        private List<MemNpcPieceData> pieces = new List<MemNpcPieceData>();
        private List<CardProduct> cardProducts = new List<CardProduct>();
        private Dictionary<int, MemTournamentData> tournaments = new Dictionary<int, MemTournamentData>();
        private Dictionary<int, int> ranks = new Dictionary<int, int>();
        private List<MemMergeData> mergeMethods = new List<MemMergeData>();

        public List<MemChangeCardData> GetChangeCardData()
        {
            int time = TimeTool.DateTimeToUnixTime(DateTime.Now);
            if (cards != null && UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastCardChangeTime) < time - SysConstants.ChangeCardDura)
            {
                cards.Clear();
                for (int i = 0; i < 5; i++)
                {
                    cards.Add(CreateMethod(i));
                }
                UserProfile.InfoRecord.SetRecordById((int)MemPlayerRecordTypes.LastCardChangeTime, TimeManager.GetTimeOnNextInterval(UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastCardChangeTime), time, SysConstants.ChangeCardDura));
            }
            return cards;
        }

        public void AddChangeCardData()
        {
            if (cards.Count < 8)
            {
                cards.Add(CreateMethod(cards.Count));
            }
        }

        public MemChangeCardData GetChangeCardData(int index)
        {
            if (cards.Count > index)
            {
                return cards[index];
            }
            return new MemChangeCardData();
        }

        public void RemoveChangeCardData(int index)
        {            
            if (cards.Count > index)
            {
                cards[index].used = true;
            }
        }

        public void RefreshAllChangeCardData()
        {
            int count = cards.Count;
            cards.Clear();
            for (int i = 0; i < count; i++)
            {
                cards.Add(CreateMethod(i));
            }
        }

        private MemChangeCardData CreateMethod(int index)
        {
            MemChangeCardData chg = new MemChangeCardData();
            int level = MathTool.GetRandom(Math.Max(index/2, 1), index/2 + 3);
            chg.id1 = MonsterBook.GetRandStarMid(level);
            while (true)
            {
                chg.id2 = MonsterBook.GetRandStarMid(level);
                if (chg.id2 != chg.id1)
                {
                    break;
                }
            }
            chg.type1 = 1;
            chg.type2 = 1;

            return chg;
        }

        public List<MemNpcPieceData> GetPieceData()
        {
            int time = TimeTool.DateTimeToUnixTime(DateTime.Now);
            if (cards != null && UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastNpcPieceTime) < time - SysConstants.NpcPieceDura)
            {
                pieces.Clear();
                for (int i = 0; i < 5; i++)
                {
                    pieces.Add(CreatePieceMethod(i));
                }
                UserProfile.InfoRecord.SetRecordById((int)MemPlayerRecordTypes.LastNpcPieceTime, TimeManager.GetTimeOnNextInterval(UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastNpcPieceTime), time, SysConstants.NpcPieceDura));
            }
            return pieces;
        }

        public void AddPieceData()
        {
            if (pieces.Count < 8)
            {
                pieces.Add(CreatePieceMethod(cards.Count));
            }
        }

        public MemNpcPieceData GetPieceData(int index)
        {
            if (pieces.Count > index)
            {
                return pieces[index];
            }
            return new MemNpcPieceData();
        }

        public void RemovePieceData(int index)
        {
            if (pieces.Count > index)
            {
                pieces[index].used = true;
            }
        }

        public void RefreshAllPieceData()
        {
            int count = pieces.Count;
            pieces.Clear();
            for (int i = 0; i < count; i++)
            {
                pieces.Add(CreatePieceMethod(i));
            }
        }

        private MemNpcPieceData CreatePieceMethod(int index)
        {
            MemNpcPieceData piece = new MemNpcPieceData();
            int rare = MathTool.GetRandom(Math.Max(index / 2, 1), index / 2 + 3);
            piece.id = HItemBook.GetRandRareMid(rare);
            piece.count = MathTool.GetRandom((8 - rare) / 2, 8 - rare);

            return piece;
        }

        public CardProduct[] GetCardProductsByType(int type)
        {
            int time = TimeTool.DateTimeToUnixTime(DateTime.Now);
            if (cardProducts == null || UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastCardShopTime) < time - SysConstants.CardShopDura)
            {
                cardProducts = new List<CardProduct>();
                ReinstallCardProducts();
                UserProfile.InfoRecord.SetRecordById((int)MemPlayerRecordTypes.LastCardShopTime, TimeManager.GetTimeOnNextInterval(UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastCardShopTime), time, SysConstants.CardShopDura));
            }

            List<CardProduct> pros = new List<CardProduct>();
            foreach (CardProduct cardProduct in cardProducts)
            {
                if ((int)CardAssistant.GetCard(cardProduct.cid).GetCardType() == type)
                    pros.Add(cardProduct);
            }

            return pros.ToArray();
        }

        public void RemoveCardProduct(int id)
        {
            foreach (CardProduct cardProduct in cardProducts)
            {
                if (cardProduct.cid == id)
                {
                    cardProducts.Remove(cardProduct);
                    break;
                }
            }
        }

        private void ReinstallCardProducts()
        {
            int index = 1;
            for (int i = 0; i < ConfigData.MonsterDict.Count; i++)
            {
                Monster mon = new Monster(i + 10001);
                int rate = mon.GetSellRate();
                CardProductMarkTypes mark = CardProductMarkTypes.Null;
                if (i > ConfigData.MonsterDict.Count - 6 && mon.MonsterConfig.Star < 6)
                {
                    rate = 100;
                    mark = CardProductMarkTypes.New;
                }
                if (MathTool.GetRandom(100) < rate)
                {
                    if (mark == 0)
                    {
                        mark = mon.GetSellMark();
                    }
                    cardProducts.Add(new CardProduct(index++, mon.Id, mark));
                }
            }
            for (int i = 0; i < ConfigData.WeaponDict.Count; i++)
            {
                Weapon wpn = new Weapon(i + 20001);
                int rate = wpn.GetSellRate();
                CardProductMarkTypes mark = CardProductMarkTypes.Null;
                if (i > ConfigData.WeaponDict.Count - 6)
                {
                    rate = 100;
                    mark = CardProductMarkTypes.New;
                }
                if (MathTool.GetRandom(100) < rate)
                {
                    if (mark == 0)
                    {
                        mark = wpn.GetSellMark();
                    }
                    cardProducts.Add(new CardProduct(index++, wpn.Id, mark));
                }
            }
            for (int i = 0; i < ConfigData.SpellDict.Count; i++)
            {
                Spell spl = new Spell(i + 30001);
                int rate = spl.GetSellRate();
                CardProductMarkTypes mark = CardProductMarkTypes.Null;
                if (i > ConfigData.SpellDict.Count - 6)
                {
                    rate = 100;
                    mark = CardProductMarkTypes.New;
                }
                if (MathTool.GetRandom(100) < rate)
                {
                    if (mark == 0)
                    {
                        mark = spl.GetSellMark();
                    }
                    cardProducts.Add(new CardProduct(index++, spl.Id, mark));
                }
            }
        }

        public MemTournamentData GetTournamentData(int tid)
        {
            if (tournaments == null)
            {
                tournaments = new Dictionary<int, MemTournamentData>();
            }
            if (!tournaments.ContainsKey(tid))
            {
                MemTournamentData tourdata = new MemTournamentData(tid);
                tournaments.Add(tid, tourdata);
            }
            return tournaments[tid];
        }

        public void CheckAllTournament(int day)
        {
            foreach (TournamentConfig tournamentConfig in ConfigData.TournamentDict.Values)
            {
                if (tournamentConfig.ApplyDate == day)
                {
                    MemTournamentData tourdata = GetTournamentData(tournamentConfig.Id);
                    tourdata.pids = PeopleBook.GetRandNPeople(tournamentConfig.PlayerCount, tournamentConfig.MinLevel, tournamentConfig.MaxLevel);
                    if (tourdata.engage)
                    {
                        tourdata.pids[MathTool.GetRandom(tournamentConfig.PlayerCount)] = -1; //player
                    }
                    tourdata.results = new MatchResult[tournamentConfig.MaxLevel];
                }

                if (tournamentConfig.BeginDate <= day && tournamentConfig.EndDate >= day)
                {
                    foreach (int mid in TournamentBook.GetTournamentMatchIds(tournamentConfig.Id))
                    {
                        TournamentMatchConfig tournamentMatchConfig = ConfigData.GetTournamentMatchConfig(mid);
                        if (tournamentMatchConfig.Date == day && tournaments[tournamentConfig.Id].results[tournamentMatchConfig.Offset].winner == 0)
                        {
                            tournaments[tournamentConfig.Id].CheckMatch(tournamentMatchConfig.Offset, true);
                        }
                    }
                }
                if (tournamentConfig.EndDate == day)
                {
                    tournaments[tournamentConfig.Id].Award();
                }
            }
        }

        public void UpdatePeopleRank(int personid, int mark)
        {
            if (!ranks.ContainsKey(personid))
            {
                if (personid > 0)
                {
                    ranks.Add(personid, ConfigData.GetPeopleConfig(personid).Level * 10);
                }
                else
                {
                    ranks.Add(personid, 0);
                }
            }
            ranks[personid] += mark;
        }

        public MemRankData[] GetAllPeopleRank()
        {
            foreach (PeopleConfig peopleConfig in ConfigData.PeopleDict.Values)
            {
                if (PeopleBook.IsPeople(peopleConfig.Id))
                {
                    if (!ranks.ContainsKey(peopleConfig.Id))
                    {
                        ranks.Add(peopleConfig.Id, peopleConfig.Level * 10);
                    }
                }
            }
            List<MemRankData> rks = new List<MemRankData>();
            foreach (int key in ranks.Keys)
            {
                MemRankData data = new MemRankData();
                data.id = key;
                data.mark = ranks[key];
                rks.Add(data);
            }
            return rks.ToArray();
        }

        public MemMergeData[] GetAllMergeData()
        {
            int time = TimeTool.DateTimeToUnixTime(DateTime.Now);
            if (mergeMethods == null || UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastMergeTime) < time - SysConstants.MergeWeaponDura)
            {
                int[] ids = EquipBook.GetCanMergeId();
                List<int> newids = RandomShuffle.Process(ids);
                mergeMethods = new List<MemMergeData>();
                for (int i = 0; i < 8; i++)
                {
                    mergeMethods.Add(CreateMergeMethod(newids[i]));
                }
                UserProfile.InfoRecord.SetRecordById((int)MemPlayerRecordTypes.LastMergeTime, TimeManager.GetTimeOnNextInterval(UserProfile.InfoRecord.GetRecordById((int)MemPlayerRecordTypes.LastMergeTime), time, SysConstants.MergeWeaponDura));
            }

            return mergeMethods.ToArray();
        }

        public MemMergeData CreateMergeMethod(int mid)
        {
            EquipConfig equipConfig = ConfigData.GetEquipConfig(mid);
            int elevel = equipConfig.Level + EQualityBonus(equipConfig.Quality);

            MemMergeData mthds = new MemMergeData();
            mthds.target = mid;
            int mcount = GetMethodCount(elevel);
            for (int i = 0; i < mcount; i++)
            {
                List<IntPair> mthd = new List<IntPair>();
                int icount = GetItemCount(elevel);
                int itrare = GetItemRareByEquipLevel(elevel);
                Dictionary<int, bool> has = new Dictionary<int, bool>();
                for (int j = 0; j < icount; j++)
                {
                    IntPair pv = new IntPair();
                    if (j == 0)
                    {
                        pv.type = HItemBook.GetRandRareMid(itrare);
                        pv.value = GetItemCountByEquipLevel(elevel, itrare) + 1;
                    }
                    else
                    {
                        int nrare = MathTool.GetRandom(Math.Max(1, itrare - 3), itrare);
                        pv.type = HItemBook.GetRandRareMid(nrare);
                        pv.value = GetItemCountByEquipLevel(elevel, nrare);
                    }
                    if (has.ContainsKey(pv.type))
                    {
                        j--;
                    }
                    else
                    {
                        has.Add(pv.type, true);
                        mthd.Add(pv);
                    }
                }

                mthds.Add(mthd);
            }
            return mthds;
        }

        private static int EQualityBonus(int quality)
        {
            int rt = 0;
            switch (quality)
            {
                case EquipQualityTypes.Common: rt = -1; break;
                case EquipQualityTypes.Good: rt = 0; break;
                case EquipQualityTypes.Excel: rt = 1; break;
                case EquipQualityTypes.Epic: rt = 2; break;
                case EquipQualityTypes.Legend: rt = 4; break;
                case EquipQualityTypes.Set: rt = 2; break;
            }
            return rt;
        }

        private int GetMethodCount(int elevel)
        {
            if (elevel < 8)
            {
                return 4;
            }
            if (elevel < 16)
            {
                return 3;
            }
            return 2;
        }

        private int GetItemCount(int elevel)
        {
            if (elevel < 10)
            {
                return 2;
            }
            if (elevel < 18)
            {
                return 3;
            }
            return 4;
        }

        private int GetItemRareByEquipLevel(int level)
        {
            int lv = level / 5 + 1;
            return lv > 7 ? 7 : lv;
        }

        private int GetItemCountByEquipLevel(int elevel, int rare)
        {
            int[] levelp = { 0, 2, 3, 4, 6, 8, 10, 14 };
            return elevel / levelp[rare] + MathTool.GetRandom(0, 2);
        }

        #region INlSerlizable 成员

        public void Write(BinaryWriter bw)
        {
            bw.Write(cards.Count);
            foreach (MemChangeCardData memChangeCardData in cards)
            {
                memChangeCardData.Write(bw);
            }
            bw.Write(pieces.Count);
            foreach (MemNpcPieceData memNpcPieceData in pieces)
            {
                memNpcPieceData.Write(bw);
            }
            bw.Write(cardProducts.Count);
            foreach (CardProduct cardProduct in cardProducts)
            {
                cardProduct.Write(bw);
            }
            bw.Write(tournaments.Count);
            foreach (KeyValuePair<int, MemTournamentData> memTournamentData in tournaments)
            {
                bw.Write(memTournamentData.Key);
                memTournamentData.Value.Write(bw);
            }
            bw.Write(ranks.Count);
            foreach (KeyValuePair<int, int> memRankData in ranks)
            {
                bw.Write(memRankData.Key);
                bw.Write(memRankData.Value);
            }
            bw.Write(mergeMethods.Count);
            foreach (MemMergeData memMergeData in mergeMethods)
            {
                memMergeData.Write(bw);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            cards = new List<MemChangeCardData>();
            for (int i = 0; i < count; i++)
            {
                MemChangeCardData cardData =new MemChangeCardData();
                cardData.Read(br);
                cards.Add(cardData);
            }
            count = br.ReadInt32();
            pieces = new List<MemNpcPieceData>();
            for (int i = 0; i < count; i++)
            {
                MemNpcPieceData pieceData = new MemNpcPieceData();
                pieceData.Read(br);
                pieces.Add(pieceData);
            }
            count = br.ReadInt32();
            cardProducts = new List<CardProduct>();
            for (int i = 0; i < count; i++)
            {
                CardProduct cardProduct = new CardProduct();
                cardProduct.Read(br);
                cardProducts.Add(cardProduct);
            }
            count = br.ReadInt32();
            tournaments = new Dictionary<int, MemTournamentData>();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                MemTournamentData tournamentData = new MemTournamentData();
                tournamentData.Read(br);
                tournaments.Add(key, tournamentData);
            }
            count = br.ReadInt32();
            ranks = new Dictionary<int, int>();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                int value = br.ReadInt32();
                ranks.Add(key, value);
            }
            count = br.ReadInt32();
            mergeMethods = new List<MemMergeData>();
            for (int i = 0; i < count; i++)
            {
                MemMergeData mergeMethod = new MemMergeData();
                mergeMethod.Read(br);
                mergeMethods.Add(mergeMethod);
            }
        }

        #endregion
    }
}
