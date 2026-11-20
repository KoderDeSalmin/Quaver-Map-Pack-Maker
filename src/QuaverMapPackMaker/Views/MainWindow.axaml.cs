using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;

using System;
using System.Diagnostics;



namespace QuaverMapPackMaker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        String dragText = "Drag and Drop the background image";
        DRAGDROP.Text = dragText;
    }


    //Function For button press
    private void Export(object? o, RoutedEventArgs e){
    }




    //Functino for when dragged
    private void OnDragOver(object? sender, DragEventArgs e)
    {
        // Accept file drops only; reject everything else
        if(e.DragEffects.Equals(e.DataTransfer.Contains(DataFormat.File))){
            //DragDropEffects.Copy;
            Debug.WriteLine(e.DragEffects);
            Debug.WriteLine(e.DataTransfer.Contains(DataFormat.File));
        } else{
            //DragDropEffects.None;
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