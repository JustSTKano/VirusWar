using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirusWar.Data.Cells;

namespace VirusWar.Data.DTO
{
    internal class JSRead
    {
        public List<List<TypeEnum>> Map { get; set; }
        public List<Position> Positions { get; set; }
    }

    internal class Position
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
