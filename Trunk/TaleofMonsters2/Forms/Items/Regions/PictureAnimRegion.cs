using System.Drawing;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Forms.Items.Regions.Decorators;

namespace TaleofMonsters.Forms.Items.Regions
{
    internal class PictureAnimRegion : PictureRegion
    {
        public PictureAnimRegion(int id, int x, int y, int width, int height, int info, VirtualRegionCellType type, int nid)
            : base(id, x, y, width, height, info, type, nid)
        {
        }

        public override void Draw(Graphics g)
        {
            if (nid == 0)
            {
                return;
            }

            if (type == VirtualRegionCellType.Npc)
            {
                g.DrawImage(DataType.NPCs.NPCBook.GetPersonImage(nid), x, y, width, height);
            }
            else if (type == VirtualRegionCellType.Item)
            {
                g.DrawImage(DataType.Items.HItemBook.GetHItemImage(nid), x, y, width, height);
            }
            else if (type == VirtualRegionCellType.Equip)
            {
                g.DrawImage(DataType.Equips.EquipBook.GetEquipImage(nid), x, y, width, height);
            }
            else if (type == VirtualRegionCellType.Card)
            {
                g.DrawImage(DataType.Cards.CardAssistant.GetCard(nid).GetCardImage(60, 60), x, y, width, height);
            }
            else if (type == VirtualRegionCellType.People)
            {
                if (IsIn)
                {
                    Image border = PicLoader.Read("Border", "border2.PNG");
                    g.DrawImage(border, x, y, width,height);
                    border.Dispose();
                }
                g.DrawImage(DataType.Peoples.PeopleBook.GetPersonImage(nid), x, y, width, height);
            }

            foreach (RegionTextDecorator decorator in decorators)
            {
                decorator.Draw(g);
            }

            if (IsIn)
            {
                g.DrawRectangle(Pens.Yellow, x, y, width - 2, height - 2);
            }
        }

        public override void Enter()
        {
            base.Enter();
            if (Parent != null)
            {
                Parent.Invalidate(new Rectangle(x, y, width, height));
            }
        }

        public override void Left()
        {
            base.Left();
            if (Parent != null)
            {
                Parent.Invalidate(new Rectangle(x, y, width, height));
            }
        }
    }
}
