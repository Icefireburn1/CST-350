namespace CST350_CLC.Models
{
    public class CellModel
    {
        public int Id { get; set; }
        public int CellState { get; set; }
        public int Neighbors { get; set; }
        public bool isBomb { get; set; }
    }
}
