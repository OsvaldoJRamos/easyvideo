namespace CortadorVideo.Services
{
    public interface IGerenciadorCortes
    {
        public Task CortarTamanho9Por16(string caminhoVideoOriginal, string nomeNovoArquivo, bool gerarMarcaDagua, CancellationToken cancellationToken);
        public Task CortarTamanhoOriginal(TimeSpan tempoInicioTimeSpan, TimeSpan tempoDuracaoCorte, string caminhoVideoOriginal, string caminhoNovoVideo, CancellationToken cancellationToken);
        public Task GerarComMarcaDagua(string caminhoVideoOriginal, string nomeNovoArquivo, CancellationToken cancellationToken);
    }
}
