namespace Ivan.DianPing.V2.Models.PO
{
    public class Shop
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long TypeId { get; set; }

        public string Images { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public long AvgPrice { get; set; }

        public long Sold {  get; set; }

        public long Comments { get; set; }

        public int Score { get; set; }

        public string OpenHours { get; set; }
    }
}
