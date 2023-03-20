using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RRHH.API.Data.Entities;

namespace RRHH.API.Data;

public class DataContext : IdentityDbContext<AplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {    
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    #region Entities
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region Configure Cascade Delete
        var cascadeFKs = builder.Model.GetEntityTypes()
                                      .SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs) fk.DeleteBehavior = DeleteBehavior.Restrict;
        #endregion

        #region Keys/Constraints

        #endregion

    }

}