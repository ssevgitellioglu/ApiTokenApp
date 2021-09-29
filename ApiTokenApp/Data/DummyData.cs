using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTokenApp.Data
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Context>();
                context.Database.EnsureCreated();
                context.Database.Migrate();

                // Look for any ailments
                if (context.users != null && context.users.Any())
                    return;   // DB has already been seeded

                var users = GetUsers().ToArray();
                context.users.AddRange(users);
                context.SaveChanges();

              
            }
        }
        public static List<Users> GetUsers()
        {
            List<Users> user = new List<Users>() {
                new Users {ad="Sevgi",soyad="Tellioğlu",kullaniciAdi="sevgitellioglu",sifre="123456"},
                 new Users {ad="Sevgi",soyad="Tellioğlu",kullaniciAdi="sevgitellioglu",sifre="123456"},
                  new Users {ad="Sevgi",soyad="Tellioğlu",kullaniciAdi="sevgitellioglu",sifre="123456"},
                   new Users {ad="Sevgi",soyad="Tellioğlu",kullaniciAdi="sevgitellioglu",sifre="123456"},
         };
            return user;
        }

    }
}
