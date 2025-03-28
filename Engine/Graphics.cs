﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VirusWar.Data.Cells;
//using System.Drawing;


namespace VirusWar.Engine
{
    /// <summary>
    /// Графические отрисовщик
    /// </summary>
    internal class Graphics
    {
        private readonly Canvas _canvas;

        public Graphics(Canvas canvas)
        {
            _canvas = canvas;
        }

        /// <summary>
        /// Константа размера ячейки
        /// </summary>
        private const int SizeCell = 50;
        /// <summary>
        /// Отрисовка карты и объектов
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="Canvas"></param>
        public void RenderMap(MapStorage Storage)
        {
            _canvas.Children.Clear();

            for (int i = 0; i < Storage.Size.y; i++)
            {
                for (int j = 0; j < Storage.Size.x; j++)
                {
                    _canvas.Children.Add(MapGen((j + 1) * SizeCell, (i + 1) * SizeCell, Storage.Map[i,j].Type));
                    if (Storage.Map[i, j].Status == StatusEnum.Virus) _canvas.Children.Add(VirGen((j + 1) * SizeCell, (i + 1) * SizeCell, Storage.Map[i, j].Belong));
                    if (Storage.Map[i, j].Status == StatusEnum.Fort) _canvas.Children.Add(FortGen((j + 1) * SizeCell, (i + 1) * SizeCell, Storage.Map[i, j].Belong));

                }
            }
        }

        private Ellipse VirGen(double x, double y, BelongEnum belong)
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

        private Polyline MapGen(double x, double y, TypeEnum status) => new()
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
        private Polyline FortGen(double x, double y, BelongEnum belong)
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
