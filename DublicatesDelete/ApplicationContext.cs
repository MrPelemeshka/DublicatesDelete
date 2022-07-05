using System.Data.Entity;

namespace DublicatesDelete
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Folder> Folders { get; set; }
        
        public DbSet<File> Files { get; set; }
        public DbSet<Duplicate> Duplicates { get; set; }
    }
}
