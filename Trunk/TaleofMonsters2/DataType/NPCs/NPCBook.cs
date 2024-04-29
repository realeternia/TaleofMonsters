﻿using System.Collections.Generic;
using System.Drawing;
using ConfigDatas;
using TaleofMonsters.Controler.Loader;
using TaleofMonsters.Controler.Resource;
using TaleofMonsters.DataType.Scenes.SceneObjects;

namespace TaleofMonsters.DataType.NPCs
{
    static class NPCBook
    {
        static public Image GetPersonImage(int id)
        {
            string fname = string.Format("NPC/{0}.PNG", ConfigData.NpcDict[id].Figue);
            if (!ImageManager.HasImage(fname))
            {
                Image image = PicLoader.Read("NPC", string.Format("{0}.PNG", ConfigData.NpcDict[id].Figue));
                ImageManager.AddImage(fname, image);
            }
            return ImageManager.GetImage(fname);
        }

        static public SceneObject[] GetNPCOnMap(int id)
        {
            List<SceneNPC> rt = new List<SceneNPC>();
            foreach (NpcConfig npcConfig in ConfigData.NpcDict.Values)
            {
                if (npcConfig.MapId == id)
                    rt.Add(new SceneNPC(npcConfig.Id));
            }
            return rt.ToArray();
        }

        static public string[] GetNPCIconsOnMap(int id)
        {
            List<string> strs = new List<string>();
            foreach (NpcConfig npcConfig in ConfigData.NpcDict.Values)
            {
                if (npcConfig.MapId == id && npcConfig.Icon != "")
                    strs.Add(npcConfig.Icon);
            }
            return strs.ToArray();
        }
    }
}
