using System.Windows.Controls;
using System.Windows.Media.Animation;
using VirusWar.Data.Cells;

namespace VirusWar.Engine
{

    /// <summary>
    /// Логика проверки ячейки на возможность сделать ход.
    /// </summary>
    class CoordinateChecker
    {

        //BelongEnum CurrentPlayer = new();
        /// <summary>
        /// Основной первичный метод
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <param name="Player"></param>
        /// <returns></returns>
        public bool CheckCoord(MapStorage Storage, (int x, int y) coord)
        {
            ClearChecker(Storage);
            if (
                (Storage.Map[coord.y, coord.x].Type != TypeEnum.Wall) &&
                (Storage.Map[coord.y, coord.x].Status != StatusEnum.Fort) &&
                (Storage.Map[coord.y, coord.x].Belong != Storage.CurrentPlayer)
                )
            {
                if (CheckVirusLocal(Storage, coord)) return true;
                if (CheckFort(Storage, coord)) return true;
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
        public bool CheckVirusLocal(MapStorage Storage, (int x, int y) coord)
        {     
            var check = false;
            foreach(var a in Storage.Local)
            {
                var (x, y) = (coord.x+a.x,coord.y+a.y);
                if ((Storage.Map[y, x].Status == StatusEnum.Virus) && (Storage.Map[y, x].Belong == Storage.CurrentPlayer)) check = true;
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
        public bool CheckFort(MapStorage Storage, (int x, int y) coord)
        {
            var check = false;
            Storage.Map[coord.y, coord.x].IsCheck = true;
            foreach (var a in Storage.Local)
            {
                var (x, y) = (coord.x + a.x, coord.y + a.y);
                if (LastChek(Storage, (x, y))) check = true;
            }
            return check;
        }
        /// <summary>
        /// Рекурсивный метод для проверки цепочки из крепостей. 2
        /// </summary>
        /// <param name="Storage"></param>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool LastChek(MapStorage Storage, (int x, int y) coord)
       {
            bool check = false;
            var  (x, y) = coord;
            if ((Storage.Map[y, x].Status == StatusEnum.Fort) &&
                (Storage.Map[y, x].Belong == Storage.CurrentPlayer) &&
                (Storage.Map[y, x].IsCheck == false))
            {
                if (CheckVirusLocal(Storage, (x, y)))
                {
                    return true;
                }
                else
                {
                    check = CheckFort(Storage, (x, y));
                } 
            }
            return check;
        }
        /// <summary>
        /// Очистка карты после проверки крепостей
        /// </summary>
        /// <param name="Storage"></param>
        public void ClearChecker(MapStorage Storage)
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
