using Microsoft.VisualBasic;
using System;
using System.Windows;
using System.Windows.Controls;
using VirusWar.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using VirusWar.Data.Cells;
using System.Diagnostics;
//using System.Drawing;


namespace VirusWar.Engine
{
    internal static class Render
    {
        /// <summary>
        /// Константа размера ячейки
        /// </summary>
        private const int SizeCell = 50;


        /// <summary>
        /// Отрисовка карты и объектов
        /// </summary>
        /// <param name="Reader"></param>
        /// <param name="Canvas"></param>
        public static void RenderMap(Reader Reader, Canvas Canvas)
        {
            Canvas.Children.Clear();

            for (int i = 0; i < Reader.Size.y; i++)
            {
                for (int j = 0; j < Reader.Size.x; j++)
                {
                    Canvas.Children.Add(SquareGen((j + 1) * SizeCell, (i + 1) * SizeCell, Reader.Map[i,j].Status));
                    if (Reader.Map[i, j].Status == StatusEnum.Virus)
                    {
                        Canvas.Children.Add(VirGen((j + 1) * SizeCell, (i + 1) * SizeCell, Reader.Map[i, j].Status, Reader.Map[i, j].Belong));
                    }
                    if (Reader.Map[i, j].Status == StatusEnum.Fort)
                    {
                        Canvas.Children.Add(FortGen((j + 1) * SizeCell, (i + 1) * SizeCell, Reader.Map[i, j].Status, Reader.Map[i, j].Belong));
                    }
                }
            }
        }

        private static Ellipse VirGen(double x, double y, StatusEnum status, BelongEnum belong)
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

        private static Polyline SquareGen(double x, double y, StatusEnum status) => new()
        {
            Stroke = Brushes.Black,
            Fill = status switch
            {
                StatusEnum.Empty => Brushes.White,
                StatusEnum.Wall => Brushes.Black,
                StatusEnum.Virus => Brushes.White,
                StatusEnum.Fort => Brushes.White,
                StatusEnum.Posible => Brushes.White,
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
        private static Polyline FortGen(double x, double y, StatusEnum status, BelongEnum belong)
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
                    new Point(x+(SizeCell/2), y),
                    new Point(x+ SizeCell, y + (SizeCell/2)),
                    new Point(x+(SizeCell/2), y + SizeCell),
                    new Point(x, y +(SizeCell/2)),
                    new Point(x+(SizeCell/2), y)
                };
            return Virus;
        }
        

    }
}
