//  By Claessic
//  This project is free to use, as long as you give credit where credit is due.
//  I hope this will help smoothe out trade for a while.
//  I do not own any of the included Images. Icons belong to Activision Blizzard, except for the blank rune icon. That one is from Phrozen keep, not sure who made it.
//
//  Cheers,
//  *Claessic (IGN: Claerrow)
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using System.Globalization;
using System.Collections.Specialized;
using System.Windows.Controls.Primitives;
using Newtonsoft.Json.Converters;
using System.Reflection;


namespace PD2Sales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<PD2Character> characters;
        public ItemCategory activeCategory;
        PD2Item activeDetailPopupItem;

        public List<PD2ItemStatNames> loadedPD2ItemStatNames = new List<PD2ItemStatNames>();

        public List<PD2Item> activeLoadedCategoryItems = new List<PD2Item>();
        public List<PD2Item> itemsForSaleList = new List<PD2Item>();

        public List<PD2Item> loadedAmulets = new List<PD2Item>();
        public List<PD2Item> loadedAxes = new List<PD2Item>();
        public List<PD2Item> loadedBases = new List<PD2Item>();
        public List<PD2Item> loadedBelts = new List<PD2Item>();
        public List<PD2Item> loadedArmors = new List<PD2Item>();
        public List<PD2Item> loadedBoots = new List<PD2Item>();
        public List<PD2Item> loadedBows = new List<PD2Item>();
        public List<PD2Item> loadedCharms = new List<PD2Item>();
        public List<PD2Item> loadedClaws = new List<PD2Item>();
        public List<PD2Item> loadedGloves = new List<PD2Item>();
        public List<PD2Item> loadedHelmets = new List<PD2Item>();
        public List<PD2Item> loadedJavelins = new List<PD2Item>();
        public List<PD2Item> loadedJewels = new List<PD2Item>();
        public List<PD2Item> loadedMaces = new List<PD2Item>();
        public List<PD2Item> loadedMaps = new List<PD2Item>();
        public List<PD2Item> loadedMisc = new List<PD2Item>();
        public List<PD2Item> loadedPolearms = new List<PD2Item>();
        public List<PD2Item> loadedRings = new List<PD2Item>();
        public List<PD2Item> loadedRunes = new List<PD2Item>();
        public List<PD2Item> loadedRunewords = new List<PD2Item>();
        public List<PD2Item> loadedScepters = new List<PD2Item>();
        public List<PD2Item> loadedSets = new List<PD2Item>();
        public List<PD2Item> loadedShields = new List<PD2Item>();
        public List<PD2Item> loadedSpears = new List<PD2Item>();
        public List<PD2Item> loadedSwords = new List<PD2Item>();
        public List<PD2Item> loadedCrossbows = new List<PD2Item>();
        public List<PD2Item> loadedDaggers = new List<PD2Item>();
        public List<PD2Item> loadedThrow = new List<PD2Item>();
        public List<PD2Item> loadedOrbs = new List<PD2Item>();
        public List<PD2Item> loadedWands = new List<PD2Item>();
        public List<PD2Item> loadedStaves = new List<PD2Item>();



        public MainWindow()
        {
            
            string[] itemStatNamesArray = Properties.Resources.PD2Stat_Names.Replace("\r", "").Split('\n'); // File.ReadAllLines("Data/PD2Sales Types - Sheet1.tsv");

            for (int i = 0; i < itemStatNamesArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(itemStatNamesArray[i]))
                {
                    PD2ItemStatNames newNames = new PD2ItemStatNames();

                    string[] splitString = itemStatNamesArray[i].Split('\t');
                    newNames.name = splitString[0];
                    newNames.alias1 = splitString[1];
                    newNames.alias2 = splitString[2];
                    newNames.readableName = splitString[3];
                    newNames.shortenedName = splitString[4];

                    loadedPD2ItemStatNames.Add(newNames);
                }
            }


            characters = new List<PD2Character>();
            DirectoryInfo d = new DirectoryInfo(Directory.GetCurrentDirectory());//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

            foreach (FileInfo file in Files)
            {
                if (file.Name.Contains("SalesExport_"))
                    continue;
                characters.Add(new PD2Character(file.Name, loadedPD2ItemStatNames));
            }

            FileInfo[] ExistingSales = d.GetFiles("sales.pd2");
            if (ExistingSales.Length > 0)
            {
                //Load existing sales
                string readData = "";
                readData = File.ReadAllText(ExistingSales[0].Name);

                itemsForSaleList = JsonConvert.DeserializeObject<List<PD2Item>>(readData);

                for (int i = 0; i < itemsForSaleList.Count; i++)
                {

                    if (itemsForSaleList[i].exportStatName == null)
                        itemsForSaleList[i].exportStatName = new List<string>();
                }
            }

            activeCategory = ItemCategory.Amulets;

            CategorizeAllItems();

            InitializeComponent();


            ItemCategoriesListBox.ItemsSource = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>();
            ItemCategoriesListBox.SelectionChanged += ItemCategoriesListBox_SelectionChanged;

            ItemsInCategoryListBox.SelectionChanged += ItemsInCategoryListBox_SelectionChanged;
            ItemsInCategoryListBox.GotFocus += ItemsInCategoryListBox_GotFocus;
            ItemsInCategoryListBox.LostFocus += ItemsInCategoryListBox_LostFocus;

            ItemsForSaleListBox.ItemsSource = itemsForSaleList;
            ItemsForSaleListBox.SelectionChanged += ItemsForSaleListBox_SelectionChanged;
            ItemsForSaleListBox.GotFocus += ItemsForSaleListBox_GotFocus;
            ItemsForSaleListBox.LostFocus += ItemsForSaleListBox_LostFocus;

            ItemSaleDetailsSave.Click += SaleDetailsPopup_Close;
            AppendInfoTextBox.TextChanged += AppendInfoTextBox_TextChanged;


            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            HeaderIcon.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Images/c.png", UriKind.RelativeOrAbsolute));
            HeaderTitle.Text = "PD2 Sales";

        }

        private void AppendInfoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            activeDetailPopupItem.exportAppend = AppendInfoTextBox.Text;//
            PopupExportExampleString.Text = activeDetailPopupItem.GetExportString();
        }

        private void ItemsForSaleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsForSaleListBox.SelectedItem == null)
                return;


            PD2Item selectedItem = itemsForSaleList[ItemsForSaleListBox.SelectedIndex];
            FlowDocument itemDetailsText = detailsTextbox.Document;
            UpdateItemDetailsView(selectedItem, itemDetailsText);
        }

        void ItemsForSaleListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ItemsForSaleListBox.SelectedItem == null)
                return;

            PD2Item selectedItem = itemsForSaleList[ItemsForSaleListBox.SelectedIndex];

            if (selectedItem.stats == null)
                selectedItem.stats = new List<PD2ItemStat>();

            activeDetailPopupItem = selectedItem;

            PopupDetailsGrid.RowDefinitions.Clear();
            PopupDetailsGrid.Children.Clear();
            PopupTitle.Text = selectedItem.name;
            PopupTitle.Foreground = selectedItem.solidColorBrush;
            AppendInfoTextBox.Text = selectedItem.exportAppend;


            FontFamily fontFamily = new FontFamily("Lucida Console");

            SolidColorBrush newSolidColorBrushBackground = new SolidColorBrush();
            Color y = new Color();
            y.R = 47;
            y.G = 47;
            y.B = 47;
            y.A = 255;

            newSolidColorBrushBackground.Color = y;

            SolidColorBrush newSolidColorBrushForeground = new SolidColorBrush();
            Color x = new Color();
            x.R = 242;
            x.G = 242;
            x.B = 242;
            x.A = 255;
            newSolidColorBrushForeground.Color = x;

            TextBlock textBlock = new TextBlock();



            for (int i = 0; i < selectedItem.stats.Count; i++)
            {
                PopupDetailsGrid.RowDefinitions.Add(new RowDefinition());

                //Adding stat names
                TextBlock newStatText = new TextBlock();
                newStatText.Text = selectedItem.stats[i].statNames.readableName;
                newStatText.FontFamily = fontFamily;
                newStatText.FontSize = 12;
                newStatText.Margin = new Thickness(10, 10, 10, 10);
                newStatText.Foreground = newSolidColorBrushForeground;

                if (selectedItem.stats[i].range != null)
                    newStatText.Text += " (" + selectedItem.stats[i].range.min + " - " + selectedItem.stats[i].range.max + ")";

                PopupDetailsGrid.Children.Add(newStatText);

                //Adding changeable stat values
                TextBox newTextBox = new TextBox();
                newTextBox.Name = "index" + i.ToString();
                newTextBox.Text = selectedItem.stats[i].value;
                newTextBox.Foreground = newSolidColorBrushForeground;
                newTextBox.Background = newSolidColorBrushBackground;
                newTextBox.FontSize = 12;
                newTextBox.FontFamily = fontFamily;
                newTextBox.Margin = new Thickness(4);
                newTextBox.TextAlignment = TextAlignment.Left;
                newTextBox.Padding = new Thickness(10);
                newTextBox.VerticalContentAlignment = VerticalAlignment.Center;
                newTextBox.BorderBrush = newSolidColorBrushForeground;
                newTextBox.BorderThickness = new Thickness(1, 1, 1, 1);
                newTextBox.TextChanged += StatValueTextBox_TextChanged;

                PopupDetailsGrid.Children.Add(newTextBox);


                ItemsControl itemsControl = new ItemsControl();
                itemsControl.ItemContainerStyle = Application.Current.Resources["YesNoToggleStyle"] as Style;
                PopupDetailsGrid.Children.Add(itemsControl);

                ToggleButton newToggleButton = new ToggleButton();



                newToggleButton.Name = "index" + i.ToString();
                newToggleButton.Margin = new Thickness(10, 10, 10, 10);
                newToggleButton.HorizontalAlignment = HorizontalAlignment.Center;
                newToggleButton.VerticalAlignment = VerticalAlignment.Center;
                newToggleButton.Height = 20;
                newToggleButton.Width = 20;
                newToggleButton.BorderBrush = new SolidColorBrush(new Color() { R = 42, G = 42, B = 42, A = 255 });
                newToggleButton.BorderThickness = new Thickness(2);


                newToggleButton.Checked += statsExport_Checked;
                newToggleButton.Unchecked += statsExport_UnChecked;

                if (selectedItem.exportStatName.Exists(ex => ex == selectedItem.stats[i].name))
                {
                    newToggleButton.IsChecked = true;
                }
                else if (selectedItem.stats[i].range != null)
                {
                    newToggleButton.IsChecked = true;
                    if (!selectedItem.exportStatName.Exists(f => f == selectedItem.stats[i].name))
                        selectedItem.exportStatName.Add(selectedItem.stats[i].name);
                }
                else
                {
                    newToggleButton.IsChecked = false;
                    if (selectedItem.exportStatName.Exists(f => f == selectedItem.stats[i].name))
                        selectedItem.exportStatName.Remove(selectedItem.stats[i].name);
                }

                itemsControl.Items.Add(newToggleButton);

                Grid.SetRow(newStatText, i);
                Grid.SetRow(newTextBox, i);
                Grid.SetRow(itemsControl, i);
                Grid.SetColumn(newStatText, 0);
                Grid.SetColumn(newTextBox, 1);
                Grid.SetColumn(itemsControl, 2);

            }

            PopupExportExampleString.Text = selectedItem.GetExportString();


            SaleDetailsPopup.IsOpen = true;
            SaleDetailsPopup.Focus();

        }

        private void StatValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            activeDetailPopupItem.stats[int.Parse(textBox.Name.Replace("index", ""))].value = textBox.Text;
            PopupExportExampleString.Text = activeDetailPopupItem.GetExportString();
        }

        private void SaleDetailsPopup_Close(object sender, RoutedEventArgs e)
        {
            SaleDetailsPopup.IsOpen = false;
        }

        private void statsExport_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            if (!activeDetailPopupItem.exportStatName.Exists(x => x == activeDetailPopupItem.stats[int.Parse(button.Name.Replace("index", ""))].name))
                activeDetailPopupItem.exportStatName.Add(activeDetailPopupItem.stats[int.Parse(button.Name.Replace("index", ""))].name);

            PopupExportExampleString.Text = activeDetailPopupItem.GetExportString();

        }

        private void statsExport_UnChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton button = (ToggleButton)sender;
            activeDetailPopupItem.exportStatName.Remove(activeDetailPopupItem.stats[int.Parse(button.Name.Replace("index", ""))].name);
            PopupExportExampleString.Text = activeDetailPopupItem.GetExportString();

        }

        private void ItemsForSaleListBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ItemsForSaleListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ItemsInCategoryListBox.SelectedItem == null)
                return;

            ItemsInCategoryListBox.SelectedItem = null;
        }

        private void ItemsInCategoryListBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ItemsInCategoryListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ItemsForSaleListBox.SelectedItem == null)
                return;

            ItemsForSaleListBox.SelectedItem = null;
        }

        private void ItemsInCategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsInCategoryListBox.SelectedItem == null)
                return;

            PD2Item selectedItem = activeLoadedCategoryItems[ItemsInCategoryListBox.SelectedIndex];
            FlowDocument itemDetailsText = detailsTextbox.Document;
            UpdateItemDetailsView(selectedItem, itemDetailsText);

        }

        private void UpdateItemDetailsView(PD2Item selectedItem, FlowDocument itemDetailsText)
        {
            itemDetailsText.Blocks.Clear();

            Paragraph itemOnAccount = new Paragraph();
            itemOnAccount.LineHeight = 25;
            itemOnAccount.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;//Block.LineHeight="25" Block.LineStackingStrategy="BlockLineHeight"
            itemOnAccount.Inlines.Add("Account: " + selectedItem.accountOwner);
            itemDetailsText.Blocks.Add(itemOnAccount);

            Paragraph itemOnCharacter = new Paragraph();
            itemOnCharacter.Inlines.Add("Character: " + selectedItem.characterOwner);
            itemDetailsText.Blocks.Add(itemOnCharacter);

            Paragraph itemDetails = new Paragraph();
            itemDetails.Inlines.Add("Name: \t");
            if (string.IsNullOrEmpty(selectedItem.name))
            {
                if (selectedItem.isRuneword)
                {
                    itemDetails.Inlines.Add(new Run(selectedItem.type) { Foreground = GetRarityColor("Normal") });
                    itemDetails.Inlines.Add(new Run(" " + selectedItem.runeword + Environment.NewLine) { Foreground = GetRarityColor("Unique") });
                }
                else
                {
                    itemDetails.Inlines.Add(new Run(selectedItem.type + Environment.NewLine) { Foreground = GetRarityColor(selectedItem.quality) });
                }
            }
            else
                itemDetails.Inlines.Add(new Run(selectedItem.name + Environment.NewLine) { Foreground = GetRarityColor(selectedItem.quality) });



            if (selectedItem.isEthereal)
            {
                itemDetails.Inlines.Add("Type: \t" + selectedItem.type);
                itemDetails.Inlines.Add(new Run(" **ETHEREAL**" + Environment.NewLine) { Foreground = GetRarityColor("ETHEREAL") });
            }
            else
            {
                itemDetails.Inlines.Add("Type: \t" + selectedItem.type + Environment.NewLine);
            }

            if (selectedItem.unidentified)
                itemDetails.Inlines.Add("UNIDENTIFIED" + Environment.NewLine);

            itemDetails.Inlines.Add("iLVL: \t" + selectedItem.iLevel + Environment.NewLine);
            itemDetails.Inlines.Add("Quality: " + selectedItem.quality + Environment.NewLine);
            if (!string.IsNullOrEmpty(selectedItem.sockets))
                itemDetails.Inlines.Add("Sockets: \t" + selectedItem.sockets + Environment.NewLine);

            if (selectedItem.stats != null)
                for (int i = 0; i < selectedItem.stats.Count; i++)
                {
                    if (selectedItem.stats[i].statNames == null)
                        selectedItem.stats[i].GrabNames(loadedPD2ItemStatNames);

                    itemDetails.Inlines.Add(selectedItem.stats[i].statNames.readableName + ": " + selectedItem.stats[i].value + Environment.NewLine);
                }

            itemDetailsText.Blocks.Add(itemDetails);
        }

        private void ItemCategoriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            activeCategory = (ItemCategory)ItemCategoriesListBox.SelectedItem;


            switch (activeCategory)
            {
                case ItemCategory.Amulets:
                    activeLoadedCategoryItems = loadedAmulets;
                    break;
                case ItemCategory.Axes:
                    activeLoadedCategoryItems = loadedAxes;
                    break;
                case ItemCategory.Bases:
                    activeLoadedCategoryItems = loadedBases;
                    break;
                case ItemCategory.Belts:
                    activeLoadedCategoryItems = loadedBelts;
                    break;
                case ItemCategory.Armors:
                    activeLoadedCategoryItems = loadedArmors;
                    break;
                case ItemCategory.Boots:
                    activeLoadedCategoryItems = loadedBoots;
                    break;
                case ItemCategory.Bows:
                    activeLoadedCategoryItems = loadedBows;
                    break;
                case ItemCategory.Charms:
                    activeLoadedCategoryItems = loadedCharms;
                    break;
                case ItemCategory.Claws:
                    activeLoadedCategoryItems = loadedClaws;
                    break;
                case ItemCategory.Gloves:
                    activeLoadedCategoryItems = loadedGloves;
                    break;
                case ItemCategory.Helmets:
                    activeLoadedCategoryItems = loadedHelmets;
                    break;
                case ItemCategory.Javelins:
                    activeLoadedCategoryItems = loadedJavelins;
                    break;
                case ItemCategory.Jewels:
                    activeLoadedCategoryItems = loadedJewels;
                    break;
                case ItemCategory.Maces:
                    activeLoadedCategoryItems = loadedMaces;
                    break;
                case ItemCategory.Maps:
                    activeLoadedCategoryItems = loadedMaps;
                    break;
                case ItemCategory.Misc:
                    activeLoadedCategoryItems = loadedMisc;
                    break;
                case ItemCategory.Polearms:
                    activeLoadedCategoryItems = loadedPolearms;
                    break;
                case ItemCategory.Rings:
                    activeLoadedCategoryItems = loadedRings;
                    break;
                case ItemCategory.Runes:
                    activeLoadedCategoryItems = loadedRunes;
                    break;
                case ItemCategory.Runewords:
                    activeLoadedCategoryItems = loadedRunewords;
                    break;
                case ItemCategory.Scepters:
                    activeLoadedCategoryItems = loadedScepters;
                    break;
                case ItemCategory.Sets:
                    activeLoadedCategoryItems = loadedSets;
                    break;
                case ItemCategory.Shields:
                    activeLoadedCategoryItems = loadedShields;
                    break;
                case ItemCategory.Spears:
                    activeLoadedCategoryItems = loadedSpears;
                    break;
                case ItemCategory.Swords:
                    activeLoadedCategoryItems = loadedSwords;
                    break;
                case ItemCategory.Crossbows:
                    activeLoadedCategoryItems = loadedCrossbows;
                    break;
                case ItemCategory.Daggers:
                    activeLoadedCategoryItems = loadedDaggers;
                    break;
                case ItemCategory.Throw:
                    activeLoadedCategoryItems = loadedThrow;
                    break;
                case ItemCategory.Orbs:
                    activeLoadedCategoryItems = loadedOrbs;
                    break;
                case ItemCategory.Wands:
                    activeLoadedCategoryItems = loadedWands;
                    break;
                case ItemCategory.Staves:
                    activeLoadedCategoryItems = loadedStaves;
                    break;
                default:
                    activeLoadedCategoryItems = loadedMisc;
                    break;
            }

            ItemsInCategoryListBox.ItemsSource = activeLoadedCategoryItems;

        }

        public SolidColorBrush GetRarityColor(string quality)
        {
            SolidColorBrush cb = new SolidColorBrush();
            Color c = new Color();

            switch (quality)
            {
                case "Normal":
                    c.R = 235;
                    c.G = 235;
                    c.B = 235;
                    c.A = 255;
                    break;
                case "Magic":
                    c.R = 48;
                    c.G = 92;
                    c.B = 201;
                    c.A = 255;
                    break;
                case "Rare":
                    c.R = 210;
                    c.G = 210;
                    c.B = 30;
                    c.A = 255;
                    break;
                case "Unique":
                    c.R = 140;
                    c.G = 84;
                    c.B = 4;
                    c.A = 255;
                    break;
                case "ETHEREAL":
                    c.R = 150;
                    c.G = 150;
                    c.B = 150;
                    c.A = 255;
                    break;
                case "Set":
                    c.R = 6;
                    c.G = 196;
                    c.B = 15;
                    c.A = 255;
                    break;
                case "Crafted"://219, 104, 15
                    c.R = 219;
                    c.G = 104;
                    c.B = 15;
                    c.A = 255;
                    break;
                default:
                    c.R = 255;
                    c.G = 255;
                    c.B = 255;
                    c.A = 255;
                    break;
            }

            cb.Color = c;

            return cb;

        }
        public void CategorizeAllItems()
        {
            for (int i = 0; i < characters.Count; i++)
            {
                for (int j = 0; j < characters[i].inventory.Count; j++)
                {
                    switch (characters[i].inventory[j].category)
                    {
                        case ItemCategory.Amulets:
                            loadedAmulets.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Axes:
                            loadedAxes.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Bases:
                            loadedBases.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Belts:
                            loadedBelts.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Armors:
                            loadedArmors.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Boots:
                            loadedBoots.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Bows:
                            loadedBows.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Charms:
                            loadedCharms.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Claws:
                            loadedClaws.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Gloves:
                            loadedGloves.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Helmets:
                            loadedHelmets.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Javelins:
                            loadedJavelins.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Jewels:
                            loadedJewels.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Maces:
                            loadedMaces.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Maps:
                            loadedMaps.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Misc:
                            loadedMisc.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Polearms:
                            loadedPolearms.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Rings:
                            loadedRings.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Runes:
                            loadedRunes.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Runewords:
                            loadedRunewords.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Scepters:
                            loadedScepters.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Sets:
                            loadedSets.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Shields:
                            loadedShields.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Spears:
                            loadedSpears.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Swords:
                            loadedSwords.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Crossbows:
                            loadedCrossbows.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Daggers:
                            loadedDaggers.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Throw:
                            loadedThrow.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Orbs:
                            loadedOrbs.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Wands:
                            loadedWands.Add(characters[i].inventory[j]);
                            break;
                        case ItemCategory.Staves:
                            loadedStaves.Add(characters[i].inventory[j]);
                            break;
                        default:
                            break;
                    }
                }
            }

            loadedAmulets = loadedAmulets.OrderBy(x => x.name).ToList();
            loadedAxes = loadedAxes.OrderBy(x => x.name).ToList();
            loadedBases = loadedBases.OrderBy(x => x.name).ToList();
            loadedBelts = loadedBelts.OrderBy(x => x.name).ToList();
            loadedArmors = loadedArmors.OrderBy(x => x.name).ToList();
            loadedBoots = loadedBoots.OrderBy(x => x.name).ToList();
            loadedBows = loadedBows.OrderBy(x => x.name).ToList();
            loadedCharms = loadedCharms.OrderBy(x => x.name).ToList();
            loadedClaws = loadedClaws.OrderBy(x => x.name).ToList();
            loadedGloves = loadedGloves.OrderBy(x => x.name).ToList();
            loadedHelmets = loadedHelmets.OrderBy(x => x.name).ToList();
            loadedJavelins = loadedJavelins.OrderBy(x => x.name).ToList();
            loadedJewels = loadedJewels.OrderBy(x => x.name).ToList();
            loadedMaces = loadedMaces.OrderBy(x => x.name).ToList();
            loadedMaps = loadedMaps.OrderBy(x => x.name).ToList();
            loadedMisc = loadedMisc.OrderBy(x => x.name).ToList();
            loadedPolearms = loadedPolearms.OrderBy(x => x.name).ToList();
            loadedRings = loadedRings.OrderBy(x => x.name).ToList();
            loadedRunes = loadedRunes.OrderBy(x => x.name).ToList();
            loadedRunewords = loadedRunewords.OrderBy(x => x.name).ToList();
            loadedScepters = loadedScepters.OrderBy(x => x.name).ToList();
            loadedSets = loadedSets.OrderBy(x => x.name).ToList();
            loadedShields = loadedShields.OrderBy(x => x.name).ToList();
            loadedSpears = loadedSpears.OrderBy(x => x.name).ToList();
            loadedSwords = loadedSwords.OrderBy(x => x.name).ToList();
            loadedCrossbows = loadedCrossbows.OrderBy(x => x.name).ToList();
            loadedDaggers = loadedDaggers.OrderBy(x => x.name).ToList();
            loadedThrow = loadedThrow.OrderBy(x => x.name).ToList();
            loadedOrbs = loadedOrbs.OrderBy(x => x.name).ToList();
            loadedWands = loadedWands.OrderBy(x => x.name).ToList();
            loadedStaves = loadedStaves.OrderBy(x => x.name).ToList();

        }

        private void PriceClicked(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Name;

            switch (content)
            {
                case "AddForElButton":
                    AddToSales(SalesPrice.El);
                    break;
                case "AddForEldButton":
                    AddToSales(SalesPrice.Eld);
                    break;
                case "AddForTirButton":
                    AddToSales(SalesPrice.Tir);
                    break;
                case "AddForNefButton":
                    AddToSales(SalesPrice.Nef);
                    break;
                case "AddForEthButton":
                    AddToSales(SalesPrice.Eth);
                    break;
                case "AddForIthButton":
                    AddToSales(SalesPrice.Ith);
                    break;
                case "AddForTalButton":
                    AddToSales(SalesPrice.Tal);
                    break;
                case "AddForRalButton":
                    AddToSales(SalesPrice.Ral);
                    break;
                case "AddForOrtButton":
                    AddToSales(SalesPrice.Ort);
                    break;
                case "AddForThulButton":
                    AddToSales(SalesPrice.Thul);
                    break;
                case "AddForAmnButton":
                    AddToSales(SalesPrice.Amn);
                    break;
                case "AddForSolButton":
                    AddToSales(SalesPrice.Sol);
                    break;
                case "AddForShaelButton":
                    AddToSales(SalesPrice.Shael);
                    break;
                case "AddForDolButton":
                    AddToSales(SalesPrice.Dol);
                    break;
                case "AddForHelButton":
                    AddToSales(SalesPrice.Hel);
                    break;
                case "AddForIoButton":
                    AddToSales(SalesPrice.Io);
                    break;
                case "AddForLumButton":
                    AddToSales(SalesPrice.Lum);
                    break;
                case "AddForKoButton":
                    AddToSales(SalesPrice.Ko);
                    break;
                case "AddForFalButton":
                    AddToSales(SalesPrice.Fal);
                    break;
                case "AddForLemButton":
                    AddToSales(SalesPrice.Lem);
                    break;
                case "AddForPulButton":
                    AddToSales(SalesPrice.Pul);
                    break;
                case "AddForUmButton":
                    AddToSales(SalesPrice.Um);
                    break;
                case "AddForMalButton":
                    AddToSales(SalesPrice.Mal);
                    break;
                case "AddForIstButton":
                    AddToSales(SalesPrice.Ist);
                    break;
                case "AddForGulButton":
                    AddToSales(SalesPrice.Gul);
                    break;
                case "AddForVexButton":
                    AddToSales(SalesPrice.Vex);
                    break;
                case "AddForOhmButton":
                    AddToSales(SalesPrice.Ohm);
                    break;
                case "AddForLoButton":
                    AddToSales(SalesPrice.Lo);
                    break;
                case "AddForSurButton":
                    AddToSales(SalesPrice.Sur);
                    break;
                case "AddForBerButton":
                    AddToSales(SalesPrice.Ber);
                    break;
                case "AddForJahButton":
                    AddToSales(SalesPrice.Jah);
                    break;
                case "AddForChamButton":
                    AddToSales(SalesPrice.Cham);
                    break;
                case "AddForZodButton":
                    AddToSales(SalesPrice.Zod);
                    break;
                case "AddForOfferButton":
                    AddToSales(SalesPrice.Offer);
                    break;
                case "AddForWSSButton":
                    AddToSales(SalesPrice.WSS);
                    break;
                case "AddForHRButton":
                    AddToSales(SalesPrice.HR);
                    break;
                default:
                    break;
            }
        }
        private void PriceRightClicked(object sender, MouseButtonEventArgs e)
        {
            string content = (sender as Button).Name;

            switch (content)
            {
                case "AddForElButton":
                    AddToSales(SalesPrice.El, true);
                    break;
                case "AddForEldButton":
                    AddToSales(SalesPrice.Eld, true);
                    break;
                case "AddForTirButton":
                    AddToSales(SalesPrice.Tir, true);
                    break;
                case "AddForNefButton":
                    AddToSales(SalesPrice.Nef, true);
                    break;
                case "AddForEthButton":
                    AddToSales(SalesPrice.Eth, true);
                    break;
                case "AddForIthButton":
                    AddToSales(SalesPrice.Ith, true);
                    break;
                case "AddForTalButton":
                    AddToSales(SalesPrice.Tal, true);
                    break;
                case "AddForRalButton":
                    AddToSales(SalesPrice.Ral, true);
                    break;
                case "AddForOrtButton":
                    AddToSales(SalesPrice.Ort, true);
                    break;
                case "AddForThulButton":
                    AddToSales(SalesPrice.Thul, true);
                    break;
                case "AddForAmnButton":
                    AddToSales(SalesPrice.Amn, true);
                    break;
                case "AddForSolButton":
                    AddToSales(SalesPrice.Sol, true);
                    break;
                case "AddForShaelButton":
                    AddToSales(SalesPrice.Shael, true);
                    break;
                case "AddForDolButton":
                    AddToSales(SalesPrice.Dol, true);
                    break;
                case "AddForHelButton":
                    AddToSales(SalesPrice.Hel, true);
                    break;
                case "AddForIoButton":
                    AddToSales(SalesPrice.Io, true);
                    break;
                case "AddForLumButton":
                    AddToSales(SalesPrice.Lum, true);
                    break;
                case "AddForKoButton":
                    AddToSales(SalesPrice.Ko, true);
                    break;
                case "AddForFalButton":
                    AddToSales(SalesPrice.Fal, true);
                    break;
                case "AddForLemButton":
                    AddToSales(SalesPrice.Lem, true);
                    break;
                case "AddForPulButton":
                    AddToSales(SalesPrice.Pul, true);
                    break;
                case "AddForUmButton":
                    AddToSales(SalesPrice.Um, true);
                    break;
                case "AddForMalButton":
                    AddToSales(SalesPrice.Mal, true);
                    break;
                case "AddForIstButton":
                    AddToSales(SalesPrice.Ist, true);
                    break;
                case "AddForGulButton":
                    AddToSales(SalesPrice.Gul, true);
                    break;
                case "AddForVexButton":
                    AddToSales(SalesPrice.Vex, true);
                    break;
                case "AddForOhmButton":
                    AddToSales(SalesPrice.Ohm, true);
                    break;
                case "AddForLoButton":
                    AddToSales(SalesPrice.Lo, true);
                    break;
                case "AddForSurButton":
                    AddToSales(SalesPrice.Sur, true);
                    break;
                case "AddForBerButton":
                    AddToSales(SalesPrice.Ber, true);
                    break;
                case "AddForChamButton":
                    AddToSales(SalesPrice.Cham, true);
                    break;
                case "AddForZodButton":
                    AddToSales(SalesPrice.Zod, true);
                    break;
                case "AddForOfferButton":
                    AddToSales(SalesPrice.Offer, true);
                    break;
                case "AddForWSSButton":
                    AddToSales(SalesPrice.WSS, true);
                    break;
                case "AddForHRButton":
                    AddToSales(SalesPrice.HR, true);
                    break;
                default:
                    break;
            }
        }

        private void AddToSales(SalesPrice price, bool subtract = false)
        {
            PD2Item targetItem;

            if (ItemsInCategoryListBox.SelectedIndex > -1)
            {
                targetItem = activeLoadedCategoryItems[ItemsInCategoryListBox.SelectedIndex];
            }
            else if (ItemsForSaleListBox.SelectedIndex > -1)
            {
                targetItem = itemsForSaleList[ItemsForSaleListBox.SelectedIndex];
            }
            else
                return;

            if (subtract)
            {
                if (price == SalesPrice.Offer)
                    itemsForSaleList.RemoveAt(ItemsForSaleListBox.SelectedIndex);
                else if (price == SalesPrice.HR)
                {
                    if (targetItem.priceCount > 0.25f)
                        targetItem.priceCount -= 0.25f;
                    else
                    {
                        if (itemsForSaleList.Contains(targetItem))
                        {
                            targetItem.priceCount = 0;
                            itemsForSaleList.Remove(targetItem);
                        }
                    }
                }
                else
                {
                    if (targetItem.priceCount > 1)
                        targetItem.priceCount -= 1;
                    else
                    {
                        if (itemsForSaleList.Contains(targetItem))
                        {
                            targetItem.priceCount = 0;
                            itemsForSaleList.Remove(targetItem);
                        }
                    }
                }
            }
            else
            {
                if (targetItem.price == SalesPrice.HR && price == SalesPrice.HR)
                {
                    targetItem.price = price;
                    targetItem.priceCount += 0.25f;
                }
                else
                {
                    if (targetItem.price == price)
                    {
                        targetItem.priceCount += 1;
                    }
                    else
                    {
                        targetItem.price = price;
                        if (targetItem.price == SalesPrice.HR)
                            targetItem.priceCount = 0.25f;
                        else
                            targetItem.priceCount = 1;
                    }
                }

                if (!itemsForSaleList.Exists(x => x == targetItem))
                {
                    itemsForSaleList.Add(targetItem);
                }
            }

            itemsForSaleList = itemsForSaleList.OrderBy(x => (int)x.price).ToList();
            ItemsForSaleListBox.ItemsSource = itemsForSaleList;
            ItemsForSaleListBox.Items.Refresh();
        }

        private void RemoveFromSales_Clicked(object sender, RoutedEventArgs e)
        {
            if (ItemsForSaleListBox.SelectedIndex < 0)
            {
                return;
            }

            itemsForSaleList.RemoveAt(ItemsForSaleListBox.SelectedIndex);

            itemsForSaleList = itemsForSaleList.OrderBy(x => (int)x.price).ToList();
            ItemsForSaleListBox.ItemsSource = itemsForSaleList;
            ItemsForSaleListBox.Items.Refresh();

        }

        #region ResizeWindows
        bool ResizeInProcess = false;
        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (ResizeInProcess)
            {
                Rectangle senderRect = sender as Rectangle;
                Window mainWindow = senderRect.Tag as Window;
                if (senderRect != null)
                {
                    double width = e.GetPosition(mainWindow).X;
                    double height = e.GetPosition(mainWindow).Y;
                    senderRect.CaptureMouse();
                    if (senderRect.Name.ToLower().Contains("right"))
                    {
                        width += 5;
                        if (width > 0)
                            mainWindow.Width = width;
                    }
                    if (senderRect.Name.ToLower().Contains("left"))
                    {
                        width -= 5;
                        mainWindow.Left += width;
                        width = mainWindow.Width - width;
                        if (width > 0)
                        {
                            mainWindow.Width = width;
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("bottom"))
                    {
                        height += 5;
                        if (height > 0)
                            mainWindow.Height = height;
                    }
                    if (senderRect.Name.ToLower().Contains("top"))
                    {
                        height -= 5;
                        mainWindow.Top += height;
                        height = mainWindow.Height - height;
                        if (height > 0)
                        {
                            mainWindow.Height = height;
                        }
                    }
                }
            }
        }
        #endregion
        #region TitleButtons
        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    App.Current.MainWindow.DragMove();
                }
            }
        }

        private void AdjustWindowSize()
        {
            if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                App.Current.MainWindow.WindowState = WindowState.Normal;
                MaximizeButton.Content = "";
            }
            else if (App.Current.MainWindow.WindowState == WindowState.Normal)
            {
                App.Current.MainWindow.WindowState = WindowState.Maximized;
                MaximizeButton.Content = "";
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }
        #endregion

        private void ExportSales_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"sales.pd2"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, itemsForSaleList);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }

            List<string> exportStrings = new List<string>();

            for (int i = 0; i < itemsForSaleList.Count; i++)
            {
                exportStrings.Add(itemsForSaleList[i].GetExportString());
            }

            File.WriteAllLines("SalesExport_ByRune.txt", exportStrings);

            MessageBox.Show("Export Complete.\rCheck the ProjectD2/Stash folder.");


        }

    }
    
}
