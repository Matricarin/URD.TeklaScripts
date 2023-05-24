using System.IO;
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
        }

        /// <summary>
        /// Creating a main directory for storing export files in the model directory
        /// </summary>
        /// <param name="modelPath">the path to the model directory</param>
        /// <param name="directoryName">main directory</param>
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
    }
}
