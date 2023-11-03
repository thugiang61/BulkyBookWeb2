using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BulkyBookWeb2.Data;
using System;
using System.Linq;

namespace BulkyBookWeb2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BulkyBookWeb2Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BulkyBookWeb2Context>>()))
            {
                // Look for any Books.
                if (context.Book.Any())
                {
                    return;   // DB has been seeded
                }

                context.Book.AddRange(
                    new Book
                    {
                        Name = "The Help",
                        Author = "Kathryn Stockett",
                        Genre = "Black people, Novel, Historical fiction",
                        StartDate = DateTime.Parse("2022-6-12"),
                        Status = Enums.BookStatus.IsReading,
                        OtherNote = "Quyen nay la minh muon no th nha",
                        Price = 130000M
                    },
                    new Book
                    {
                        Name = "Sherlock Holmes",
                        Author = "Arthur Conan Doyle",
                        Genre = "Thrilling, Detective, Fiction",
                        StartDate = DateTime.Parse("2022-2-12"),
                        FinishDate = DateTime.Parse("2022-5-12"),
                        Status = Enums.BookStatus.Finished,
                        Review = "What an exciting book, each Holmes' adventure is an mysterious case",
                        Price = 150000M
                    },
                    new Book
                    {
                        Name = "Người trong lưới",
                        Author = "Chan Ho Kei",
                        Status = Enums.BookStatus.IntendToBuy,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}