using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System;
using System.Diagnostics;
using QuaverMapPackMaker.Helpers;

namespace QuaverMapPackMaker.Views;

public partial class MainWindow : Window
{
    public MainWindow(){
        InitializeComponent();
        //first status of text
        DRAGDROP.Text = "Drag and Drop the background image";
        INSTRUCTIONS.Text = "Drop ONE image file (PNG JPG JPEG)";
        //initialize name
        Tools.SetName(packName.Text);
        Tools.SetCreator(creator.Text);
    }


    //Function For button press
    private void Export(object? o, RoutedEventArgs e){
        
        //
        Merge.Export();

        //Delete temp folders and reset step to reset program.
        Tools.SetConfirmation(false);
        Tools.DeleteTemp();
        Tools.step = 0;
        Merge.qpStored = false;
        DRAGDROP.Text = "Drag and Drop the background image";
        INSTRUCTIONS.Text = "Drop ONE image file (PNG JPG JPEG)";
        DISCLAIMER.Text = "You must confirm name before draggin QP files";
    }

    private void Confirmed(object? o, RoutedEventArgs e){
        if(!Tools.GetConfirmation()){
            Tools.SetName(packName.Text);
            Tools.SetCreator(creator.Text);
            Tools.SetConfirmation(true);

            DISCLAIMER.Text = "You have Confirmed Name; You can no longer change";
        }
    }


    //Functino for when dragged
    private void OnDragOver(object? sender, DragEventArgs e){
        // Accept file drops only; reject everything else
        if(e.DataTransfer.Contains(DataFormat.File)){
            //we will go through each step (bg image, banner img then qps)

            if(Tools.step == 0){
                //check file type
                var files = e.DataTransfer.TryGetFiles();
                if(files?.Length == 1){
                    //correct extension
                    if(Tools.CheckIMG(files[0].Path.LocalPath)){
                        e.DragEffects = DragDropEffects.Copy;
                    }
                }
            } else if(Tools.step == 1){
                var files = e.DataTransfer.TryGetFiles();
                if(files?.Length == 1){
                    //correct extension
                    if(Tools.CheckIMG(files[0].Path.LocalPath)){
                        e.DragEffects = DragDropEffects.Copy;
                    }
                }
            } else{
                //QP FILES
                //Cases will be checked in another function, we only need to check if files are dropped.
                //(They could accidentally drop 9 qp then 1 xyz file)
                //We will check for that in the OnDrag Function / a helper function
                e.DragEffects = DragDropEffects.Copy;
            }
        } else{
            e.DragEffects = DragDropEffects.None;
        }
    }


    //Function for when dropped
    private void OnDrop(object? sender, DragEventArgs e){   
        //Go through the steps (bg, ban, qp)
        if(Tools.step == 0){
            //create temp folder
            Tools.CreateTemp();


            //get and store image
            var files = e.DataTransfer.TryGetFiles();
            var imgPath = files?[0].Path.LocalPath;
            Tools.StoreIMG(imgPath);


            //move on to the next step and change text
            Tools.step++;
            DRAGDROP.Text = "Drag and Drop the Banner image";
            INSTRUCTIONS.Text = "Drop ONE image file (PNG JPG JPEG)";
        } else if(Tools.step == 1){
            //get and store image
            var files = e.DataTransfer.TryGetFiles();
            var imgPath = files?[0].Path.LocalPath;
            Tools.StoreIMG(imgPath);


            //move on to the next step and change text
            Tools.step++;
            DRAGDROP.Text = "Drag and Drop the QPs";
            INSTRUCTIONS.Text = "Drop your QP files here";
            
        }   else{
            //QP FILES
            //we names are confirmed, check files that are dropped
            if(Tools.GetConfirmation()){
                if (e.DataTransfer.TryGetFiles() is { } files){
                    foreach (var file in files){
                        var qpPath = file.Path.LocalPath;
                        if(Tools.CheckQP(qpPath)){
                            try{
                                Merge.StoreFiles(qpPath);
                            } catch(Exception error){
                                Debug.WriteLine(error);
                            }
                        }
                    }
                }
            }
        }
    }
}