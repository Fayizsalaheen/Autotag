using ImageAIService.Data;
using ImageAIService.DTO.UsereServcieDTO;
using ImageAIService.Exceptions;
using ImageAIService.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ImageAIService.Helpers
{
    public class UserValidator
    {
        private readonly ApplicationDbContext _context;

        public UserValidator(ApplicationDbContext context)
        {
            _context = context;
        }

        //public bool IsValidEmail(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //    {
        //        return false;
        //    }

        //    string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        //    return Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase);
        //}

        //public bool IsValidUsername(string userName)
        //{
        //    return !string.IsNullOrEmpty(userName) && userName.Length >= 4;
        //}

        public async Task CheckDuplicateUserAsync(CreateUserDto user)
        {
            var existingUser = await _context.Users
            .Where(u => u.Email == user.Email || u.Username == user.Username)
            .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                if (existingUser.Email == user.Email)
                    throw new InvalidEmailException("Email is already taken.");
                if (existingUser.Username == user.Username)
                    throw new InvalidUsernameException("Username is already taken.");
            }

        }
    }
}