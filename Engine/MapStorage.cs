using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirusWar.Data.Cells;
using VirusWar.Data.DTO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VirusWar.Engine
{
    /// <summary>
    /// 
    /// </summary>
    class MapStorage
    {
        /// <summary>
        /// Путь до файлов уровней
        /// </summary>
        public string Pathlevel;
        /// <summary>
        /// Размерность игрового поля
        /// </summary>
        public (int x, int y) Size { get; private set; }
        public int Mode { get; set; }
        public int SizeList { get; private set; }

        public MapCell[,] Map { get; private set; } = new MapCell[0, 0];


        /// <summary>
        /// Проверка наличия файлов
        /// </summary>
        /// <param name="levelsFolder"></param>
        public MapStorage(string levelsFolder)
        {
            Pathlevel = levelsFolder;
            if (Directory.Exists(Pathlevel))
            {
                ReadJsMap();
                Mode = 1; // 0 - игроки 1 - пк
            }
        }

        public void ReadJsMap()
        {
            var fileJson = File.ReadAllText($"{Pathlevel}Info.json");
            
            JSRead? JsMain = JsonSerializer.Deserialize<JSRead>(fileJson);

            var MapList = JsMain.Map;

            Size = (MapList.Count, MapList[0].Count);
            Map = new MapCell[Size.x, Size.y];


            for (int i = 0; i < Size.x; i++)
            {
                var linestr = MapList[i];

                for (int j = 0; j < Size.y; j++)
                {
                    Map[i, j] = new MapCell
                    {
                        Type = linestr[j],
                        Status = StatusEnum.None,
                        Belong = BelongEnum.None,
                        Checker = false
                    };

                }
            }
            foreach (var a in JsMain.Positions)
            {
                Map[a.y, a.x].Status = StatusEnum.Virus;
                Map[a.y, a.x].Belong = BelongEnum.FirstPlayer;
                Map[a.x, a.y].Status = StatusEnum.Virus;
                Map[a.x, a.y].Belong = BelongEnum.SecondPlayer;              
            }
           // TempEnemy();
        }

        public void TempEnemy()
        {
            Map[4, 5].Status = StatusEnum.Fort;
            Map[4, 5].Belong = BelongEnum.SecondPlayer;
            Map[5, 5].Status = StatusEnum.Fort;
            Map[5, 5].Belong = BelongEnum.SecondPlayer;
            Map[5, 4].Status = StatusEnum.Fort;
            Map[5, 4].Belong = BelongEnum.SecondPlayer;
            Map[6, 4].Status = StatusEnum.Fort;
            Map[6, 4].Belong = BelongEnum.SecondPlayer;
            Map[6, 5].Status = StatusEnum.Virus;
            Map[6, 5].Belong = BelongEnum.SecondPlayer;
            Map[6, 3].Status = StatusEnum.Virus;
            Map[6, 3].Belong = BelongEnum.SecondPlayer;
        }






    }
}
