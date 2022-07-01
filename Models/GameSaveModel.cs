using System;

namespace CST350_CLC.Models
{
    public class GameSaveModel
    {
        public int ID { get; set; }
        public string GameData { get; set; }

        public DateTime Date { get; set; }
        public int PercentFinished { get; set; }
    }
}
