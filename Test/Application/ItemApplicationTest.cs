using Application.Application;
using Application.Interfaces;
using Domain.Entidades;
using Domain.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Application
{
    public class ItemApplicationTest
    {
        private Mock<IItemRepository> _itemRepositoryMock;
        [Test]
        public async Task ListarItens_QuandoExistirRegistros_RetornaListaPreenchida()
        {
            //Arrange
            IItemApplication itemApplication = ObterApplication();
            var dataAtual = DateTime.Now;
            _itemRepositoryMock.Setup(m => m.ListarItemAsync()).ReturnsAsync(new List<ItemEntidade> { new ItemEntidade(1, "Cadeira", "No canto inferior", "C:/img", "123456", dataAtual) });

            //Act
            var resultado = await itemApplication.ListarAsync();
            var itemResponse = resultado.Objeto?.FirstOrDefault();
            
            //Assert
            Assert.That(resultado.EhSucesso, Is.True);
            Assert.NotNull(itemResponse);
            Assert.That(itemResponse.IdItem, Is.EqualTo(1));
            Assert.That(itemResponse.NomeItem, Is.EqualTo("Cadeira"));
            Assert.That(itemResponse.DescricaoDetalhada, Is.EqualTo("No canto inferior"));
            Assert.NotNull(itemResponse.Imagem);
            Assert.That(itemResponse.Imagem, Is.EqualTo("C:/img"));
            Assert.NotNull(itemResponse.MatriculaAlteracao);
            Assert.That(itemResponse.MatriculaAlteracao, Is.EqualTo("123456"));
            Assert.That(itemResponse.DataAlteracao, Is.EqualTo(dataAtual));
            _itemRepositoryMock.Verify(m => m.ListarItemAsync(), Times.Once);
        }

        public async Task ListarItens_QuandoNaoExistirRegistros_RetornaListaPreenchida()
        {
            //Arrange
            IItemApplication itemApplication = ObterApplication();
            var dataAtual = DateTime.Now;
            _itemRepositoryMock.Setup(m => m.ListarItemAsync()).ReturnsAsync(new List<ItemEntidade> { });

            //Act
            var resultado = await itemApplication.ListarAsync();
            var itemResponse = resultado.Objeto;

            //Assert
            Assert.That(resultado.EhSucesso, Is.True);
            Assert.NotNull(itemResponse);
            Assert.IsEmpty(itemResponse);
            _itemRepositoryMock.Verify(m => m.ListarItemAsync(), Times.Once);
        }

        private IItemApplication ObterApplication()
        {
            _itemRepositoryMock = new Mock<IItemRepository>();
            return new ItemApplication(_itemRepositoryMock.Object);
        }
    }
}
