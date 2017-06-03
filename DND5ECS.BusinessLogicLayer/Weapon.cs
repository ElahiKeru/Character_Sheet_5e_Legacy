using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND5ECS.BusinessLogicLayer
{
    public class Weapon
    {
        #region Variables
        string name;
        public enum DamageType
        {
            Acid = 0,
            Bludgeoning,
            Cold,
            Fire,
            Force,
            Lightning,
            Necrotic,
            Piercing,
            Poison,
            Psychic,
            Radiant,
            Slashing,
            Thunder
        }
        DamageType dmgType;
        string damage;
        string properties;
        string classification;
        int weight;
        #endregion

        #region Properties 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DamageType DmgType
        {
            get { return dmgType; }
            //private set { dmgType = value; }
        }

        public string Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public string Properties
        {
            get { return properties; }
            set { properties = value; }
        }

        public string Classification
        {
            get { return classification; }
            set { classification = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        #endregion

        #region Constructor
        public Weapon(string nm, DamageType dt, string dmg, string prop, string wpcls, int wgt)
        {
            name = nm;
            dmgType = dt;
            damage = dmg;
            properties = prop;
            classification = wpcls;
            weight = wgt;
        }
        #endregion
    }
}
