using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using VirusWar.Engine;
using VirusWar.Engine.Map;

namespace VirusWar;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private GameLogic Game { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        Game = new GameLogic(
            new MapStorage("Data/DTO\\"),
            new Graphics(Canvas)
        );
        Game.UpdateState();
        Game.Stepper((0, 0));
    }
    private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {     
        (int x, int y) Position = ((int)(e.GetPosition(Canvas).X / 50) - 1, (int)(e.GetPosition(Canvas).Y / 50) - 1);
        Trace.WriteLine($"X = {Position.x}, Y = {Position.y}");
        Game.Stepper(Position);
    }
}