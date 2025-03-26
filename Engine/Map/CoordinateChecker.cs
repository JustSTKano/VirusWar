using System.Windows.Controls;
using System.Windows.Media.Animation;
using VirusWar.Data.Cells;

namespace VirusWar.Engine.Map
{

    /// <summary>
    /// Логика проверки ячейки на возможность сделать ход.
    /// </summary>
    internal static class CoordinateChecker
    {
        /// <summary>
        /// Передаточные координаты окрестности вокруг ячейки
        /// </summary>
        public static readonly (int x, int y)[] Local = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];
        public static BelongEnum _player;

        //BelongEnum CurrentPlayer = new();
        /// <summary>
        /// Основной первичный метод
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool CheckCoord(this MapStorage Storage, (int x, int y) coord, BelongEnum player)
        {
            _player = player;
            ClearChecker(Storage);
            if (
                Storage.Map[coord.y, coord.x].Type != TypeEnum.Wall &&
                Storage.Map[coord.y, coord.x].Status != StatusEnum.Fort &&
                Storage.Map[coord.y, coord.x].Belong != _player
                )
            {
                if (Storage.CheckVirusLocal(coord)) return true;
                if (Storage.CheckFort(coord)) return true;
            }
            return false;
        }
        /// <summary>
        /// Проверка окрестности вокруг текущей ячейки
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <param name="type"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool CheckVirusLocal(this MapStorage Storage, (int x, int y) coord)
        {     
            var check = false;
            foreach(var a in Local)
            {
                var (x, y) = (coord.x+a.x,coord.y+a.y);
                if (Storage.Map[y, x].Status == StatusEnum.Virus && Storage.Map[y, x].Belong == _player) check = true;
            }
            return check;
        }
        /// <summary>
        /// Рекурсивный метод для проверки цепочки из крепостей. 1
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public static bool CheckFort(this MapStorage Storage, (int x, int y) coord)
        {
            var check = false;
            Storage.Map[coord.y, coord.x].IsCheck = true;
            foreach (var a in Local)
            {
                var (x, y) = (coord.x + a.x, coord.y + a.y);
                if (Storage.LastChek((x, y))) check = true;
            }
            return check;
        }
        /// <summary>
        /// Рекурсивный метод для проверки цепочки из крепостей. 2
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <returns></returns>
        public static bool LastChek(this MapStorage Storage, (int x, int y) coord)
       {
            bool check = false;
            var  (x, y) = coord;
            if (Storage.Map[y, x].Status == StatusEnum.Fort &&
                Storage.Map[y, x].Belong == _player &&
                Storage.Map[y, x].IsCheck == false)
            {
                if (Storage.CheckVirusLocal((x, y)))
                {
                    return true;
                }
                else
                {
                    check = Storage.CheckFort((x, y));
                } 
            }
            return check;
        }
        /// <summary>
        /// Очистка карты после проверки крепостей
        /// </summary>
        /// <param name="Storage"></param>
        public static void ClearChecker(this MapStorage Storage)
        {
            for (int i = 0; i < Storage.Size.x; i++)
            {
                for (int j = 0; j < Storage.Size.y; j++)
                {
                    Storage.Map[i, j].IsCheck = false;
                }
            }
        }
    }
}
