using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TaleofMonsters.Core.Interface;
using TaleofMonsters.Forms.Items.Regions.Decorators;

namespace TaleofMonsters.Forms.Items.Regions
{
    internal class SubVirtualRegion
    {
        public int id;
        public int x;
        public int y;
        public int width;
        public int height;
        public int info;
        private bool isIn;
        private Control parent;
        protected List<IRegionDecorator> decorators;
        protected RegionState state;

        public bool IsIn
        {
            get { return isIn; }
        }

        public Control Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public SubVirtualRegion(int id, int x, int y, int width, int height, int info)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.info = info;
            decorators = new List<IRegionDecorator>();
        }

        public void AddDecorator(IRegionDecorator decorator)
        {
            decorators.Add(decorator);
        }

        public virtual void Draw(Graphics g)
        {
            foreach (RegionTextDecorator decorator in decorators)
            {
                decorator.Draw(g);
            }
        }

        public virtual void SetKeyValue(int value)
        {
        }

        public virtual int GetKeyValue()
        {
            return 0;
        }

        public virtual void SetType(VirtualRegionCellType value)
        {
        }

        public void SetDecorator(int idx, object value)//ÉèÖÃ×Ó×°ÊÎÆ÷×´Ì¬
        {
            if (decorators.Count > idx)
            {
                decorators[idx].SetState(value);
            }
        }

        public void SetState(RegionState newState)
        {
            state = newState;
        }

        public virtual void Enter()
        {
            isIn = true;
        }

        public virtual void Left()
        {
            isIn = false;
        }
    }

    public enum RegionState
    {
        Free = 0,
        Rectangled = 1,
        Blacken=2,
    }
}
