using System;
using System.IO;
using Force.DeepCloner;
using System.Diagnostics;

using Quaver.API.Maps;

namespace TestCode;

class Test{
    public static void MapCreateTest(){
        String quaDir = "C:\\Users\\koder\\Downloads\\Code\\C#\\Project\\QuaverMapPackMaker\\src\\QuaverMapPackMaker\\map\\1793049676000.qua";
        String outputDir = "C:\\Users\\koder\\Downloads\\Code\\C#\\Project\\QuaverMapPackMaker\\src\\QuaverMapPackMaker\\map";

        try{
            var qua = Qua.Parse(quaDir);


            var newQUA = qua.DeepClone();
            
            newQUA.Title = "Test";
            newQUA.DifficultyName = "Test Diff";
            newQUA.Creator = "KoderDeSalmin";

            //newQUA.Save(outputDir);


            Debug.WriteLine($"Title: {qua.Title}");
            Debug.WriteLine($"Diffname: {qua.DifficultyName}");
            Debug.WriteLine($"Creator: {qua.Creator}");
            
            Debug.WriteLine("-------");

            Debug.WriteLine($"Title: {newQUA.Title}");
            Debug.WriteLine($"Diffname: {newQUA.DifficultyName}");
            Debug.WriteLine($"Creator: {newQUA.Creator}");
            
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