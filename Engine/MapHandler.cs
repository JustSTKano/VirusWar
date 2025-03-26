using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using VirusWar.Data.Cells;

namespace VirusWar.Engine
{
    /// <summary>
    /// Обработчик информации о карте
    /// </summary>
    internal class MapHandler
    {
        private readonly CoordinateChecker _coordinateChecker = new();
        /// <summary>
        /// Проверка Всех ячеек на возможность сделать ход
        /// </summary>
        /// <param name="Storage"></param>
        /// <returns></returns>
        /// 
        internal void AllMapChecker(MapStorage Storage)
        {
            Storage.CurrentPlayer = GetPlayerTurn(Storage);
            AllMapCleaner(Storage);
            Storage.IsGameStatus = false;
            for (int i = 1; i < Storage.Size.x - 1; i++)
            {
                for (int j = 1; j < Storage.Size.y - 1; j++)
                {
                    if (_coordinateChecker.CheckCoord(Storage, (i, j)))
                    {
                        Storage.Map[j, i].Type = TypeEnum.Possible;
                        Storage.IsGameStatus = true;
                    }
                }
            }
        }
        /// <summary>
        /// Очистка ячеек перед проверкой
        /// </summary>
        /// <param name="Storage"></param>
        internal void AllMapCleaner(MapStorage Storage)
        {
            for (int i = 1; i < Storage.Size.x - 1; i++)
            {
                for (int j = 1; j < Storage.Size.y - 1; j++)
                {
                    Storage.Map[j, i].Type = TypeEnum.Empty;
                }
            }
        }
        /// <summary>
        /// Сравнение входящих координат и ячеек в которые возможно сделать ход
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool IsCheckPossibleCoord(MapStorage Storage, (int x, int y) coord)
        {
            if (Storage.Map[coord.y, coord.x].Type == TypeEnum.Possible) return true;
            return false;
        }
        /// <summary>
        /// Получение информации об порядке хода (Какой игрок ходит)
        /// </summary>
        /// <returns></returns>
        internal BelongEnum GetPlayerTurn(MapStorage Storage)
        {
            if (((Storage.StepCount / 3) % 2) == 0) return BelongEnum.FirstPlayer;
            else return BelongEnum.SecondPlayer;
        }
        /// <summary>
        /// Перезапись ячейки после сделанного хода
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        internal void ReWrite(MapStorage Storage, (int x, int y) coord)
        {
            var (x, y) = coord;
            if (Storage.Map[y, x].Status == StatusEnum.Virus)
            {
                Storage.Map[y, x].Status = StatusEnum.Fort;
                Storage.Map[y, x].Belong = Storage.CurrentPlayer;
            }
            else
            {
                Storage.Map[y, x].Status = StatusEnum.Virus;
                Storage.Map[y, x].Belong = Storage.CurrentPlayer;
            }
        }
        /// <summary>
        /// Проверка победы
        /// </summary>
        internal void FindWinner(MapStorage Storage)
        {
            if (Storage.CurrentPlayer == Storage.LastPlayerStep)
            {
                switch (Storage.LastPlayerStep)   /// Ситуация 1. Ты не сделал 3 хода, ходов не осталось. Значит проиграл
                {
                    case BelongEnum.FirstPlayer: { MessageBox.Show("Сорян GG, Победил Синий, ты не сделал 3 хода"); break; }
                    case BelongEnum.SecondPlayer: { MessageBox.Show("Сорян GG, Победил Красный, ты не сделал 3 хода"); break; }
                    default: break;
                }
            }
            else
            {
                switch (Storage.LastPlayerStep) /// Ситуация 2. Ты сделал 3 хода, и следующему игроку некуда ходить, ты выиграл
                {
                    case BelongEnum.FirstPlayer: { MessageBox.Show("Сорян GG, Победил Красный, у Синего нет доступных ходов"); break; }
                    case BelongEnum.SecondPlayer: { MessageBox.Show("Сорян GG, Победил Синий, у Красного нет доступных ходов"); break; }
                    default: break;
                }
            }
        }
    }
}
