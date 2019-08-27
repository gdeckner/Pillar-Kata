using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pencil_Durability_Kata
{
    public class PencilFunctions
    {

        private Pencil newPencil = new Pencil();
        // filePath points to a txt file that sets the values for pencil, default is set to current directory of this folder.
        private readonly string filePath = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\config.txt");
        private string createdWriteFilePath = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\WriteFile.txt");



        //created specifically for testing in order to keep it private.
        public Pencil TestPencil
        {
            get
            {
                return newPencil;
            }
        }
        //After using the WritingPreperation function it takes the output and appends it to an existing file or creates a new one
        public void WriteToFile(string input)
        {
            using (StreamWriter sw = File.AppendText(createdWriteFilePath))
            {
                sw.WriteLine(input);
            }
        }
        //Calls the CalculateDegradationPoints function in order to determine how much is allowed to be written, afterwards returns the new string that takes into account the pencil durability
        public string WritingPreperation(string input)
        {
            string output = "";
            int degradationPoints = CalculatePencilDegradationPoints(input);


            if (degradationPoints <= newPencil.CurrentPencilDurability)
            {
                output = input;

            }
            else
            {
                int count = newPencil.CurrentPencilDurability;

                foreach (char x in input)
                {

                    if (char.IsUpper(x) && count >= 2)
                    {
                        count -= 2;
                        output += x;
                    }
                    else if (!char.IsWhiteSpace(x) && !char.IsUpper(x) && count >= 1)
                    {
                        count -= 1;
                        output += x;
                    }
                    else
                    {
                        output += " ";
                    }
                }
            }

            DegradePencil(degradationPoints);
            return output;
        }
        //Calculates how many points the inputed string will degrade the pencil
        public int CalculatePencilDegradationPoints(string input)
        {

            int lowerCaseCount = input.Count(c => !char.IsWhiteSpace(c) && !char.IsUpper(c));
            int upperCaseCount = input.Count(c => char.IsUpper(c));
            int degradationPoints = lowerCaseCount + (upperCaseCount * 2);

            return degradationPoints;
        }
        //Degrades the pencil after CalculateDegradationPoints is finished
        public void DegradePencil(int pencilDegrationPoints)
        {

            newPencil.CurrentPencilDurability -= pencilDegrationPoints;
            if (newPencil.CurrentPencilDurability <= 0)
            {
                newPencil.CurrentPencilDurability = 0; //Adjust so you can't have a negative durability
            }
        }
        //Resets the pencil durability at the cost of one length
        public void SharpenPencil()
        {
            if (newPencil.PencilLength == 0)
            {
                //do nothing
            }
            else
            {
                newPencil.PencilLength -= 1;
                newPencil.CurrentPencilDurability = newPencil.StartPencilDurability;
            }
        }
        //Degrades the eraser after CalculateEraserDegradationPoints is finished
        public void DegradeEraser(int eraserDegrationPoints)
        {
            newPencil.EraserDurability -= eraserDegrationPoints;
            if (newPencil.EraserDurability <= 0)
            {
                newPencil.EraserDurability = 0;
            }
        }
        //Calculates how many points the inputed string will degrade the eraser
        public int CalculateEraserDegradationPoints(string input)
        {
            int result = input.Count(x => x != ' ');

            if(result > newPencil.EraserDurability)
            {
                result = newPencil.EraserDurability;
            }

            return result;
        }
        //Reads the txt file and converts it back into a string to prepare it for the eraser.
        public string ReadTxtFile()
        {
            string convertedTxtToString = "";
            string line;

            using (StreamReader sr = File.OpenText(createdWriteFilePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    convertedTxtToString += line;
                }
            }

            return convertedTxtToString;
        }
        //Calls the CalculateEraseDegradationPoints function to determine how much it can erase, then takes the string pulled from ReadTxtFile function and edits it to remove the selected txt
        public string EraserPreperation(string input, string targetToErase)
        {
            string output = "";


            int eraserPoints = CalculateEraserDegradationPoints(targetToErase);


            if (input.Contains(targetToErase))
            {
                int i = 0;
                char x;

                DegradeEraser(eraserPoints);
                int startIndex = input.LastIndexOf(targetToErase);

                StringBuilder sb = new StringBuilder(input);
                while (eraserPoints != 0)
                {
                    x = input[startIndex + i];

                    if (char.IsWhiteSpace(x))
                    {
                        i++;

                    }
                    else
                    {
                        eraserPoints--;
                        sb[startIndex + i] = ' ';
                        i++;
                    }

                }
                output = Convert.ToString(sb);
            }
            else
            {
                //Do nothing, can be changed to try catch but can change depending on how everything is implemented
            }

            return output;
        }
        //Takes the file path and reads the txt file located in that path and sets the pencil's properties.
        public string EditPreperation(string originalInput, string eraserPreppedString, string targetToEdit, string replacementString)
        {
            string output = "";
            int pencilPoints = CalculatePencilDegradationPoints(replacementString);
            int startIndex = originalInput.LastIndexOf(targetToEdit);
            StringBuilder sb = new StringBuilder(originalInput);
            char x;
            int i = 0;

            while(pencilPoints != 0 && i < replacementString.Length)
            {
                x = eraserPreppedString[startIndex + i];

               
                 if(char.IsWhiteSpace(x) && char.IsUpper(replacementString[i]) && pencilPoints >= 2)
                {
                    pencilPoints -= 2;
                    sb[startIndex + i] = replacementString[i];
                    i++;
                }
                else if(char.IsWhiteSpace(x))
                {
                    pencilPoints--;
                    sb[startIndex + i] = replacementString[i];
                    i++;
                }
                else if (!char.IsWhiteSpace(x))
                {
                    pencilPoints--;
                    sb[startIndex + i] = '@';
                    i++;
                }
            }
            output = Convert.ToString(sb);



            return output;
        }
        public void SetPencilSettings(string filePath)
        {

            string line;
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string configValue = "";
                    int index = 0;

                    line = sr.ReadLine();

                    if (line.Contains('='))
                    {
                        index = line.IndexOf('{');

                        configValue = line.Substring(index + 1, ((line.Length - 2) - index));

                        if (line.StartsWith("Pencil Durability"))
                        {
                            newPencil.StartPencilDurability = Convert.ToInt32(configValue);
                            newPencil.CurrentPencilDurability = newPencil.StartPencilDurability;
                        }
                        else if (line.StartsWith("Pencil Length"))
                        {
                            newPencil.PencilLength = Convert.ToInt32(configValue);
                        }
                        else if (line.StartsWith("Eraser Durability"))
                        {
                            newPencil.EraserDurability = Convert.ToInt32(configValue);
                        }
                    }


                }
            }
        }
    }
}
