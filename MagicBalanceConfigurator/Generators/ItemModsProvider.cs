using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace MagicBalanceConfigurator.Generators
{
    public class ItemModsProvider
    {
        public static ItemMod[] ItemMods { get; private set; } = new ItemMod[] { };
        public static string[] ItemsPrefixes { get; set; } = new string[] { };
        public static string[] ItemsAfixes { get; set; } = new string[] { };
        public static string[] ItemsSufixes { get; set; } = new string[] { };

        public static void InitializeItemsMods()
        {
            if (!IsItemModsConfigsExist())
            {
                ItemMods = GetDefaultMods();
                UpdateItemsMods();
            }
            string raw = File.ReadAllText(Consts.ItemModsConfigsPath);
            ItemsModsContainer itemsModsContainer = JsonConvert.DeserializeObject<ItemsModsContainer>(raw);
            ItemMods = itemsModsContainer.ItemMods;
            ItemsPrefixes = itemsModsContainer.ItemsPrefixes;
            ItemsAfixes = itemsModsContainer.ItemsAfixes;
            ItemsSufixes = itemsModsContainer.ItemsSufixes;
        }

        public static void UpdateItemsMods()
        {
            ItemsModsContainer itemsModsContainer = new ItemsModsContainer() 
            { 
                ItemMods = ItemMods,
                ItemsAfixes = ItemsAfixes, 
                ItemsPrefixes = ItemsPrefixes, 
                ItemsSufixes = ItemsSufixes 
            };
            string raw = JsonConvert.SerializeObject(itemsModsContainer);
            File.WriteAllText(Consts.ItemModsConfigsPath, raw);
        }

        public static bool IsItemModsConfigsExist() => File.Exists(Consts.ItemModsConfigsPath);

        public static ItemMod GetModByName(string name) => ItemMods.FirstOrDefault(x => x.ModName == name);

        private static ItemMod[] GetDefaultMods()
        {
            return new ItemMod[]
            {
                new ItemMod(1, 1 ,3) {ModName = "Mana regeneration", Template_Text = "StExt_str_JwlrOpt_ManaRegen", ModRarity = 15,
                    Template_OnEquip = "StExt_Jwlr_ManaRegen += [Value]", Template_OnUnequip = "StExt_Jwlr_ManaRegen -= [Value]"  },
                new ItemMod(2, 5, 15) { ModName = "Summon HP bonus", Template_Text = "StExt_Str_ShowWeapStats_SumHp", ModRarity = 30,
                    Template_OnEquip = "StExt_SumBonus_Hp += [Value]", Template_OnUnequip = "StExt_SumBonus_Hp -= [Value]"  },
                new ItemMod(3, 3, 9) { ModName = "Summon stats bonus", Template_Text = "StExt_Str_ShowWeapStats_SumPwr", ModRarity = 30,
                    Template_OnEquip = "StExt_SumBonus_Stats += [Value]", Template_OnUnequip = "StExt_SumBonus_Stats -= [Value]"  },
                new ItemMod(4, 3, 9) { ModName = "Summon protection bonus", Template_Text = "StExt_Str_ShowWeapStats_SumProt", ModRarity = 30,
                    Template_OnEquip = "StExt_SumBonus_Prot += [Value]", Template_OnUnequip = "StExt_SumBonus_Prot -= [Value]"  },
                new ItemMod(5, 4, 12) { ModName = "Energy shield regen", Template_Text = "StExt_str_JwlrOpt_EsRegen", ModRarity = 30,
                    Template_OnEquip = "StExt_Jwlr_EsRegen += [Value]", Template_OnUnequip = "StExt_Jwlr_EsRegen -= [Value]"  },
                new ItemMod(6, 1, 15) { ModName = "Luck", Template_Text = "StExt_Str_ShowWeapStats_Luck", ModRarity = 10,
                    Template_OnEquip = "StExt_LootBonus += [Value]", Template_OnUnequip = "StExt_LootBonus -= [Value]" },
                new ItemMod(7, 5, 10) { ModName = "Str + Agility", Template_Text = "StExt_str_JwlrOpt_Stats", ModRarity = 25,
                    Template_OnEquip = "npc_changeattribute(self, atr_dexterity, [Value]);\r\n\tnpc_changeattribute(self, atr_strength, [Value])", Template_OnUnequip = "npc_changeattribute(self, atr_dexterity, -[Value]);\r\n\tnpc_changeattribute(self, atr_strength, -[Value])" },
                new ItemMod(8, 10, 30) { ModName = "Mana", Template_Text = "name_bonus_manamax", ModRarity = 70,
                    Template_OnEquip = "self.attribute[3] += [Value]", Template_OnUnequip = "self.attribute[3] -= [Value]" },
                new ItemMod(9, 10, 30) { ModName = "Hp", Template_Text = "name_bonus_hpmax", ModRarity = 70,
                    Template_OnEquip = "self.attribute[1] += [Value]", Template_OnUnequip = "self.attribute[1] -= [Value]" },
                new ItemMod(10, 5, 20) { ModName = "Hp + Mana", Template_Text = "StExt_str_JwlrOpt_StatsHpMp", ModRarity = 35,
                    Template_OnEquip = "self.attribute[1] += [Value];\r\n\tself.attribute[3] += [Value]", Template_OnUnequip = "self.attribute[1] -= [Value];\r\n\tself.attribute[3] -= [Value]" },
                new ItemMod(11, 5, 20) { ModName = "Int", Template_Text = "StExt_str_JwlrOpt_Int", ModRarity = 50,
                    Template_OnEquip = "rx_changeintquiet([Value])", Template_OnUnequip = "rx_changeintquiet(-[Value])" },
                new ItemMod(12, 15, 35) { ModName = "Energy shield", Template_Text = "StExt_str_JwlrOpt_Es", ModRarity = 50,
                    Template_OnEquip  = "StExt_Jwlr_Es += [Value]", Template_OnUnequip = "StExt_Jwlr_Es -= [Value]" },
                new ItemMod(13, 5, 10) { ModName = "Magic + Fire protection", Template_Text = "StExt_str_JwlrOpt_ProtMagic", ModRarity = 15,
                    Template_OnEquip = "self.protection[5] += [Value];\r\n\tself.protection[3] += [Value]", Template_OnUnequip = "self.protection[5] -= [Value];\r\n\tself.protection[3] -= [Value]" },
                new ItemMod(14, 5, 10) { ModName = "Physics protection", Template_Text = "StExt_str_JwlrOpt_ProtWeap", ModRarity = 15,
                    Template_OnEquip = "self.protection[6] += [Value];\r\n\tself.protection[2] += [Value];\r\n\tself.protection[1] += [Value]", Template_OnUnequip = "self.protection[6] -= [Value];\r\n\tself.protection[2] -= [Value];\r\n\tself.protection[1] -= [Value]" },
                new ItemMod(15, 10, 20) { ModName = "Point protection", Template_Text = "rx_inv_descarmor_prot_point", ModRarity = 40,
                    Template_OnEquip = "self.protection[6] += [Value]", Template_OnUnequip = "self.protection[6] -= [Value]" },
                new ItemMod(16, 10, 20) { ModName = "Edge protection", Template_Text = "rx_inv_descarmor_prot_edge", ModRarity = 40,
                    Template_OnEquip = "self.protection[2] += [Value]", Template_OnUnequip = "self.protection[2] -= [Value]" },
                new ItemMod(17, 10, 20) { ModName = "Blunt protection", Template_Text = "rx_inv_descarmor_prot_blunt", ModRarity = 40,
                    Template_OnEquip = "self.protection[1] += [Value]", Template_OnUnequip = "self.protection[1] -= [Value]" },
                new ItemMod(18, 10, 20) { ModName = "Fly protection", Template_Text = "rx_shield_endurancelevel_str", ModRarity = 40,
                    Template_OnEquip = "self.protection[4] += [Value]", Template_OnUnequip = "self.protection[4] -= [Value]" },
                new ItemMod(19, 10, 20) { ModName = "Fire protection", Template_Text = "rx_inv_descarmor_prot_fire", ModRarity = 40,
                    Template_OnEquip = "self.protection[3] += [Value]", Template_OnUnequip = "self.protection[3] -= [Value]" },
                new ItemMod(20, 10, 20) { ModName = "Magic protection", Template_Text = "rx_inv_descarmor_prot_magic", ModRarity = 40,
                    Template_OnEquip = "self.protection[5] += [Value]", Template_OnUnequip = "self.protection[5] -= [Value]" },
                new ItemMod(21, 1, 2) { ModName = "Hp regeneration", Template_Text = "StExt_str_JwlrOpt_HpRegen", ModRarity = 25,
                    Template_OnEquip = "StExt_Jwlr_HpRegen += [Value]", Template_OnUnequip = "StExt_Jwlr_HpRegen -= [Value]"  },
                new ItemMod(22, 1, 1) { ModName = "Max summons", Template_Text = "StExt_Str_ShowWeapStats_SumCount", ModRarity = 15,
                    Template_OnEquip = "StExt_Jwlr_SumCount += [Value]", Template_OnUnequip = "StExt_Jwlr_SumCount -= [Value]"  },
                new ItemMod(23, 1, 2) { ModName = "Dodge chance", Template_Text = "StExt_str_JwlrOpt_Dodge", ModRarity = 40,
                    Template_OnEquip = "rx_dodgechange += [Value]", Template_OnUnequip = "rx_dodgechange -= [Value]" },
                new ItemMod(24, 15, 30) { ModName = "Energy shield barrier", Template_Text = "StExt_str_JwlrOpt_EsBarier", ModRarity = 30,
                    Template_OnEquip = "StExt_Jwlr_EsBarrier += [Value]", Template_OnUnequip = "StExt_Jwlr_EsBarrier -= [Value]" },
                new ItemMod(25, 3, 9) { ModName = "Speed modifier", Template_Text = "StExt_str_JwlrOpt_SpeedMod", ModRarity = 10,
                    Template_OnEquip = "additionalacceleration += [Value]", Template_OnUnequip = "additionalacceleration -= [Value]" },
                new ItemMod(26, 1, 3) { ModName = "2H bonus", Template_Text = "StExt_str_JwlrOpt_2hBonus", ModRarity = 20,
                    Template_OnEquip = "b_addfightskill(self, npc_talent_2h, [Value])", Template_OnUnequip = "b_addfightskill(self, npc_talent_2h, -[Value])" },
                new ItemMod(27, 1, 3) { ModName = "1H bonus", Template_Text = "StExt_str_JwlrOpt_1hBonus", ModRarity = 20,
                    Template_OnEquip = "b_addfightskill(self, npc_talent_1h, [Value])", Template_OnUnequip = "b_addfightskill(self, npc_talent_1h, -[Value])" },
                new ItemMod(28, 1, 3) { ModName = "Bow bonus", Template_Text = "StExt_str_JwlrOpt_BowBonus", ModRarity = 20,
                    Template_OnEquip = "b_addfightskill(self, npc_talent_bow, [Value])", Template_OnUnequip = "b_addfightskill(self, npc_talent_bow, -[Value])" },
                new ItemMod(29, 1, 3) { ModName = "Crossbow bonus", Template_Text = "StExt_str_JwlrOpt_CrossbowBonus", ModRarity = 20,
                    Template_OnEquip = "b_addfightskill(self, npc_talent_crossbow, [Value])", Template_OnUnequip = "b_addfightskill(self, npc_talent_crossbow, -[Value])" },
                new ItemMod(30, 10, 50) { ModName = "Aura bonus", Template_Text = "StExt_str_Jwlr_AuraPwrDist", ModRarity = 20,
                    Template_OnEquip = "StExt_Jwlr_AuraPwrDist += [Value]", Template_OnUnequip = "StExt_Jwlr_AuraPwrDist -= [Value]" },
                new ItemMod(31, 1, 1) { ModName = "Poision immune", Template_Text = "StExt_str_Jwlr_PoisionResist", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_PoisionResist += [Value]", Template_OnUnequip = "StExt_Jwlr_PoisionResist -= [Value]" },
                new ItemMod(32, 1, 1) { ModName = "Curse immune", Template_Text = "StExt_str_Jwlr_CurseResist", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_CurseResist += [Value]", Template_OnUnequip = "StExt_Jwlr_CurseResist -= [Value]" },
                new ItemMod(33, 10, 25) { ModName = "Death magic bonus", Template_Text = "StExt_Str_DeathMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_DeathMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_DeathMasteryDamage -= [Value]" },
                new ItemMod(34, 10, 25) { ModName = "Life magic bonus", Template_Text = "StExt_Str_LifeMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_LifeMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_LifeMasteryDamage -= [Value]" },
                new ItemMod(35, 10, 25) { ModName = "Light magic bonus", Template_Text = "StExt_Str_LightMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_LightMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_LightMasteryDamage -= [Value]" },
                new ItemMod(36, 10, 25) { ModName = "Dark magic bonus", Template_Text = "StExt_Str_DarkMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_DarkMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_DarkMasteryDamage -= [Value]" },
                new ItemMod(37, 10, 25) { ModName = "Electric magic bonus", Template_Text = "StExt_Str_ElectricMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_ElectricMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_ElectricMasteryDamage -= [Value]" },
                new ItemMod(38, 10, 25) { ModName = "Air magic bonus", Template_Text = "StExt_Str_AirMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_AirMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_AirMasteryDamage -= [Value]" },
                new ItemMod(39, 10, 25) { ModName = "Earth magic bonus", Template_Text = "StExt_Str_EarthMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_EarthMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_EarthMasteryDamage -= [Value]" },
                new ItemMod(40, 10, 25) { ModName = "Ice magic bonus", Template_Text = "StExt_Str_IceMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_IceMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_IceMasteryDamage -= [Value]" },
                new ItemMod(41, 10, 25) { ModName = "Fire magic bonus", Template_Text = "StExt_Str_FireMasteryDamage", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_FireMasteryDamage += [Value]", Template_OnUnequip = "StExt_Jwlr_FireMasteryDamage -= [Value]" },
                new ItemMod(42, 15, 45) { ModName = "Weapon damage", Template_Text = "StExt_Str_WeapDam", ModRarity = 5,
                    Template_OnEquip = "StExt_Jwlr_WeapDam += [Value]", Template_OnUnequip = "StExt_Jwlr_WeapDam -= [Value]" },
                new ItemMod(43, 2, 4) { ModName = "Magic power", Template_Text = "StExt_Str_MagicPower", ModRarity = 3,
                    Template_OnEquip = "countlearnspell += [Value]", Template_OnUnequip = "countlearnspell -= [Value]" },


                new ItemMod(101, 50, 250) { ModName = "Potion Hp", Template_Text = "StExt_Str_Alch_HpRestore", ModRarity = 75,
                    Template_OnEquip = "npc_changeattribute(self, atr_hitpoints, [Value]);\r\n\tif(self.attribute > self.attribute[1]) { self.attribute = self.attribute[1]; }" },
                new ItemMod(102, 25, 125) { ModName = "Potion Hp debuf", Template_Text = "StExt_Str_Alch_HpRestoreDebuf", ModRarity = 75,
                    Template_OnEquip = "npc_changeattribute(self, atr_hitpoints, -[Value])" },
                new ItemMod(103, 5, 15) { ModName = "Potion Hp perc", Template_Text = "StExt_Str_Alch_HpRestorePerc", ModRarity = 25,
                    Template_OnEquip = "rx_restorehealthpercent(self, [Value])" },
                new ItemMod(104, 3, 9) { ModName = "Potion Hp perc debuf", Template_Text = "StExt_Str_Alch_HpRestorePercDebuf", ModRarity = 25,
                    Template_OnEquip = "rx_restorehealthpercent(self, -[Value])" },
                new ItemMod(105, 30, 150) { ModName = "Potion Mp", Template_Text = "StExt_Str_Alch_MpRestore", ModRarity = 75,
                    Template_OnEquip = "npc_changeattribute(self, atr_mana, [Value]);\r\n\tif(self.attribute[2] > self.attribute[3]) { self.attribute[2] = self.attribute[3]; }" },
                new ItemMod(106, 20, 100) { ModName = "Potion Mp debuf", Template_Text = "StExt_Str_Alch_MpRestoreDebuf", ModRarity = 75,
                    Template_OnEquip = "npc_changeattribute(self, atr_mana, -[Value])" },
                new ItemMod(107, 5, 15) { ModName = "Potion Mp perc", Template_Text = "StExt_Str_Alch_MpRestorePerc", ModRarity = 25,
                    Template_OnEquip = "rx_restoremanapercent([Value])" },
                new ItemMod(108, 3, 9) { ModName = "Potion Mp perc debuf", Template_Text = "StExt_Str_Alch_MpRestorePercDebuf", ModRarity = 25,
                    Template_OnEquip = "rx_restoremanapercent(-[Value])" },
                new ItemMod(109, 5, 15) { ModName = "Potion Stam perc", Template_Text = "StExt_Str_Alch_StamRestorePerc", ModRarity = 50,
                    Template_OnEquip = "rx_restorestaminapercent([Value])" },
                new ItemMod(110, 3, 9) { ModName = "Potion Stam perc debuf", Template_Text = "StExt_Str_Alch_StamRestorePercDebuf", ModRarity = 50,
                    Template_OnEquip = "rx_restorestaminapercent(-[Value])" },
                new ItemMod(111, 50, 150) { ModName = "Potion Stam", Template_Text = "StExt_Str_Alch_StamRestore", ModRarity = 75,
                    Template_OnEquip = "rx_restorestamina([Value])" },
                new ItemMod(112, 30, 90) { ModName = "Potion Stam debuf", Template_Text = "StExt_Str_Alch_StamRestoreDebuf", ModRarity = 75,
                    Template_OnEquip = "rx_restorestamina(-[Value])" },
                new ItemMod(113, 60, 120) { ModName = "Potion sprint", Template_Text = "StExt_Str_Alch_Sprint", ModRarity = 25,
                    Template_OnEquip = "rx_applysprint([Value])" },
                new ItemMod(114, 15, 50) { ModName = "Potion str", Template_Text = "StExt_PotionEffect_ExtraStr_potion_str", ModRarity = 25,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraStr, [Value], [Duration])" },
                new ItemMod(115, 15, 50) { ModName = "Potion agi", Template_Text = "StExt_PotionEffect_ExtraAgi_potion_str", ModRarity = 25,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraAgi, [Value], [Duration])" },
                new ItemMod(116, 15, 50) { ModName = "Potion long hp", Template_Text = "StExt_PotionEffect_ExtraHp_potion_str", ModRarity = 25,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraHp, [Value], [Duration])" },
                new ItemMod(117, 5, 25) { ModName = "Potion long hp debuf", Template_Text = "StExt_PotionEffect_ExtraHp_potion_str", ModRarity = 15,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraHp, -[Value], [Duration])" },
                new ItemMod(118, 15, 50) { ModName = "Potion long mp", Template_Text = "StExt_PotionEffect_ExtraMana_potion_str", ModRarity = 25,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraMana, [Value], [Duration])" },
                new ItemMod(119, 5, 25) { ModName = "Potion long mp debuf", Template_Text = "StExt_PotionEffect_ExtraMana_potion_str", ModRarity = 15,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraMana, -[Value], [Duration])" },
                new ItemMod(120, 15, 50) { ModName = "Potion int", Template_Text = "StExt_PotionEffect_ExtraInt_potion_str", ModRarity = 25,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraInt, [Value], [Duration])" },
                new ItemMod(121, 2, 6) { ModName = "Potion hp regen", Template_Text = "StExt_PotionEffect_HpRegen_potion_str", ModRarity = 15,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_HpRegen, [Value], [Duration])" },
                new ItemMod(122, 2, 6) { ModName = "Potion mp regen", Template_Text = "StExt_PotionEffect_ManaReg_potion_str", ModRarity = 15,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ManaReg, [Value], [Duration])" },
                new ItemMod(123, 3, 9) { ModName = "Potion stam regen", Template_Text = "StExt_PotionEffect_StamRegen_potion_str", ModRarity = 35,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_StamRegen, [Value], [Duration])" },
                new ItemMod(124, 10, 30) { ModName = "Potion es regen", Template_Text = "StExt_PotionEffect_EsRegen_potion_str", ModRarity = 30,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_EsRegen, [Value], [Duration])" },
                new ItemMod(125, 50, 150) { ModName = "Potion es", Template_Text = "StExt_PotionEffect_ExtraEs_potion_str", ModRarity = 40,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraEs, [Value], [Duration])" },
                new ItemMod(126, 10, 25) { ModName = "Potion weap prot", Template_Text = "StExt_PotionEffect_ExtraWeapProt_potion_str", ModRarity = 45,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraWeapProt, [Value], [Duration])" },
                new ItemMod(127, 15, 25) { ModName = "Potion magic prot", Template_Text = "StExt_PotionEffect_ExtraMagicProt_potion_str", ModRarity = 60,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraMagicProt, [Value], [Duration])" },
                new ItemMod(128, 15, 25) { ModName = "Potion fire prot", Template_Text = "StExt_PotionEffect_ExtraFireProt_potion_str", ModRarity = 60,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraFireProt, [Value], [Duration])" },
                new ItemMod(129, 15, 50) { ModName = "Potion fly prot", Template_Text = "StExt_PotionEffect_ExtraFlyProt_potion_str", ModRarity = 50,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraFlyProt, [Value], [Duration])" },
                new ItemMod(130, 5, 15) { ModName = "Potion speed", Template_Text = "StExt_PotionEffect_ExtraSpeed_potion_str", ModRarity = 5,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ExtraSpeed, [Value], [Duration])" },
                new ItemMod(131, 10, 30) { ModName = "Potion sum heal", Template_Text = "StExt_PotionEffect_SumHeal_potion_str", ModRarity = 25,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_SumHeal, [Value], [Duration])" },
                new ItemMod(132, 1, 3) { ModName = "Potion dodge", Template_Text = "StExt_PotionEffect_Dodge_potion_str", ModRarity = 5,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_Dodge, [Value], [Duration])" },
                new ItemMod(133, 15, 50) { ModName = "Potion luck", Template_Text = "StExt_PotionEffect_Luck_potion_str", ModRarity = 25,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_Luck, [Value], [Duration])" },
                new ItemMod(134, 1, 3) { ModName = "Potion magic pwr", Template_Text = "StExt_PotionEffect_MagicPwr_potion_str", ModRarity = 5,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_MagicPwr, [Value], [Duration])" },
                new ItemMod(135, 50, 150) { ModName = "Potion weap damage", Template_Text = "StExt_PotionEffect_WeapDam_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_WeapDam, [Value], [Duration])" },
                new ItemMod(136, 15, 50) { ModName = "Potion fire mastery", Template_Text = "StExt_PotionEffect_FireMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_FireMasteryDamage, [Value], [Duration])" },
                new ItemMod(137, 15, 50) { ModName = "Potion ice mastery", Template_Text = "StExt_PotionEffect_IceMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_IceMasteryDamage, [Value], [Duration])" },
                new ItemMod(138, 15, 50) { ModName = "Potion earth mastery", Template_Text = "StExt_PotionEffect_EarthMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_EarthMasteryDamage, [Value], [Duration])" },
                new ItemMod(139, 15, 50) { ModName = "Potion air mastery", Template_Text = "StExt_PotionEffect_AirMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_AirMasteryDamage, [Value], [Duration])" },
                new ItemMod(140, 15, 50) { ModName = "Potion electro mastery", Template_Text = "StExt_PotionEffect_ElectricMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ElectricMasteryDamage, [Value], [Duration])" },
                new ItemMod(141, 15, 50) { ModName = "Potion darkness matery", Template_Text = "StExt_PotionEffect_DarkMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_DarkMasteryDamage, [Value], [Duration])" },
                new ItemMod(142, 15, 50) { ModName = "Potion light mastery", Template_Text = "StExt_PotionEffect_LightMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_LightMasteryDamage, [Value], [Duration])" },
                new ItemMod(143, 15, 50) { ModName = "Potion life mastery", Template_Text = "StExt_PotionEffect_LifeMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_LifeMasteryDamage, [Value], [Duration])" },
                new ItemMod(144, 15, 50) { ModName = "Potion death mastery", Template_Text = "StExt_PotionEffect_DeathMasteryDamage_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_DeathMasteryDamage, [Value], [Duration])" },
                new ItemMod(145, 25, 75) { ModName = "Potion icewave", Template_Text = "StExt_PotionEffect_IceWave_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_IceWave, [Value], 6)" },
                new ItemMod(146, 25, 75) { ModName = "Potion electrowave", Template_Text = "StExt_PotionEffect_ElectroWave_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_ElectroWave, [Value], 1)" },
                new ItemMod(147, 25, 75) { ModName = "Potion poisionwave", Template_Text = "StExt_PotionEffect_PoisWave_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_PoisWave, [Value], 5)" },
                new ItemMod(148, 25, 75) { ModName = "Potion firewave", Template_Text = "StExt_PotionEffect_FireWave_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_FireWave, [Value], 3)" },
                new ItemMod(149, 25, 75) { ModName = "Potion darkwave", Template_Text = "StExt_PotionEffect_DarkWave_potion_str", ModRarity = 10,
                    Template_OnEquip = "StExt_ApplyPotionEffect(StExt_PotionEffect_DarkWave, [Value], 3)" },


                new ItemMod(201, 1, 1) {ModName = "Aura Effect Fire", Template_Text = "StExt_FireAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Fire)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Fire)"  },
                new ItemMod(202, 1, 1) {ModName = "Aura Effect Ice", Template_Text = "StExt_IceAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Ice)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Ice)"  },
                new ItemMod(203, 1, 1) {ModName = "Aura Effect Life Regen", Template_Text = "StExt_HpRegenAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Liferegen)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Liferegen)"  },
                new ItemMod(204, 1, 1) {ModName = "Aura Effect Mana Regen", Template_Text = "StExt_MpRegenAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Manaregen)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Manaregen)"  },
                new ItemMod(205, 1, 1) {ModName = "Aura Effect Lifelich", Template_Text = "StExt_LifeLichRegenAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Lifelich)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Lifelich)"  },
                new ItemMod(206, 1, 1) {ModName = "Aura Effect Manalich", Template_Text = "StExt_MpLichRegenAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Manalich)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Manalich)"  },
                new ItemMod(207, 1, 1) {ModName = "Aura Effect Energy Shield", Template_Text = "StExt_EsAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Energyshield)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Energyshield)"  },
                new ItemMod(208, 1, 1) {ModName = "Aura Effect Stoned Skin", Template_Text = "StExt_StonedSkinAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Stonedskin)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Stonedskin)"  },
                new ItemMod(209, 1, 1) {ModName = "Aura Effect Evasion", Template_Text = "StExt_EvAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Evasion)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Evasion)"  },
                new ItemMod(210, 1, 1) {ModName = "Aura Effect Poision", Template_Text = "StExt_PoisionAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Poision)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Poision)"  },
                new ItemMod(211, 1, 1) {ModName = "Aura Effect Light", Template_Text = "StExt_LightAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Light)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Light)"  },
                new ItemMod(212, 1, 1) {ModName = "Aura Effect Electric", Template_Text = "StExt_ElAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Electric)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Electric)"  },
                new ItemMod(213, 1, 1) {ModName = "Aura Effect Magicamplify", Template_Text = "StExt_AmplAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Magicamplify)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Magicamplify)"  },
                new ItemMod(214, 1, 1) {ModName = "Aura Effect Dark", Template_Text = "StExt_DarkAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Dark)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Dark)"  },
                new ItemMod(215, 1, 1) {ModName = "Aura Effect Greed", Template_Text = "StExt_GreedAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Greed)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Greed)"  },
                new ItemMod(216, 1, 1) {ModName = "Aura Effect Stamina Regen", Template_Text = "StExt_StamAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Staminaregen)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Staminaregen)"  },
                new ItemMod(217, 1, 1) {ModName = "Aura Effect Poision Protect", Template_Text = "StExt_AntidotAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Poisionrotect)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Poisionrotect)"  },
                new ItemMod(218, 1, 1) {ModName = "Aura Effect Curse Protect", Template_Text = "StExt_CurseAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Curseprotect)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Curseprotect)"  },
                new ItemMod(219, 1, 1) {ModName = "Aura Effect Speed", Template_Text = "StExt_SpeedAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Speedaura)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Speedaura)"  },
                new ItemMod(220, 1, 1) {ModName = "Aura Effect Extra Strenght", Template_Text = "StExt_StrAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Extrastrenght)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Extrastrenght)"  },
                new ItemMod(221, 1, 1) {ModName = "Aura Effect Extra Agility", Template_Text = "StExt_AgiAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Extraagility)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Extraagility)"  },
                new ItemMod(222, 1, 1) {ModName = "Aura Effect Firedot", Template_Text = "StExt_IgnAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Firedot)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Firedot)"  },
                new ItemMod(223, 1, 1) {ModName = "Aura Effect Forest Help", Template_Text = "StExt_ForHlpAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Foresthelp)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Foresthelp)"  },
                new ItemMod(224, 1, 1) {ModName = "Aura Effect Graveyardaura", Template_Text = "StExt_GraveAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Graveyardaura)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Graveyardaura)"  },
                new ItemMod(225, 1, 1) {ModName = "Aura Effect Greententakles", Template_Text = "StExt_GreenAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Greententakles)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Greententakles)"  },
                new ItemMod(226, 1, 1) {ModName = "Aura Effect Demonaura", Template_Text = "StExt_DmtAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Demonaura)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Demonaura)"  },
                new ItemMod(227, 1, 1) {ModName = "Aura Effect Magicfind", Template_Text = "StExt_MfAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Magicfind)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Magicfind)"  },
                new ItemMod(228, 1, 1) {ModName = "Aura Effect Icewawe", Template_Text = "StExt_BlizzAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Icewawe)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Icewawe)"  },
                new ItemMod(229, 1, 1) {ModName = "Aura Effect Firewave", Template_Text = "StExt_InfAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Firewave)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Firewave)"  },
                new ItemMod(230, 1, 1) {ModName = "Aura Effect Electricwave", Template_Text = "StExt_ImpAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Electricwave)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Electricwave)"  },
                new ItemMod(231, 1, 1) {ModName = "Aura Effect Wind Shield", Template_Text = "StExt_WndShAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Windshield)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Windshield)"  },
                new ItemMod(232, 1, 1) {ModName = "Aura Effect Golemstuner", Template_Text = "StExt_GolAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Golemstuner)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Golemstuner)"  },
                new ItemMod(233, 1, 1) {ModName = "Aura Effect Demonwings", Template_Text = "StExt_DemAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Demonwings)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Demonwings)"  },
                new ItemMod(234, 1, 1) {ModName = "Aura Effect Forest Rage", Template_Text = "StExt_FrAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Forestrage)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Forestrage)"  },
                new ItemMod(235, 1, 1) {ModName = "Aura Effect Mindovermatter", Template_Text = "StExt_MomAuraName", ModRarity = 1,
                    Template_OnEquip = "StExt_FastAuraEquip(StExt_AuraIndex_Mindovermatter)", Template_OnUnequip = "StExt_FastAuraUnEquip(StExt_AuraIndex_Mindovermatter)"  },
                
            };
        }

        [Serializable]
        class ItemsModsContainer
        {
            public ItemMod[] ItemMods { get; set; } = new ItemMod[] { };

            public string[] ItemsPrefixes { get; set; } = new string[] { };
            public string[] ItemsAfixes { get; set; } = new string[] { };
            public string[] ItemsSufixes { get; set; } = new string[] { };
        }
    }
}
