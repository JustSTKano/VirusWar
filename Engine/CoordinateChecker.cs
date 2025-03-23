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
            var (x, y) = coord;

            if ((Storage.Map[y + 1, x    ].Status == type) && (Storage.Map[y + 1, x    ].Belong == Player)) return true;
            if ((Storage.Map[y + 1, x + 1].Status == type) && (Storage.Map[y + 1, x + 1].Belong == Player)) return true;
            if ((Storage.Map[y + 1, x - 1].Status == type) && (Storage.Map[y + 1, x - 1].Belong == Player)) return true;
            if ((Storage.Map[y    , x + 1].Status == type) && (Storage.Map[y    , x + 1].Belong == Player)) return true;
            if ((Storage.Map[y    , x - 1].Status == type) && (Storage.Map[y    , x - 1].Belong == Player)) return true;
            if ((Storage.Map[y - 1, x + 1].Status == type) && (Storage.Map[y - 1, x + 1].Belong == Player)) return true;
            if ((Storage.Map[y - 1, x    ].Status == type) && (Storage.Map[y - 1, x    ].Belong == Player)) return true;
            if ((Storage.Map[y - 1, x - 1].Status == type) && (Storage.Map[y - 1, x - 1].Belong == Player)) return true;

            return false;
        }
        public static bool CheckFort(MapStorage Storage, (int x, int y) coord, BelongEnum Player)
        {
            Storage.Map[coord.y, coord.x].Checker = true;
            if (LastChek(Storage, (coord.x, coord.y + 1), Player)) return true;
            if (LastChek(Storage, (coord.x + 1, coord.y + 1), Player)) return true;
            if (LastChek(Storage, (coord.x - 1, coord.y + 1), Player)) return true;
            if (LastChek(Storage, (coord.x + 1, coord.y), Player)) return true;
            if (LastChek(Storage, (coord.x - 1, coord.y), Player)) return true;
            if (LastChek(Storage, (coord.x, coord.y - 1), Player)) return true;
            if (LastChek(Storage, (coord.x + 1, coord.y - 1), Player)) return true;
            if (LastChek(Storage, (coord.x - 1, coord.y - 1), Player)) return true;
            return false;
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
