using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{
   /// <summary>
   /// Utility Class for files/folders.
   /// </summary>
   public static class FileUtilities
   {
      /// <summary>
      /// Creates a new directory
      /// </summary>
      /// <param name="path"></param>
      /// <returns>The new directory information</returns>
      public static string CreateDirectory(string path)
      {
         if (!DirectoryExists(path))
         {
            Directory.CreateDirectory(path);
         }

         return path;
      }

      /// <summary>
      /// Returns true if a file exists.
      /// </summary>
      /// <param name="path"></param>
      /// <returns></returns>
      public static bool FileExists(string path)
      {
         return File.Exists(path);
      }

      /// <summary>
      /// Return true if directory exists.
      /// </summary>
      /// <param name="path"></param>
      /// <returns></returns>
      public static bool DirectoryExists(string path)
      {
         return Directory.Exists(path);
      }

      /// <summary>
      /// Returns true if paths exists.
      /// </summary>
      /// <param name="paths"></param>
      /// <param name="isFiles"></param>
      /// <returns></returns>
      public static bool PathsExist(List<string> paths, bool isFiles = true)
      {
         bool allPathsExists = true;
         foreach(string path in paths)
         {
            bool pathExists = isFiles ? 
               FileExists(path) : DirectoryExists(path);

            if(!pathExists)
            {
               allPathsExists = false;
               break;
            }
         }

         return allPathsExists;
      }

      /// <summary>
      /// Returns the current directory.
      /// </summary>
      /// <returns></returns>
      public static string CurrentDirectory()
      {
         return Directory.GetCurrentDirectory();
      }

      /// <summary>
      /// Returns a list of directory names in a given directory.
      /// </summary>
      /// <param name="path"></param>
      /// <returns></returns>
      public static List<string> GetDirectories(string path)
      {
         return new List<string>(Directory.EnumerateDirectories(path));
      }
   }
}
