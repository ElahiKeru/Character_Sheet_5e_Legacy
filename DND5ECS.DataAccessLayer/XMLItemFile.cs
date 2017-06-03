using DND5ECS.BusinessLogicLayer;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DND5ECS.DataAccessLayer
{
    public class XMLItemFile : XMLFile
    {
        #region Constructors
        public XMLItemFile(string resource) : base(resource)
        {
            itemsDocument = XDocument.Load(ResourcePath);
        }
        #endregion

        #region Properties
        private XDocument itemsDocument;

        public XDocument ItemsDocument
        {
            get { return itemsDocument; }
            //private set { itemsDocument = value; }
        }

        private List<string> weapons;

        public List<string> Weapons
        {
            get
            {
                if(weapons == null)
                {
                    weapons = new List<string>();
                }

                return weapons;
            }
            //private set { weapons = value; }
        }

        private List<string> weaponNames;

        public List<string> WeaponNames
        {
            get
            {
                if(weaponNames == null)
                {
                    weaponNames = new List<string>();
                }

                return weaponNames;
            }
            //private set { weaponNames = value; }
        }

        private Dictionary<string, Weapon> weaponList;

        public Dictionary<string, Weapon> WeaponList
        {
            get
            {
                if(weaponList == null)
                {
                    weaponList = new Dictionary<string, Weapon>();
                }

                return weaponList;
            }
            //private set { weaponList = value; }
        }

        private List<string> armors;

        public List<string> Armors
        {
            get
            {
                if(armors == null)
                {
                    armors = new List<string>();
                }

                return armors;
            }
            //private set { armors = value; }
        }

        private List<string> armorNames;

        public List<string> ArmorNames
        {
            get
            {
                if(armorNames == null)
                {
                    armorNames = new List<string>();
                }

                return armorNames;
            }
            //private set { armorNames = value; }
        }

        private Dictionary<string, Armor> armorList;

        public Dictionary<string, Armor> ArmorList
        {
            get
            {
                if(armorList == null)
                {
                    armorList = new Dictionary<string, Armor>();
                }

                return armorList;
            }
            //private set { armorList = value; }
        }


        #endregion

        #region Methods
        public override bool ProcessFileTags()
        {
            foreach (XElement element in ItemsDocument.Descendants())
            {
                if (element.Name.ToString().Equals("armor"))
                {
                    foreach (XAttribute armorAttribute in element.Attributes())
                    {
                        Armors.Add(armorAttribute.Value);
                    }

                    ArmorNames.Add(Armors[0]);
                    ArmorList.Add(Armors[0], new Armor(Armors[0], int.Parse(Armors[5]),
                        int.Parse(Armors[4]), bool.Parse(Armors[3]), int.Parse(Armors[2]),
                        (Armor.ArmorClassification)int.Parse(Armors[1])));

                    Armors.Clear();
                }
                else if (element.Name.ToString().Equals("weapon"))
                {
                    foreach (XAttribute weaatr in element.Attributes())
                    {
                        Weapons.Add(weaatr.Value);
                    }

                    WeaponNames.Add(Weapons[0]);
                    WeaponList.Add(Weapons[0], new Weapon(Weapons[0],
                        (Weapon.DamageType)int.Parse(Weapons[1]), Weapons[2],
                        Weapons[3], Weapons[4], int.Parse(Weapons[5])));

                    Weapons.Clear();
                }
            }

            return true;
        }
        #endregion

    }
}
