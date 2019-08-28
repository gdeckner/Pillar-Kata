using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pencil_Durability_Kata;
using System.IO;

namespace Pencil_Durability_Kata_Testing
{
    [TestClass]
    public class PencilFunctionTests
    {
        //File Path for test file, must change path to match your settings.
        private string filePath = @"C:\Coding Stuff\Pillar-Kata\Pencil Durability Kata\config.txt";
        private string createdWriteFilePath = @System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\WriteFile.txt");

        PencilFunctions testFunctions = new PencilFunctions();
        private int TestPencilCurrentDurability { get
            {
                return testFunctions.TestPencil.CurrentPencilDurability;
            }
            set { }
        }

        public PencilFunctionTests()
        {
            testFunctions.SetPencilSettings(filePath);
            File.Delete(createdWriteFilePath);
        }
        [TestMethod]
        public void SetPencilSettingsTest()
        {
           
            Assert.AreNotEqual(1000, testFunctions.TestPencil.CurrentPencilDurability);
            Assert.AreEqual(40000, testFunctions.TestPencil.CurrentPencilDurability);

            Assert.AreNotEqual(2, testFunctions.TestPencil.PencilLength);
            Assert.AreEqual(3, testFunctions.TestPencil.PencilLength);

            Assert.AreNotEqual(400, testFunctions.TestPencil.EraserDurability);
            Assert.AreEqual(5000, testFunctions.TestPencil.EraserDurability);

        }
        [TestMethod]
        public void DegradePencilTest()
        {
            testFunctions.DegradePencil(20000);
            Assert.AreEqual(20000, TestPencilCurrentDurability);

            testFunctions.DegradePencil(40000);
            Assert.AreEqual(0, TestPencilCurrentDurability);

        }
        [TestMethod]
        public void CalculateDegradationPencilTest()
        {
            string testInput1 = "This is a test statement!!!";
            string testInput2 = "How Much Wood Would a Woodchuck Chuck If a Woodchuck Could Chuck Wood?";


            Assert.AreEqual(24,testFunctions.CalculatePencilDegradationPoints(testInput1));

            Assert.AreEqual(69, testFunctions.CalculatePencilDegradationPoints(testInput2));

        }
        [TestMethod]
        public void WritingPreperationTest()
        {
            string testInput1 = "This is a test statement!!!";
            string testInput2 = "How Much Wood Would a Woodchuck Chuck If a Woodchuck Could Chuck Wood?T";


            testFunctions.TestPencil.CurrentPencilDurability = 20;

            Assert.AreEqual("This is a test statemen    ", testFunctions.WritingPreperation(testInput1));

            testFunctions.TestPencil.CurrentPencilDurability = 70;

            Assert.AreEqual("How Much Wood Would a Woodchuck Chuck If a Woodchuck Could Chuck Wood? ", testFunctions.WritingPreperation(testInput2));
        }
        [TestMethod]
        public void WriteToTxtFileTest()
        {
            string testInput1 = "This is a test statement!!!";
            testFunctions.WriteToFile(testInput1);

            string testResults = "";

            using(StreamReader sr = File.OpenText(createdWriteFilePath))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    testResults += line;
                }
            }

