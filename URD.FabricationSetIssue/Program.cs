using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Tekla.Structures.Model;
using TSMO = Tekla.Structures.Model.Operations;

namespace URD.FabricationSetIssue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Model currentModel = new Model();
            string modelPath = currentModel.GetInfo().ModelPath;
            string mainDirectoryName = "FabricationSet";
            string ncPlatesDirectory = "NC_plates";
            string ncProfilesDirectory = "NC_profiles";

            CreateMainDirectory(modelPath, mainDirectoryName);
            List<string> ncSettings = GetNCSettingsFromModel(modelPath);
            ncSettings = CheckJVKCSettings(ncSettings);
            CreateNCFiles(ncSettings, modelPath, mainDirectoryName, ncPlatesDirectory, ncProfilesDirectory);
        }        

        /// <summary>
        /// Creating a main directory for storing export files in the model directory
        /// </summary>
        /// <param name="modelPath">The path to the model directory</param>
        /// <param name="directoryName">Main directory</param>
        private static void CreateMainDirectory(string modelPath,
                                                string directoryName)
        {
            string directoryPath = Path.Combine(modelPath, directoryName);
            if(Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// Getting a list of settings to export
        /// </summary>
        /// <param name="modelPath">The path to the model directory</param>
        /// <returns>List of available settings in the model</returns>
        /// <exception cref="NotImplementedException"></exception>
        private static List<string> GetNCSettingsFromModel(string modelPath)
        {
            DirectoryInfo modelInfo = new DirectoryInfo(modelPath);
            List<string> settings = new List<string>();
            foreach(FileInfo file in modelInfo.GetFiles("*.ncfs", SearchOption.AllDirectories))
            {
                int index = file.Name.IndexOf('.');
                settings.Add(file.Name.Remove(index));
            }
            return settings;
        }

        /// <summary>
        /// Checks if there are JVKC environment settings.
        /// </summary>
        /// <param name="settings">NC settings in model folder.</param>
        /// <returns>List with only JVKC settings</returns>
        private static List<string> CheckJVKCSettings(List<string> settings)
        {
            int counter = 0;
            for(int i = 0; i < settings.Count; i++)
            {
                if (settings[i].Contains("JVKC"))
                {
                    counter++;
                }
                else
                {
                    settings[i] = string.Empty;
                }
            }
            if(counter != 2)
            {
                MessageBox.Show("There aren't JVKC settings for nc files in model folder.");
            }
            return settings;
        }

        /// <summary>
        /// Create nc files for the JVKC environment.
        /// </summary>
        /// <param name="settings">JVKC settings</param>
        /// <param name="modelPath">Path to model folder</param>
        /// <param name="mainDirectory">Directiry for all nc files</param>
        /// <param name="platesDir">Directory for plates</param>
        /// <param name="profilesDir">Directory for profiles</param>
        private static void CreateNCFiles(List<string> settings,
            string modelPath,
            string mainDirectory,
            string platesDir,
            string profilesDir)
        {
            string platesPath = modelPath + @"\" + mainDirectory + @"\" + platesDir + @"\";
            string profilesPath = modelPath + @"\" + mainDirectory + @"\" + profilesDir +@"\";
            string platesJVKC = string.Empty;
            string profilesJVKC = string.Empty;
            foreach(var item in settings)
            {
                if(item.Contains("plates"))
                {
                    platesJVKC = item;
                }
                if (item.Contains("profiles"))
                {
                    profilesJVKC = item;
                }
            }
            bool ncPlates = TSMO.Operation.CreateNCFilesFromSelected(platesJVKC, platesPath);
            bool ncProfiles = TSMO.Operation.CreateNCFilesFromSelected(profilesJVKC, profilesPath);
        }
    }
}
