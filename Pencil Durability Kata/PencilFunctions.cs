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
        private string filePath = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\config.txt");

        //created specifically for testing in order to keep the object private.
        public Pencil testPencil
        {
            get
            {
                return newPencil;
            }
        }

        public PencilFunctions()
        {

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
                            newPencil.pencilDurability = Convert.ToInt32(configValue);
                        }
                        else if (line.StartsWith("Pencil Length"))
                        {
                            newPencil.pencilLength = Convert.ToInt32(configValue);
                        }
                        else if (line.StartsWith("Eraser Durability"))
                        {
                            newPencil.eraserDurability = Convert.ToInt32(configValue);
                        }
                    }


                }
            }
        }
    }
}
