using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWar.Engine.Graphics.Interfaces
{
    /// <summary>
    /// Финальная генерация карты 
    /// </summary>
    internal interface IRenderMap
    {
        /// <summary>
        /// Финальная генерация карты 
        /// </summary>
        /// <param name="Storage">Ссылка на хранилизе ячеек</param>
        public abstract void RenderMap(MapStorage Storage);
    }
}
