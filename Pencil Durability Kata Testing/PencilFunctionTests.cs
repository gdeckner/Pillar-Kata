using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pencil_Durability_Kata;
using System.IO;

namespace Pencil_Durability_Kata_Testing
{
    [TestClass]
    public class PencilFunctionTests
    {
        //File Path for test file, must change path to match your settings.
         private string filePath = @"C:\Kota\Pencil Durability Kata\Pencil Durability Kata\config.txt";

        PencilFunctions testFunctions = new PencilFunctions();
        

        [TestMethod]
        public void SetPencilSettingsTest()
        {
            testFunctions.SetPencilSettings(filePath);

            Assert.AreNotEqual(1000, testFunctions.testPencil.pencilDurability);
            Assert.AreEqual(40000, testFunctions.testPencil.pencilDurability);

            Assert.AreNotEqual(2, testFunctions.testPencil.pencilLength);
            Assert.AreEqual(3, testFunctions.testPencil.pencilLength);

            Assert.AreNotEqual(400, testFunctions.testPencil.eraserDurability);
            Assert.AreEqual(5000, testFunctions.testPencil.eraserDurability);

        }
    }

}
