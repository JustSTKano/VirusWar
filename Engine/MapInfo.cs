using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
            for (int i = 1; i < Storage.Size.x-1; i++)
            {
                for (int j = 1; j < Storage.Size.y-1; j++)
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
        /// Переменная хода
        /// </summary>
        static int StepCount = 0;

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
        /// <param name="Reader"></param>
        /// <param name="coord"></param>
        /// <param name="Canvas"></param>
        internal static void ReWrite(MapStorage Reader, (int x, int y) coord, Canvas Canvas)
        {
            if (Reader.Map[coord.y, coord.x].Status == StatusEnum.Virus)
            {
                Reader.Map[coord.y, coord.x].Status = StatusEnum.Fort;
                Reader.Map[coord.y, coord.x].Belong = GetPlayerTurn();
            }
            else
            {
                Reader.Map[coord.y, coord.x].Status = StatusEnum.Virus;
                Reader.Map[coord.y, coord.x].Belong = GetPlayerTurn();
            }
            StepCount++;

            AllMapChecker(Reader, GetPlayerTurn());
            Graphics.RenderMap(Reader, Canvas);
            if (Reader.Mode == 2) PCLogic.PCtry(Reader, Canvas, GetPlayerTurn());
            if (Reader.Mode == 1) PCLogic.PCtry(Reader, Canvas, BelongEnum.SecondPlayer);


        }


    }
}
