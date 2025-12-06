using ImageAIService.Data;
using ImageAIService.DTO.UsereServcieDTO;
using ImageAIService.Exceptions;
using ImageAIService.Helpers;
using ImageAIService.Interface;
using ImageAIService.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
namespace ImageAIService.Services
{

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserValidator _userValidator;
        public UserService(ApplicationDbContext context, UserValidator userValidator)
        {
            _context = context;
            _userValidator = userValidator;
        }
        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto user)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password, 12);
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Password = passwordHash,
                Phone = user.phone,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1,
            };

            await _userValidator.CheckDuplicateUserAsync(user);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Username = newUser.Username,
                Email = newUser.Email,
                Phone = newUser.Phone
            };

        }

        public async Task<bool>DeleteUserAsync(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);

            if (userToDelete == null)
            {
                throw new UserNotFoundException($"User with ID {id} not found.");
            }

            _context.Users.Remove(userToDelete);

            int deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<IEnumerable<GetAllUsersDto>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new GetAllUsersDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Username = u.Username,
                    IsActive = u.IsActive
                })
                .ToListAsync();
        }


        public async Task<User?> GetUserByIdAsync(int id)
        {
            var Getuserbyid =await _context.Users.FindAsync(id);
            if(Getuserbyid==null)
            {
                throw new UserNotFoundException($"User with ID {id} not found.");
            }
            return Getuserbyid;
        }
        public async Task<bool> SendOTP(string email)
        {
            var user = _context.Users.Where(u => u.Email == email && u.IsLogedIn == false).SingleOrDefault();
            if (user == null)
            {
                return false;
            }
            Random otp = new Random();
            user.OTP = otp.Next(11111, 99999).ToString();
            user.ExpireOTP = DateTime.Now.AddMinutes(3);
            //send otp via email

            _context.Update(user);
            _context.SaveChanges();

            return true;
        }

        public Task<bool> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> ResetPersonPassword(ResetUserPasswordInputDTO input)
        {
            var user = _context.Users.Where(u => u.Email == input.Email && u.OTP == input.OTP
            && u.IsLogedIn == false && u.ExpireOTP > DateTime.Now).SingleOrDefault();
            if (user == null)
            {
                return false;
            }
            if (input.Password != input.ConfirmPassword)
            {
                return false;
            }
            user.Password = input.ConfirmPassword;
            user.OTP = null;
            user.ExpireOTP = null;

            _context.Update(user);
            _context.SaveChanges();

            return true;
        }

        public Task<string> SignIn(SignInInputDTO input)
        {
            throw new NotImplementedException();
        }

        //public async Task<string> SignIn(SignInInputDTO input)
        //{
        //    var user = _computergyDbContext.Persons
        //        .Where(u => u.Email == input.Email && u.Password == input.Password && u.IsLogedIn == false)
        //        .SingleOrDefault();

        //    if (user == null)
        //        return "User not found";

        //    // Successful login, mark user as logged in
        //    user.IsLogedIn = true;
        //    user.LastLoginTime = DateTime.Now;
        //    user.OTP = null;
        //    user.ExpireOTP = null;

        //    _computergyDbContext.Update(user);
        //    _computergyDbContext.SaveChanges();

        //    return "Check your email OTP has been sent!";

        //    var token = _jwtTokenGenerator.CreateToken(user);
        //    return token;

        //}
    }

    
}
