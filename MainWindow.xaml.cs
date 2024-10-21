using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Converters;
using GenshinImpactMain.Command;
using GenshinImpactMain.ViewModel;
using Microsoft.Xaml.Behaviors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenshinImpactMain;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

}