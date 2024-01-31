namespace Tesst.Dtos
{
    public class MasterReponse
    {
        public int MasterId { get; set; }
        public string MasterName { get; set; } = null!;
        public List<DetailsResponse> DummyDetails { get; set; }
    }
}
