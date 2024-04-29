using System.Collections.Generic;
using System.IO;
using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.User
{
    internal class InfoAchieve : INlSerlizable
    {
        private Dictionary<int, bool> achieves = new Dictionary<int, bool>();

        public void SetAchieve(int id)
        {
            if (!achieves.ContainsKey(id))
            {
                achieves.Add(id, true);
            }
        }

        public bool GetAchieve(int id)
        {
            if (achieves.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

        #region INlSerlizable ≥…‘±

        public void Write(BinaryWriter bw)
        {
            bw.Write(achieves.Count);
            foreach (KeyValuePair<int, bool> keyValuePair in achieves)
            {
                bw.Write(keyValuePair.Key);
                bw.Write(keyValuePair.Value);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            achieves.Clear();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                bool value = br.ReadBoolean();
                achieves.Add(key, value);
            }
        }

        #endregion
    }
}
