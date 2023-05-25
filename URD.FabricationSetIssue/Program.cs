using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tekla.Structures.Model;

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
            string dxfPlatesDirectory = "DXF_plates";
            string dxfProfilesDirectory = "DXF_profiles";
            string ncTubesDirectory = "NC_tubes";

            CreateMainDirectory(modelPath, mainDirectoryName);
            List<string> ncSettings = GetNCSettingsFromModel(modelPath);
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
    }
}
