using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb2.Models.Enums
{
    public enum BookStatus
    {
        [Display(Name = "Is Reading")]
        IsReading,
        [Display(Name = "Intend to Read")]
        IntendToRead,
        [Display(Name = "Intend to Buy")]
        IntendToBuy,
        [Display(Name = "Is Having")]
        IsHaving,
        Finished
    }

    //public string toString()
}
