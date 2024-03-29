﻿namespace BulkyBookWeb2.Models
{
    public class BooksFilteredByYearViewModel
    {
        public List<Book>? Books { get; set; }

        public IEnumerable<int> Years { get; set; }  = new List<int>(); 
       
        public int SelectedYear { get; set; }
    }
}
