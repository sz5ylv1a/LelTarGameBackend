using LelTarGameBackend.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace LelTarGameBackend.Data.v1
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Difficulties> Difficulties { get; set; }
		public DbSet<Countries> Countries { get; set; }
		public DbSet<Users> Users { get; set; }
		public DbSet<Lb> Lb { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			string bigint = "bigint(20)";

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Countries>(e =>
			{
				// Table config
				e.ToTable("countries");
				e.HasKey(e => e.Id);

				// Columns
				e.Property(e => e.Id)
					.ValueGeneratedOnAdd();
				e.Property(e => e.Name)
					.IsRequired()
					.HasColumnType("varchar(128)");
				e.Property(e => e.Flag)
					.HasColumnType("varchar(4)");

				//e.HasMany(e => e.Users)
				//	.WithMany(e => e.Countries);
			});

			modelBuilder.Entity<Difficulties>(e =>
			{
				// Table config
				e.ToTable("difficulties");
				e.HasKey(e => e.Id);

				// Columns
				e.Property(e => e.Id)
					.ValueGeneratedOnAdd();
				e.Property(e => e.DifficultyName)
					.IsRequired()
					.HasMaxLength(24)
					.HasColumnType("varchar(24)");
				e.Property(e => e.Description)
					.HasMaxLength(140)
					.HasColumnType("varchar(140)");
			});

			modelBuilder.Entity<Lb>(e =>
			{
				// Table config
				e.ToTable("lb");
				e.HasKey(e => e.Id);

				// Columns
				e.Property(e => e.Id)
					.ValueGeneratedOnAdd()
					.HasColumnType(bigint);
				e.Property(e => e.UsernameID)
					.IsRequired()
					.HasColumnType(bigint);
				e.Property(e => e.Score)
					.IsRequired()
					.HasColumnType(bigint);
				e.Property(e => e.DifficultyID)
					.IsRequired();
				e.Property(e => e.AchievedAt)
					.HasColumnType("datetime")
					.HasDefaultValueSql("CURRENT_TIMESTAMP");
			});

			modelBuilder.Entity<Users>(e =>
			{
				// Table properties
				e.ToTable("users");
				e.HasKey(e => e.Id);

				// Columns
				e.Property(e => e.Id)
					.ValueGeneratedOnAdd()
					.HasColumnType(bigint);
				e.Property(e => e.Username)
					.IsRequired()
					.HasMaxLength(32)
					.HasColumnType("varchar(32)");
				e.Property(e => e.Email)
					.IsRequired()
					.HasMaxLength(256)
					.HasColumnType("varchar(256)");
				e.Property(e => e.Password)
					.IsRequired()
					.HasMaxLength(1024)
					.HasColumnType("varchar(1024)");
				e.Property(e => e.CountryID);
				e.Property(e => e.CreatedAt)
					.IsRequired()
					.HasColumnType("datetime")
					.HasDefaultValueSql("CURRENT_TIMESTAMP");
			});
		}
	}
}
