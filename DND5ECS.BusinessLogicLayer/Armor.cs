using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND5ECS.BusinessLogicLayer
{
    public class Armor
    {
        #region Variables
        private string armorName;
        private int armorClass;
        private int strengthRequirement;
        private bool stealthDisadvantage;
        private int armorWeight;
        ArmorClassification armorType;

        public enum ArmorClassification
        {
            Light = 0,
            Medium = 1,
            Heavy = 2
        }
        #endregion

        #region Properties
        public string ArmorName
        {
            get
            {
                return armorName;
            }
            set
            {
                armorName = value;
            }
        }

        public int ArmorClass
        {
            get
            {
                return armorClass;
            }
        }

        public int StrengthRequirement
        {
            get
            {
                return strengthRequirement;
            }
        }

        public bool StealthDisadvantage
        {
            get
            {
                return stealthDisadvantage;
            }
            set
            {
                stealthDisadvantage = value;
            }
        }

        public int ArmorWeight
        {
            get
            {
                return armorWeight;
            }
        }

        public ArmorClassification ArmorType
        {
            get
            {
                return armorType;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Armor()
        {
            armorName = "";
            armorClass = 0;
            strengthRequirement = 0;
            stealthDisadvantage = true;
            armorWeight = 0;
            armorType = ArmorClassification.Light;
        }

        //full constructor
        public Armor(string name, int armorClass, int strengthRequirement, bool stealthDisadvantage, int weight, ArmorClassification type)
        {
            armorName = name;
            this.armorClass = armorClass;
            this.strengthRequirement = strengthRequirement;
            this.stealthDisadvantage = stealthDisadvantage;
            armorWeight = weight;
            armorType = type;
        }
        #endregion


    }
}
