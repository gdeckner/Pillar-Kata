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
            Assert.AreEqual(20000,testFunctions.TestPencil.CurrentPencilDurability);

            testFunctions.DegradePencil(40000);
            Assert.AreEqual(0, testFunctions.TestPencil.CurrentPencilDurability);

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
        public void FullWriteAndDegradePointsTest()
        {
            string testInput1 = "This is a test statement!!!";


            testFunctions.TestPencil.CurrentPencilDurability = 20;

            string writingTestResult = testFunctions.WritingPreperation(testInput1);

            Assert.AreEqual("This is a test statemen    ", writingTestResult);
            Assert.AreEqual(0, testFunctions.TestPencil.CurrentPencilDurability);

            testFunctions.WriteToFile(writingTestResult);

           

            string testResults = "";

            using (StreamReader sr = File.OpenText(createdWriteFilePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    testResults += line;
                }
            }

            Assert.AreEqual(writingTestResult, testResults);



        }
        [TestMethod]
        public void SharpenPencilTest()
        {
            int startPencilLength = testFunctions.TestPencil.PencilLength;
            testFunctions.TestPencil.CurrentPencilDurability = 50;
            testFunctions.SharpenPencil();

            Assert.AreEqual(testFunctions.TestPencil.StartPencilDurability, testFunctions.TestPencil.CurrentPencilDurability);

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
    }

}
