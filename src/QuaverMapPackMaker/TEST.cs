using System;
using System.IO;
using System.Diagnostics;

using Quaver.API.Maps;

namespace TestCode;

class Test{
    public static void MapCreateTest(){
        String quaDir = "C:\\Users\\codys\\Downloads\\Code\\C#\\Project\\QuaverMapPackMaker\\src\\QuaverMapPackMaker\\map\\temp.txt";
        String outputDir = "C:\\Users\\codys\\Downloads\\Code\\C#\\Project\\QuaverMapPackMaker\\src\\QuaverMapPackMaker\\map";

        try{
            var qua = Qua.Parse(quaDir);

            Debug.WriteLine($"Title: {qua.Title}");
            Debug.WriteLine($"Diffname: {qua.DifficultyName}");
            Debug.WriteLine($"Creator: {qua.Creator}");

        } catch (Exception e){
            Debug.WriteLine(e);
        }

        // qua.MapId = -1;
        // qua.DifficultyName = "TEST";

        // if (createBlankDifficulty)
        //      qua.HitObjects.Clear();

        // qua.Save(outputDir);


    }
}