using TaleofMonsters.Controler.Battle.Data.MemMonster;
using TaleofMonsters.DataType.Cards.Monsters;
using ConfigDatas;

namespace TaleofMonsters.DataType.Cards.Weapons
{
    internal static class WeaponAssistant
    {
        public static void CheckWeaponEffect(LiveMonster src, TrueWeapon weapon, int symbol)
        {
            WeaponConfig weaponConfig = ConfigData.GetWeaponConfig(weapon.CardId);

            src.Atk += weapon.Avatar.Atk*symbol;
            src.Def += weapon.Avatar.Def * symbol;
            src.MAtk += weapon.Avatar.MAtk * symbol;
            src.Hit += weapon.Avatar.Hit * symbol;
            src.DHit += weapon.Avatar.Dhit * symbol;
            if (weaponConfig.Type == WeaponTypes.Scroll)
            {
                if (symbol == 1)
                    src.AttackType = weaponConfig.Attr;
                else
                    src.AttackType = (int)MonsterAttrs.None;
            }
            if (weaponConfig.SkillId != 0)
            {
                if (symbol == 1)
                {
                    src.AddSkill(weaponConfig.SkillId, weapon.Level, weaponConfig.Percent);
                }
                else
                {
                    src.RemoveSkill(weaponConfig.SkillId);
                }
            }
        }
    }
}