using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using VirusWar.Data.Cells;

namespace VirusWar.Engine.Graphics.Interfaces
{
    /// <summary>
    /// Генерация отдельной ячейки
    /// </summary>
    internal interface IRenderCell
    {
        /// <summary>
        /// Генерация ячейки форта
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="belong">Какому игроку принадлежит</param>
        /// <returns></returns>
        public abstract Polyline FortGen(double x, double y, BelongEnum belong);
        /// <summary>
        /// Генерация Ячейки фона
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="status">Какого типа ячейка</param>
        /// <returns></returns>
        public abstract Polyline FieldUnitGen(double x, double y, TypeEnum status);
        /// <summary>
        /// Генерация ячейки вируса
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="belong">принадлежит</param>
        /// <returns></returns>
        public abstract Ellipse VirGen(double x, double y, BelongEnum belong);
    }
}
