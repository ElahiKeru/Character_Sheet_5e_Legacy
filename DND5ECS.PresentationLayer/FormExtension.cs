using DND5ECS.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DND5ECS.PresentationLayer
{
    public static class FormExtension
    {
        public static bool InitializeForm()
        {
            return true;
        }

        public static string ToMod(string score)
        {
            try
            {
                if (string.IsNullOrEmpty(score))
                {
                    score = "0";
                }
                double dScore = int.Parse(score);

                dScore -= 10;
                dScore /= 2;
                dScore = Math.Floor(dScore);
                return dScore.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Information");
                return "";
            }
        }

        public static string CalculateArmorClass(Armor currentArmor, string dexterityModifier, bool shield)
        {
            int totalAC = 0;

            if (currentArmor != null)
            {
                switch (currentArmor.ArmorType)
                {
                    case Armor.ArmorClassification.Light:
                        totalAC = currentArmor.ArmorClass + int.Parse(dexterityModifier);
                        break;
                    case Armor.ArmorClassification.Medium:
                        totalAC = currentArmor.ArmorClass + Math.Min(int.Parse(dexterityModifier), 2);
                        break;
                    case Armor.ArmorClassification.Heavy:
                        totalAC = currentArmor.ArmorClass;
                        break;
                }
            }
            else
            {
                totalAC = 10;
            }

            if(shield)
            {
                totalAC += 2;
            }

            return totalAC.ToString();
        }
    }
}
