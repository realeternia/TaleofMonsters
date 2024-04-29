using System.Collections.Generic;
using TaleofMonsters.Controler.Battle.Data.MemEffect;

namespace TaleofMonsters.Controler.Battle.DataTent
{
    internal class EffectQueue
    {
        static EffectQueue instance;
        public static EffectQueue Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EffectQueue();
                }
                return instance;
            }
        }

        public static void Init()
        {
            instance = new EffectQueue();
            instance.isFast = false;
        }

        List<ActiveEffect> queue = new List<ActiveEffect>();
        private bool isFast;

        public List<ActiveEffect> Enumerator
        {
            get { return queue; }
        }

        #region 模仿数组迭代
        public ActiveEffect this[int index]
        {
            get { return queue[index]; }
            set { queue[index] = value; }
        }

        public int Count
        {
            get { return queue.Count; }
        }

        public void Add(ActiveEffect effect)
        {
            if (isFast)
            {
                return;
            }

            queue.Add(effect);
        }

        public void Remove(ActiveEffect effect)
        {
            queue.Remove(effect);
        }

        public void RemoveAt(int index)
        {
            queue.RemoveAt(index);
        }

        public void Clear()
        {
            queue.Clear();
        }
        #endregion

        public void SetFast()
        {
            isFast = true;
        }

        public void Next()
        {
            lock (queue)
            {
                for (int i = queue.Count - 1; i >= 0; i--)
                {
                    if (i < queue.Count && queue[i].IsFinished == RunState.Finished)
                    {
                        queue.RemoveAt(i);
                    }
                }
            }
        }
    }
}
