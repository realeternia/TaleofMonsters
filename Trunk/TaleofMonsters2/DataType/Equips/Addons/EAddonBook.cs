﻿using TaleofMonsters.Controler.Battle.Data;
using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.DataType.Cards.Monsters;
using TaleofMonsters.DataType.Cards.Weapons;

namespace TaleofMonsters.DataType.Equips.Addons
{
    static class EAddonBook
    {
        public static void UpdateMonsterData(LiveMonster mon, int[] skillid, int[] skillvalue)
        {
            for (int i = 0; i < skillid.Length; i++)
            {
                switch (skillid[i])
                {
                    case 101: mon.Atk += skillvalue[i]; break;
                    case 102: mon.Def += skillvalue[i]; break;
                    case 103: mon.MAtk += skillvalue[i]; break;
                    case 104: mon.Hit += skillvalue[i]; break;
                    case 105: mon.DHit += skillvalue[i]; break;
                    case 106: mon.MaxHp += skillvalue[i]; break;
                    case 107: mon.HpReg += skillvalue[i]; break;
                    case 108: mon.MagicRatePlus += skillvalue[i]; break;
                    case 109: mon.TileEffectPlus += skillvalue[i]; break;
                    case 111: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.Water) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 112: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.Wind) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 113: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.Fire) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 114: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.Earth) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 115: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.Ice) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 116: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.Thunder) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 117: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.Light) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 118: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.Dark) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 119: if (mon.Avatar.MonsterConfig.Type == (int)MonsterAttrs.None) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 120: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Goblin) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 121: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Devil) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 122: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Machine) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 123: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Spirit) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 124: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Insect) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 125: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Dragon) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 126: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Bird) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 127: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Crawling) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 128: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Human) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 129: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Orc) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 130: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Undead) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 131: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Beast) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 132: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Fish) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 133: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Element) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 134: if (mon.Avatar.MonsterConfig.Race == (int)MonsterRaces.Plant) mon.AddStrengthLevel(skillvalue[i]); break;
                    case 201: mon.RemoveSkill(233); break;
                    case 202: if (mon.Avatar.MonsterConfig.Star < 3) mon.AddSkill(60, 1, 100); break;
                    case 302: mon.Def *= (double)skillvalue[i] / 100; break;
                    case 304: mon.Spd *= (double)skillvalue[i] / 100; break;
                    case 305: mon.Hit *= (double)skillvalue[i] / 100; break;
                    case 306: mon.DHit *= (double)skillvalue[i] / 100; break;
                }
            }
        }

        public static void UpdateWeaponData(TrueWeapon weapon, int[] skillid, int[] skillvalue)
        {
            for (int i = 0; i < skillid.Length; i++)
            {
                switch (skillid[i])
                {
                    case 501: if (weapon.Avatar.WeaponConfig.Type == WeaponTypes.Weapon) weapon.Avatar.AddStrengthLevel(skillvalue[i]); break;
                    case 502: if (weapon.Avatar.WeaponConfig.Type == WeaponTypes.Armor) weapon.Avatar.AddStrengthLevel(skillvalue[i]); break;
                    case 503: if (weapon.Avatar.WeaponConfig.Type == WeaponTypes.Scroll) weapon.Avatar.AddStrengthLevel(skillvalue[i]); break;
                    case 504: if (weapon.Avatar.WeaponConfig.Type == WeaponTypes.Weapon) weapon.Avatar.AddHit(skillvalue[i]); break;
                    case 505: if (weapon.Avatar.WeaponConfig.Type == WeaponTypes.Armor) weapon.Avatar.AddDhit(skillvalue[i]); break;
                    case 506: weapon.Avatar.RemoveNegaPoint(); break;
                }
            }
        }

        public static void UpdateMasterData(int[] skillid, int[] skillvalue)
        {
            for (int i = 0; i < skillid.Length; i++)
            {
                switch (skillid[i])
                {
                    //case 11: manger = manger * (100 + skillvalue[i]) / 100; break;
                    //case 12: mmana = mmana * (100 + skillvalue[i]) / 100; break;
                    //case 14: angerr += skillvalue[i] * 10; break;
                    //case 15: manar += skillvalue[i] * 4; break;
                    case 51: BattleInfo.Instance.ExpRatePlus += skillvalue[i]; break;
                    case 52: BattleInfo.Instance.GoldRatePlus += skillvalue[i]; break;
                }
            }
        }
    }
}
