using System.Collections.Generic;

namespace TaleofMonsters.Core
{
    public struct IntPair
    {
        public int type;
        public int value;

        public override string ToString()
        {
            return string.Format("{0}:{1}", type, value);
        }
    }

    internal class CompareByMid : IComparer<IntPair>
    {
        #region IComparer<IntPair> 成员

        public int Compare(IntPair x, IntPair y)
        {
            if (y.type == 0 && x.type == 0)
                return 0;
            if (x.type == 0)
                return 1;
            if (y.type == 0)
                return -1;
            if (x.type != y.type)
            {
                return x.type.CompareTo(y.type);
            }
            return y.value.CompareTo(x.value);
        }

        #endregion
    }

    internal class CompareBySid : IComparer<IntPair>
    {
        #region IComparer<IntPair> 成员

        public int Compare(IntPair x, IntPair y)
        {
            if (x.type != y.type)
            {
                if (x.type == 0)
                    return 1;
                if (y.type == 0)
                    return -1;
                return x.type.CompareTo(y.type);
            }
            return x.value.CompareTo(y.value);
        }

        #endregion
    }
}