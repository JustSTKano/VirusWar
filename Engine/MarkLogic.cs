using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VirusWar.Data;
using VirusWar.Data.Cells;
using VirusWar.Data.DTO;

namespace VirusWar.Engine
{
    class MarkLogic
    {
       
        public static void GetCoord(Reader Reader, (int x, int y) Position)
        {
            Trace.WriteLine($"Клик по точке с координатами (X = {Position.x} и Y = {Position.y}) !");
           
            if (CheckCoord(Reader, Position, Turner.Turn(Turner.Step))) ReWrite(Reader, Position, Turner.Turn(Turner.Step));
             

        }

        public static void ReWrite(Reader Reader, (int x, int y) coord, BelongEnum Player)
        {
            if (Reader.Map[coord.y, coord.x].Status == StatusEnum.Virus)
            {
                Reader.Map[coord.y, coord.x].Status = StatusEnum.Fort;
                Reader.Map[coord.y, coord.x].Belong = Player;
            }
            else
            {
                Reader.Map[coord.y, coord.x].Status = StatusEnum.Virus;
                Reader.Map[coord.y, coord.x].Belong = Player;
            }

            Turner.Step++;

        }

        public static bool CheckCoord(Reader Reader, (int x, int y) coord, BelongEnum Player)
        {
            ClearChecker(Reader);
            bool check = false;
            if ((Reader.Map[coord.y, coord.x].Status != StatusEnum.Wall)&&(Reader.Map[coord.y, coord.x].Status != StatusEnum.Fort)&&(Reader.Map[coord.y, coord.x].Belong != Player))
            {
                if (CheckLocal(Reader, coord, StatusEnum.Virus, Player)) return true;
                if (CheckLocal(Reader, coord, StatusEnum.Fort, Player))
                {
                    check = CheckFort(Reader, coord, Player);
                }

            }
            return check;
        }
        public static bool CheckLocal(Reader Reader, (int x, int y) coord, StatusEnum type, BelongEnum Player)
        {
            var (x, y) = coord;

            if ((Reader.Map[y + 1, x    ].Status == type) && (Reader.Map[y + 1, x    ].Belong == Player)) return true;
            if ((Reader.Map[y + 1, x + 1].Status == type) && (Reader.Map[y + 1, x + 1].Belong == Player)) return true;
            if ((Reader.Map[y + 1, x - 1].Status == type) && (Reader.Map[y + 1, x - 1].Belong == Player)) return true;
            if ((Reader.Map[y    , x + 1].Status == type) && (Reader.Map[y    , x + 1].Belong == Player)) return true;
            if ((Reader.Map[y    , x - 1].Status == type) && (Reader.Map[y    , x - 1].Belong == Player)) return true;
            if ((Reader.Map[y - 1, x + 1].Status == type) && (Reader.Map[y - 1, x + 1].Belong == Player)) return true;
            if ((Reader.Map[y - 1, x    ].Status == type) && (Reader.Map[y - 1, x    ].Belong == Player)) return true;
            if ((Reader.Map[y - 1, x - 1].Status == type) && (Reader.Map[y - 1, x - 1].Belong == Player)) return true;

            return false;
        }
        public static bool CheckFort(Reader Reader, (int x, int y) coord, BelongEnum Player)
        {
            Reader.Map[coord.y, coord.x].Checker = true;
            if (LastChek(Reader, (coord.x, coord.y + 1), Player)) return true;
            if (LastChek(Reader, (coord.x + 1, coord.y + 1), Player)) return true;
            if (LastChek(Reader, (coord.x - 1, coord.y + 1), Player)) return true;
            if (LastChek(Reader, (coord.x + 1, coord.y), Player)) return true;
            if (LastChek(Reader, (coord.x - 1, coord.y), Player)) return true;
            if (LastChek(Reader, (coord.x, coord.y - 1), Player)) return true;
            if (LastChek(Reader, (coord.x + 1, coord.y - 1), Player)) return true;
            if (LastChek(Reader, (coord.x - 1, coord.y - 1), Player)) return true;
            return false;
        }
       public static bool LastChek(Reader Reader, (int x, int y) coord, BelongEnum Player)
       {
            Trace.WriteLine($"X = {coord.x}, Y = {coord.y}");
            bool check = false;
            var  (x, y) = coord;
            if ((Reader.Map[y, x].Status == StatusEnum.Fort) && (Reader.Map[y, x].Belong == Player) && (Reader.Map[y, x].Checker == false))
            {
                Trace.WriteLine("Try");
                if (CheckLocal(Reader, (x, y), StatusEnum.Virus, Player)) { Trace.WriteLine(1); return true; }

                else check = CheckFort(Reader, (x, y), Player);
                Trace.WriteLine("No");
            }
            return check;
        }

        public static void ClearChecker(Reader Reader)
        {
            for (int i = 0; i < Reader.Size.x; i++)
            {
                for (int j = 0; j < Reader.Size.y; j++)
                {
                    Reader.Map[i, j].Checker = false;
                }
            }
        }
    }
}
