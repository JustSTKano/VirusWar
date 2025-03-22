using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirusWar.Data.Cells;

namespace VirusWar.Engine
{
   public static class Turner
    {
        public static int Step = 0;

        internal static BelongEnum Turn (int Step)
        {
            var a = Step;
            if (((a / 3) % 2) == 0)  return BelongEnum.FirstPlayer; 
            else  return BelongEnum.SecondPlayer; 
        }


    }
}
