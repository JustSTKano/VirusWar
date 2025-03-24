using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using VirusWar.Data.Cells;

namespace VirusWar.Engine
{
    /// <summary>
    /// Обработчик информации о карте
    /// </summary>
    internal class MapInfo
    {
        public static bool GameStatus = false;
        /// <summary>
        /// Переменная хода
        /// </summary>
        static int StepCount = 0;
        /// <summary>
        /// Выбор режима игры(Ну бля пока так). 0 - игроки, 1-против компа, 2-комп против компа(тупо отладка)
        /// </summary>
        public static int Mode = 2;
        /// <summary>
        /// Игрок, сделавший последний ход.
        /// </summary>
        public static BelongEnum LastPlayerStep = BelongEnum.None;

        /// <summary>
        /// Проверка Всех ячеек на возможность сделать ход
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        /// 
        internal static void AllMapChecker(MapStorage Storage, BelongEnum player)
        {
            AllMapCleaner(Storage);
            GameStatus = false;
            for (int i = 1; i < Storage.Size.x - 1; i++)
            {
                for (int j = 1; j < Storage.Size.y - 1; j++)
                {
                    if (CoordinateChecker.CheckCoord(Storage, (i, j), player))
                    {
                        Storage.Map[j, i].Type = TypeEnum.Possible;
                        GameStatus = true;
                    }

                }
            }
        }
        /// <summary>
        /// Очистка ячеек перед проверкой
        /// </summary>
        /// <param name="Reader"></param>
        internal static void AllMapCleaner(MapStorage Reader)
        {
            for (int i = 1; i < Reader.Size.x - 1; i++)
            {
                for (int j = 1; j < Reader.Size.y - 1; j++)
                {
                    Reader.Map[j, i].Type = TypeEnum.Empty;
                }
            }
        }
        /// <summary>
        /// Сравнение входящих координат и ячеек в которые возможно сделать ход
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <returns></returns>
        public static bool CheckPossibleCoord(MapStorage Storage, (int x, int y) coord)
        {
            if (Storage.Map[coord.y, coord.x].Type == TypeEnum.Possible) return true;
            return false;
        }



        /// <summary>
        /// Получение информации об порядке хода (Какой игрок ходит)
        /// </summary>
        /// <returns></returns>
        internal static BelongEnum GetPlayerTurn()
        {
            if (((StepCount / 3) % 2) == 0) return BelongEnum.FirstPlayer;
            else return BelongEnum.SecondPlayer;
        }


        /// <summary>
        /// Перезапись ячейки после сделанного хода
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <param name="Canvas"></param>
        internal static void ReWrite(MapStorage Storage, (int x, int y) coord, Canvas Canvas)
        {
            if (Storage.Map[coord.y, coord.x].Status == StatusEnum.Virus)
            {
                Storage.Map[coord.y, coord.x].Status = StatusEnum.Fort;
                Storage.Map[coord.y, coord.x].Belong = GetPlayerTurn();
            }
            else
            {
                Storage.Map[coord.y, coord.x].Status = StatusEnum.Virus;
                Storage.Map[coord.y, coord.x].Belong = GetPlayerTurn();
            }
            LastPlayerStep = GetPlayerTurn();
            StepCount++;

            AllMapChecker(Storage, GetPlayerTurn());
            Graphics.RenderMap(Storage, Canvas);

            if(!GameStatus) Winner();

            PCLogic.PCtry(Storage, Canvas);
            

        }

        /// <summary>
        /// Проверка победы
        /// </summary>
        internal static void Winner()
        {
            var playertemp = GetPlayerTurn();

            if (playertemp == LastPlayerStep)
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
           // MessageBox.Show("ы");

        }
    }
}
