# Pillar-Kata
Kata for pillar, Pencil Exercise

##Getting Started

This coding exercise was created within visual studios

1)The default pencil stats are set in Pillar-Kata\Pencil Durability Kata in the config.txt file
  and the program will look in that directory for that file, to change just edit the file path within the 
  filePath string inside the program. 
 
 2) The output file or WriteFile.txt is also in the same folder as config.txt, just change the filepath for createdWRiteFilePath string
 inside the c# program to change the location.
 
 
 Currently there is no interface, once one is added everything you need is within 3 methods
 
 1) PencilWrite - this will call on several methods/functions to write to the file, all that is needed is input which will be the text
  you wish to add
  
 2) PencilErase - All that is needed is the txt to be erased, it will erase the last occurance of that line.
 
 3) PencilEdit - Just needs txt to be erased/edited and the replacement text in order to function.
 
 
 There is currently no failsafes in case of bad input at this time
 
 The tests for this solution are included and all thats needed is to be run however if you wish to change the filepath for the config/writefile
 it will also need to be changed in the tests as well.
 
 
 
