using System;
using System.IO;
using System.Diagnostics;

using Quaver.API.Maps;

namespace TestCode;

class Test{
    public static void MapCreateTest(){
        String quaDir = "C:\\Users\\codys\\Downloads\\Code\\C#\\Project\\QuaverMapPackMaker\\src\\QuaverMapPackMaker\\map\\1780695854000.qua";


        var qua = Qua.Parse(quaDir);

        Debug.WriteLine($"Title: {qua.Title}");
        Debug.WriteLine($"Diffname: {qua.DifficultyName}");
        Debug.WriteLine($"Creator: {qua.Creator}");


        // qua.MapId = -1;
        // qua.DifficultyName = "TEST";

        // if (createBlankDifficulty)
        //     qua.HitObjects.Clear();

        // qua.Save(outputPath);


    }
}