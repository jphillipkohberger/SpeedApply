using SpeedApply.Api.Dtos;
using SpeedApply.Api.Models;
using SpeedApply.Api.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace SpeedApply.Api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IPasswordHasher<UsersDto> _passwordHasher;

        public UsersService(IUsersRepository repository, IPasswordHasher<UsersDto> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsersDto?> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            return new UsersDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                MinSal = user.MinSal,
                Password = user.Password,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UsersDto?> GetUserByIdWithQueriesAsync(int id)
        {
            var user = await _repository.GetUserWithQueriesAsync(id);
            if (user == null) return null;

            return new UsersDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Address = user.Address,
                MinSal = user.MinSal,
                Queries = user.Queries.Select(q => new QueriesDto
                {
                    Id = q.Id,
                    Query = q.Query,
                    CreatedAt = q.CreatedAt,
                    UserId = q.UserId,
                }).ToList()
            };
        }

        public async Task<UsersDto?> GetUserByIdWithFilesAsync(int id)
        {
            var user = await _repository.GetUserWithFilesAsync(id);
            if (user == null) return null;

            return new UsersDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Address = user.Address,
                MinSal = user.MinSal,
                Files = user.Files.Select(f => new FilesDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    CreatedAt = f.CreatedAt,
                    UserId = f.UserId,
                }).ToList()
            };
        }

        public async Task<List<UsersDto>> GetUsersAsync()
        {
            List<Users> users = await _repository.GetUsersAsync();

            List<UsersDto> usersDto = users.Select(u => new UsersDto {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Password = u.Password,
                Address = u.Address,
                MinSal = u.MinSal,
                CreatedAt = u.CreatedAt
            }).ToList();

            return usersDto;
        }


        public async Task<UsersDto> CreateUserAsync(UsersDto usersDto)
        {
            Users user = new Users
            {
                UserName = usersDto.UserName,
                Email = usersDto.Email,
                Password = HashPassword(usersDto, usersDto.Password),
                Address = usersDto.Address,
                MinSal = usersDto.MinSal,
                CreatedAt = DateTime.UtcNow
            };

            user = await _repository.CreateUserAsync(user);

            return new UsersDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                MinSal = user.MinSal,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UsersDto?> SaveUserProfileAsync(int id, string address, string minSal)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            user.Address = address;
            user.MinSal = minSal;

            // update database
            var userSaved = await _repository.UpdateUserAsync(user);
            if (userSaved == null) return null;

            return new UsersDto
            {
                Id = userSaved.Id,
                UserName = userSaved.UserName,
                Email = userSaved.Email,
                Password = userSaved.Password,
                Address = userSaved.Address,
                MinSal = userSaved.MinSal,
                CreatedAt = userSaved.CreatedAt
            };
        }

        public async Task<UsersDto?> LoginUserAsync(UsersDto clientUsersDto)
        {
            var user  = await _repository.GetByEmailAsync(clientUsersDto.Email);
           
            if(user == null)
            {
                return null;
            }

            /**
             * clientUsersDto coming from client side
             * dbUsersDto coming from database
             */
            UsersDto dbUsersDto = new UsersDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                MinSal = user.MinSal,
                CreatedAt = user.CreatedAt
            };

            /**
             * verify client Password vs database PasswordHash
             */
            var passwordResult = _passwordHasher.VerifyHashedPassword(dbUsersDto, dbUsersDto.Password, clientUsersDto.Password);

            if (passwordResult == PasswordVerificationResult.Success)
            {
                // Password is correct, proceed with login
                return dbUsersDto;
            }

            if (passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = HashPassword(dbUsersDto, clientUsersDto.Password);
                user = await _repository.UpdateUserAsync(user);
                return new UsersDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    Address = user.Address,
                    MinSal = user.MinSal,
                    CreatedAt = user.CreatedAt
                };
            }

            // password match fail
            return null;
        }

        public String HashPassword(UsersDto usersDto, String password)
        {
            return _passwordHasher.HashPassword(usersDto, password); 
        }
    }
}
