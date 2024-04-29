using System.Collections.Generic;
using TaleofMonsters.Core;
using ConfigDatas;
using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.User.Mem
{
    internal class MemMergeData : INlSerlizable
    {
        public int target;
        public List<List<IntPair>> methods;

        public MemMergeData()
        {
            methods = new List<List<IntPair>>();
        }

        public void Add(List<IntPair> mthd)
        {
            methods.Add(mthd);
        }

        public int Count
        {
            get { return methods.Count; }
        }

        public List<IntPair> this[int index]
        {
            get { return methods[index]; }
            set { methods[index] = value; }
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(target);
            bw.Write(methods.Count);
            foreach (List<IntPair> intPairs in methods)
            {
                bw.Write(intPairs.Count);
                foreach (IntPair intPair in intPairs)
                {
                    bw.Write(intPair.type);
                    bw.Write(intPair.value);
                }
            }
        }

        public void Read(System.IO.BinaryReader br)
        {
            target = br.ReadInt32();
            methods = new List<List<IntPair>>();
            int count = br.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                int count2 = br.ReadInt32();
                List<IntPair> pairs = new List<IntPair>();
                for (int j = 0; j < count2; j++)
                {
                    IntPair pair =new IntPair();
                    pair.type = br.ReadInt32();
                    pair.value = br.ReadInt32();
                    pairs.Add(pair);
                }
                methods.Add(pairs);
            }
        }

        #endregion
    }

    internal class CompareByMethod : IComparer<MemMergeData>
    {
        #region IComparer<MemMergeData> 成员

        public int Compare(MemMergeData x, MemMergeData y)
        {
            EquipConfig ea = ConfigData.GetEquipConfig(x.target);
            EquipConfig eb = ConfigData.GetEquipConfig(y.target);

            if (ea.Level != eb.Level)
            {
                return ea.Level.CompareTo(eb.Level);
            }
            if (ea.Quality != eb.Quality)
            {
                return ea.Quality.CompareTo(eb.Quality);
            }
            return ea.Id.CompareTo(eb.Id);
        }

        #endregion
    }
}
