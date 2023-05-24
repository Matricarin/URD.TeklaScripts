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
        }
    }
}
