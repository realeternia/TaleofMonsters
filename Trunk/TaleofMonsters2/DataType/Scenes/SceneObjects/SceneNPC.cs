using System.Collections.Generic;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Forms;
using TaleofMonsters.DataType.Tasks;
using System.Drawing;
using ConfigDatas;

namespace TaleofMonsters.DataType.Scenes.SceneObjects
{
    class SceneNPC : SceneObject
    {
        private List<int> taskAvails;
        private List<int> taskFinishs;

        public SceneNPC(int npcid)
        {
            id = npcid;
            NpcConfig npcConfig = ConfigData.GetNpcConfig(npcid);
            name = npcConfig.Name;
            x = npcConfig.X;
            y = npcConfig.Y;
            figue = npcConfig.Figue;

            taskAvails = TaskBook.GetAvailTask(id);
            taskFinishs = TaskBook.GetFinishingTask(id);
        }

        public override void CheckClick()
        {
            int npcFunc = ConfigData.GetNpcConfig(id).Func;
            if (npcFunc == 0) //∆’Õ®NPC
            {
                NpcTalkForm npcTalkForm = new NpcTalkForm();
                npcTalkForm.NpcId = id;
                npcTalkForm.SetTasks(taskAvails, taskFinishs);
                MainForm.Instance.DealPanel(npcTalkForm);
            }
            else if (npcFunc < 10) //≤…ºØNPC
            {
                NpcDigForm npcDigForm = new NpcDigForm();
                npcDigForm.NpcId = id;
                MainForm.Instance.DealPanel(npcDigForm);
            }
        }

        public override void Draw(Graphics g, int target)
        {
            Image head = PicLoader.Read("NPC", string.Format("{0}.PNG", Figue));
            int ty = y + 50;
            int ty2 = ty;
            if (target == id)
            {
                g.DrawImage(head, x - 5, ty - 5, 70, 70);
                ty += 2;
                ty2 -= 2;
            }
            else
            {
                g.DrawImage(head, x, ty, 60, 60);
            }

            head.Dispose();

            Font font = new Font("Œ¢»Ì—≈∫⁄", 12, FontStyle.Bold);
            g.DrawString(name, font, Brushes.Black, x + 3, ty + 47);
            g.DrawString(name, font, Brushes.White, x, ty + 45);
            font.Dispose();

            if (taskFinishs.Count > 0)
            {
                Image img = PicLoader.Read("System", "MarkTaskEnd.PNG");
                g.DrawImage(img, x + 15, ty2 - 35, 21, 30);
                img.Dispose();
            }
            else if (taskAvails.Count > 0)
            {
                Image img = PicLoader.Read("System", "MarkTaskBegin.PNG");
                g.DrawImage(img, x + 15, ty2 - 35, 24, 30);
                img.Dispose();
            }
        }
    }
}

