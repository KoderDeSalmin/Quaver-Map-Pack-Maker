using System;
using System.IO;
using System.IO.Compression;
using Quaver.API.Maps;

/*
a6st code snippet to merge files into a mappack (it works)

Take 1 qp add it to target


every other qp goes in source
*/

namespace TestCode;

class Test{
    public static void MergeMapset(string[] args)
    {       
        
        // Your parameters
        var targetQp = args[0]; // path to Reference .qp file
        var sourceQp = args[1]; // path to .qp file to merge
        var outputQp = args[2]; // path to where we create the merged .qp file
        var mapsetName = args[3]; // if you want to give a custom name to your mapset
        var creator = args[4]; // if you want to set custom creator name

        // Create temp directory to prevent file conflict
        var temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var target = Path.Combine(temp, "target");
        var source = Path.Combine(temp, "source");

        Directory.CreateDirectory(target);
        Directory.CreateDirectory(source);

        // Extract .qp files to these directory
        ZipFile.ExtractToDirectory(targetQp, target);
        ZipFile.ExtractToDirectory(sourceQp, source);

        // Extract all assets from merging .qp file
        var index = 0;
        foreach (var file in Directory.GetFiles(source, "*.qua")) // This loops on every .qua files from source folder
        {
            var qua = Qua.Parse(file);
            qua.AudioFile = CopyAsset(qua.AudioFile, source, target);
            qua.BackgroundFile = CopyAsset(qua.BackgroundFile, source, target);
            qua.BannerFile = CopyAsset(qua.BannerFile, source, target);

            // Saved inside the temporary Reference folder
            qua.Save(Path.Combine(target, $"imported-{index++}.qua"));
        }

        // Convert all difficulties from Reference folder into a single mapset
        foreach (var file in Directory.GetFiles(target, "*.qua"))
        {
            var qua = Qua.Parse(file);
            var song = string.IsNullOrWhiteSpace(qua.Artist) ? qua.Title : $"{qua.Artist} - {qua.Title}";
            var difficulty = string.IsNullOrWhiteSpace(qua.DifficultyName) ? "Difficulty" : qua.DifficultyName;

            qua.MapId = -1;
            qua.MapSetId = -1;
            qua.DifficultyName = $"{song} [{difficulty}] [Map Pack]"; // We use a custom difficulty name to prevent duplication detection from quaver if one of the difficulty already exists in the user's game
            qua.Artist = "Various Artists";
            qua.Title = mapsetName;
            qua.Creator = creator;

            qua.Save(file);
        }

        // We convert that file into a zip folder, renamed into a .qp file at output path (if named so in arg, you could use any file extension you want in theory)
        ZipFile.CreateFromDirectory(target, outputQp);
        Directory.Delete(temp, true); // delete temp folder
        Console.WriteLine($"Created {outputQp}");
    }

    // Util function to move .qp asset files to said directory
    static string CopyAsset(string? name, string source, string target){
        if (string.IsNullOrWhiteSpace(name))
            return "";

        var newName = $"imported_{Path.GetFileName(name)}";
        File.Copy(Path.Combine(source, name), Path.Combine(target, newName), true);
        return newName;
    }
}