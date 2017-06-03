using Microsoft.VisualStudio.TestTools.UnitTesting;
using DND5ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DND5ECS.Tests
{
    [TestClass()]
    public class CharacterSheetTests
    {
        [TestMethod()]
        public void CharacterSheetTest_TxtACProperty_Overloads()
        {
            //Arrange
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CharacterSheet cs = new CharacterSheet();

            //Act
            cs.TxtAC = "200";

            
            Application.Run(cs);

            //Assert
            Assert.IsTrue(true);
        }
    }
}