using System.Collections.Generic;
using System.IO;
using TaleofMonsters.Controler.World;
using TaleofMonsters.Core;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Achieves;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Decks;
using TaleofMonsters.DataType.User.Mem;
using NarlonLib.Math;

namespace TaleofMonsters.DataType.User
{
    internal class InfoCard : INlSerlizable
    {
        public Dictionary<int, DeckCard> cards = new Dictionary<int, DeckCard>();
        public List<int> newcards = new List<int>();
        private MemDeckData[] decks;
        public int deckId;

        public InfoCard()
        {
            decks = new MemDeckData[SysConstants.PlayDeckCount];
            for (int i = 0; i < decks.Length; i++)
            {
                decks[i] = new MemDeckData(i + 1);
            }
        }

        public int GetCardCountByType(CardTypes type)
        {
            if (type == CardTypes.Null)
            {
                return cards.Count;
            }
            int count = 0;
            foreach (DeckCard cd in cards.Values)
            {
                if (cd.GetCardType() == type)
                {
                    count++;
                }
            }
            return count;
        }

        public MemDeckData SelectedDeck
        {
            get
            {
                return decks[deckId];
            }
        }

        public int AddCard(int cid)
        {
            DeckCard card = new DeckCard(WorldInfoManager.GetCardUniqueId(), cid, 1, 0);
            if (MathTool.GetRandom(1,3)==1)
            {
                card.Quality = 1;//todo 稀有卡片规则
            }
            if (card.GetCardType()== CardTypes.Monster)
            {
                card.Name = RandomNameManager.GetMonsterName();
            }
            else if (card.GetCardType() == CardTypes.Weapon)
            {
                card.Name = RandomNameManager.GetWeaponName();
            }
            else if (card.GetCardType() == CardTypes.Spell)
            {
                card.Name = RandomNameManager.GetSpellName();
            }
            cards.Add(card.Id, card);
            newcards.Add(card.Id);
            if (newcards.Count > 10)
                newcards.RemoveAt(0);

            AchieveBook.CheckByCheckType("card");
            Card cardData = CardAssistant.GetCard(cid);
            MainForm.Instance.AddTip(string.Format("|获得卡片-|{0}|{1}[{2}]", HSTypes.I2CardLevelColor(cardData.Star), cardData.Name,card.Name), "White");

            return card.Id;
        }

        public void AddCard(int cid, int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddCard(cid);
            }
        }

        public void RemoveCard(int id)
        {
            newcards.Remove(id);

            if (cards.ContainsKey(id))
            {
                cards.Remove(id);
            }

            foreach (MemDeckData deck in decks)
            {
                deck.RemoveCardById(id);
            }
        }

        public int GetCardCount(int id)
        {
            //int count = 0;
            //for (int i = cards.Count - 1; i >= 0; i--)
            //{
            //    if (cards[i].BaseId == id)
            //    {
            //        count ++;
            //    }
            //}
            //return count;
            return 0;
        }

        public string[] GetDeckNames()
        {
            string[] names = new string[decks.Length];
            for (int i = 0; i < decks.Length; i++)
            {
                names[i] = decks[i].Name;
            }
            return names;
        }

        public DeckCard GetDeckCardById(int id)
        {
            if (cards.ContainsKey(id))
            {
                return cards[id];
            }
            return new DeckCard(-1, 0, 0, 0);
        }

        public bool AddCardExp(int id, int expadd)
        {
            if (cards.ContainsKey(id))
            {
                return cards[id].AddExp(expadd);
            }
            return false;
        }

        #region INlSerlizable 成员

        public void Write(BinaryWriter bw)
        {
            bw.Write(cards.Count);
            foreach (KeyValuePair<int, DeckCard> keyValuePair in cards)
            {
                bw.Write(keyValuePair.Key);
                keyValuePair.Value.Write(bw);
            }
            bw.Write(newcards.Count);
            for (int i = 0; i < newcards.Count; i++)
            {
                bw.Write(newcards[i]);
            }
            bw.Write(decks.Length);
            for (int i = 0; i < decks.Length; i++)
            {
                decks[i].Write(bw);
            }
            bw.Write(deckId);
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            cards.Clear();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                DeckCard value = new DeckCard();
                value.Read(br);
                cards.Add(key, value);
            }
            count = br.ReadInt32();
            newcards.Clear();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                newcards.Add(key);
            }
            count = br.ReadInt32();
            decks = new MemDeckData[count];
            for (int i = 0; i < count; i++)
            {
                decks[i] = new MemDeckData();
                decks[i].Read(br);
            }
            deckId = br.ReadInt32();
        }

        #endregion
    }
}
