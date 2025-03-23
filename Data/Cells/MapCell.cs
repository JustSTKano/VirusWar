using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWar.Data.Cells
{
    class MapCell
    {
        internal StatusEnum Status { get; set; }
        internal BelongEnum Belong { get; set; }  // 0 - not, 1 - player 1, 2 - player 2
        internal TypeEnum Type { get; set; }
        internal bool Checker { get; set; }
    }
}
