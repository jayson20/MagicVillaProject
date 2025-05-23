﻿using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;
using MagicVilla_VillaAPI.Repository.IRepostiory;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration, IMapper mapper
			/*UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager*/)
        {
            _db = db;
            _mapper = mapper;
            //_userManager = userManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            //_roleManager = roleManager;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {

            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName == loginRequestDTO.UserName.ToLower()
            && u.Password == loginRequestDTO.Password);

            if (user == null)
            {
                return null;
            }

            //if user was found generate JWT Token
            //var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDTO>(user)

            };
            return loginResponseDTO;


            //var user = _db.LocalUsers
            //    .FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            //bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);


            //if (user == null || isValid == false)
            //{
            //    return new LoginResponseDTO()
            //    {
            //        Token = "",
            //        User = null
            //    };
            //}

            ////if user was found generate JWT Token
            //var roles = await _userManager.GetRolesAsync(user);
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(secretKey);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.UserName.ToString()),
            //        new Claim(ClaimTypes.Role, roles.FirstOrDefault())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            //{
            //    Token = tokenHandler.WriteToken(token),
            //    User = _mapper.Map<UserDTO>(user),

            //};
            //return loginResponseDTO;
        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {

            LocalUser user = new()
            {
                UserName = registerationRequestDTO.UserName,
                Password = registerationRequestDTO.Password,
                Name = registerationRequestDTO.Name,
                Role = registerationRequestDTO.Role,
            };

            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();

            user.Password = "";
            return user;

            //    ApplicationUser user = new()
            //    {
            //        UserName = registerationRequestDTO.UserName,
            //        Email=registerationRequestDTO.UserName,
            //        NormalizedEmail=registerationRequestDTO.UserName.ToUpper(),
            //        Name = registerationRequestDTO.Name
            //    };

            //    try
            //    {
            //        var result = await _userManager.CreateAsync(user, registerationRequestDTO.Password);
            //        if (result.Succeeded)
            //        {
            //            if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult()){
            //                await _roleManager.CreateAsync(new IdentityRole("admin"));
            //                await _roleManager.CreateAsync(new IdentityRole("customer"));
            //            }
            //            await _userManager.AddToRoleAsync(user, "admin");
            //            var userToReturn = _db.ApplicationUsers
            //                .FirstOrDefault(u => u.UserName == registerationRequestDTO.UserName);
            //            return _mapper.Map<UserDTO>(userToReturn);

            //        }
            //    }
            //    catch(Exception e)
            //    {

            //    }

            //    return new UserDTO();
            //}
        }
    }

}