using GameProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GameProject.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    
    public DbSet<Genre> Genres => Set<Genre>();
    
    
}