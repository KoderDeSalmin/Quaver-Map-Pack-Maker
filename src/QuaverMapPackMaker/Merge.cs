using System;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using Quaver.API.Maps;


namespace QuaverMapPackMaker.Helpers;

class Merge{
    public static bool qpStored = false;
    private static int idx = 0;

    public static void StoreFiles(String qpPath){
        if(!qpStored)
            ZipFile.ExtractToDirectory(qpPath, Tools.targetDir);
            qpStored = true;

        ZipFile.ExtractToDirectory(qpPath, Tools.sourceDir);
        Combine();
    }

    

    public static void Combine(){
        try{
            DelImg();
        } catch (Exception error){
            Debug.WriteLine(error);
        }

        foreach (var file in Directory.GetFiles(Tools.sourceDir, "*.qua")) // This loops on every .qua files from source folder
        {
            if(!Path.GetFileName(file).Substring(0,8).Equals("Imported")){
                var qua = Qua.Parse(file);
                qua.AudioFile = CopyAsset(qua.AudioFile, Tools.sourceDir, Tools.targetDir);

                // Saved inside the temporary Reference folder
                qua.Save(Path.Combine(Tools.targetDir, $"imported-{idx++}.qua"));
            }
        }
        




        // Convert all difficulties from Reference folder into a single mapset
        // Credit: A6sT, Small edits from Koder
        foreach (var file in Directory.GetFiles(Tools.targetDir, "*.qua"))
        {   
            if(!Path.GetFileName(file).Substring(0,8).Equals("Imported")){
                var qua = Qua.Parse(file);
                var song = string.IsNullOrWhiteSpace(qua.Artist) ? qua.Title : $"{qua.Artist} - {qua.Title}";
                var difficulty = string.IsNullOrWhiteSpace(qua.DifficultyName) ? "Difficulty" : qua.DifficultyName;

                qua.MapId = -1;
                qua.MapSetId = -1;
                qua.DifficultyName = $"{song} [{qua.Creator}]"; // We use a custom difficulty name to prevent duplication detection from quaver if one of the difficulty already exists in the user's game
                qua.Artist = "Various Artists";
                qua.Title = MapsetInfo.Name;
                qua.Creator = MapsetInfo.Creator;
                qua.BackgroundFile = MapsetInfo.Banner;
                qua.BannerFile = MapsetInfo.Background;

                qua.Save(file);
            }
        }
    }

    public static void DelImg(){
        foreach (var file in Directory.GetFiles(Tools.sourceDir, "*.png")) // This loops on every .qua files from source folder
        {
            File.Delete(file);
        }
        foreach (var file in Directory.GetFiles(Tools.sourceDir, "*.jpg")) // This loops on every .qua files from source folder
        {
            File.Delete(file);
        }
        foreach (var file in Directory.GetFiles(Tools.sourceDir, "*.jpeg")) // This loops on every .qua files from source folder
        {
            File.Delete(file);
        }
    }



    public static void Export(){
        String outputDir = Path.Combine(Directory.GetCurrentDirectory(), "OUTPUT");
        String outputQP = Path.Combine(outputDir, $"{MapsetInfo.Name}.qp");
        if(!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        if(File.Exists(outputQP))
            File.Delete(outputQP);
        

        ZipFile.CreateFromDirectory(Tools.targetDir, outputQP);
        Directory.Delete(Tools.tempDir, true); // delete temp folder
        Debug.WriteLine($"Created {outputDir}");
    }


    // Util function to move .qp asset files to said directory
    // Credit: A6sT
    public static string CopyAsset(string? name, string source, string target){
        if (string.IsNullOrWhiteSpace(name))
            return "";

        var newName = $"imported{idx}_{Path.GetFileName(name)}";
        File.Copy(Path.Combine(source, name), Path.Combine(target, newName), true);
        return newName;
    }
}