
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VirusWar.Data.Cells;
using VirusWar.Engine.Graphics.Interfaces;

namespace VirusWar.Engine.Graphics
{
    internal class GraphicsImage(Canvas canvas, Window field) : BaseGraphics(canvas, field), IRenderCell, IRenderImage
    {
        public override void RenderMap(MapStorage storage)
        {
            _canvas.Children.Clear();

            _field.Height = (SizeCell * (storage.Size.y + 3));
            _field.Width = (SizeCell * (storage.Size.x + 3));

            for (int i = 0; i < storage.Size.y; i++)
            {
                for (int j = 0; j < storage.Size.x; j++)
                {
                    _canvas.Children.Add(FieldUnitGen((j + 1) * SizeCell, (i + 1) * SizeCell, storage.Map[i, j].Type));
                    if (storage.Map[i, j].Status == StatusEnum.Virus) _canvas.Children.Add(ImageGen((j + 1) * SizeCell, (i + 1) * SizeCell, storage.Map[i, j].Belong, storage.Map[i, j].Status));
                    if (storage.Map[i, j].Status == StatusEnum.Fort) _canvas.Children.Add(ImageGen((j + 1) * SizeCell, (i + 1) * SizeCell, storage.Map[i, j].Belong, storage.Map[i, j].Status));

                }
            }
        }
        public Polyline FieldUnitGen(double x, double y, TypeEnum status) => new()
        {
            Stroke = Brushes.Black,
            Fill = status switch
            {
                TypeEnum.Empty => Brushes.White,
                TypeEnum.Wall => Brushes.Black,
                TypeEnum.Possible => Brushes.Gray,
                _ => throw new NotImplementedException()
            },
            Points = new PointCollection()
            {
                new Point(x, y),
                new Point(x, y + SizeCell),
                new Point(x + SizeCell, y + SizeCell),
                new Point(x + SizeCell, y),
                new Point(x, y)
            }
        };
        public Image ImageGen(double x, double y, BelongEnum belong, StatusEnum status)
        {
            string fname = status switch
            {
                StatusEnum.Fort => "Fort",
                StatusEnum.Virus => "Virus",
                _ => throw new NotImplementedException()
            };
            string sname = belong switch
            {
                BelongEnum.FirstPlayer => "Red",
                BelongEnum.SecondPlayer => "Green",
                _ => throw new NotImplementedException()
            };
            Image unit = new Image()
            {
                Width = SizeCell,
                Height = SizeCell,

                Source = new BitmapImage(new Uri($"Engine/Graphics/Icons/{fname+sname}.png", UriKind.Relative))
            };
            unit.SetValue(Canvas.LeftProperty, x);
            unit.SetValue(Canvas.TopProperty, y);

            return unit;
        }



        public Polyline FortGen(double x, double y, BelongEnum belong)
        {
            throw new NotImplementedException();
        }

        public Ellipse VirGen(double x, double y, BelongEnum belong)
        {
            throw new NotImplementedException();
        }

    }
}
