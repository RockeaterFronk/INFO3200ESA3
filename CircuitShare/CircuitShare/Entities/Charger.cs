namespace CircuitShare.Entities
{
    public class Charger
    {
        public int ChargerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double HourlyRate { get; set; } = 0;
        public DateTime? RentalStartTime { get; set; }
        public DateTime? RentalEndTime { get; set; }
        public double AmountDue { get; set; } = 0;
        public string? RentedByUserId { get; set; }

        public double TotalElapsedTime { get; set; } = 0;
    }

}
