using ApiMoneda.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiMoneda.Data
{
    public class ConversorContext : DbContext
    {
        public DbSet<Conversion> Conversions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }

        public ConversorContext(DbContextOptions<ConversorContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // RELACIÓN MUCHOS A MUCHOS ENTRE USER Y CURRENCY UTILIZANDO UNA TABLA DE UNIÒN
            modelBuilder.Entity<User>()
                .HasMany(u => u.Currencies)
                .WithMany()
                .UsingEntity("UserCurrencies");


            // RELACION 1, N SUSCRIPCION/USUARIO
            modelBuilder.Entity<Subscription>()
                .HasMany(s => s.Users) 
                .WithOne(u => u.Subscription)  
                .HasForeignKey(u => u.SubscriptionId);

            // RELACION 1, N USUARIO/CONVERSION
            modelBuilder.Entity<User>()
                .HasMany(u => u.Conversions)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);


            Subscription subsFree = new Subscription()
            {
                Id= 1,
                Name= "Free",
                AmountOfConvertions = 10,
                Price=0,
            };
            Subscription subsTrial = new Subscription()
            {
                Id = 2,
                Name = "Trial",
                AmountOfConvertions = 100,
                Price = 0.80,
            };
            Subscription subsPro = new Subscription()
            { 
                Id = 3,
                Name = "Pro",
                AmountOfConvertions = 999999999,
                Price = 1,
            };
            Currency PesoArgentino = new Currency()
            {
                Id = 1,   
                Name = "Peso argentino",
                ISOcode = "ARS",
                Value = 0.0012m,

            };
            Currency DolarEEUU = new Currency()
            {
                Id = 2,
                Name = "Dolar estadounidense",
                ISOcode = "USD",
                Value = 1,

            };
            Currency Euro = new Currency()
            {
                Id = 3,
                Name = "Euro",
                ISOcode = "EUR",
                Value = 1.11m,

            };
            Currency LibraEsterlina = new Currency()
            {
                Id = 4,
                Name = "Libra esterlina",
                ISOcode = "GBP",
                Value = 1.28m,

            };
            Currency YenJapones = new Currency()
            {
                Id = 5,
                Name = "Yen",
                ISOcode = "JPY",
                Value = 0.0071m,

            };
            Currency DolarCanadiense = new Currency()
            {
                Id = 6,
                Name = "Dolar canadiense",
                ISOcode = "CAD",
                Value = 0.76m,

            };
            Currency DolarAustraliano = new Currency()
            {
                Id = 7,
                Name = "Dolar australiano",
                ISOcode = "AUD",
                Value = 0.69m,

            };
            Currency FrancoSuizo = new Currency()
            {
                Id = 8,
                Name = "Franco suizo",
                ISOcode = "CHF",
                Value = 1.19m,

            };
            Currency YuanChino = new Currency()
            {
                Id = 9,
                Name = "Yuan chino",
                ISOcode = "CNY",
                Value = 0.14m,

            };
            Currency PesoMexicano = new Currency()
            {
                Id = 10,
                Name = "Peso mexicano",
                ISOcode = "$",
                Value = 0.059m,

            };

            Currency PesoUruguayo = new Currency()
            {
                Id = 11,
                Name = "Peso uruguayo",
                ISOcode = "UYI",
                Value = 0.026m,

            };
            Currency PesoChileno = new Currency()
            {
                Id = 12,
                Name = "Peso chileno",
                ISOcode = "CLP",
                Value = 0.0011m,

            };
            Currency Real = new Currency()
            {
                Id = 13,
                Name = "Real",
                ISOcode = "BRL",
                Value = 0.21m,

            };

            User user1 = new User()
            {
                Id = 1,
                UserName = "MicaG",
                Password = "mica123",
                FirstName = "Micaela",
                LastName = "Germi",
                Email = "micaela@mail.com",
                SubscriptionId = subsPro.Id,
                Role = Models.Enum.Role.Admin
            };
            User user2 = new User()
            {
                Id = 2,
                UserName = "Juancito",
                Password = "juan123",
                FirstName = "Juan",
                LastName = "Perez",
                Email = "Juan@mail.com",
                SubscriptionId = subsTrial.Id,
            };
            User user3 = new User()
            {
                Id = 3,
                UserName = "MariaJ",
                Password = "maria123",
                FirstName = "Maria",
                LastName = "Lopez",
                Email = "Maria@mail.com",
                SubscriptionId = subsFree.Id,
            };

            modelBuilder.Entity<Subscription>().HasData(subsFree, subsTrial, subsPro);
           modelBuilder.Entity<Currency>().HasData(
               PesoArgentino,PesoChileno,PesoMexicano,PesoUruguayo,DolarAustraliano,DolarCanadiense,DolarEEUU, LibraEsterlina,
               YenJapones,YuanChino,FrancoSuizo, Real,Euro);
          modelBuilder.Entity<User>().HasData(user1,user2, user3);

            base.OnModelCreating(modelBuilder);
        }
    }

}
