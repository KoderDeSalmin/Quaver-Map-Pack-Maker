using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using System;
using System.Diagnostics;
using QuaverMapPackMaker.Helpers;



namespace QuaverMapPackMaker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //first status of text
        String dragText = "Drag and Drop the background image";
        String instructions = "Drop ONE image file (PNG JPG JPEG)";
        DRAGDROP.Text = dragText;
        INSTRUCTIONS.Text = instructions;
    }


    //Function For button press
    private void Export(object? o, RoutedEventArgs e){
    }




    //Functino for when dragged
    private void OnDragOver(object? sender, DragEventArgs e)
    {
        // Accept file drops only; reject everything else
        if(e.DataTransfer.Contains(DataFormat.File)){
            //e.DragEffects = DragDropEffects.Copy;

            //check file type
            var files = e.DataTransfer.TryGetFiles();
            if(files?.Length == 1){
                //correct extension
                if(Tools.CheckIMG(files[0].Path.LocalPath)){
                    e.DragEffects = DragDropEffects.Copy;
                    Debug.WriteLine("YES");
                }
            }

        } else{
            e.DragEffects = DragDropEffects.None;
        }
    }


    //Function for when dropped
    private void OnDrop(object? sender, DragEventArgs e)
    {
        if (e.DataTransfer.TryGetFiles() is { } files)
        {
            foreach (var file in files)
            {
                var path = file.Path.LocalPath;
                //Debug.WriteLine(path);
            }
        }
    }
}