using System.Collections.Generic;
using System.IO;
using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.User
{
    internal class InfoMinigame : INlSerlizable
    {
        public List<int> miniGames = new List<int>();

        public bool HasPlayMiniGame(int id)
        {
            foreach (int mid in miniGames)
            {
                if (mid == id)
                {
                    return true;
                }
            }
            return false;
        }

        #region INlSerlizable ≥…‘±

        public void Write(BinaryWriter bw)
        {
            bw.Write(miniGames.Count);
            foreach (int miniGame in miniGames)
            {
                bw.Write(miniGame);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            miniGames.Clear();
            for (int i = 0; i < count; i++)
            {
                miniGames.Add(br.ReadInt32());
            }
        }

        #endregion
    }
}
