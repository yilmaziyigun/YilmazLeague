namespace YilmazLeague.WebApi.Dtos.Match
{
    public class CreateMatchDto
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public int WeekNumber { get; set; }
        public int SeasonId { get; set; } = 4;
    }
}
