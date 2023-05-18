﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs.Input;
using Repository;
using Utils.Enums;
using BCryptNet = BCrypt.Net.BCrypt;
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
        var user = await _context.Users.FindAsync(id);

        return user;
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
        user.Password = this.HashPassword(user.Password);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var addedUser = _context.Entry(user).Entity;

        return addedUser.Id;
    }

    public async Task<User> Update(UpdateUserDTO updateUserDTO)
    {
        var user = _mapper.Map<User>(updateUserDTO);

        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user != null)
        {
            user.Status = Status.Inactive;
            await _context.SaveChangesAsync();
            return user;
        }
        
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == username);
    }

    private string HashPassword(string password)
    {
        return BCryptNet.HashPassword(password);
    }

}