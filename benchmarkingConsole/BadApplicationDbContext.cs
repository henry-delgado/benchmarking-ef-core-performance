using Microsoft.EntityFrameworkCore;
using benchmarkingConsole.Models.Bad;
using System.Linq;
using System;

namespace benchmarkingConsole
{
    public class BadApplicationDbContext : DbContext
    {
        public BadApplicationDbContext()
        {

        }

        public DbSet<BadCompany> BadCompany { get; set; }
        public DbSet<BadProject> BadProject { get; set; }
        public DbSet<BadPenetration> BadPenetration { get; set; }
        public DbSet<BadPenetrationAttribute> BadPenetrationAttribute { get; set; }
        public DbSet<BadStandardProjectPenetration> BadStandardProjectPenetration { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BadCompany>().HasIndex(x => x.PortfolioId);
            modelBuilder.Entity<BadCompany>().HasIndex(x => x.LicenseId);
            modelBuilder.Entity<BadPenetration>().HasIndex(x => x.ProductId);
            modelBuilder.Entity<BadPenetration>().HasIndex(x => x.CategoryId);
            modelBuilder.Entity<BadPenetration>().HasIndex(x => x.Level1Id);
            modelBuilder.Entity<BadPenetration>().HasIndex(x => x.Level2Id);
            modelBuilder.Entity<BadPenetration>().HasIndex(x => x.Level3Id);
            modelBuilder.Entity<BadPenetration>().HasIndex(x => x.ApprovalId);

            modelBuilder.Entity<BadPenetrationAttribute>().HasIndex(x => x.BadPenetrationId);
            modelBuilder.Entity<BadPenetrationAttribute>().HasIndex(x => x.CategoryId);

            modelBuilder.Entity<BadStandardProjectPenetration>().HasIndex(x => x.BadProjectId);
            modelBuilder.Entity<BadStandardProjectPenetration>().HasIndex(x => x.CategoryId);

            modelBuilder.Entity<BadProject>().HasIndex(x => x.BadCompanyId);

            modelBuilder.Entity<BadStandardProjectPenetration>()
                .HasOne(x => x.BadProject)
                .WithMany(c => c.BadStandardProjectPenetrations)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BadPenetrationAttribute>()
                .HasOne(x => x.BadPenetration)
                .WithMany(c => c.BadPenetrationAttributes)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BadPenetration>()
                .HasOne(x => x.BadProject)
                .WithMany(c => c.BadPenetrations)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BadProject>()
                .HasOne(x => x.BadCompany)
                .WithMany(c => c.BadProjects)
                .OnDelete(DeleteBehavior.Cascade);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                        => optionsBuilder.UseSqlServer(@"Data Source=TOWER-DELGADO\LOCALSERVER;Initial Catalog=Benchmarking;Integrated Security=True");

        public void SeedData(int totalRecords, int totalProjectsPerCompany)
        {
            DeleteData(totalRecords, totalProjectsPerCompany);
            SeedCompanyData(totalRecords);
            SeedProjectData(totalProjectsPerCompany);
            SeedPenetrationData();
            SeedPenetrationAttributeData();
            SeedStandardProjectPenetrationData();
        }

        public void DeleteData(int totalRecords, int totalProjectsPerCompany)
        {
            try
            {
                System.Console.WriteLine("Deleting Standard Project Penetration");
                if (BadStandardProjectPenetration.Any())
                    BadStandardProjectPenetration.RemoveRange(BadStandardProjectPenetration.ToList());

                System.Console.WriteLine("Deleting Penetration Attributes");
                if (BadStandardProjectPenetration.Any())
                    BadPenetrationAttribute.RemoveRange(BadPenetrationAttribute.ToList());

                System.Console.WriteLine("Deleting Penetrations");
                if (BadPenetration.Any())
                    BadPenetration.RemoveRange(BadPenetration.ToList());

                System.Console.WriteLine("Deleting Projects");
                if (BadProject.Any())
                    BadProject.RemoveRange(BadProject.ToList());

                System.Console.WriteLine("Deleting Companies");
                if (BadCompany.Any())
                    BadCompany.RemoveRange(BadCompany.ToList());

                SaveChanges();

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error from seeder. Message: {ex.Message}");
            }
        }

        private void SeedCompanyData(int totalRecords)
        {
            try
            {
                System.Console.WriteLine($"Adding [{totalRecords}] new companies");
                BadCompany.AddRange(
                Enumerable.Range(0, totalRecords).Select(
                    i => new BadCompany
                    {
                        Name = $"A Company [{i}]",
                        Address1 = $"100{i} Main Street # {i}",
                        Address2 = $"Ste {i}",
                        City = $"City # {i}",
                        State = $"State # {i}",
                        Zip = $"7{i}0{(i)}",
                        Building = $"Building # {i}",
                        LicenseId = i,
                        PortfolioId = i
                    }));
                SaveChanges();
                System.Console.WriteLine("Completed adding companies!");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error from seeder. Message: {ex.Message}");
            }
        }

        private void SeedProjectData(int totalProjectsPerCompany)
        {
            try
            {
                var companies = BadCompany.ToList();
                System.Console.WriteLine($"Adding [{(companies.Count * totalProjectsPerCompany)}] Projects. {totalProjectsPerCompany} Projects of each of the [{companies.Count}] companies");

                foreach (var item in companies)
                {

                    var x = item.Id;
                    BadProject.AddRange(
                    Enumerable.Range(0, totalProjectsPerCompany).Select(
                        i => new BadProject
                        {
                            Name = $"A Project [{(i * x + x + i)}]",
                            Address1 = $"200{(i * x + x)} Main Street # {(i * x + x)}",
                            Address2 = $"Ste {(i * x + x)}",
                            City = $"City # {(i * x + x)}",
                            State = $"State # {(i * x + x)}",
                            Zip = $"9{i}0{((i * x + x))}",
                            Building = $"Building # {(i * x + x)}",
                            BadCompanyId = item.Id,
                            ClientName = $"Client {(i * x + i + x)}",
                            ClientContactPerson = $"Contact {(i * x + i + x)}",
                            Comments = $"A lot of details can go inside this comments. Commment: {(i * x + i + x)}",
                            CreatedBy = "SeedData",
                            ModifiedBy = "SeedData",
                            Status = "A"
                        }));
                }
                SaveChanges();
                System.Console.WriteLine("Completed adding projects!");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error from seeder. Message: {ex.Message}");
            }
        }

        private void SeedPenetrationData()
        {
            try
            {
                var companies = BadCompany.ToList();
                System.Console.WriteLine("Adding penetrations.");

                foreach (var item in companies)
                {
                    var projects = BadProject.Where(x => x.BadCompanyId == item.Id).ToList();
                    foreach (var p in projects)
                    {
                        var x = item.Id;
                        var someId = (p.Id + 1) % 2 == 0 ? 1 : 2;
                        BadPenetration.Add(
                            new BadPenetration
                            {
                                ProductId = someId,
                                ApprovalId = someId,
                                CategoryId = 1,
                                Level1Id = someId,
                                Level2Id = someId + 1,
                                Level3Id = someId + 2,
                                LocalUniqueId = System.Guid.NewGuid().ToString(),
                                PenetrationNumber = (int)item.Id + (int)p.Id,
                                BadProjectId = p.Id,
                                QrCode = $"AB{item.Id + p.Id}",
                                Status = "A"
                            });
                    }
                }
                SaveChanges();
                System.Console.WriteLine($"Completed adding [{BadPenetration.Count()}] penetrations!");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error from seeder. Message: {ex.Message}");
            }
        }

        private void SeedPenetrationAttributeData()
        {
            try
            {
                var penetrations = BadPenetration.ToList();
                System.Console.WriteLine($"Adding [{penetrations.Count()}] penetration attributes.");

                foreach (var item in penetrations)
                {
                    BadPenetrationAttribute.Add(new Models.Bad.BadPenetrationAttribute
                    {
                        CategoryId = 1,
                        IsEditable = "Y",
                        Name = $"Attribute {item.Id}",
                        BadPenetrationId = item.Id,
                        Priority = (int)(item.Id + 1),
                        SelectedValue = $"A large description for this value here. {item.Id}",
                        Type = $"Type {item.Id}"
                    });
                }
                SaveChanges();
                System.Console.WriteLine("Completed adding penetration attributes!");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error from seeder. Message: {ex.Message}");
            }
        }

        private void SeedStandardProjectPenetrationData()
        {
            try
            {
                var projects = BadProject.ToList();
                System.Console.WriteLine($"Adding [{projects.Count()}] standard project penetrations.");

                foreach (var item in projects)
                {
                    var someId = (item.Id + 1) % 2 == 0 ? 1 : 2;
                    BadStandardProjectPenetration.Add(new Models.Bad.BadStandardProjectPenetration
                    {
                        BadProjectId = item.Id,
                        CategoryId = 1,
                        IsEditable = "Y",
                        Name = $"Attribute {item.Id}",
                        Priority = (int)(item.Id + 1),
                        Value = $"A large description for this value here. {item.Id}",
                        Type = $"Type {item.Id}",
                        IsCustomizable = "Y",
                        IsDeleted = ((item.Id + 1) % 2 == 0) ? "Y" : "N",
                    });
                }
                SaveChanges();
                System.Console.WriteLine("Completed adding penetration attributes!");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error from seeder. Message: {ex.Message}");
            }
        }
    }
}