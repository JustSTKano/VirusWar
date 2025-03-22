using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VirusWar.Data;
using VirusWar.Data.Cells;
using VirusWar.Data.DTO;

namespace VirusWar.Engine
{
    static class WinChecker
    {
        public static bool Win (Reader Reader)
        {
            
            for (int i = 0; i < Reader.Size.x; i++)
            {
                for (int j = 0; j < Reader.Size.y; j++)
                {
                    if (MarkLogic.CheckCoord(Reader, (i, j), Turner.Turn(Turner.Step))) return true;
                }
            }

            return false;
        }
    }
}
