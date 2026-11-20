using System;
using System.IO;
using Quaver.API.Maps;


namespace QuaverMapPackMaker.Helpers;

class Tools{
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
}