using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryGenTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGenTree.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void EncriptarTest()
        {
            string encryptedPassword = Utils.Encriptar("strongSecurePassword");

            Assert.IsNotNull(encryptedPassword);
        }

        [TestMethod()]
        public void NivelComplejidadTest()
        {
            Assert.AreEqual(0, Utils.NivelComplejidad("test"));
            Assert.AreEqual(2, Utils.NivelComplejidad("Teststring"));
            Assert.AreEqual(4, Utils.NivelComplejidad("TestString1@"));
        }

        [TestMethod()]
        public void EsEMailTest()
        {
            Assert.IsTrue(Utils.EsEMail("test@test.com"));
            Assert.IsFalse(Utils.EsEMail("no-email"));
        }
    }
}