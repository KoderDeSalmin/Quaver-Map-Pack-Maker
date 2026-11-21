using System;
using System.IO;
using System.Diagnostics;

namespace QuaverMapPackMaker.Helpers;

class Tools{
    public static String tempDir = "";
    public static String targetDir = "";
    public static String sourceDir = "";

    public static int step = 0;


    //Check file extension for images
    public static bool CheckIMG(String file){
        String ext = Path.GetExtension(file);
        switch(ext){
            case ".png":
            case ".jpg":
            case ".jpeg": return true;
        }
        return false;
    }
    //Check file extension for QPs
    public static bool CheckQP(String file){
        String ext = Path.GetExtension(file);
        if(ext.Equals(".qp"))
            return true;
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
        
        switch (step){
            case 0:
            MapsetInfo.Background = imageName;
            MapsetInfo.BackgroundPath = imagePath;
            File.Copy(Path.Combine(imagePath, imageName), Path.Combine(targetDir, $"BACKGROUNDIMAGE{Path.GetExtension(imagePath)}"), true);
            break;
            case 1:
            MapsetInfo.Banner = imageName;
            MapsetInfo.BannerPath = imagePath;
            File.Copy(Path.Combine(imagePath, imageName), Path.Combine(targetDir, $"BANNERIMAGE{Path.GetExtension(imagePath)}"), true);
            break;
        }
    }

    public static void SetCreator(String? creator){
        if (string.IsNullOrWhiteSpace(creator)){
            creator = "someone";
        }

        MapsetInfo.Creator = creator;
    }
    public static void SetName(String? name){
        if (string.IsNullOrWhiteSpace(name)){
            name = "someone";
        }

        MapsetInfo.Name = name;
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