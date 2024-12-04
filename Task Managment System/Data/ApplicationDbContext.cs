using Microsoft.EntityFrameworkCore;
using Task_Managment_System.Models;

namespace Task_Managment_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tasks> Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tasks>().HasData(

                    new Tasks
                    {
                        TaskId = 1,
                        Description = "Lorem Ipsum",
                        Status = "Completed",
                        Title = "Important Task 1"

                    },
                     new Tasks
                     {
                         TaskId = 2,
                         Description = "Lorem Ipsum",
                         Status = "Pending",
                         Title = " Not Vert Important Task"

                     },
                      new Tasks
                      {
                          TaskId = 3,
                          Description = "Lorem Ipsum",
                          Status = "Completed",
                          Title = "Important Task 2"

                      }
            );


        }
    }
}
