using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

//! Users jer tako hocemo da nam se zove tabela u bazi
    public DbSet<AppUser> Users
    {
        get; set;
    }
}
