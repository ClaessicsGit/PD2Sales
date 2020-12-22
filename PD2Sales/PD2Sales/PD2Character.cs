using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace PD2Sales
{
    public class PD2Character
    {
        public string accountName;
        public string name;
        public List<PD2Item> inventory;

        public PD2Character(string fileName, List<PD2ItemStatNames> pD2ItemStatNames)
        {
            string[] splitString = fileName.Split('_');

            if (splitString.Length > 0)
            {
                accountName = splitString[0];
                for (int i = 1; i < splitString.Length; i++)
                {
                    name += splitString[i].Replace(".txt","");

                    if (i < splitString.Length - 1)
                        name += "_";

                }
            }

            LoadInventory(fileName, pD2ItemStatNames);
        }

        public void LoadInventory(string fileName, List<PD2ItemStatNames> pD2ItemStatNames)
        {
            string readData = "";
            readData = File.ReadAllText(fileName);


            inventory = JsonConvert.DeserializeObject<List<PD2Item>>(readData);

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].AssignParentType(accountName, name);
                if (string.IsNullOrEmpty(inventory[i].name))
                    inventory[i].name = inventory[i].type;
                

                if (inventory[i].stats != null)
                    for (int j = 0; j < inventory[i].stats.Count; j++)
                    {
                        inventory[i].stats[j].GrabNames(pD2ItemStatNames);
                    }
                if (inventory[i].exportStatName == null)
                    inventory[i].exportStatName = new List<string>();
            }
        }
    }
}
