using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VirusWar.Data.Cells;
using VirusWar.Data.DTO;

namespace VirusWar.Engine
{
    internal class GameLogic
    {
        private readonly MapStorage _storage;
        private readonly MapHandler _do;
        private readonly Graphics _graphics;

        public GameLogic(MapStorage storage, MapHandler handler, Graphics graphics)
        {
            _storage = storage;
            _do = handler;
            _graphics = graphics;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="Info"></param>
        /// <param name="Canvas"></param>
        public void FirstInit()
        {
            _do.AllMapChecker(_storage);
            _graphics.RenderMap(_storage);
           // Stepper(Storage,Info,Canvas, GetCordFromPC());  // А эта хуйня тоже не робит
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_storage"></param>
        /// <param name="_do"></param>
        /// <param name="Canvas"></param>
        /// <param name="coord"></param>
        public async void Stepper((int x, int y) coord)
        {
            BelongEnum player = BelongEnum.None;
            var tempcoord = coord;
            do
            { 
                switch (_storage.Mode)
                {
                    case 0: {break; } 
                    case 1: { player = BelongEnum.SecondPlayer; if (_do.GetPlayerTurn(_storage) == player) tempcoord = GetCordFromPC(); break; }
                    case 2: { player = _do.GetPlayerTurn(_storage); tempcoord = GetCordFromPC(); break; }
                    default: break;
                }
                //Step(Storage,Info,Canvas, tempcoord);  // ХУИТА БЛЯТЬ
                if (_do.IsCheckPossibleCoord(_storage, tempcoord))
                {
                    _do.ReWrite(_storage, tempcoord);
                    _storage.LastPlayerStep = _do.GetPlayerTurn(_storage);
                    _storage.StepCount++;
                    _do.AllMapChecker(_storage);
                    _graphics.RenderMap(_storage);
                    if (!(_storage.Mode == 0))
                    {
                        await Task.Run(() => Thread.Sleep(300));
                    }
                }
                if (_storage.Mode == 2)
                {
                    player = _do.GetPlayerTurn(_storage);
                }           
            } while (_storage.IsGameStatus && (_do.GetPlayerTurn(_storage) == player));
            if (!_storage.IsGameStatus)
            {
                _do.FindWinner(_storage);
            }
            
        }
        /// <summary>
        /// Эта хуита блять мозга ебет. НАхуй. Если блять юзается через метод, отрисовка не работает. Все блять разом вжух и нахуй. Если напрямую, то все ок. Пидр
        /// </summary>
        /// <param name="_storage"></param>
        /// <param name="_info"></param>
        /// <param name="Canvas"></param>
        /// <param name="coord"></param>
        public async void Step((int x, int y) coord)
        {
            if (_storage.IsGameStatus && _do.IsCheckPossibleCoord(_storage, coord))
            {
                _do.ReWrite(_storage, coord);
                _storage.LastPlayerStep = _do.GetPlayerTurn(_storage);
                _storage.StepCount++;
                _do.AllMapChecker(_storage);
                _graphics.RenderMap(_storage);
                if (!(_storage.Mode == 0))
                {
                    await Task.Run(() => Thread.Sleep(500));
                }
            }
        }
        /// <summary>
        /// Тупой рандом. Берем координаты для пк
        /// </summary>
        /// <returns></returns>
        public static (int x, int y) GetCordFromPC()
        {
            Random rnd = new Random();
            return (rnd.Next(0, 11), rnd.Next(0, 11));
        }




    }
}
