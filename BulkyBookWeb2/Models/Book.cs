using BulkyBookWeb2.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? Genre { get; set; }

        [DisplayName("Start Reading")]
        [DataType(DataType.Date)]
        //s ma mac dinh thay han hien dung format len gdien r nhi
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; } = default(DateTime?);

        [DisplayName("Finish Reading")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
        public DateTime? FinishDate { get; set; } = default(DateTime?);
        public BookStatus Status { get; set; }
        public string? Review { get; set; }

        [DisplayName("Other Note")]
        public string? OtherNote { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(9,0)")]
        public decimal? Price { get; set; }
    }
}
