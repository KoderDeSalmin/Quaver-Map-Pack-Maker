using System;
using System.IO;
using System.Diagnostics;
using QuaverMapPackMaker.Info;
using Quaver.API.Maps;


namespace QuaverMapPackMaker.Helpers;

class Tools{
    private static String tempDir = "";
    private static String targetDir = "";
    private static String sourceDir = "";

    public static int step = 0;



    //check file extension for images
    public static bool CheckIMG(String file){
        String ext = Path.GetExtension(file);
        switch(ext){
            case ".png":
            case ".jpg":
            case ".jpeg": return true;
        }
        return false;
    }

    
    
    //Store Image
    public static void StoreIMG(String? imagePath){
        String? imageName = Path.GetFileName(imagePath);
        if (string.IsNullOrWhiteSpace(imageName))
            return;



        imagePath = Path.GetDirectoryName(imagePath);
        if (string.IsNullOrWhiteSpace(imagePath))
            return;

        MapsetInfo.Image = imageName;
        MapsetInfo.ImagePath = imagePath;
        File.Copy(Path.Combine(imagePath, imageName), Path.Combine(targetDir, imageName), true);
    }




    //Create temporary folder
    public static void CreateTemp(){
        tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        targetDir = Path.Combine(tempDir, "target");
        sourceDir = Path.Combine(tempDir, "source");


        Directory.CreateDirectory(targetDir);
        Directory.CreateDirectory(sourceDir);

        Debug.WriteLine(tempDir);
    }
    //Delete the temp folder
    public static void DeleteTemp(){
        // delete temp folder
        Directory.Delete(tempDir, true); 
    }
}