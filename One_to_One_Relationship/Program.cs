using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

ESirketDbContext context = new();

#region Default Convention
//Her iki entity'de Navigation Property ile birbirlerini tekil olarak referans ederek fiziksel bir ilişkinin olacağı ifade edilir.
//One to One ilişki türünde, dependent entity'nin hangisi olduğunu default olarak belirleyebilmek pek kolay değildir. Bu durumda fiziksel olarak bir foreign key'e karşılık property/kolon tanımlayarak çözüm getirebiliyoruz.
//Böylece foreign key'e karşılık property tanımlayarak lüzumsuz bir kolon oluşturmuş oluyoruz.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }  //her bir çalışanın bir adresi olur. bu yüzden bire bir ilişki yapıldı.
//}
//class CalisanAdresi
//{
//    public int Id { get; set; }
//    public int CalisanId { get; set; } //hangi depndet hangi principal bunu anlayamaz bu nedenler foreign key olduğnu anlamsı içi koymak zorndayız
//    public string Adres { get; set; }

//    public Calisan Calisan { get; set; }
//}
#endregion
#region Data Annotations
//Navigation Property'ler tanımlanmalıdır.
//Foreign koonunun ismi default convention'ın dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebiliriz.
//Foreign Key kolonu oluşturulmak zorunda değildir. 
//1'e 1 ilişkide ekstradan foreign key kolonuna ihtiyaç olmayacağından dolayı dependent entity'deki id kolonunun hem foreign key hem de primary key olarak kullanmayı tercih ediyoruz ve bu duruma özen gösterilidir diyoruz.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; }
//}
//class CalisanAdresi
//{
//    [Key, ForeignKey(nameof(Calisan))]  //alabileceği id değerleri Calisan sınıfında ki ıd değerleri ile kesinlikle anı olmalı çünkü ForeignKey ve
//    key dedi başna çünkü ay zamanda peincipal key dedik. yani her değerden bir tane olailir.

//    public int Id { get; set; }
//    public string Adres { get; set; }

//    public Calisan Calisan { get; set; }
//}
#endregion
#region Fluent API
//Navigation Proeprtyler tanımlanmalı!
//Fleunt API yönteminde entity'ler arasındaki ilişki context sınıfı içerisinde OnModelCreating fonksiyonun override edilerek metotlar aracılığıyla tasarlanması gerekmektedir. Yani tüm sorumluluk bu fonksiyon içerisindeki çalışmalardadır.
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }

    public CalisanAdresi CalisanAdresi { get; set; }
}
class CalisanAdresi
{
    public int Id { get; set; }
    public string Adres { get; set; }

    public Calisan Calisan { get; set; }
}
#endregion
class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ESirketDB;User ID=SA;Password=1q2w3e4r+!");
    }
    //Model'ların(entity) veritabanında generate edilecek yapıları bu fonksiyonda içerisinde konfigüre edilir
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>()  //CalisanAdresi içerisinde ki ıd değer primary keydir dedin.
            .HasKey(c => c.Id);
         
        modelBuilder.Entity<Calisan>()  //çalışn entitsini hasone yapıyoruz .
             .HasOne(c => c.CalisanAdresi)   //Calisan içerisinde ki CalisanAdresi propertysi ile ilişki kuruyoruz. 
             .WithOne(c => c.Calisan)  //CalisanAdresi. classından da Calisan classıd Calisan ile bağlıyorum.
             .HasForeignKey<CalisanAdresi>(c => c.Id);   ///CalisanAdresi içerisinde ki ıd değer foreign keydir dedin.
    }
}

