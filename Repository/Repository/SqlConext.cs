using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    public class SqlConext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public SqlConext()
        {
        }

        public SqlConext(DbContextOptions<SqlConext> options) : base(options)
        {

        }

        public DbSet<ItemDbModel> Itens { get; set; }
        public DbSet<LocalizacaoDbModel> Localizacoes { get; set; }

    }
}
