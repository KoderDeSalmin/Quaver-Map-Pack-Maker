using Avalonia.Controls;
using Avalonia.Interactivity;
using TestCode;
using System;
namespace QuaverMapPackMaker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ButtonClicked(object? o, RoutedEventArgs e){
        String[] args = new String[5];

        args[0] = "C:\\Users\\codys\\Downloads\\Code\\C#\\Project\\QuaverMapPackMaker\\src\\QuaverMapPackMaker\\map\\Iyowa feat. RIME - Leave you on The Back of the Earth - 25671.qp";
        args[1] = "C:\\Users\\codys\\Downloads\\Code\\C#\\Project\\QuaverMapPackMaker\\src\\QuaverMapPackMaker\\map\\DUSTCELL - STIGMA - 10225.qp";
        args[2] = "C:\\Users\\codys\\Downloads\\Code\\C#\\Project\\QuaverMapPackMaker\\src\\QuaverMapPackMaker\\output";
        args[3] = "Koder Pack";
        args[4] = "KoerDeSalmin";

        Test.MergeMapset(args);
    }
}