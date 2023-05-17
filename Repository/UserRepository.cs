using Models;
using Repository;
using System.Data.Entity;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context)
    {
        _context = context;
    }

    public User GetById(int id)
    {
        return _context.Users.Find(id);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public void Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}