using Microsoft.EntityFrameworkCore;

namespace iakademi47_proje.Models
{
	public class iakademi47Context : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

			var configuration= builder.Build();

			optionsBuilder.UseSqlServer(configuration["ConnectionStrings:iakademi47Connection"]);
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Setting> Settings { get; set; }
        public DbSet<vw_MyOrders> vw_MyOrders { get; set; }

        public DbSet<sp_arama> sp_Aramas { get; set; }
    }
}
