namespace Rudhire_BE.DTOs
{
    public class TblUserQualificationDTO
    {
        //public int RowId { get; set; }

        public int UserId { get; set; }

        public string Degree { get; set; } = null!;

        public string UniversityName { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Percentage { get; set; }
    }
}
