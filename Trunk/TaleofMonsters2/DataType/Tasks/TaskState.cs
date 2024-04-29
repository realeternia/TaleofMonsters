using TaleofMonsters.Core.Interface;

namespace TaleofMonsters.DataType.Tasks
{
    internal class TaskState : INlSerlizable
    {
        public int tid;
        public int state; //0,未接受,1,接受,2,完成,3,领取奖励结束,4,失败
        public int addon;

        public TaskState()
        {
        }

        public TaskState(int tid, int state, int addon)
        {
            this.tid = tid;
            this.state = state;
            this.addon = addon;
        }

        #region INlSerlizable 成员

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write(tid);
            bw.Write(state);
            bw.Write(addon);
        }

        public void Read(System.IO.BinaryReader br)
        {
            tid = br.ReadInt32();
            state = br.ReadInt32();
            addon = br.ReadInt32();
        }

        #endregion
    }
}
