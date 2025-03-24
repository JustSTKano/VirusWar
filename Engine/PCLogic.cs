using System.Windows.Controls;
using VirusWar.Data.Cells;

namespace VirusWar.Engine
{
    public static class PCLogic
    {

        internal static void PCtry(MapStorage Storage, Canvas Canvas)
        {
            BelongEnum player = BelongEnum.None;
            switch (MapInfo.Mode)
            {
                case 0: break;
                case 1: { player = BelongEnum.SecondPlayer; break; }
                case 2: { player = MapInfo.GetPlayerTurn(); break; }
                default: break;
            }
            while (MapInfo.GameStatus && (MapInfo.GetPlayerTurn() == player))
            {
                var temp = GetCord();
                if (MapInfo.CheckPossibleCoord(Storage, temp)) MapInfo.ReWrite(Storage, temp, Canvas);
            }
        }


        public static(int x, int y) GetCord()
        {
            Random rnd = new Random();
            return (rnd.Next(0, 11), rnd.Next(0, 11));
        }



    }
}
