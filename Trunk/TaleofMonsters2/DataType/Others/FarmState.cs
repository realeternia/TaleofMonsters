using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.Others
{
    internal class FarmState : INlSerlizable
    {
        public int type;
        public int time;

        public FarmState()
        {
            
        }

        public FarmState(int type, int time)
        {
            this.type = type;
            this.time = time;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", type, time);
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(type);
            bw.Write(time);
        }

        public void Read(System.IO.BinaryReader br)
        {
            type = br.ReadInt32();
            time = br.ReadInt32();
        }

        #endregion
    }
}
