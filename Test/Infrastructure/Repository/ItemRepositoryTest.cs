using Infrastructure.Model;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Test.Infrastructure.Repository
{
    public class ItemRepositoryTest : IDisposable
    {
        private readonly DbContextOptions<SqlConext> dbContextOptions = new DbContextOptionsBuilder<SqlConext>()
        .UseInMemoryDatabase(databaseName: "database-name")
        .Options;

        [TearDown]
        public void Dispose()
        {
            using var context = new SqlConext(dbContextOptions);

            context.Itens.ForEachAsync(obj => { context.Itens.Remove(obj); });
            context.SaveChanges();
        }

        [Test]
        public async Task WhenListItem_IfListAItemEmpty()
        {
            await Task.Delay(1000);
            var repositorory = new ItemRepository(new SqlConext(dbContextOptions));
            var result = await repositorory.ListarItemAsync();

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task WhenListItem_IfListFilled()
        {
            InsertItemInDatabase(new ItemDbModel[] 
            { 
                new ItemDbModel 
                { 
                    IdItem = 1, DataAlteracao = DateTime.Now, IdLocalizacao = 1, DescricaoDetalhada = "", Imagem = "", MatriculaAlteracao = "", NomeItem = "" 
                } 
            });

            var repositorory = new ItemRepository(new SqlConext(dbContextOptions));

            var result = await repositorory.ListarItemAsync();

            Assert.IsNotEmpty(result);

            await Task.Delay(3000);
        }

        private void InsertItemInDatabase(IEnumerable<ItemDbModel> itens)
        {
            using var context = new SqlConext(dbContextOptions);
            context.Itens.AddRange(itens);
            context.SaveChanges();
        }

    }
}
