using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FileIO;
using JSON_Parser;

namespace Party_Squares_Game
{
   public class GameConfiguration
   {
      /// <summary>
      /// Constructor
      /// </summary>
      public GameConfiguration()
      {
         FilePath = "";
         DataFileName = "GameDataFile.json";
         TeamName1 = "";
         TeamName2 = "";
      }

      /// <summary>
      /// Constructor with filepath parameter.
      /// </summary>
      /// <param name="filePath"></param>
      public GameConfiguration(string filePath)
      {
         SetFilePath(filePath);
         DataFileName = "GameDataFile.json";
         TeamName1 = "";
         TeamName2 = "";
      }

      /// <summary>
      /// Method for finding GameDataFile
      /// configuration file.
      /// </summary>
      /// <returns></returns>
      public string FindDataFile()
      {
         string dataFile = DataFileName;
         string currentDirPath = FileUtilities.CurrentDirectory();
         string dataFilePath = "";
         List<string> pathsToTest = new List<string>();
         pathsToTest.Add(currentDirPath);
         pathsToTest.Add(currentDirPath + @"\..\");
         pathsToTest.Add(currentDirPath + @"\..\..\");
         pathsToTest.Add(currentDirPath + @"\..\..\..\");

         foreach(string path in pathsToTest)
         {
            string dirPath = FindDataDirectory(path, "Data");
            if(dirPath != "")
            {
               dataFilePath = dirPath;
               SetDataDirectoryPath(dirPath);
               break;
            }
         }

         dataFilePath = dataFilePath + "\\" + dataFile;

         if(!FileUtilities.FileExists(dataFilePath))
         {
            dataFilePath = "";
         }

         return dataFilePath; ;
      }

      /// <summary>
      /// Find the Data directory
      /// </summary>
      /// <param name="path"></param>
      /// <param name="directoryName"></param>
      /// <returns></returns>
      private string FindDataDirectory(string path, string directoryName = "Data")
      {
         string returnPath = "";
         List<string> directories = FileUtilities.GetDirectories(path);
         foreach(string directory in directories)
         {
            int lastIndex = directory.LastIndexOf('\\') + 1;
            string dirName = directory.Substring(lastIndex);
            if(dirName == directoryName)
            {
               returnPath = directory;
               break;
            }
         }

         return returnPath;
      }

      /// <summary>
      /// Set the filepath of the data file.
      /// </summary>
      /// <param name="filePath"></param>
      public void SetFilePath(string filePath)
      {
         FilePath = filePath;
      }

      /// <summary>
      /// Set the Data Directory path
      /// </summary>
      /// <param name="dirPath"></param>
      public void SetDataDirectoryPath(string dirPath)
      {
         DirectoryPath = dirPath;
      }

      /// <summary>
      /// Read the stored data file path
      /// </summary>
      /// <returns></returns>
      public bool ReadFile()
      {
         FilePath = FindDataFile();

         return ReadFile(FilePath);
      }

      /// <summary>
      /// Reads in the provided data file
      /// </summary>
      /// <param name="filePath"></param>
      /// <returns></returns>
      public bool ReadFile(string filePath)
      {
         bool readFileSuccess = false;
         SetFilePath(filePath);

         JSON_Reader reader = new JSON_Reader(FilePath);
         if (reader.ParseFile())
         {
            ImageDirectory = reader.GetValue("Image Directory");
            JSON_Node team1 = reader.GetChild("Team 1");
            JSON_Node team2 = reader.GetChild("Team 2");

            if (team1 != null)
            {
               if (team1.ContainsKey("Image"))
               {
                  TeamImage1 = ImageDirectory + "\\" + team1.GetValue("Image");
                  readFileSuccess = true;
               }

               if (team1.ContainsKey("Name"))
               {
                  TeamName1 = team1.GetValue("Name");
                  readFileSuccess = true;
               }
            }

            if (team2 != null)
            {
               if (team2.ContainsKey("Image"))
               {
                  TeamImage2 = ImageDirectory + "\\" + team2.GetValue("Image");
                  readFileSuccess = true;
               }

               if (team2.ContainsKey("Name"))
               {
                  TeamName2 = team2.GetValue("Name");
                  readFileSuccess &= true;
               }
            }
         }

         return readFileSuccess;
      }

      public string DirectoryPath
      {
         get;
         set;
      }

      public string FilePath
      {
         get;
         set;
      }

      public string DataFileName
      {
         get;
         set;
      }

      public string ImageDirectory
      {
         get;
         private set;
      }

      public string TeamImage1
      {
         get;
         private set;
      }

      public string TeamImage2
      {
         get;
         private set;
      }

      public string TeamName1
      {
         get;
         private set;
      }

      public string TeamName2
      {
         get;
         private set;
      }
   }
}
