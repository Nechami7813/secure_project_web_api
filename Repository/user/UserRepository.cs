using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.user;

public class UserRepository : IUserRepository
{
    Product326075108Context productContext;
    public UserRepository(Product326075108Context product326075108Context)
    {
        productContext = product326075108Context;
    }

    public async Task<IEnumerable<User>> Get()
    {
        return await productContext.Users.ToListAsync();

    }
    public async Task<User> GetById(int id)
    {

        var user = await productContext.Users.FindAsync(id);
        return user;

    }
    public async Task<User> Register(User user)
    {
        await productContext.Users.AddAsync(user);
        await productContext.SaveChangesAsync();
        return user;

    }
    public async Task<User> Login(string userName, string password)
    {
        var f = await productContext.Users.FirstOrDefaultAsync(i => i.Email == userName && i.Password == password);
        return f;

    }
    public async Task<User> getUser(string email, string password)
    {
        User user = await productContext.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        return user;
    }
    public async Task<User> Put(int id, User user)
    {
        var userToUpdate = await productContext.Users.FirstOrDefaultAsync(i => i.UserId == (short)id);

        if (userToUpdate == null)
        {
            return null;
        }
        user.UserId = userToUpdate.UserId;
        productContext.Entry(userToUpdate).CurrentValues.SetValues(user);
        await productContext.SaveChangesAsync();
        return user;
    }
}

