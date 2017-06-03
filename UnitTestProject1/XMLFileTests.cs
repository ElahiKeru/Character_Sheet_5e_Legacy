using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DND5ECS.DataAccessLayer;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class XMLFileTests
    {
        [TestMethod]
        public void XMLRaceFileTest_Constructor_Success()
        {
            //Arrange
            string filename = @"C:\Users\Michael\Desktop\Programming\DND5ECS\DND5ECS\RaceList.xml";

            //Act
            XMLRaceFile xmlRaceFile = new XMLRaceFile(filename);

            //Assert
            Assert.AreEqual(xmlRaceFile.ResourcePath, filename);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void XMLRaceFileTest_Constructor_Failure()
        {
            //Arrange
            string filename = "thing.xml";

            //Act
            try
            {
                XMLRaceFile xrf = new XMLRaceFile(filename);
            }
            catch(FileNotFoundException fnfe)
            {
                //Assert
                Assert.IsTrue(fnfe.Message.Contains("Could not find file"));
                throw;
            }
        }

        [TestMethod]
        public void XMLRaceFileTest_ProcessFileTags_Success()
        {
            //Arrange
            string filename = @"C:\Users\Michael\Desktop\Programming\DND5ECS\DND5ECS\RaceList.xml";
            XMLRaceFile xrf = new XMLRaceFile(filename);

            //Act
            xrf.ProcessFileTags();

            //Assert
            Assert.IsNotNull(xrf.RaceList);
        }

        [TestMethod]
        public void XMLItemFileTest_Constructor_Success()
        {
            //Arrange
            string filename = @"C:\Users\Michael\Desktop\Programming\DND5ECS\DND5ECS\ItemList.xml";

            //Act
            XMLItemFile xif = new XMLItemFile(filename);

            //Assert
            Assert.AreEqual(xif.ResourcePath, filename);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void XMLItemFileTest_Constructor_Failure()
        {
            //Arrange
            string filename = "thing.xml";

            //Act
            try
            {
                XMLItemFile xif = new XMLItemFile(filename);
            }
            catch(FileNotFoundException fnfe)
            {
                Assert.IsTrue(fnfe.Message.Contains("Could not find file"));
                throw;
            }
        }

        [TestMethod]
        public void XMLItemFileTest_ProcessFileTags_Success()
        {
            //Arrange
            string filename = @"C:\Users\Michael\Desktop\Programming\DND5ECS\DND5ECS\ItemList.xml";
            XMLItemFile xif = new XMLItemFile(filename);

            //Act
            xif.ProcessFileTags();

            //Assert
            Assert.IsNotNull(xif.WeaponList);
        }
    }
}
