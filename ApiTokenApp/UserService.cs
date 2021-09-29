using ApiTokenApp.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiTokenApp
{

    public interface IUserService
    {
        Users Authenticate(string kullaniciAdi, string sifre);
        IEnumerable<Users> GetAll();
    }


    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


        List<Users> _users = new List<Users>();
        public Users Authenticate(string kullaniciAdi, string sifre)
        {

            Db db = new Db();
            string msg;
            Users users = new Users();
            DataSet ds = db.Kullanicilar(users, out msg);
            
            
            if(ds!=null && ds.Tables.Count>0)
                {  
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                             
                    _users.Add(new Users

                {
                    kullaniciAdi = dr["kullaniciAdi"].ToString(),
                    sifre = dr["sifre"].ToString()

                });

                }

            }
             var user = _users.SingleOrDefault(x => x.kullaniciAdi == kullaniciAdi && x.sifre == sifre);
      // Kullanici bulunamadıysa null döner.
            if (user == null)
                return null;

            // Authentication(Yetkilendirme) başarılı ise JWT token üretilir.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            // Sifre null olarak gonderilir.
            user.sifre = null;

            return user;
        }

        public IEnumerable<Users> GetAll()
        {
            // Kullanicilar sifre olmadan dondurulur.
            return _users.Select(x =>
            {
                x.sifre = null;
                return x;
            });

        }
    }
}

