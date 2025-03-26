using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using VirusWar.Data.Cells;

namespace VirusWar.Engine.Map
{
    /// <summary>
    /// Обработчик информации о карте
    /// </summary>
    internal class MapHandler
    {
        //private readonly CoordinateChecker _coordinateChecker = new();
        private readonly MapStorage _storage;
        private readonly BelongEnum _player;

        public MapHandler(MapStorage storage, BelongEnum player)
        {
            _storage = storage;
            _player = player;
        }


       /* public MapCell[,] Map => _storage.Map;
        public (int x, int y) Size => _storage.Size;*/

        /// <summary>
        /// Проверка Всех ячеек на возможность сделать ход
        /// </summary>
        /// <param name="_storage"></param>
        /// <returns></returns>
        /// 
        internal bool AllMapChecker()
        {
           AllMapCleaner();
           var check = false;
            for (int i = 1; i < _storage.Size.x - 1; i++)
            {
                for (int j = 1; j < _storage.Size.y - 1; j++)
                {
                    if (_storage.CheckCoord((i, j), _player))
                    {
                        _storage.Map[j, i].Type = TypeEnum.Possible;
                        check = true;
                        
                    }
                }
            }
            return check;
        }
        /// <summary>
        /// Очистка ячеек перед проверкой
        /// </summary>
        /// <param name="Storage"></param>
        internal void AllMapCleaner()
        {
            for (int i = 1; i < _storage.Size.x - 1; i++)
            {
                for (int j = 1; j < _storage.Size.y - 1; j++)
                {
                    _storage.Map[j, i].Type = TypeEnum.Empty;
                }
            }
        }
        
        /// <summary>
        /// Получение информации об порядке хода (Какой игрок ходит)
        /// </summary>
        /// <returns></returns>
        
        /// <summary>
        /// Перезапись ячейки после сделанного хода
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        internal void ReWrite((int x, int y) coord)
        {
            var (x, y) = coord;
            if (_storage.Map[y, x].Status == StatusEnum.Virus)
            {
                _storage.Map[y, x].Status = StatusEnum.Fort;
                _storage.Map[y, x].Belong = _player;
            }
            else
            {
                _storage.Map[y, x].Status = StatusEnum.Virus;
                _storage.Map[y, x].Belong = _player;
            }
        }
        /// <summary>
        /// Проверка победы
        /// </summary>
        
    }
}
