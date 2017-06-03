using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND5ECS.BusinessLogicLayer
{
    public class Race
    {
        #region Variables
        private string name;
        private int strengthBonus;
        private int dexterityBonus;
        private int constitutionBonus;
        private int intelligenceBonus;
        private int wisdomBonus;
        private int charismaBonus;
        private string abilities;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int StrengthBonus
        {
            get { return strengthBonus; }
            set { strengthBonus = value; }
        }

        public int DexterityBonus
        {
            get { return dexterityBonus; }
            set { dexterityBonus = value; }
        }

        public int ConstitutionBonus
        {
            get { return constitutionBonus; }
            set { constitutionBonus = value; }
        }

        public int IntelligenceBonus
        {
            get { return intelligenceBonus; }
            set { intelligenceBonus = value; }
        }

        public int WisdomBonus
        {
            get { return wisdomBonus; }
            set { wisdomBonus = value; }
        }

        public int CharismaBonus
        {
            get { return charismaBonus; }
            set { charismaBonus = value; }
        }

        public string Abilities
        {
            get { return abilities; }
            set { abilities = value; }
        }
        #endregion

        #region Constructor
        public Race(string name, string strengh, string dexterity, string constitution, string intelligence, string wisdom, string charisma, string abilitiy)
        {
            this.name = name;
            strengthBonus = int.Parse(strengh);
            dexterityBonus = int.Parse(dexterity);
            constitutionBonus = int.Parse(constitution);
            intelligenceBonus = int.Parse(intelligence);
            wisdomBonus = int.Parse(wisdom);
            charismaBonus = int.Parse(charisma);
            abilities = abilitiy;
        }
        #endregion
    }
}
