﻿using System;
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

        //created specifically for testing in order to keep the object private.
        public Pencil TestPencil
        {
            get
            {
                return newPencil;
            }
        }

        public string WritingPreperation(string input)
        {
            string output = "";
            int degradationPoints = CalculateDegradationPoints(input);
            

            if(degradationPoints <= newPencil.CurrentPencilDurability)
            {
                output = input;
               
            }
            else
            {
                int count = newPencil.CurrentPencilDurability;

                foreach(char x in input)
                {
                   
                    if(char.IsUpper(x) && count >= 2)
                    {
                        count -= 2;
                        output += x;
                    }
                    else if(!char.IsWhiteSpace(x) && !char.IsUpper(x) && count >= 1)
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
        public int CalculateDegradationPoints(string input)
        {
            int degradationPoints;
            int lowerCaseCount = 0;
            int upperCaseCount = 0;

            foreach(char c in input)
            {
                if(char.IsWhiteSpace(c))
                {
                    //do nothing
                }
                else if(char.IsUpper(c))
                {
                    upperCaseCount++;
                }
                else
                {
                    lowerCaseCount++;
                }
                
            }
            degradationPoints = lowerCaseCount + (upperCaseCount * 2);

            return degradationPoints;
        }
        public void DegradePencil(int degrationPoints)
        {
            
            newPencil.CurrentPencilDurability -= degrationPoints;
            if(newPencil.CurrentPencilDurability <= 0)
            {
                newPencil.CurrentPencilDurability = 0; //Adjust so you can't have a negative durability
            }
        }
        //Takes the file path and reads the txt file located in that path and sets the pencil's properties.
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
                        
                        configValue = line.Substring(index + 1, ((line.Length -2) - index));

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
