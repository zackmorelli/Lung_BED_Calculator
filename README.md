# Lung_BED_Calculator

This program is for use with ARIA/Eclipse, which is a commerical radiation treatment planning software suite made by Varian Medical Systems which is used in Radiation Oncology. This is one of several programs which I have made while working in the Radiation Oncology department at Lahey Hospital and Medical Center in Burlington, MA. I have licensed it under GPL V3 so it is open-source and publicly available.

There is also a .docx README file in the repo that describes what the program does and how it is organized.

This is a simple ESAPI script I made to assist with a research project we did about Lung SBRT plans. The program has a small WinForms GUI that the user uses to select which plan, among those they have open in Eclipse that they want to run the program on. The program then finds the Lungs-GTV and Lung structures, if they are present, and loops through the DVH curves of both to calculate the mean BED of both structures, as well as the volume percentage of the Lungs-GTV structure at BED = 70. These values are then displayed on the GUI.
