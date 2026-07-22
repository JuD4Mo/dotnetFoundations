using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
    {
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

      services.AddScoped<IRepository<PersonEntity, Guid>, PersonRepository>();
      services.AddScoped<ICodeRepository<PersonEntity>, PersonRepository>();

      return services;
    }
  }
}
