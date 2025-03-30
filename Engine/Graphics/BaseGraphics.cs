using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VirusWar.Engine.Graphics.Interfaces;

namespace VirusWar.Engine.Graphics
{
    internal abstract class BaseGraphics : IRenderMap
    {
        internal readonly Canvas _canvas;
        internal readonly Window _field;

        internal const int SizeCell = 50;

        public BaseGraphics (Canvas canvas, Window field)
        {
            _canvas = canvas;
            _field = field;

        }
        public abstract void RenderMap(MapStorage storage);

    }
}
