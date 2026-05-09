namespace YilmazLeague.WebUI.Dtos
{
    public class SeasonDto
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool IsActive { get; set; }
    }
}
