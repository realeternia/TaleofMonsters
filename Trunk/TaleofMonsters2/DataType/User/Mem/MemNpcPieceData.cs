using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.User.Mem
{
    internal class MemNpcPieceData : INlSerlizable
    {
        public int id;
        public int count;        
        public bool used;

        public bool IsEmpty()
        {
            return id == 0;
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(id);
            bw.Write(count);
            bw.Write(used);
        }

        public void Read(System.IO.BinaryReader br)
        {
            id = br.ReadInt32();
            count = br.ReadInt32();
            used = br.ReadBoolean();
        }

        #endregion
    }
}
