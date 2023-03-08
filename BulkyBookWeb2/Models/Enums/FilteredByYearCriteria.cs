using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb2.Models.Enums
{
    public enum FilteredByYearCriteria
    {
        ShowAll,
        [Display(Name = "Not finisished yet")]
        NotFinishedYet
    }
}
