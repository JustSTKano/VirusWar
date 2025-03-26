using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VirusWar.Data.Cells;
using VirusWar.Data.DTO;
using VirusWar.Engine.Map;

namespace VirusWar.Engine
{
    internal class GameLogic
    {

        private MapHandler _do;
        private readonly Graphics _graphics;
        private readonly MapStorage _storage;

        public GameLogic(MapStorage storage,  Graphics graphics)
        {
            _storage = storage;
            //_do = handler;
            _graphics = graphics;
        }
        /// <summary>
        /// Статус игры
        /// </summary>
        public bool IsGameStatus = false;
        /// <summary>
        /// Переменная хода
        /// </summary>
        public int StepCount = 0;
        /// <summary>
        /// Выбор режима игры(Ну бля пока так). 0 - игроки, 1-против компа, 2-комп против компа(тупо отладка)
        /// </summary>
        public int Mode = 2;
        /// <summary>
        /// Игрок, сделавший последний ход.
        /// </summary>
        public BelongEnum LastPlayerStep = BelongEnum.None;
        /// <summary>
        /// Текущий игрок
        /// </summary>
        public BelongEnum CurrentPlayer = BelongEnum.None;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="Info"></param>
        /// <param name="Canvas"></param>
        public void Init()
        {
            _do = new MapHandler(_storage, GetPlayerTurn());
            IsGameStatus = _do.AllMapChecker();
            _graphics.RenderMap(_storage);
            //Stepper(GetCordFromPC());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_do"></param>
        /// <param name="Canvas"></param>
        /// <param name="coord"></param>
        public async void Stepper((int x, int y) coord)
        {
            BelongEnum player = BelongEnum.None;
            var tempcoord = coord;
            do
            {
                switch (Mode)
                {
                    case 0: {break; } 
                    case 1: { player = BelongEnum.SecondPlayer; if (GetPlayerTurn() == player) tempcoord = GetCordFromPC(); break; }
                    case 2: { player = GetPlayerTurn(); tempcoord = GetCordFromPC(); break; }
                    default: break;
                }

                if (IsCheckPossibleCoord(tempcoord))
                {
                    await Step(tempcoord);
                   Init();
                }    
                    
                if (Mode == 2)
                {
                    player = GetPlayerTurn();
                }           
            } while (IsGameStatus && (GetPlayerTurn() == player));
            if (!IsGameStatus)
            {
                FindWinner();
            }            
        }
        /// <summary>
        /// Единичный шаг
        /// </summary>
        /// <param name="_storage"></param>
        /// <param name="_info"></param>
        /// <param name="Canvas"></param>
        /// <param name="coord"></param>
        public async Task Step((int x, int y) coord)
        {
            _do.ReWrite(coord);
            LastPlayerStep = GetPlayerTurn();
            StepCount++;
            if (!(Mode == 0))
            {
                await Task.Run(() => Thread.Sleep(300));
            }
            /*_do = new MapHandler(_storage, GetPlayerTurn());
            IsGameStatus = _do.AllMapChecker();
            _graphics.RenderMap(_storage);*/
        }
        /// <summary>
        /// Тупой рандом. Берем координаты для пк
        /// </summary>
        /// <returns></returns>
        /// 
        internal BelongEnum GetPlayerTurn()
        {
            if (StepCount / 3 % 2 == 0) return BelongEnum.FirstPlayer;
            else return BelongEnum.SecondPlayer;
        }

        public static (int x, int y) GetCordFromPC()
        {
            Random rnd = new Random();
            return (rnd.Next(0, 11), rnd.Next(0, 11));
        }

        /// <summary>
        /// Сравнение входящих координат и ячеек в которые возможно сделать ход
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool IsCheckPossibleCoord((int x, int y) coord)
        {
            if (_storage.Map[coord.y, coord.x].Type == TypeEnum.Possible) return true;
            return false;
        }

        internal void FindWinner()
        {
            if (GetPlayerTurn() == LastPlayerStep)
            {
                switch (LastPlayerStep)   /// Ситуация 1. Ты не сделал 3 хода, ходов не осталось. Значит проиграл
                {
                    case BelongEnum.FirstPlayer: { MessageBox.Show("Сорян GG, Победил Синий, ты не сделал 3 хода"); break; }
                    case BelongEnum.SecondPlayer: { MessageBox.Show("Сорян GG, Победил Красный, ты не сделал 3 хода"); break; }
                    default: break;
                }
            }
            else
            {
                switch (LastPlayerStep) /// Ситуация 2. Ты сделал 3 хода, и следующему игроку некуда ходить, ты выиграл
                {
                    case BelongEnum.FirstPlayer: { MessageBox.Show("Сорян GG, Победил Красный, у Синего нет доступных ходов"); break; }
                    case BelongEnum.SecondPlayer: { MessageBox.Show("Сорян GG, Победил Синий, у Красного нет доступных ходов"); break; }
                    default: break;
                }
            }
        }
    }
}
