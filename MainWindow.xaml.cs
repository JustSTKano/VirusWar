using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VirusWar.Data.Cells;
using VirusWar.Engine;

namespace VirusWar;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MapStorage Storage { get; set; }
    public MainWindow()
    {
        InitializeComponent();

        Storage = new MapStorage("Data/DTO\\");
        MapInfo.AllMapChecker(Storage, MapInfo.GetPlayerTurn());
        Graphics.RenderMap(Storage, Canvas);
        

    }
    private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {     
        (int x, int y) Position = ((int)(e.GetPosition(Canvas).X / 50) - 1, (int)(e.GetPosition(Canvas).Y / 50) - 1);
        Trace.WriteLine($"X = {Position.x}, Y = {Position.y}");
        MapInfo.AllMapChecker(Storage, MapInfo.GetPlayerTurn());

        if (MapInfo.GameStatus&& MapInfo.CheckPossibleCoord(Storage, Position)) MapInfo.ReWrite(Storage, Position, Canvas);

        if (!MapInfo.GameStatus) MessageBox.Show("Сорян GG");



    }
}