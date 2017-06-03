using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using DND5ECS.BusinessLogicLayer;
using DND5ECS.DataAccessLayer;
using DND5ECS.PresentationLayer;

namespace DND5ECS
{
    public partial class CharacterSheet : Form
    {
        #region Form Variables
        XMLItemFile itemFileAggregator;
        XMLRaceFile raceFileAggregator;
        Armor currentArmor;
        Weapon currentWeapon;
        Race currentRace;
        #endregion

        public CharacterSheet()
        {
            InitializeComponent();
        }

        #region Properties

        public string TxtAC
        {
            get { return txtAC.Text; }
            set
            {
                if(value is string)
                {
                    txtAC.Text = value;
                }
            }
        }

        #endregion

        #region FormLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            //initialization of form controls
            txtStrScr.Text = "0";
            txtDexScr.Text = "0";
            txtConScr.Text = "0";
            txtIntScr.Text = "0";
            txtWisScr.Text = "0";
            txtChaScr.Text = "0";
            numLvl.Value = 1;

            //File Aggregator Calls
            raceFileAggregator = new XMLRaceFile("RaceList.xml");
            itemFileAggregator = new XMLItemFile("ItemList.xml");

            raceFileAggregator.ProcessFileTags();
            itemFileAggregator.ProcessFileTags();

            cmbRace.Items.AddRange(raceFileAggregator.RaceNames.ToArray<object>());
            cmbArmor.Items.AddRange(itemFileAggregator.ArmorNames.ToArray<object>());
            cmbWeapon.Items.AddRange(itemFileAggregator.WeaponNames.ToArray<object>());
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtStrScr_TextChanged(object sender, EventArgs e)
        {
            txtStrMod.Text = FormExtension.ToMod(txtStrScr.Text);
            if(currentArmor != null)
            {
                CalcStrLimit();
            }
            StrSkillsCalc();
            if (currentWeapon != null)
            {
                CalcDmg(currentWeapon);
            }
            
        }

        private void txtDexScr_TextChanged(object sender, EventArgs e)
        {
            txtDexMod.Text = FormExtension.ToMod(txtDexScr.Text);
            if (currentArmor != null)
            {
                CalcAC(chkShield.Checked);
            }
            DexSkillsCalc();
            if (currentWeapon != null)
            {
                CalcDmg(currentWeapon);
            }
        }

        private void txtConScr_TextChanged(object sender, EventArgs e)
        {
            txtConMod.Text = FormExtension.ToMod(txtConScr.Text);
        }

        private void txtIntScr_TextChanged(object sender, EventArgs e)
        {
            txtIntMod.Text = FormExtension.ToMod(txtIntScr.Text);
            IntSkillsCalc();
        }

        private void txtWisScr_TextChanged(object sender, EventArgs e)
        {
            txtWisMod.Text = FormExtension.ToMod(txtWisScr.Text);
            WisSkillsCalc();
        }

        private void txtChaScr_TextChanged(object sender, EventArgs e)
        {
            txtChaMod.Text = FormExtension.ToMod(txtChaScr.Text);
            ChaSkillsCalc();
        }

        private void cmbArmor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            currentArmor = itemFileAggregator.ArmorList[cmbArmor.SelectedItem.ToString()];
            
            CalcAC(chkShield.Checked);
            CalcStrLimit();
                       
            txtType.Text = currentArmor.ArmorType.ToString();
            if (chkShield.Checked)
            {
                txtArmrWeight.Text = (currentArmor.ArmorWeight + 6).ToString();
            }
            else
            {
                txtArmrWeight.Text = currentArmor.ArmorWeight.ToString();
            }
            chkStealthOK.Checked = currentArmor.StealthDisadvantage;
        }

        private void chkShield_CheckedChanged(object sender, EventArgs e)
        {
            if (currentArmor != null)
            {
                CalcAC(chkShield.Checked);
                if (chkShield.Checked)
                {
                    txtArmrWeight.Text = (currentArmor.ArmorWeight + 6).ToString();
                }
                else
                {
                    txtArmrWeight.Text = (currentArmor.ArmorWeight).ToString();
                }
            }
        }

        private void chkSkillChanged(object sender, System.EventArgs e)
        {
            StrSkillsCalc();
            DexSkillsCalc();
            IntSkillsCalc();
            WisSkillsCalc();
            ChaSkillsCalc();
        }

        private void numLvl_ValueChanged(object sender, EventArgs e)
        {
            if(numLvl.Value <= 4)
            {
                txtProfBonus.Text = "2";
            }
            else if(numLvl.Value > 4 && numLvl.Value <= 8)
            {
                txtProfBonus.Text = "3";
            }
            else if(numLvl.Value > 8 && numLvl.Value <= 12)
            {
                txtProfBonus.Text = "4";
            }
            else if(numLvl.Value > 12 && numLvl.Value <= 16)
            {
                txtProfBonus.Text = "5";
            }
            else if(numLvl.Value > 16 && numLvl.Value <= 20)
            {
                txtProfBonus.Text = "6";
            }
        }

