using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using VirusWar.Data.Cells;

namespace VirusWar.Engine.Graphics.Interfaces
{
    /// <summary>
    /// Генерация ячейки игрока с использованием картинок
    /// </summary>
    interface IRenderImage
    {
        /// <summary>
        /// Генерация ячейки игрока с использованием картинок
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="belong">Какому игроку принадлежит</param>
        /// <param name="status">Какого типа ячейка</param>
        /// <returns></returns>
        public abstract Image ImageGen(double x, double y, BelongEnum belong, StatusEnum status);

    }
}
