using System.Collections.Generic;
using System.IO;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.World;

namespace TaleofMonsters.DataType.Decks
{
    internal static class DeckBook
    {
        private static List<DeckCard> deck;

        public static DeckCard[] GetDeckByName(string name, int level)
        {
            deck = new List<DeckCard>();
            StreamReader sr = new StreamReader(DataLoader.Read("Deck", string.Format("{0}.dek", name)));

            for (int i = 0; i < 50; i++)
            {
                string[] datas = sr.ReadLine().Split('\t');

                deck.Add(new DeckCard(WorldInfoManager.GetCardFakeId(), int.Parse(datas[0]), int.Parse(datas[1]) + level - 1, int.Parse(datas[2])));
            }
            sr.Close();

            return deck.ToArray();
        }

        public static bool HasCard(string name, int id)
        {
            GetDeckByName(name, 1);
            foreach (DeckCard des in deck)
            {
                if (des.BaseId == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
