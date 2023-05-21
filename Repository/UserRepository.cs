using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs.Input;
using Models.DTOs.Output;
using Repository;
using Utils;
using Utils.Enums;
using BCryptNet = BCrypt.Net.BCrypt;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;
    private readonly IMapper _mapper;
    private const int PAGE_SIZE = 10;

    public UserRepository(MyDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<UserPagination> GetFilteredUsers(UserFilterDTO filter)
    {
        var query = _context.Users.AsQueryable();

        query = filterQuery(query, filter);

        return await Paginate(query, filter.PageNumber); ;
    }

    public async Task<int> Add(CreateUserDTO createUserDTO)
    {
        var user = _mapper.Map<User>(createUserDTO);

        user.InsertedAt = DateTime.UtcNow;
        user.Status = Status.Active;
        user.Password = HashPassword(user.Password);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var addedUser = _context.Entry(user).Entity;

        return addedUser.Id;
    }

    public async Task<User> Update(UpdateUserDTO updateUserDTO)
    {
        _context.ChangeTracker.Clear();

        var user = _mapper.Map<User>(updateUserDTO);

        user.UpdatedAt = DateTime.UtcNow;


        _context.Users.Update(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<Status> UpdateUserStatus(User user, Status newStatus)
    {
        _context.ChangeTracker.Clear();

        user.UpdatedAt = DateTime.UtcNow;
        user.Status = newStatus;

        _context.Users.Update(user);

        await _context.SaveChangesAsync();

        return user.Status;
    }

    public async Task<User> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user != null)
        {
            user.Status = Status.Inactive;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return user;
        }

        throw new NotImplementedException();
    }

    public async Task DeleteAllUsers()
    {
        List<User> users = await _context.Users.ToListAsync();

        users = users.Select(user =>
        {
            user.Status = Status.Inactive;
            user.UpdatedAt = DateTime.UtcNow;
            return user;
        }).ToList();

        _context.Users.UpdateRange(users);

        await _context.SaveChangesAsync();
    }

    public async Task<string> ResetPassword(User user)
    {
        try
        {
            string newPassword = GenerateRandomPassword();
            // Update the user's password in the repository
            user.Password = HashPassword(newPassword);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return newPassword;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public bool IsUserExists(string login)
    {
        return _context.Users.Any(u => u.Login == login);
    }

    public bool IsLoginExists(string login)
    {
        return _context.Users.Any(u => u.Login == login);
    }

    #region private

    private string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    private IQueryable<User> filterQuery(IQueryable<User> query, UserFilterDTO filter)
    {
        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(u => u.Name.ToUpper().Contains(filter.Name.ToUpper()));

        if (!string.IsNullOrEmpty(filter.Login))
            query = query.Where(u => u.Login.ToUpper().Contains(filter.Login.ToUpper()));

        if (!string.IsNullOrEmpty(filter.CPF))
            query = query.Where(u => u.CPF.Contains(filter.CPF));

        if (filter.Status != default)
            query = query.Where(u => u.Status == filter.Status);

        if (filter.StartDateBirth != default)
            query = query.Where(u => u.DateOfBirth >= filter.StartDateBirth);

        if (filter.EndDateBirth != default)
            query = query.Where(u =>  u.DateOfBirth <= filter.EndDateBirth);

        if (filter.StartInsertedAt != default)
            query = query.Where(u => u.DateOfBirth >= filter.StartInsertedAt);

        if (filter.EndInsertedAt != default)
            query = query.Where(u => u.DateOfBirth <= filter.EndInsertedAt);

        if (filter.StartUpdatedAt != default)
            query = query.Where(u => u.DateOfBirth >= filter.StartUpdatedAt);

        if (filter.EndUpdatedAt != default)
            query = query.Where(u => u.DateOfBirth <= filter.EndUpdatedAt);

        // TODO this query dont consider if the birthday has already occurred this year
        if (filter.StartAge != default)
            query = query.Where(u => DateTime.Today.Year - u.DateOfBirth.Year > filter.StartAge);

        // TODO this query dont consider if the birthday has already occurred this year
        if (filter.EndAge != default)
            query = query.Where(u => DateTime.Today.Year - u.DateOfBirth.Year < filter.EndAge);

        return query;
    }

    private async Task<UserPagination> Paginate(IQueryable<User> query, int? pageNumber)
    {
        if (pageNumber == null)
        {
            pageNumber = 1;
        }

        int skip = (int)((pageNumber - 1) * PAGE_SIZE);

        decimal usersCount = await query.CountAsync();

        decimal numberOfPages = (usersCount / PAGE_SIZE) <= 1 ? 1 : Math.Floor((usersCount / PAGE_SIZE)) + 1;

        List<User> users = await query.Skip(skip)
                            .Take(PAGE_SIZE)
                            .ToListAsync();

        List<UserDTO> userDtoList = _mapper.Map<List<UserDTO>>(users);

        UserPagination pageInfo = new((int)usersCount, (int)numberOfPages, pageNumber, PAGE_SIZE, userDtoList);

        return pageInfo;
    }

    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var password = new char[8];

        for (int i = 0; i < password.Length; i++)
        {
            password[i] = chars[random.Next(chars.Length)];
        }

        return new string(password);
    }

    #endregion private
}