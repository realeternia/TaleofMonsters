using ConfigDatas;
using TaleofMonsters.DataType.User;

namespace TaleofMonsters.DataType.Scenes.SceneObjects
{
    class SceneWarp : SceneObject
    {
        private int targetMap;

        public SceneWarp(int wid, int wx, int wy, int wmap)
        {
            id = wid;
            x = wx;
            y = wy;
            targetMap = wmap;
            figue = "warp";
            name = ConfigData.GetSceneConfig(wmap).Name;
        }

        public int TargetMap
        {
            get { return targetMap; }
        }

        public override string Figue
        {
            get
            {
                if (CanWarp())
                {
                    return figue;
                }
                return "warperr";
            }
        }

        private bool CanWarp()
        {
            SceneConfig sceneConfig = ConfigData.GetSceneConfig(targetMap);
            if (UserProfile.InfoBasic.level < sceneConfig.Level)
            {
                return false;
            }

            return true;
        }

        public override void CheckClick()
        {
            string err = "";

            SceneConfig sceneConfig = ConfigData.GetSceneConfig(targetMap);
            if (UserProfile.InfoBasic.level < sceneConfig.Level)
            {
                err = string.Format("需要等级到达{0}级", sceneConfig.Level);
            }
            if (err != "")
            {
                MainForm.Instance.AddTip(err, "Red");
                return;
            }

            MainForm.Instance.ChangeMap(targetMap);
        }
    }
}
