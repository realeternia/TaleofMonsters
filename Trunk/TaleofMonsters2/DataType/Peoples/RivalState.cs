using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.Peoples
{
    internal class RivalState : INlSerlizable
    {
        public int pid;
        public int win;
        public int loss;
        public bool avail;

        public RivalState()
        {
            
        }

        public RivalState(int perid)
        {
            pid = perid;
            win = 0;
            loss = 0;
            avail = false;
        }


        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(pid);
            bw.Write(win);
            bw.Write(loss);
            bw.Write(avail);
        }

        public void Read(System.IO.BinaryReader br)
        {
            pid = br.ReadInt32();
            win = br.ReadInt32();
            loss = br.ReadInt32();
            avail = br.ReadBoolean();
        }

        #endregion
    }
}
