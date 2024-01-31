namespace Tesst.Dtos
{
    public class DetailResponseWithMaster
    {
        public int DetailId { get; set; }
        public string DetailName { get; set; } = null!;
        public int MasterId { get; set; }
        public string MasterName { get; set; } = null!;
    }
}
