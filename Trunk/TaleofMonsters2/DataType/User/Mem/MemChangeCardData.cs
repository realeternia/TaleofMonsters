using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.User.Mem
{
    internal class MemChangeCardData : INlSerlizable
    {
        public int id1;
        public int type1;
        public int id2;
        public int type2;
        public bool used;

        public bool IsEmpty()
        {
            return id1 == 0 && id2 == 0;
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(id1);
            bw.Write(type1);
            bw.Write(id2);
            bw.Write(type2);
            bw.Write(used);
        }

        public void Read(System.IO.BinaryReader br)
        {
            id1 = br.ReadInt32();
            type1 = br.ReadInt32();
            id2 = br.ReadInt32();
            type2 = br.ReadInt32();
            used = br.ReadBoolean();
        }

        #endregion
    }
}
