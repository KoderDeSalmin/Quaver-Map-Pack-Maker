using Avalonia.Controls;
using Avalonia.Interactivity;
using TestCode;

namespace QuaverMapPackMaker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ButtonClicked(object? o, RoutedEventArgs e){
        Test.MapCreateTest();
    }
}