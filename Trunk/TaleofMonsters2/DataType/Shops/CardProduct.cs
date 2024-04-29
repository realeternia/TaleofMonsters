using System.Collections.Generic;
using TaleofMonsters.Core;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.DataType.Cards;
using NarlonLib.Math;

namespace TaleofMonsters.DataType.Shops
{
    internal class CardProduct : INlSerlizable
    {
        public int id;
        public int cid;
        public CardProductMarkTypes mark;

        public CardProduct()
        {
            
        }

        public CardProduct(int id, int cid, CardProductMarkTypes mark)
        {
            this.id = id;
            this.cid = cid;
            this.mark = mark;
        }

        public GameResource Price
        {
            get
            {
                Card card = CardAssistant.GetCard(cid);
                GameResource res = new GameResource();
                res.Gold = (card.Star*6 + 4)*(card.Star*6 + 14);
                if (mark == CardProductMarkTypes.Sale)
                {
                    res.Gold = res.Gold*8/10;
                }
                else if (mark == CardProductMarkTypes.Gold)
                {
                    res.Gold = res.Gold*12/10;
                }
                else if (mark == CardProductMarkTypes.Hot)
                {
                    res.Gold = MathTool.GetRound(res.Gold, 100);
                }
                else if (mark == CardProductMarkTypes.Only)
                {
                    int rt = card.Res;
                    res.Add((GameResourceType)rt, rt > 3 ? card.Star * 2 : card.Star * 5);
                }

                return res;
            }
        }

        public override string ToString()
        {
            return string.Format("id={0}", cid);
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(id);
            bw.Write(cid);
            bw.Write((int)mark);
        }

        public void Read(System.IO.BinaryReader br)
        {
            id = br.ReadInt32();
            cid = br.ReadInt32();
            mark = (CardProductMarkTypes)br.ReadInt32();
        }

        #endregion
    }

    internal class CompareByMark : IComparer<CardProduct>
    {
        #region IComparer<CardProduct> 成员

        public int Compare(CardProduct x, CardProduct y)
        {
            if (x.mark != y.mark)
            {
                if (x.mark == 0)
                {
                    return 1;
                }
                if (y.mark == 0)
                {
                    return -1;
                }
                return x.mark.CompareTo(y.mark);
            }
            return (x.cid.CompareTo(y.cid));
        }

        #endregion
    }
}
