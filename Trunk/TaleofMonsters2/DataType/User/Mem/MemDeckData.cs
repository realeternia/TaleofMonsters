using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Cards;
using TaleofMonsters.DataType.Decks;

namespace TaleofMonsters.DataType.User.Mem
{
    internal class MemDeckData : INlSerlizable
    {
        private int[] cardIds;
        private string name;
        private int mcount;
        private int wcount;

        public MemDeckData()
        {
            
        }

        public MemDeckData(int index)
        {
            cardIds = new int[50];
            for (int i = 0; i < 50; i++)
            {
                cardIds[i] = -1;
            }
            name = string.Format("卡组{0}", index);
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Mcount
        {
            get { return mcount; }
        }

        public int Wcount
        {
            get { return wcount; }
        }

        public int Count
        {
            get { return wcount+mcount; }
        }

        public int GetCardAt(int index)
        {
            return cardIds[index];
        }

        public void SetCardAt(int index, int card)
        {
            cardIds[index] = card;
            Recalculate();
        }

        public int AddCardAuto(DeckCard card)
        {
            int firstBlank = -1;
            int count = 0;
            for (int i = 0; i < 50; i++)
            {
                if ((card.GetCardType() == CardTypes.Monster && (i % 10) < 5) || (card.GetCardType() != CardTypes.Monster && (i % 10) >= 5))
                {
                    if (cardIds[i] == -1 && firstBlank == -1)
                    {
                        firstBlank = i;
                    }
                    else if (cardIds[i] != -1)
                    {
                        DeckCard tCard = UserProfile.InfoCard.GetDeckCardById(cardIds[i]);
                        if (tCard.BaseId == card.BaseId)
                            count++;
                    }
                }
            }

            if (count>=3)
            {
                return Core.HSErrorTypes.DeckCardOnlyThree;
            }
            if (firstBlank==-1)
            {
                return Core.HSErrorTypes.DeckIsFull;
            }
            SetCardAt(firstBlank, card.Id);

            return Core.HSErrorTypes.OK;
        }

        public void RemoveCardById(int id)
        {
            for (int i = 0; i < 50; i++)
            {
                if (cardIds[i] == id)
                {
                    cardIds[i] = -1;
                    break;
                }
            }
            Recalculate();
        }

        public bool HasCard(int id)
        {
            for (int i = 0; i < 50; i++)
            {
                if (cardIds[i] == id)
                {
                    return true;
                }
            }
            return false;
        }

        public void Recalculate()
        {
            mcount = 0;
            wcount = 0;
            for (int i = 0; i < 50; i++)
            {
                if (cardIds[i] != -1)
                {
                    if (UserProfile.InfoCard.GetDeckCardById(cardIds[i]).GetCardType() == CardTypes.Monster)
                    {
                        mcount = mcount + 1;
                    }
                    else
                    {
                        wcount = wcount + 1;
                    }
                }
            }
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(cardIds.Length);
            for (int i = 0; i < cardIds.Length; i++)
            {
                bw.Write(cardIds[i]);
            }
            bw.Write(name);
            bw.Write(mcount);
            bw.Write(wcount);
        }

        public void Read(System.IO.BinaryReader br)
        {
            int count = br.ReadInt32();
            cardIds = new int[count];
            for (int i = 0; i < count; i++)
            {
                cardIds[i] = br.ReadInt32();
            }
            name = br.ReadString();
            mcount = br.ReadInt32();
            wcount = br.ReadInt32();
        }

        #endregion
    }
}
