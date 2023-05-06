namespace CortadorVideo.Services
{
    public interface IGerenciadorCortes
    {
        public void CortarTamanho9Por16(string caminhoVideoOriginal, string nomeNovoArquivo, bool gerarMarcaDagua);
        public void CortarTamanhoOriginal(TimeSpan tempoInicioTimeSpan, TimeSpan tempoDuracaoCorte, string caminhoVideoOriginal, string caminhoNovoVideo);
        public void GerarComMarcaDagua(string caminhoVideoOriginal, string nomeNovoArquivo);
    }
}
