using System;
using System.Collections.Generic;
using System.IO;
using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.User
{
    internal class InfoMaze : INlSerlizable
    {
        private Dictionary<int, int> mazeStates = new Dictionary<int, int>();

        public int GetMazeState(int id)
        {
            if (mazeStates.ContainsKey(id))
            {
                return mazeStates[id];
            }
            return 0;
        }

        public void SetMazeState(int id, int value)
        {
            if (mazeStates.ContainsKey(id))
            {
                mazeStates[id] = value;
            }
            else
            {
                mazeStates.Add(id, value);
            }
        }


        #region INlSerlizable ≥…‘±

        public void Write(BinaryWriter bw)
        {
            bw.Write(mazeStates.Count);
            foreach (KeyValuePair<int, int> keyValuePair in mazeStates)
            {
                bw.Write(keyValuePair.Key);
                bw.Write(keyValuePair.Value);
            }
        }

        public void Read(BinaryReader br)
        {
            int count = br.ReadInt32();
            mazeStates.Clear();
            for (int i = 0; i < count; i++)
            {
                int key = br.ReadInt32();
                int value = br.ReadInt32();
                mazeStates.Add(key, value);
            }
        }

        #endregion
    }
}
