using Domain.Entidades;

namespace Domain.Interfaces
{
    public interface ILocalizacaoRepository
    {
        public Task<IEnumerable<LocalizacaoEntidade?>> ListarLocalizacaoAsync();
        public Task<LocalizacaoEntidade?> ObterLocalizacaoAsync(int idLocalizacao);
        public Task InserirLocalizacaoAsync(LocalizacaoEntidade localizacaoEntidade);
        public Task AtualizarLocalizacaoAsync(LocalizacaoEntidade localizacaoEntidade);
    }
}
