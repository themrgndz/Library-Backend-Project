using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UzmLibrary.Models;

namespace UzmLibrary.Services
{
    public class UserService
    {
        private readonly LibraryContext _context;

        public UserService(LibraryContext context)
        {
            _context = context;
        }

        // Tüm kullanıcıları listeleme
        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.UserRoles.Select(ur => ur.Role.RoleName).FirstOrDefault() // İlk rolün adını al
                })
                .ToListAsync();
        }

        // Belirli bir kullanıcıyı ID'ye göre getirme
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null) return null;

            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.UserRoles.Select(ur => ur.Role.RoleName).FirstOrDefault() // İlk rolün adını al
            };
        }

        // Kullanıcı adı ve şifre kontrolü
        public async Task<UserDTO> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Users
                .Where(u => u.Username == username && u.Password == password)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new UserDTO 
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.UserRoles.Select(ur => ur.Role.RoleName).FirstOrDefault() // İlk rolün adını al
                })
                .FirstOrDefaultAsync();

            return user;
        }

        // Yeni kullanıcı ekleme
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Kullanıcı güncelleme
        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Kullanıcı silme
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
