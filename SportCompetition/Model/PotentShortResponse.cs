namespace SportCompetition.Model
{
    public class PotentShortResponse
    {
        public int id { get; set; }
        public string lastname { get; set; } 

        public string firstname { get; set; }

        public string? surname { get; set; }

        public DateOnly date_birth { get; set; }

        //public BitArray gender { get; set; } = null!;

    }
}
