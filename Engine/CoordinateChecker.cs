using System.Windows.Media.Animation;
using VirusWar.Data.Cells;

namespace VirusWar.Engine
{

    /// <summary>
    /// Логика проверки ячейки на возможность сделать ход.
    /// </summary>
    class CoordinateChecker
    {
        public static bool CheckCoord(MapStorage Storage, (int x, int y) coord, BelongEnum Player)
        {
            ClearChecker(Storage);
            if ((Storage.Map[coord.y, coord.x].Type != TypeEnum.Wall)&&(Storage.Map[coord.y, coord.x].Status != StatusEnum.Fort)&&(Storage.Map[coord.y, coord.x].Belong != Player))
            {
                if (CheckLocal(Storage, coord, StatusEnum.Virus, Player)) return true;
                if (CheckFort(Storage, coord, Player)) return true;
            }
            return false;
        }
        public static bool CheckLocal(MapStorage Storage, (int x, int y) coord, StatusEnum type, BelongEnum Player)
        {
            
            var check = false;
            foreach(var a in Storage.Local)
            {
                var (x, y) = (coord.x+a.x,coord.y+a.y);
                if ((Storage.Map[y, x].Status == type) && (Storage.Map[y, x].Belong == Player)) check = true;
            }
            return check;
        }
        public static bool CheckFort(MapStorage Storage, (int x, int y) coord, BelongEnum Player)
        {
            var check = false;
            Storage.Map[coord.y, coord.x].Checker = true;
            foreach (var a in Storage.Local)
            {
                var (x, y) = (coord.x + a.x, coord.y + a.y);
                if (LastChek(Storage, (x, y), Player)) check = true;
            }
            return check;
        }
       public static bool LastChek(MapStorage Storage, (int x, int y) coord, BelongEnum Player)
       {
           // Trace.WriteLine($"X = {coord.x}, Y = {coord.y}");
            bool check = false;
            var  (x, y) = coord;
            if ((Storage.Map[y, x].Status == StatusEnum.Fort) && (Storage.Map[y, x].Belong == Player) && (Storage.Map[y, x].Checker == false))
            {
               // Trace.WriteLine("Try");
                if (CheckLocal(Storage, (x, y), StatusEnum.Virus, Player)) return true;

                else check = CheckFort(Storage, (x, y), Player);
               // Trace.WriteLine("No");
            }
            return check;
        }

        public static void ClearChecker(MapStorage Storage)
        {
            for (int i = 0; i < Storage.Size.x; i++)
            {
                for (int j = 0; j < Storage.Size.y; j++)
                {
                    Storage.Map[i, j].Checker = false;
                }
            }
        }
    }
}
