using Domain.Core;
using Domain.Entidades;
using Domain.Entidades.Validators;
using FluentValidation;

namespace Test.Domain.Entidade
{
    public class ItemValidatorTest
    {


        [TestCase(1, "aa", "XX", "bb", "12")]
        [TestCase(1, "aa", "XX", null, null)]
        public void ValidarItemEntidade_ParametrosValidos_Ok(int idItem, string nomeItem, string descricaoItem, string? imagem, string? matricula)
        {
            var item = new ItemEntidade(idItem, nomeItem, descricaoItem, imagem, matricula, DateTime.Now);
            var validator1 = new ItemValidator();
            var result1 = validator1.Validate(item);
            Assert.That(result1.IsValid, Is.True);
        }

        [Test]
        public void ValidarIdItemEntidade_ParametroValidos_Ok()
        {
            var item = new ItemEntidade { IdItem = new IdNumerico(1)};
            var validator1 = new ItemValidator();

            var result = validator1.Validate(item, options =>
            {
                options.IncludeProperties(x => x.IdItem);
            });

            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void ValidarItemEntidade_QuantidadeCaracterInvalida_Erro()
        {
            string nomeItem = "Nam quis nulla. Integer malesuada. In in eniml";
            string descricaoItem = "Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio ";
            string imagem = "Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio urna, tempus molestie, porttitor ut, iaculis quis, sem. Phasellus rhoncus. Aenean id metus id velits";
            string matricula = "12345678912";

            var item = new ItemEntidade(1, nomeItem, descricaoItem, imagem, matricula, DateTime.Now);
            var validator1 = new ItemValidator();
            var result1 = validator1.Validate(item);
            Assert.That(result1.IsValid, Is.False);
            Assert.Contains("DescricaoDetalhada_erro_caracter", result1.Errors.Select(e => e.ErrorCode).ToList());
            Assert.Contains("Imagem_erro_caracter", result1.Errors.Select(e => e.ErrorCode).ToList());
            Assert.Contains("MatriculaAlteracao_erro_caracter", result1.Errors.Select(e => e.ErrorCode).ToList());
            Assert.Contains("NomeItem_erro_caracter", result1.Errors.Select(e => e.ErrorCode).ToList());

        }

        [Test]
        public void ValidarItemEntidade_IdItemInvalidos_Erro()
        {
            var item = new ItemEntidade(-1, "", "", "", "", DateTime.Now);
            var validator1 = new ItemValidator();
            var result1 = validator1.Validate(item);

            Assert.That(result1.IsValid, Is.False);
            Assert.Contains("IdNumerico_erro_negativo", result1.Errors.Select(e => e.ErrorCode).ToList());
        }

    }
}
