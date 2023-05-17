using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs.Input;
using Repository;
using Utils.Enums;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(MyDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<User> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<int> Add(CreateUserDTO createUserDTO)
    {
        var user = _mapper.Map<User>(createUserDTO);

        user.InsertedAt = DateTime.UtcNow;
        user.Status = Status.Active;

        await _context.Users.AddAsync(user);

        return user.Id;
    }

    //public void Update(User user)
    //{
    //    _context.Entry(user).State = EntityState.Modified;
    //}

    //public void Delete(int id)
    //{
    //    var user = GetById(id);
    //    if (user != null)
    //    {
    //        _context.Users.Remove(user);
    //    }
    //}

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public Task<User> Update(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> Delete(int id)
    {
        throw new NotImplementedException();
    }

}