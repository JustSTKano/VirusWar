using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using VirusWar.Data.Cells;
using VirusWar.Engine.Graphics.Interfaces;

namespace VirusWar.Engine.Graphics
{
    internal class Graphics3D(Canvas canvas, Window field) : BaseGraphics(canvas, field), IRenderCell
    {
        public override void RenderMap(MapStorage storage)
        {
            _canvas.Children.Clear();

            _field.Height = (SizeCell * (storage.Size.y + 3));
            _field.Width = (SizeCell * (storage.Size.x + 3));

            for (int i = storage.Size.y-1; i>=0 ; i--)
            {
                for (int j = 0; j < storage.Size.x; j++)
                {
                    _canvas.Children.Add(FieldUnitGen((j + 1) * SizeCell, (i + 1) * SizeCell, storage.Map[i, j].Type));
                    if (storage.Map[i, j].Status == StatusEnum.Virus) _canvas.Children.Add(VirGen((j + 1) * SizeCell, (i + 1) * SizeCell, storage.Map[i, j].Belong));
                    if (storage.Map[i, j].Status == StatusEnum.Fort) _canvas.Children.Add(FortGen((j + 1) * SizeCell, (i + 1) * SizeCell, storage.Map[i, j].Belong));

                }
            }
        }

        public Polyline FieldUnitGen(double x, double y, TypeEnum status) => new()
        {
            Stroke = status switch
            {
                TypeEnum.Empty => Brushes.Black,
                TypeEnum.Wall => Brushes.White,
                TypeEnum.Possible => Brushes.Black,
                _ => throw new NotImplementedException()
            },
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
                new Point(x, y),
                new Point(x+SizeCell/2, y - SizeCell/2),
                new Point(x + SizeCell+SizeCell/2, y  - SizeCell/2),
                new Point(x + SizeCell, y),
                new Point(x+SizeCell, y),
                new Point(x+SizeCell+SizeCell/2, y - SizeCell/2),
                new Point(x + SizeCell+SizeCell/2, y + SizeCell/2),
                new Point(x + SizeCell, y + SizeCell),
                new Point(x + SizeCell, y)
            }
        };

        public Polyline FortGen(double x, double y, BelongEnum belong)
        {
            Polyline Virus = new Polyline();
            Virus.Stroke = Brushes.Black;
            Virus.Fill = belong switch
            {
                BelongEnum.FirstPlayer => Brushes.Red,
                BelongEnum.SecondPlayer => Brushes.Blue,
                _ => throw new NotImplementedException()
            };
            Virus.Points = new PointCollection()
                {
                    new Point(x+SizeCell/2, y),
                    new Point(x+ SizeCell, y + SizeCell/2),
                    new Point(x+SizeCell/2, y + SizeCell),
                    new Point(x, y +SizeCell/2),
                    new Point(x+SizeCell/2, y)
                };
            return Virus;
        }

        public Ellipse VirGen(double x, double y, BelongEnum belong)
        {
            Ellipse vir = new Ellipse()
            {
                Width = SizeCell,
                Height = SizeCell,
                Stroke = Brushes.Black,
                Fill = belong switch
                {
                    BelongEnum.FirstPlayer => Brushes.Red,
                    BelongEnum.SecondPlayer => Brushes.Blue,
                    _ => throw new NotImplementedException()
                },

            };
            vir.SetValue(Canvas.LeftProperty, x);
            vir.SetValue(Canvas.TopProperty, y);
            return vir;
        }
    }
}
