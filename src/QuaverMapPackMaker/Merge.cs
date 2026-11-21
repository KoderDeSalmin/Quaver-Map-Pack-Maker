using System;
using System.IO;
using System.Diagnostics;
using Quaver.API.Maps;


namespace QuaverMapPackMaker.Helpers;

class Merge{




    // Util function to move .qp asset files to said directory
    // Credit: A6sT
    static string CopyAsset(string? name, string source, string target){
        if (string.IsNullOrWhiteSpace(name))
            return "";

        var newName = $"imported_{Path.GetFileName(name)}";
        File.Copy(Path.Combine(source, name), Path.Combine(target, newName), true);
        return newName;
    }
}