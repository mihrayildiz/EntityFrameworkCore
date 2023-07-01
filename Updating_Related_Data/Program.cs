﻿using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region One to One İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Person person = new()
//{
//    Name = "Gençay",
//    Address = new()
//    {
//        PersonAddress = "Yenimahalle/ANKARA"
//    }
//};

//Person person2 = new()
//{
//    Name = "Hilmi"
//};

//await context.AddAsync(person);
//await context.AddAsync(person2);
//await context.SaveChangesAsync();
#endregion

#region 1. Durum | Esas tablodaki veriye bağımlı veriyi değiştirme
//blogların içerisinde idsi 2 olanı sil ve 4. ve 5. postu ekle.


//Person? person = await context.Persons  //hedef adres bilgisine person nesnesi  üzerinden erişiyoruz. 
//    .Include(p => p.Address)
//    .FirstOrDefaultAsync(p => p.Id == 1);

//context.Addresses.Remove(person.Address);  //adres bilgisini siliyoruz.
//person.Address = new()
//{
//    PersonAddress = "Yeni adres"
//};

//await context.SaveChangesAsync();  //yeni adres bilgisini ekliyoruz.
#endregion
#region 2. Durum | Bağımlı verinin ilişkisel olduğu ana veriyi güncelleme
//mesela 5. post yazan post 2. blogun omalıydı dersem.


//Address? address = await context.Addresses.FindAsync(1);
//address.Id = 2;
//await context.SaveChangesAsync();

//Address? address = await context.Addresses.FindAsync(2);
//context.Addresses.Remove(address);
//await context.SaveChangesAsync();

////Person? person = await context.Persons.FindAsync(2);
////address.Person = person;

//address.Person = new()
//{
//    Name = "Rıfkı"
//};
//await context.Addresses.AddAsync(address);

//await context.SaveChangesAsync();
#endregion
#endregion

#region One to Many İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Blog blog = new()
//{
//    Name = "Gencayyildiz.com Blog",
//    Posts = new List<Post>
//    {
//        new(){ Title = "1. Post" },
//        new(){ Title = "2. Post" },
//        new(){ Title = "3. Post" },
//    }
//};

//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();
#endregion

#region 1. Durum | Esas tablodaki veriye bağımlı verileri değiştirme
//Blog? blog = await context.Blogs   //ilk önce esas tabloya gittim.
//    .Include(b => b.Posts)  //esast tablo içerisniden bğımlı tabloya geçtim.blogun postlarını tut dedim.
//    .FirstOrDefaultAsync(b => b.Id == 1);  //bağımıın 1 ili erisini aldım

//bunun sonucunda blog Pstlrı ile birlikte elimizde.

//Post? silinecekPost = blog.Posts.FirstOrDefault(p => p.Id == 2);  //iki ıd li postu silinecekPost ile tuttuk..
//blog.Posts.Remove(silinecekPost);  //burada da sildik.

//blog.Posts.Add(new() { Title = "4. Post" });  //blog.Posts. blogun içindeki Posts lar yeni verileri atadık.
//blog.Posts.Add(new() { Title = "5. Post" });

//await context.SaveChangesAsync();
#endregion
#region 2. Durum | Bağımlı verilerin ilişkisel olduğu ana veriyi güncelleme
//Post? post = await context.Posts.FindAsync(4);  //id si 4 olan postu bul.post değişkininde tut.
//post.Blog = new()  //blog 2 yoktu olmadığdan dolayı önce oluşturdu.
//{
//    Name = "2. Blog"
//};
//await context.SaveChangesAsync();

//aşağıda ise 2.blog vardı 5 id li postu bağladm.
//Post? post = await context.Posts.FindAsync(5); 
//Blog? blog = await context.Blogs.FindAsync(2);
//post.Blog = blog;
//await context.SaveChangesAsync();
#endregion
#endregion

#region Many to Many İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Book book1 = new() { BookName = "1. Kitap" };  //kitaplar oluşturuldu.
//Book book2 = new() { BookName = "2. Kitap" };
//Book book3 = new() { BookName = "3. Kitap" };

//Author author1 = new() { AuthorName = "1. Yazar" };  //yazarlar ouşturuldu.
//Author author2 = new() { AuthorName = "2. Yazar" };
//Author author3 = new() { AuthorName = "3. Yazar" };

//book1.Authors.Add(author1); //book1 in yazarı olarak author1 eklenmiş.
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddAsync(book1);
//await context.AddAsync(book2);
//await context.AddAsync(book3);
//await context.SaveChangesAsync();
#endregion

#region 1. Örnek
// id si 1 olan book u 3 numaralı yazarlada eşleştir.


//Book? book = await context.Books.FindAsync(1);
//Author? author = await context.Authors.FindAsync(3);
//book.Authors.Add(author);

//await context.SaveChangesAsync();
#endregion
#region 2. Örnek

// 3 id li yazarın id nosu 1 olan kitalar dışındaki kitaplarla ilgisi kalmasın dedik.

//Author? author = await context.Authors
//    .Include(a => a.Books)  //Include ile FindAsync kullaılmz FirstOrDefaultAsync kullanılır.
//    .FirstOrDefaultAsync(a => a.Id == 3);  //id si 3 olan yzarın tüm kitapları gldi.

//foreach (var book in author.Books)  //yazarın kitaplarında dolaş
//{
//    if (book.Id != 1)  //bookun id si 1 değilse 
//        author.Books.Remove(book);  //yazarın kitaplarından sil.
//}

//await context.SaveChangesAsync();
#endregion
#region 3. Örnek
//Book? book = await context.Books    // şu an book id ==2 değerini tutuyo.
//    .Include(b => b.Authors)
//    .FirstOrDefaultAsync(b => b.Id == 2);  //İD Sİ 2 olan book ve yazarları getirildi.

//Author silinecekYazar = book.Authors.FirstOrDefault(a => a.Id == 1);  //bookun yazarlarından id si 1 olan yazar silindi.
//book.Authors.Remove(silinecekYazar);

//Author author = await context.Authors.FindAsync(3); //3. YZARA GİTTİM.
//book.Authors.Add(author); //2 ıd li 3 id li yzar ile ilişkilendrildi.
//book.Authors.Add(new() { AuthorName = "4. Yazar" });  //4. yazarı ekledik.

//await context.SaveChangesAsync();
#endregion
#endregion


class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }

    public Person Person { get; set; }
}
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }

    public Blog Blog { get; set; }
}
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=ApplicationDb;User ID=SA;Password=1q2w3e4r+!");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
    }
}