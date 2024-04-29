using System.Drawing;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.Core;
using TaleofMonsters.DataType.Effects;

namespace TaleofMonsters.Controler.Battle.Data.MemEffect
{
    internal class ActiveEffect
    {
        private Effect effect;
        private RunState isFinished;
        private int frameId;
        private LiveMonster mon;
        private Point point;
        private bool isMute;
        private bool repeat;

        internal ActiveEffect(Effect effect, LiveMonster mon, bool isMute)
        {
            this.effect = effect;
            this.mon = mon;
            this.isMute = isMute;
            frameId = -1;
            isFinished = RunState.Run;
        }

        internal ActiveEffect(Effect effect, Point p, bool isMute)
        {
            this.effect = effect;
            this.mon = null;
            this.point = p;
            this.isMute = isMute;
            frameId = -1;
            isFinished = RunState.Run;
        }

        internal RunState IsFinished
        {
            get
            {
                if (frameId == 0 && effect.SoundName != "null" && !isMute)
                {
                    SoundManager.Play("Effect", string.Format("{0}.wav", effect.SoundName));
                }
                frameId++;
                if (repeat && mon.Life<=0)
                {
                    isFinished = isFinished == RunState.Run ? RunState.Finished : RunState.Zombie;
                    frameId = effect.Frames.Length - 1;
                }
                else if (frameId >= effect.Frames.Length)
                {
                    if (repeat && mon.Life > 0)
                    {
                        frameId = 0;
                    }
                    else
                    {
                        isFinished = isFinished == RunState.Run ? RunState.Finished : RunState.Zombie;
                        frameId = effect.Frames.Length - 1;
                    }
                }
                return isFinished;
            }
        }

        internal bool Repeat
        {
            set { repeat = value; }
        }

        internal void Draw(Graphics g)
        {
            if (frameId >= 0 && frameId < effect.Frames.Length)
            {
                int size = BattleInfo.Instance.MemMap.CardSize;
                int x = ((mon == null) ? point.X - size / 2 : mon.Position.X);
                int y = ((mon == null) ? point.Y - size / 2 : mon.Position.Y);
                effect.Frames[frameId].Draw(g, x, y, size, size);
            }
        }
    }

    internal enum RunState { Run, Finished, Zombie};
}
