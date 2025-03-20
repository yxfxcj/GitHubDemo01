namespace Ivan.DianPing.Models.PO
{
    public class User
    {
        public long Id { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Nickname { get; set; }

        public string Icon { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
