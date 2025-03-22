using System.Diagnostics;
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
using VirusWar.Data;
using VirusWar.Engine;

namespace VirusWar;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Reader Reader { get; set; }
    public MainWindow()
    {
        InitializeComponent();

        Reader = new Reader("Data/DTO\\");

        Render.RenderMap(Reader, Canvas);
    }
    private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {     
        (int x, int y) Position = ((int)(e.GetPosition(Canvas).X / 50) - 1, (int)(e.GetPosition(Canvas).Y / 50) - 1);

        if (WinChecker.Win(Reader))
        {
            MarkLogic.GetCoord(Reader, Position);
            Render.RenderMap(Reader, Canvas);
        }
        else MessageBox.Show("Сорян GG");


    }
}