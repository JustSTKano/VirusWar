
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VirusWar.Data.Cells;
using VirusWar.Engine.Graphics.Interfaces;

namespace VirusWar.Engine.Graphics
{
    internal class GraphicsImage(Canvas canvas, Window field) : BaseGraphics(canvas, field), IRenderImage
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
                    _canvas.Children.Add(ImageFieldGen((j + 1) * SizeCell, (i + 1) * SizeCell,storage.Map[i, j].Type));
                    if (storage.Map[i, j].Status != StatusEnum.None)
                    {
                        _canvas.Children.Add(ImageUnitGen((j + 1) * SizeCell, (i + 1) * SizeCell, storage.Map[i, j].Belong, storage.Map[i, j].Status));
                    }
                }
            }
        }
        public Image ImageFieldGen(double x, double y,TypeEnum type)
        {
            string fname = type switch
            {
                TypeEnum.Wall=> "Wall",
                TypeEnum.Empty => "Empty",
                TypeEnum.Possible => "Possyble",
                _ => throw new NotImplementedException()
            };
            Image unit = new Image()
            {
                Width = SizeCell,
                Height = SizeCell,

                Source = new BitmapImage(new Uri($"Engine/Graphics/Icons/{fname}.png", UriKind.Relative))
            };
            unit.SetValue(Canvas.LeftProperty, x);
            unit.SetValue(Canvas.TopProperty, y);

            return unit;
        }

        public Image ImageUnitGen(double x, double y, BelongEnum belong, StatusEnum status)
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
    }
}
