using Microsoft.EntityFrameworkCore;

namespace To_do_list_Application.Models
{
    public class Demo:DbContext
    {
       public DbSet<UserModel> Tasks
        { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlite(@"Data Source=C:\Users\Internee00\Desktop\database\Gohar2.db");
        }
    }
}
