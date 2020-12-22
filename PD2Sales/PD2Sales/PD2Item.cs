using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PD2Sales
{
    public enum ItemCategory
    {
        Amulets, Axes, Bases, Belts, Armors, Boots, Bows, Charms, Claws, Gloves, Helmets, Javelins, Jewels, Maces, Maps, Misc, Polearms, Rings, Runes, Runewords, Scepters,
        Sets, Shields, Spears, Swords,
        Crossbows,
        Daggers,
        Throw,
        Orbs,
        Wands,
        Staves
    }

    public enum SalesPrice
    {
        El, Eld, Tir, Nef, Eth, Ith, Tal, Ral, Ort, Thul, Amn, Sol, Shael, Dol, Hel, Io, Lum, Ko, Fal, Lem, Pul, Um, Mal, Ist, Gul, Vex, Ohm, Lo, Sur, Ber, Jah, Cham, Zod, WSS, Offer, HR
    }
    public class PD2Item
    {
        //json properties
        public string iLevel { get; set; }
        public bool isRuneword { get; set; }
        public string runeword { get; set; }
        public string name { get; set; }
        public string quality { get; set; }
        public List<PD2ItemStat> stats { get; set; }
        public List<PD2Item> socketed { get; set; }
        public string sockets { get; set; }
        public string type { get; set; }
        public bool unidentified { get; set; }
        public bool isEthereal { get; set; }

        //PD2Sales properties
        public ItemCategory category;
        public string characterOwner;
        public string accountOwner;
        public float priceCount { get; set; }
        public SalesPrice price { get; set; }
        public SolidColorBrush solidColorBrush { get; set; }
        public List<string> exportStatName { get; set; }
        public string exportAppend { get; set; }

        public void AssignParentType(string accountName, string characterName)
        {

            if (!string.IsNullOrEmpty(type))
            {
                if (type.Contains("c8") || type.Contains("c;"))
                    type = type.Remove(0, 3);

                if (type.Contains(@"c/"))
                    type = type.Remove(type.Length - 3, 3);

            }



            accountOwner = accountName;
            characterOwner = characterName;
            solidColorBrush = (Application.Current.MainWindow as PD2Sales.MainWindow).GetRarityColor(quality);// PD2Sales.MainWindow..GetRarityColor(quality);

            if (!string.IsNullOrEmpty(type))
            {
                if (type == "Amulet")
                {
                    category = ItemCategory.Amulets;
                }
                else if ((type == "Axe") ||
                (type == "Military Pick") ||
                (type == "Naga") ||
                (type == "Ogre Axe") ||
                (type == "Silver Edged Axe") ||
                (type == "Small Crescent") ||
                (type == "Tabar") ||
                (type == "Twin Axe") ||
                (type == "War Spike") ||
                (type == "War Axe") ||
                (type == "Ancient Axe") ||
                (type == "Axe") ||
                (type == "Balanced Axe") ||
                (type == "Battle Axe") ||
                (type == "Bearded Axe") ||
                (type == "Broad Axe") ||
                (type == "Berserker Axe") ||
                (type == "Champion Axe") ||
                (type == "Cleaver") ||
                (type == "Crowbill") ||
                (type == "Cryptic Axe") ||
                (type == "Decapitator") ||
                (type == "Double Axe") ||
                (type == "Ettin Axe") ||
                (type == "Feral Axe") ||
                (type == "Giant Axe") ||
                (type == "Glorious Axe") ||
                (type == "Gothic Axe") ||
                (type == "Great Axe") ||
                (type == "Hand Axe") ||
                (type == "Hatchet") ||
                (type == "Large Axe") ||
                (type == "Silver-edged Axe") ||
                (type == "Military Axe"))
                {
                    category = ItemCategory.Axes;
                }

                else if ((type == "Belt") ||
                (type == "Battle Belt") ||
                (type == "Belt") ||
                (type == "Colossus Girdle") ||
                (type == "Demonhide Sash") ||
                (type == "Girdle") ||
                (type == "Heavy Belt") ||
                (type == "Light Belt") ||
                (type == "Mesh Belt") ||
                (type == "Mithril Coil") ||
                (type == "Sash") ||
                (type == "Sharkskin Belt") ||
                (type == "Sharktooth Armor") ||
                (type == "Spiderweb Sash") ||
                (type == "Troll Belt") ||
                (type == "Vampirefang Belt") ||
                (type == "War Belt"))
                {
                    category = ItemCategory.Belts;
                }

                else if ((type == "Body_Armor") ||
                (type == "Leather Armor") ||
                (type == "Light Plate") ||
                (type == "Linked Mail") ||
                (type == "Loricated Mail") ||
                (type == "Mage Plate") ||
                (type == "Mesh Armor") ||
                (type == "Ornate Plate") ||
                (type == "Quilted Armor") ||
                (type == "Ring Mail") ||
                (type == "Russet Armor") ||
                (type == "Sacred Armor") ||
                (type == "Scale Mail") ||
                (type == "Scarab Husk") ||
                (type == "Serpentskin Armor") ||
                (type == "Shadow Plate") ||
                (type == "Splint Mail") ||
                (type == "Studded Leather") ||
                (type == "Templar Coat") ||
                (type == "Tigulated Mail") ||
                (type == "Trellised Armor") ||
                (type == "Ancient Armor") ||
                (type == "Archon Plate") ||
                (type == "Balrog Skin") ||
                (type == "Boneweave") ||
                (type == "Breast Plate") ||
                (type == "Chain Mail") ||
                (type == "Chaos Armor") ||
                (type == "Cuirass") ||
                (type == "Demonhide Armor") ||
                (type == "Diamond Mail") ||
                (type == "Dusk Shroud") ||
                (type == "Embossed Plate") ||
                (type == "Field Plate") ||
                (type == "Plate Mail") ||
                (type == "Full Plate Mail") ||
                (type == "Ghost Armor") ||
                (type == "Gothic Plate") ||
                (type == "Great Hauberk") ||
                (type == "Hard Leather Armor") ||
                (type == "Hellforged Plate") ||
                (type == "Kraken Shell") ||
                (type == "Lacquered Plate") ||
                (type == "Wire Fleece") ||
                (type == "Wyrmhide"))
                {
                    category = ItemCategory.Armors;
                }

                else if ((type == "Boots") ||
                (type == "Battle Boots") ||
                (type == "Boneweave Boots") ||
                (type == "Chain Boots") ||
                (type == "Demonhide Boots") ||
                (type == "Heavy Boots") ||
                (type == "Leather Boots") ||
                (type == "Light Plated Boots") ||
                (type == "Mesh Boots") ||
                (type == "Mirrored Boots") ||
                (type == "Myrmidon Greaves") ||
                (type == "Plate Boots") ||
                (type == "Scarabshell Boots") ||
                (type == "Sharkskin Boots") ||
                (type == "War Boots") ||
                (type == "Greaves") ||
                (type == "Wyrmhide Boots"))
                {
                    category = ItemCategory.Boots;
                }

                else if (
                (type == "Bow") ||
                (type == "Rune Bow") ||
                (type == "Shadow Bow") ||
                (type == "Short Battle Bow") ||
                (type == "Short Bow") ||
                (type == "Short Siege Bow") ||
                (type == "Short War Bow") ||
                (type == "Spider Bow") ||
                (type == "Stag Bow") ||
                (type == "Ward Bow") ||
                (type == "Ashwood Bow") ||
                (type == "Large Siege Bow") ||
                (type == "Blade Bow") ||
                (type == "Cedar Bow") ||
                (type == "Ceremonial Bow") ||
                (type == "Composite Bow") ||
                (type == "Crusader Bow") ||
                (type == "Diamond Bow") ||
                (type == "Double Bow") ||
                (type == "Edge Bow") ||
                (type == "Gothic Bow") ||
                (type == "Grand Matron Bow") ||
                (type == "Great Bow") ||
                (type == "Hunters Bow") ||
                (type == "Hydra Bow") ||
                (type == "Long Battle Bow") ||
                (type == "Long Bow") ||
                (type == "Long Siege Bow") ||
                (type == "Long War Bow") ||
                (type == "Matriarchal Bow") ||
                (type == "Pellet Bow") ||
                (type == "Razor Bow") ||
                (type == "Reflex Bow"))
                {
                    category = ItemCategory.Bows;
                }


                else if ((type == "Charm") || (type == "Small Charm") || (type == "Large Charm") || (type == "Grand Charm"))
                {
                    category = ItemCategory.Charms;
                }

                else if (
                (type == "Claw") ||
                (type == "Blade Talons") ||
                (type == "Battle Cestus") ||
                (type == "Cestus") ||
                (type == "Claws") ||
                (type == "Feral Claws") ||
                (type == "Greater Claws") ||
                (type == "Greater Talons") ||
                (type == "Hand Scythe") ||
                (type == "HatchetHands") ||
                (type == "Katar") ||
                (type == "Quhab") ||
                (type == "Runic Talons") ||
                (type == "Scissors Katar") ||
                (type == "Scissors Quhab") ||
                (type == "Scissors Suwayyah") ||
                (type == "Suwayyah") ||
                (type == "War Fist") ||
                (type == "Wrist Blade") ||
                (type == "Wrist Spike") ||
                (type == "Wrist Sword"))
                {
                    category = ItemCategory.Claws;
                }

                else if (
                (type == "Crossbow") ||
                (type == "Arbalest") ||
                (type == "Balista") ||
                (type == "Chu-Ko-Nu") ||
                (type == "Colossus Crossbow") ||
                (type == "Crossbow") ||
                (type == "Demon Crossbow") ||
                (type == "Gorgon Crossbow") ||
                (type == "Heavy Crossbow") ||
                (type == "Light Crossbow") ||
                (type == "Repeating Crossbow") ||
                (type == "Siege Crossbow"))
                {
                    category = ItemCategory.Crossbows;
                }

                else if (
                (type == "Dagger") ||
                (type == "Blade") ||
                (type == "Bone Knife") ||
                (type == "Cinquedeas") ||
                (type == "Dagger") ||
                (type == "Decoy Dagger") ||
                (type == "Dirk") ||
                (type == "Fanged Knife") ||
                (type == "Kris") ||
                (type == "Legend Spike") ||
                (type == "Mithril Point") ||
                (type == "Poignard") ||
                (type == "Stalagmite") ||
                (type == "Stilleto") ||
                (type == "Rondel"))
                {
                    category = ItemCategory.Daggers;
                }

                else if (
                (type == "Glove") ||
                (type == "Leather Gloves") ||
                (type == "Battle Gauntlets") ||
                (type == "Bracers") ||
                (type == "Bramble Mitts") ||
                (type == "Crusader Gauntlets") ||
                (type == "Demonhide Gloves") ||
                (type == "Gauntlets") ||
                (type == "Chain Gloves") ||
                (type == "Gloves") ||
                (type == "Heavy Bracers") ||
                (type == "Heavy Gloves") ||
                (type == "Light Gauntlets") ||
                (type == "Ogre Gauntlets") ||
                (type == "Sharkskin Gloves") ||
                (type == "Vambraces") ||
                (type == "Vampirebone Gloves") ||
                (type == "War Gauntlets"))
                {
                    category = ItemCategory.Gloves;
                }

                else if (
                (type == "Helm") ||
                (type == "Rage Mask") ||
                (type == "Sacred Feathers") ||
                (type == "Sallet") ||
                (type == "Savage Helmet") ||
                (type == "Shako") ||
                (type == "Skull Cap") ||
                (type == "Sky Spirit") ||
                (type == "Slayer Guard") ||
                (type == "Spired Helm") ||
                (type == "Spirit Mask") ||
                (type == "Sun Spirit") ||
                (type == "Tiara") ||
                (type == "Totemic Mask") ||
                (type == "War Hat") ||
                (type == "Winged Helm") ||
                (type == "Wolf Head") ||
                (type == "Earth Spirit") ||
                (type == "Falcon Mask") ||
                (type == "Fanged Helm") ||
                (type == "Full Helm") ||
                (type == "Fury Visor") ||
                (type == "Giant Conch") ||
                (type == "Grand Crown") ||
                (type == "Great Helm") ||
                (type == "Griffon Headress") ||
                (type == "Grim Helm") ||
                (type == "Guardian Crown") ||
                (type == "Hawk Helm") ||
                (type == "Helm") ||
                (type == "Horned Helm") ||
                (type == "Hunter's Guise") ||
                (type == "Hydraskull") ||
                (type == "Jawbone Cap") ||
                (type == "Jawbone Visor") ||
                (type == "Lion Helm") ||
                (type == "Mask") ||
                (type == "Alpha Helm") ||
                (type == "Antlers") ||
                (type == "Armet") ||
                (type == "Assault Helmet") ||
                (type == "Avenger Guard") ||
                (type == "Basinet") ||
                (type == "Blood Spirit") ||
                (type == "Bone Helm") ||
                (type == "Bone Visage") ||
                (type == "Cap") ||
                (type == "Carnage Helm") ||
                (type == "Casque") ||
                (type == "Circlet") ||
                (type == "Conqueror Crown") ||
                (type == "Corona") ||
                (type == "Coronet") ||
                (type == "Crown") ||
                (type == "Death Mask") ||
                (type == "Demonhead") ||
                (type == "Destroyer Helm") ||
                (type == "Diadem") ||
                (type == "Dream Spirit"))
                {
                    category = ItemCategory.Helmets;
                }

                else if (type == "Javelin")
                {
                    category = ItemCategory.Throw;
                }

                else if (type == "Jewel")
                {
                    category = ItemCategory.Jewels;
                }

                else if (
                (type == "Mace") ||
                (type == "Barbed Club") ||
                (type == "Battle Hammer") ||
                (type == "Club") ||
                (type == "Cudgel") ||
                (type == "Devil Star") ||
                (type == "Flail") ||
                (type == "Flanged Mace") ||
                (type == "Great Maul") ||
                (type == "Jagged Star") ||
                (type == "Knout") ||
                (type == "Legendary Mallet") ||
                (type == "Mace") ||
                (type == "Martel de Fer") ||
                (type == "Maul") ||
                (type == "Morning Star") ||
                (type == "Ogre Maul") ||
                (type == "Reinforced Mace") ||
                (type == "Scourge") ||
                (type == "Spiked Club") ||
                (type == "Thunder Maul") ||
                (type == "Truncheon") ||
                (type == "Tyrant Club") ||
                (type == "War Hammer") ||
                (type == "War Club"))
                {
                    category = ItemCategory.Maces;
                }

                //if ((type == "Misc"))
                //{

                //}

                else if (
                (type == "Orb") ||
                (type == "Clasped Orb") ||
                (type == "Cloudy Sphere") ||
                (type == "Crystalline Globe") ||
                (type == "Demon Heart") ||
                (type == "Dimensional Shard") ||
                (type == "Eagle Orb") ||
                (type == "Eldritch Orb") ||
                (type == "Glowing Orb") ||
                (type == "Heavenly Stone") ||
                (type == "Sacred Globe") ||
                (type == "Smoked Sphere") ||
                (type == "Sparkling Ball") ||
                (type == "Swirling Crystal") ||
                (type == "Vortex Orb"))
                {
                    category = ItemCategory.Orbs;
                }

                else if (
                (type == "Polearm") ||
                (type == "Bec De Corbin") ||
                (type == "Bardiche") ||
                (type == "Battle Scythe") ||
                (type == "Bill") ||
                (type == "Colossus Voulge") ||
                (type == "Giant Thresher") ||
                (type == "Great Poleaxe") ||
                (type == "Grim Scythe") ||
                (type == "Halberd") ||
                (type == "Lochaber Axe") ||
                (type == "Partizan") ||
                (type == "Poleaxe") ||
                (type == "Scythe") ||
                (type == "Thresher") ||
                (type == "Voulge") ||
                (type == "War Scythe"))
                {
                    category = ItemCategory.Polearms;
                }

                else if (
                type == "Ring")
                {
                    category = ItemCategory.Rings;
                }




                else if (
                (type == "Scepter") ||
                (type == "Caduceus") ||
                (type == "Divine Scepter") ||
                (type == "Grand Scepter") ||
                (type == "Holy Water Sprinkler") ||
                (type == "Mighty Scepter") ||
                (type == "Rune Scepter") ||
                (type == "Scepter") ||
                (type == "Seraph Rod") ||
                (type == "War Scepter"))
                {
                    category = ItemCategory.Scepters;
                }



                else if (
                (type == "Shield") ||
                (type == "Hyperion") ||
                (type == "Aegis") ||
                (type == "Aerin Shield") ||
                (type == "Akaran Rondache") ||
                (type == "Akaran Targe") ||
                (type == "Kurast Shield") ||
                (type == "Ancient Shield") ||
                (type == "Barbed Shield") ||
                (type == "Blade Barrier") ||
                (type == "Bloodlord Skull") ||
                (type == "Bone Shield") ||
                (type == "Buckler") ||
                (type == "Cantor Trophy") ||
                (type == "Crown Shield") ||
                (type == "Defender") ||
                (type == "Demon Head") ||
                (type == "Dragon Shield") ||
                (type == "Fetish Trophy") ||
                (type == "Gargoyle Head") ||
                (type == "Gothic Shield") ||
                (type == "Grim Shield") ||
                (type == "Gilded Shield") ||
                (type == "Heirophant Trophy") ||
                (type == "Heater") ||
                (type == "Hellspawn Skull") ||
                (type == "Heraldic Shield") ||
                (type == "Kite Shield") ||
                (type == "Large Shield") ||
                (type == "Luna") ||
                (type == "Minion Skull") ||
                (type == "Monarch") ||
                (type == "Mummified Trophy") ||
                (type == "Overseer Skull") ||
                (type == "Pavise") ||
                (type == "Preserved Head") ||
                (type == "Protector Shield") ||
                (type == "Rondache") ||
                (type == "Round Shield") ||
                (type == "Royal Shield") ||
                (type == "Sacred Rondache") ||
                (type == "Sacred Targe") ||
                (type == "Scutum") ||
                (type == "Sexton Trophy") ||
                (type == "Small Shield") ||
                (type == "Spiked Shield") ||
                (type == "Succubus Skull") ||
                (type == "Targe") ||
                (type == "Tower Shield") ||
                (type == "Troll Nest") ||
                (type == "Unraveller Head") ||
                (type == "Vortex Shield") ||
                (type == "Ward") ||
                (type == "Zakarum Shield") ||
                (type == "Zombie Head"))
                {
                    category = ItemCategory.Shields;
                }

                else if (
                (type == "Spear") ||
                (type == "Brandistock") ||
                (type == "Ceremonial Pike") ||
                (type == "Ceremonial Spear") ||
                (type == "Fuscina") ||
                (type == "Ghost Spear") ||
                (type == "Hyperion Spear") ||
                (type == "Lance") ||
                (type == "Maiden Pike") ||
                (type == "Maiden Spear") ||
                (type == "Mancatcher") ||
                (type == "Matriarchal Pike") ||
                (type == "Matriarchal Spear") ||
                (type == "Pike") ||
                (type == "Spear") ||
                (type == "Spetum") ||
                (type == "Stygian Pike") ||
                (type == "Trident") ||
                (type == "War Spear") ||
                (type == "War Pike") ||
                (type == "War Fork") ||
                (type == "Yari"))
                {
                    category = ItemCategory.Spears;
                }

                else if (
                (type == "Staff") ||
                (type == "Archon Staff") ||
                (type == "Battle Staff") ||
                (type == "Cedar Staff") ||
                (type == "Elder Staff") ||
                (type == "Gnarled Staff") ||
                (type == "Gothic Staff") ||
                (type == "Jo Staff") ||
                (type == "Long Staff") ||
                (type == "Quarter Staff") ||
                (type == "Rune Staff") ||
                (type == "Shillelagh") ||
                (type == "Short Staff") ||
                (type == "Walking Stick") ||
                (type == "War Staff"))
                {
                    category = ItemCategory.Staves;
                }

                else if (
                (type == "Sword") ||
                (type == "Shamshir") ||
                (type == "Short Sword") ||
                (type == "Tulwar") ||
                (type == "Tusk Sword") ||
                (type == "Two Handed Sword") ||
                (type == "War Sword") ||
                (type == "Zweihander") ||
                (type == "Mythical Sword") ||
                (type == "Phase Blade") ||
                (type == "Rune Sword") ||
                (type == "Sabre") ||
                (type == "Scimitar") ||
                (type == "Ancient Sword") ||
                (type == "Ataghan") ||
                (type == "Balrog Blade") ||
                (type == "Bastard Sword") ||
                (type == "Broad Sword") ||
                (type == "Champion Sword") ||
                (type == "Claymore") ||
                (type == "Colossal Sword") ||
                (type == "Colossal Blade") ||
                (type == "Conquest Sword") ||
                (type == "Cryptic Sword") ||
                (type == "Crystal Sword") ||
                (type == "Cutlass") ||
                (type == "Dacian Falx") ||
                (type == "Dimensional Blade") ||
                (type == "Elegant Blade") ||
                (type == "Espadon") ||
                (type == "Executioner Sword") ||
                (type == "Falcata") ||
                (type == "Falchion") ||
                (type == "Flamberge") ||
                (type == "Giant Sword") ||
                (type == "Long Sword") ||
                (type == "Gladius") ||
                (type == "Gothic Sword") ||
                (type == "Highland Blade") ||
                (type == "Hydra Edge") ||
                (type == "Legend Sword") ||
                (type == "Great Sword") ||
                (type == "Battle Sword"))
                {
                    category = ItemCategory.Swords;
                }

                else if ((type == "Throw") ||
                (type == "Stygian Pilum") ||
                (type == "Throwing Axe") ||
                (type == "Winged Knife") ||
                (type == "Throwing Knife") ||
                (type == "Throwing Spear") ||
                (type == "Tomahawk") ||
                (type == "Winged Harpoon") ||
                (type == "Winged Axe") ||
                (type == "War Javelin") ||
                (type == "War Dart") ||
                (type == "Ceremonial Javelin") ||
                (type == "Balanced Knife") ||
                (type == "Balrog Spear") ||
                (type == "Battle Dart") ||
                (type == "Exploding Potion") ||
                (type == "Fascia") ||
                (type == "Flying Axe") ||
                (type == "Flying Knife") ||
                (type == "Francisca") ||
                (type == "Ghost Glaive") ||
                (type == "Glaive") ||
                (type == "Great Pilum") ||
                (type == "Harpoon") ||
                (type == "Hurlbat") ||
                (type == "Hyperion Javelin") ||
                (type == "Javelin") ||
                (type == "Maiden Javelin") ||
                (type == "Matriarchal Javelin") ||
                (type == "Pilum") ||
                (type == "Short Spear") ||
                (type == "Simbilan") ||
                (type == "Spiculum"))
                {
                    category = ItemCategory.Throw;
                }

                else if ((type == "Wand") ||
                (type == "Bone Wand") ||
                (type == "Burnt Wand") ||
                (type == "Ghost Wand") ||
                (type == "Grave Wand") ||
                (type == "Grim Wand") ||
                (type == "Lich Wand") ||
                (type == "Petrified Wand") ||
                (type == "Polished Wand") ||
                (type == "Tomb Wand") ||
                (type == "Unearthed Wand") ||
                (type == "Wand") ||
                (type == "Yew Wand"))
                {
                    category = ItemCategory.Wands;
                }

                else if (type.Contains("Map"))
                {
                    category = ItemCategory.Maps;
                }


                else if
                (type.Contains(" Rune"))
                {
                    category = ItemCategory.Runes;
                }

                else
                    category = ItemCategory.Misc;

                if (category != ItemCategory.Misc && category != ItemCategory.Runes && (quality == "Normal" || quality == "Superior"))
                    category = ItemCategory.Bases;


                if (quality == "Set")
                    category = ItemCategory.Sets;

                if (isRuneword)
                {
                    category = ItemCategory.Runewords;
                }


                if (isEthereal)
                {
                    if (!string.IsNullOrEmpty(name))
                        name = name + "*";

                    type = type + "*";
                }

                if (!string.IsNullOrEmpty(name))
                    if (name.Contains("RI "))
                        name = name.Replace("RI ", " ");



            }
        }

        public object ConvertTextColor(object value,
                     Type targetType,
                     object parameter,
                     CultureInfo culture)
        {

            return (Application.Current.MainWindow as PD2Sales.MainWindow).GetRarityColor(quality);

        }

        public List<string> GetStatsExport()
        {
            List<string> returnList = new List<string>();

            for (int i = 0; i < stats.Count; i++)
            {
                if (stats[i].range != null)
                {
                    returnList.Add(stats[i].value);
                }
            }

            //only add the values that have ranges to them

            return returnList;
        }

        public string GetExportString()
        {
            string returnString = "";

            if (priceCount == 1 && price != SalesPrice.HR)
                returnString += "[" + price + "] ";
            else
                returnString += "[" + priceCount.ToString() + "x" + price + "] ";
            if (quality == "Unique")
            {
                returnString += name;
                if (stats.Count > 0)
                {
                    returnString += @"|";
                }
            }
            else
            {
                if (type == "Small Charm")
                {
                    returnString += "SC";
                }
                else if (type == "Large Charm")
                {
                    returnString += "LC";
                }
                else if (type == "Grand Charm")
                {
                    returnString += "GC";
                }
                else
                    returnString += quality + " " + type;

                if (stats.Count > 0)
                {
                    returnString += @"|";
                }
            }

            for (int i = 0; i < exportStatName.Count; i++)
            {
                returnString += stats.Find(x => x.name == exportStatName[i]).GetExportString();
                if (i < exportStatName.Count - 1)
                    returnString += @"|";
            }

            
            if (!string.IsNullOrEmpty(exportAppend))
                returnString += @"|" + exportAppend;

            return returnString;

        }



    }

    public class PD2ItemStatNames
    {
        public string name { get; set; }
        public string alias1 { get; set; }
        public string alias2 { get; set; }
        public string readableName { get; set; }
        public string shortenedName { get; set; }

    }


    public class PD2ItemStat
    {
        public string name { get; set; }
        public PD2ItemStatRange range { get; set; }
        public string value { get; set; }
        public bool export { get; set; }
        public PD2ItemStatNames statNames {get;set;}


        public string GetExportString()
        {
            return value + " " + statNames.shortenedName;
        }

        public void GrabNames(List<PD2ItemStatNames> namesList)
        {
            PD2ItemStatNames pD2ItemStatName = namesList.Find(x => x.name == name);
            if (pD2ItemStatName == null)
                pD2ItemStatName = namesList.Find(x => x.alias1 == name);
            if (pD2ItemStatName == null)
                pD2ItemStatName = namesList.Find(x => x.alias2 == name);
            if (pD2ItemStatName == null)
            {
                PD2ItemStatNames newNames = new PD2ItemStatNames();

                newNames.name = name;
                newNames.alias1 = "";
                newNames.alias2 = "";
                newNames.readableName = name;
                newNames.shortenedName = name;

                pD2ItemStatName = newNames;
                if (!namesList.Exists(x=>x.name == name))
                    namesList.Add(pD2ItemStatName);
            }
            else
            {
                statNames = pD2ItemStatName;
            }
        }

    }

    public class PD2ItemStatRange
    {
        public string max { get; set; }
        public string min { get; set; }
    }


}