            Assert.AreEqual(testInput1, testResults);
        }
        [TestMethod]
        public void SharpenPencilTest()
        {
            int startPencilLength = testFunctions.TestPencil.PencilLength;
            testFunctions.TestPencil.CurrentPencilDurability = 50;
            testFunctions.SharpenPencil();

            Assert.AreEqual(testFunctions.TestPencil.StartPencilDurability, TestPencilCurrentDurability);

            Assert.AreEqual(startPencilLength - 1, testFunctions.TestPencil.PencilLength);
        }
        [TestMethod]
        public void DegradeEraserTest()
        {
            testFunctions.DegradeEraser(5000);

            Assert.AreEqual(0, testFunctions.TestPencil.EraserDurability);

            testFunctions.TestPencil.EraserDurability = 10000;
            testFunctions.DegradeEraser(5000);
            Assert.AreEqual(5000, testFunctions.TestPencil.EraserDurability);

            testFunctions.DegradeEraser(500000);
            Assert.AreEqual(0, testFunctions.TestPencil.EraserDurability);

        }
        [TestMethod]
        public void CalculateEraserDegradePointsTest()
        {
            string input = "This is an eraser test, I really hope it works that would be nice!";

            int result = testFunctions.CalculateEraserDegradationPoints(input);

            Assert.AreEqual(53, result);

        }
        [TestMethod]
        public void ReadFileTest()
        {
            string testStatement = "This is a test statement, please do something that makes the world a better place! Thundercats are on the move, Thundercats are loose! Feel the magic hear the roar Thundercats are loose!";

            testFunctions.WriteToFile(testStatement);

            Assert.AreEqual(testStatement, testFunctions.ReadTxtFile());

        }
        [TestMethod]
        public void EraserPreperationTest()
        {
            string testStatement = "Thundercats are on the move, Thundercats are loose! Feel the magic hear the roar Thundercats are loose!";

            testFunctions.WriteToFile(testStatement);

            testFunctions.TestPencil.EraserDurability = 1000;

            Assert.AreEqual("Thundercats are on the move, Thundercats are loose! Feel the magic hear the roar Thundercats are       ", testFunctions.EraserPreperation(testStatement, "loose!"));

            Assert.AreEqual("Thundercats are on the     , Thundercats are loose! Feel the magic hear the roar Thundercats are loose!", testFunctions.EraserPreperation(testStatement, "move"));

            testFunctions.TestPencil.EraserDurability = 5;

            Assert.AreEqual("Thundercats are on the move, Thundercats are loose! Feel the magic hear the roar Thundercats are      !", testFunctions.EraserPreperation(testStatement, "loose!"));

            
 
        }
        [TestMethod]
        public void EditPreperationTest()
        {
            string testStatement = "Thundercats are on the move, Thundercats are loose! Feel the magic hear the roar Thundercats are loose!";

           string eraserPrepped = testFunctions.EraserPreperation(testStatement, "move");

            Assert.AreEqual("Thundercats are on the OINK, Thundercats are loose! Feel the magic hear the roar Thundercats are loose!", testFunctions.EditPreperation(testStatement, eraserPrepped,"move","OINK"));

            eraserPrepped = testFunctions.EraserPreperation(testStatement, "move");
            Assert.AreEqual("Thundercats are on the HOOO@O@hundercats are loose! Feel the magic hear the roar Thundercats are loose!", testFunctions.EditPreperation(testStatement, eraserPrepped, "move", "HOOOOOO"));
        }
        [TestMethod]
        public void FullIntegrationTesting()
        {
            string testString1 = "Thundercats HOOOOOOOOOOO!!!";
            string testString2 = " Thundercats are loose!";
            testFunctions.PencilWrite(testString1);

            Assert.AreEqual(39961, TestPencilCurrentDurability);

            testFunctions.SharpenPencil();
            Assert.AreEqual(40000, TestPencilCurrentDurability);
            Assert.AreEqual(2, testFunctions.TestPencil.PencilLength);
            Assert.AreEqual(testString1, testFunctions.ReadTxtFile());

            testFunctions.PencilWrite(testString2);

            Assert.AreEqual(testString1 + testString2, testFunctions.ReadTxtFile());
            Assert.AreEqual(39979, TestPencilCurrentDurability);

            testFunctions.PencilErase("are");

            Assert.AreEqual("Thundercats HOOOOOOOOOOO!!! Thundercats     loose!", testFunctions.ReadTxtFile());
            Assert.AreEqual(4997, testFunctions.TestPencil.EraserDurability);

            testFunctions.PencilEdit("loose","MoOsee");

            Assert.AreEqual("Thundercats HOOOOOOOOOOO!!! Thundercats     MoOse@", testFunctions.ReadTxtFile());
            Assert.AreEqual(4992, testFunctions.TestPencil.EraserDurability);
            Assert.AreEqual(39971, TestPencilCurrentDurability);

            testFunctions.TestPencil.EraserDurability = 5;
            testFunctions.PencilErase("Thundercats");

            Assert.AreEqual("Thundercats HOOOOOOOOOOO!!!      ercats     MoOse@", testFunctions.ReadTxtFile());
            Assert.AreEqual(0, testFunctions.TestPencil.EraserDurability);

            testFunctions.TestPencil.PencilLength = 1;
            testFunctions.TestPencil.CurrentPencilDurability = 3;
            testFunctions.TestPencil.EraserDurability = 5;

            testFunctions.PencilEdit("ercats", "PILLAR");

            Assert.AreEqual(0, TestPencilCurrentDurability);

            testFunctions.SharpenPencil();

            Assert.AreEqual(40000, TestPencilCurrentDurability);
            Assert.AreEqual(0, testFunctions.TestPencil.PencilLength);

            testFunctions.TestPencil.CurrentPencilDurability = 3;
            Assert.AreEqual("Thundercats HOOOOOOOOOOO!!!      P    @     MoOse@", testFunctions.ReadTxtFile());
            
            
            



        }
    }

}
