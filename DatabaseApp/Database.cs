using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    class Workshop
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ICollection<Worker> Workers { get; set; }
    }

    class Job
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String test { get; set; }
        public ICollection<Worker> Workers { get; set; }
    }
    class Worker
    {
        public int Id
        { get; set; }
        public int JobId
        { get; set; }
        public int WorkshopId { get; set; }
        public String FIO { get; set; }
        public float Discount { get; set; }
        public Job Job { get; set; }
        public Workshop Workshop { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
    class Invoice
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public DateTime Time { get; set; }
        public Worker Worker { get; set; }
        public ICollection<Distribution> Distributions { get; set; }
    }
    class WearType
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ICollection<WorkWear> WorkWears { get; set; }
    }
    class WorkWear
    {
        public int Id { get; set; }
        public int WearTypeId { get; set; }
        public String Name { get; set; }
        public float Cost { get; set; }
        public WearType WearType { get; set; }
        public ICollection<Distribution> Distributions { get; set; }
    }
    class Distribution
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int WorkWearId { get; set; }
        public bool Issued { get; set; }
        public WorkWear WorkWear { get; set; }
        public Invoice Invoice { get; set; }
    }
    class AppContext : DbContext
    {
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<WearType> WearTypes { get; set; }
        public DbSet<WorkWear> WorkWears { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public AppContext() : base("MyApp")
        {
            Database.SetInitializer(new AppInitializer());
            SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workshop>()
                .HasMany(x => x.Workers)
                .WithRequired(x => x.Workshop)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Job>()
                .HasMany(x => x.Workers)
                .WithRequired(x => x.Job)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Worker>()
                .HasMany(x => x.Invoices)
                .WithRequired(x => x.Worker)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<WearType>()
                .HasMany(x => x.WorkWears)
                .WithRequired(x => x.WearType)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<WorkWear>()
                .HasMany(x => x.Distributions)
                .WithRequired(x => x.WorkWear)
                .WillCascadeOnDelete(false);
        }
    }
    internal class AppInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            IList<Job> jobs = new List<Job>()
            {
                new Job() {Name = "Начальник цеха"},
                new Job() {Name = "Бункеровщик"},
                new Job() {Name = "Вулканизаторщик"},
                new Job() {Name = "Газогенераторщик"},
                new Job() {Name = "Гидромониторщик"},
                new Job() {Name = "Грохотовщик"},
                new Job() {Name = "Грузчик"},
            };
            context.Jobs.AddRange(jobs);
            IList<Workshop> workshops = new List<Workshop>()
            {
                new Workshop(){Name = "Копровый"},
                new Workshop(){Name = "Cталеплавильные печи"},
                new Workshop(){Name = "Прокатные станы"}
            };
            context.Workshops.AddRange(workshops);
            IList<Worker> workers = new List<Worker>()
            {
                new Worker(){
                    FIO = "Архипов Варлаам Мэлорович",
                    Job = jobs.First(x=>x.Name == "Начальник цеха"),
                    Workshop = workshops.First(x=>x.Name == "Копровый"),
                    Discount = 1
                },
                new Worker(){
                    FIO = "Ларионов Александр Константинович",
                    Job = jobs.First(x=>x.Name == "Бункеровщик"),
                    Workshop = workshops.First(x=>x.Name == "Копровый"),
                    Discount = 10
                },
                new Worker(){
                    FIO = "Макаров Антон Христофорович",
                    Job = jobs.First(x=>x.Name == "Газогенераторщик"),
                    Workshop = workshops.First(x=>x.Name == "Копровый"),
                    Discount = 5
                }
            };
            context.Workers.AddRange(workers);
            IList<Invoice> invoices = new List<Invoice>()
            {
                new Invoice()
                {
                    Time = DateTime.Now,
                    Worker = workers.First(x=>x.FIO == "Ларионов Александр Константинович")
                },
                new Invoice()
                {
                    Time = DateTime.Now,
                    Worker = workers.First(x=>x.FIO == "Макаров Антон Христофорович")
                }
            };
            context.Invoices.AddRange(invoices);
            IList<WearType> wearTypes = new List<WearType>()
            {
                new WearType() {Name = "Обувь"}
            };
            context.WearTypes.AddRange(wearTypes);
            IList<WorkWear> workWears = new List<WorkWear>()
            {
                new WorkWear() {
                    WearType = wearTypes.First(x => x.Name == "Обувь"),
                    Name = "Ботинки утепленные (натуральная кожа)",
                    Cost = 2790
                }
            };
            context.WorkWears.AddRange(workWears);
            IList<Distribution> distributions = new List<Distribution>()
            {
               new Distribution()
               {
                   Invoice = invoices.First(x=>x.Worker.FIO == "Ларионов Александр Константинович"),
                   WorkWear = workWears.First(x=>x.Name == "Ботинки утепленные (натуральная кожа)"),
                   Issued = false
               },
               new Distribution()
               {
                   Invoice = invoices.First(x=>x.Worker.FIO == "Макаров Антон Христофорович"),
                   WorkWear = workWears.First(x=>x.Name == "Ботинки утепленные (натуральная кожа)"),
                   Issued = false
               }
            };
            base.Seed(context);
        }
    }
}
