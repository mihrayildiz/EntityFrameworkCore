using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region Index Nedir?
//Index, bir sütuna dayalı sorgulamaları daha verimli ve performanslı hale getirmek için kullanılan yapıdır.
#endregion
#region Index'leme Nasıl Yapılır?
//PK, FK ve AK olan kolonlar otomatik olarak indexlenir. 

#region Index Attribute'u

#endregion
#region HasIndex Metodu

#endregion
#endregion
#region Composite Index
//context.Employees.Where(e => e.Name == "" || e.Surname == "")
#endregion
#region Birden Fazla Index Tanımlama

#endregion
#region Index Uniqueness

#endregion
#region Index Sort Order - Sıralama Düzeni (EF Core 7.0)

#region AllDescending - Attribute
//Tüm indexlemelerde descending davranışının bütünsel olarak konfigürasyonunu sağlar.
#endregion
#region IsDescending - Attribute
//Indexleme sürecindeki her bir kolona göre sıralama davranışını hususi ayarlamak istiyorsak kullanılır.
#endregion
#region IsDescending Metodu

#endregion
#endregion
#region Index Name

#endregion
#region Index Filter

#region HasFilter Metodu

#endregion
#endregion
#region Included Columns

#region IncludeProperties Metodu

#endregion
#endregion



//[Index(nameof(Name))] //name kolonuna index vermiş olduk.böylece name üzerinden yapılan işlemler daha az maliyetli ve yüksek erformans lı olur.
//[Index(nameof(Surname))]
//[Index(nameof(Name), nameof(Surname))] //comopsite indexleme
//[Index(nameof(Name), AllDescending = true)]  //name indexi için değerler descending olarak başlayacak.
//[Index(nameof(Name), nameof(Surname), IsDescending = new[] { true, false })] //nam için descending ancak surname için değil demek.
//[Index(nameof(Name), Name = "name_index")]
class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Employee>()
        //.HasIndex(x => x.Name);
        //.HasIndex(x => new { x.Name, x.Surname });
        //.HasIndex(nameof(Employee.Name), nameof(Employee.Surname));
        //.HasIndex(x => x.Name)
        //.IsUnique();

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .IsDescending();

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => new { x.Name, x.Surname })
        //    .IsDescending(true, false);

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .HasDatabaseName("name_index");

        //modelBuilder.Entity<Employee>()
        //    .HasIndex(x => x.Name)
        //    .HasFilter("[NAME] IS NOT NULL");  //şarta uymayan değerler idexedahil edilmez.

        modelBuilder.Entity<Employee>()
            .HasIndex(x => new { x.Name, x.Surname })
            .IncludeProperties(x => x.Salary);  //idex dışındaki değerlere de ulaşmış oldum. ,ndexleme dışında da kolon getiriyorum sen vermliliği düşürme demiş oluyorum
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDB;User ID=SA;Password=1q2w3e4r+!;TrustServerCertificate=True");
    }
}