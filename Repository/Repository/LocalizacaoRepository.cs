using Domain.Entidades;
using Domain.Interfaces;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly SqlConext _sqlConext;

        public LocalizacaoRepository(SqlConext sqlConext)
        {
            _sqlConext = sqlConext;
        }

        public async Task AtualizarLocalizacaoAsync(LocalizacaoEntidade localizacaoEntidade)
        {
            var localizacaoDbModel = await _sqlConext.Localizacoes.SingleOrDefaultAsync(m => m.IdLocalizacao == localizacaoEntidade.IdLocalizacao.Valor);
            if (localizacaoDbModel != null)
            {
                localizacaoDbModel.NomeLocalizacao = localizacaoEntidade.Nome;
                localizacaoDbModel.MatriculaAlteracao = localizacaoEntidade.MatriculaAlteracao;
                localizacaoDbModel.DataAlteracao = DateTime.Now;
            }

            await _sqlConext.SaveChangesAsync();
        }

        public async Task InserirLocalizacaoAsync(LocalizacaoEntidade localizacaoEntidade)
        {
            await _sqlConext.Localizacoes.AddAsync(MapearLocalizacaoEntidadeParaDbModel(localizacaoEntidade));
            await _sqlConext.SaveChangesAsync();
        }

        public async Task<IEnumerable<LocalizacaoEntidade?>> ListarLocalizacaoAsync()
        {
            var localizacoes = await _sqlConext.Localizacoes.ToListAsync();
            return MapearLocalizacaoDbModelParaEntidade(localizacoes);
        }

        public async Task<LocalizacaoEntidade?> ObterLocalizacaoAsync(int idLocalizacao)
        {
            var localizacaoDbModel = await _sqlConext.Localizacoes.SingleOrDefaultAsync(m => m.IdLocalizacao == idLocalizacao);

            return MapearLocalizacaoDbModelParaEntidade(localizacaoDbModel);
        }

        private static LocalizacaoEntidade? MapearLocalizacaoDbModelParaEntidade(LocalizacaoDbModel? localizacaoDbModel)
        {
            if (localizacaoDbModel == null)
                return null;

            return new LocalizacaoEntidade(localizacaoDbModel.IdLocalizacao, localizacaoDbModel.NomeLocalizacao, localizacaoDbModel.DataAlteracao, localizacaoDbModel.MatriculaAlteracao);
        }

        private static IEnumerable<LocalizacaoEntidade?> MapearLocalizacaoDbModelParaEntidade(IEnumerable<LocalizacaoDbModel?> localizacaoDbModel)
        {
            foreach (var localizacao in localizacaoDbModel)
            {
                yield return MapearLocalizacaoDbModelParaEntidade(localizacao);
            }
        }

        private static LocalizacaoDbModel MapearLocalizacaoEntidadeParaDbModel(LocalizacaoEntidade localizacaoEntidade)
        {
            return new LocalizacaoDbModel
            {
                IdLocalizacao = localizacaoEntidade.IdLocalizacao.Valor,
                NomeLocalizacao = localizacaoEntidade.Nome,
                MatriculaAlteracao = localizacaoEntidade.MatriculaAlteracao,
                DataAlteracao = localizacaoEntidade.DataAlteracao,
            };
        }
    }
}