        private void cmbWeapon_SelectionChangeCommitted(object sender, EventArgs e)
        {
            currentWeapon = itemFileAggregator.WeaponList[cmbWeapon.SelectedItem.ToString()];

            CalcDmg(currentWeapon);
            txtWeapProps.Text = currentWeapon.Properties;
            txtWeapWeight.Text = currentWeapon.Weight.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (currentWeapon != null)
            {
                lbAttacks.Items.Add(currentWeapon.Name);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbAttacks.SelectedItem != null)
            {
                lbAttacks.Items.Remove(lbAttacks.SelectedItem);
            }
        }

        private void lbAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string viewedWeap = lbAttacks.Text.ToString();
            if(!string.IsNullOrEmpty(viewedWeap))
            {
                currentWeapon = itemFileAggregator.WeaponList[viewedWeap];

                CalcDmg(currentWeapon);
                txtWeapProps.Text = currentWeapon.Properties;
                txtWeapWeight.Text = currentWeapon.Weight.ToString();
            }
            else
            {
                if (lbAttacks.Items.Count > 0)
                {
                    lbAttacks.SelectedIndex = 0;
                    viewedWeap = lbAttacks.Text.ToString();
                    currentWeapon = itemFileAggregator.WeaponList[viewedWeap];

                    CalcDmg(currentWeapon);
                    txtWeapProps.Text = currentWeapon.Properties;
                    txtWeapWeight.Text = currentWeapon.Weight.ToString();
                }
                else
                {
                    txtDamage.Text = "";
                    txtWeapProps.Text = "";
                    txtWeapWeight.Text = "";
                }
            }
        }

        private void cmbRace_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (currentRace != null)
            {
                //remove old stuff
                txtAbils.Text = txtAbils.Text.Remove(txtAbils.Text.IndexOf(currentRace.Abilities), currentRace.Abilities.Length);
                txtStrScr.Text = (int.Parse(txtStrScr.Text) - currentRace.StrengthBonus).ToString();
                txtDexScr.Text = (int.Parse(txtDexScr.Text) - currentRace.DexterityBonus).ToString();
                txtConScr.Text = (int.Parse(txtConScr.Text) - currentRace.ConstitutionBonus).ToString();
                txtIntScr.Text = (int.Parse(txtIntScr.Text) - currentRace.IntelligenceBonus).ToString();
                txtWisScr.Text = (int.Parse(txtWisScr.Text) - currentRace.WisdomBonus).ToString();
                txtChaScr.Text = (int.Parse(txtChaScr.Text) - currentRace.CharismaBonus).ToString();
            }

            currentRace = raceFileAggregator.RaceList[cmbRace.SelectedItem.ToString()];

            txtAbils.Text = currentRace.Abilities;
            txtStrScr.Text = (int.Parse(txtStrScr.Text) + currentRace.StrengthBonus).ToString();
            txtDexScr.Text = (int.Parse(txtDexScr.Text) + currentRace.DexterityBonus).ToString();
            txtConScr.Text = (int.Parse(txtConScr.Text) + currentRace.ConstitutionBonus).ToString();
            txtIntScr.Text = (int.Parse(txtIntScr.Text) + currentRace.IntelligenceBonus).ToString();
            txtWisScr.Text = (int.Parse(txtWisScr.Text) + currentRace.WisdomBonus).ToString();
            txtChaScr.Text = (int.Parse(txtChaScr.Text) + currentRace.CharismaBonus).ToString();
        }

        #region calculations
        private void CalcAC(bool shield)
        {
            int totalAC;
            switch (currentArmor.ArmorType)
            {
                case Armor.ArmorClassification.Light:
                    if (shield)
                    {
                        totalAC = currentArmor.ArmorClass + int.Parse(txtDexMod.Text.ToString()) + 2;
                    }
                    else
                    {
                        totalAC = currentArmor.ArmorClass + int.Parse(txtDexMod.Text.ToString());
                    }
                    txtAC.Text = totalAC.ToString();
                    break;
                case Armor.ArmorClassification.Medium:
                    if (int.Parse(txtDexMod.Text.ToString()) > 2)
                    {
                        totalAC = currentArmor.ArmorClass + 2;
                    }
                    else
                    {
                        totalAC = currentArmor.ArmorClass + int.Parse(txtDexMod.Text.ToString());
                    }
                    if (shield)
                    {
                        totalAC += 2;
                    }
                    txtAC.Text = totalAC.ToString();
                    break;
                case Armor.ArmorClassification.Heavy:
                    totalAC = currentArmor.ArmorClass;
                    if (shield)
                    {
                        totalAC += 2;
                    }
                    txtAC.Text = totalAC.ToString();
                    break;
            }
        }

        private void CalcDmg(Weapon pWeap)
        {
            if (pWeap.Properties.Contains("Finesse"))
            {
                txtDamage.Text = pWeap.Damage + "+" + Math.Max(int.Parse(txtStrMod.Text), int.Parse(txtDexMod.Text)) + " " + pWeap.DmgType.ToString();
            }
            else if (pWeap.Properties.Contains("Ammunition") || pWeap.Properties.Contains("Thrown"))
            {
                txtDamage.Text = pWeap.Damage + "+" + txtDexMod.Text + " " + pWeap.DmgType.ToString();
            }
            else
            {
                txtDamage.Text = pWeap.Damage + "+" + txtStrMod.Text + " " + pWeap.DmgType.ToString();
            }
        }

        private void CalcStrLimit()
        {
            if (currentArmor.StrengthRequirement > int.Parse(txtStrScr.Text.ToString()))
            {
                chkStrEncmbr.Checked = true;
            }
            else
            {
                chkStrEncmbr.Checked = false;
            }
        }

        private void StrSkillsCalc()
        {
            //Athletics
            if (chkAthletic.Checked)
            {
                txtAtheltic.Text = (int.Parse(txtStrMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtAtheltic.Text = (int.Parse(txtStrMod.Text.ToString())).ToString();
            }
        }

        private void DexSkillsCalc()
        {
            //Acrobatics
            if (chkAcrobatics.Checked)
            {
                txtAcrobatics.Text = (int.Parse(txtDexMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtAcrobatics.Text = (int.Parse(txtDexMod.Text.ToString())).ToString();
            }
            //Sleight of Hand
            if (chkSleight.Checked)
            {
                txtSleight.Text = (int.Parse(txtDexMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtSleight.Text = (int.Parse(txtDexMod.Text.ToString())).ToString();
            }
            //Stealth
            if (chkStealth.Checked)
            {
                txtStealth.Text = (int.Parse(txtDexMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtStealth.Text = (int.Parse(txtDexMod.Text.ToString())).ToString();
            }
        }

        private void IntSkillsCalc()
        {
            //Arcana
            if (chkArcana.Checked)
            {
                txtArcana.Text = (int.Parse(txtIntMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtArcana.Text = (int.Parse(txtIntMod.Text.ToString())).ToString();
            }
            //History
            if (chkHistory.Checked)
            {
                txtHistory.Text = (int.Parse(txtIntMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtHistory.Text = (int.Parse(txtIntMod.Text.ToString())).ToString();
            }
            //Investigation
            if (chkInvest.Checked)
            {
                txtInvest.Text = (int.Parse(txtIntMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtInvest.Text = (int.Parse(txtIntMod.Text.ToString())).ToString();
            }
            //Nature
            if (chkNature.Checked)
            {
                txtNature.Text = (int.Parse(txtIntMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtNature.Text = (int.Parse(txtIntMod.Text.ToString())).ToString();
            }
            //Religion
            if (chkReligion.Checked)
            {
                txtReligion.Text = (int.Parse(txtIntMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtReligion.Text = (int.Parse(txtIntMod.Text.ToString())).ToString();
            }
        }

        private void WisSkillsCalc()
        {
            //Animal Handling
            if (chkAnimHand.Checked)
            {
                txtAnimHand.Text = (int.Parse(txtWisMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtAnimHand.Text = (int.Parse(txtWisMod.Text.ToString())).ToString();
            }
            //Insight
            if (chkInsight.Checked)
            {
                txtInsight.Text = (int.Parse(txtWisMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtInsight.Text = (int.Parse(txtWisMod.Text.ToString())).ToString();
            }
            //Medicine
            if (chkMed.Checked)
            {
                txtMed.Text = (int.Parse(txtWisMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtMed.Text = (int.Parse(txtWisMod.Text.ToString())).ToString();
            }
            //Perception
            if (chkPercept.Checked)
            {
                txtPercept.Text = (int.Parse(txtWisMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
                txtPassPerc.Text = (10 + int.Parse(txtPercept.Text.ToString())).ToString();
            }
            else
            {
                txtPercept.Text = (int.Parse(txtWisMod.Text.ToString())).ToString();
                txtPassPerc.Text = (10 + int.Parse(txtPercept.Text.ToString())).ToString();
            }
            //Survival
            if (chkSurvive.Checked)
            {
                txtSurvive.Text = (int.Parse(txtWisMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtSurvive.Text = (int.Parse(txtWisMod.Text.ToString())).ToString();
            }
        }

        private void ChaSkillsCalc()
        {
            //Deception
            if (chkDecept.Checked)
            {
                txtDecept.Text = (int.Parse(txtChaMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtDecept.Text = (int.Parse(txtChaMod.Text.ToString())).ToString();
            }
            //Intimidation
            if (chkIntim.Checked)
            {
                txtIntim.Text = (int.Parse(txtChaMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtIntim.Text = (int.Parse(txtChaMod.Text.ToString())).ToString();
            }
            //Performance
            if (chkPerf.Checked)
            {
                txtPerf.Text = (int.Parse(txtChaMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtPerf.Text = (int.Parse(txtChaMod.Text.ToString())).ToString();
            }
            //Persuasion
            if (chkPersuade.Checked)
            {
                txtPersuade.Text = (int.Parse(txtChaMod.Text.ToString()) + int.Parse(txtProfBonus.Text.ToString())).ToString();
            }
            else
            {
                txtPersuade.Text = (int.Parse(txtChaMod.Text.ToString())).ToString();
            }
        }
        #endregion
    }
}
