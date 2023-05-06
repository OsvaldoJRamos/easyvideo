namespace CortadorVideo.Services
{
    public class Validations
    {
        public bool TemErro { get; private set; } = false;
        public List<string> Erros { get; private set; } = new List<string>();

        public Validations ValidarSeTemOffmpeg()
        {
            if (!File.Exists(Constants.caminhoCompletoffmpeg))
            {
                TemErro = true;
                Erros.Add($"O ffmpeg não existe nessa máquina no  caminho {Constants.caminhoCompletoffmpeg}. Para baixa-lo acesse o link: {Constants.linkDownloadffmpeg}");
            }

            return this;
        }

        public Validations ValidarCaminhoVideoOriginal(string caminhoVideoOriginal)
        {
            if (!File.Exists(caminhoVideoOriginal))
            {
                TemErro = true;
                Erros.Add($"O vídeo não existe no caminho {caminhoVideoOriginal}.");
            }

            return this;
        }

        public Validations ValidarCaminhoVideosCortados(string caminhoVideosCortados)
        {
            if (!Path.Exists(caminhoVideosCortados))
            {
                TemErro = true;
                Erros.Add($"O caminho informado para gerar os vídeos cortados não existe. {caminhoVideosCortados}");
            }

            return this;
        }

        public Validations ValidarSeTemCortesParaProcessar(ListBox.ObjectCollection temposCortesString)
        {
            if (temposCortesString == null || temposCortesString.Count == 0)
            {
                TemErro = true;
                Erros.Add("Você não criou nenhum tempo de corte para ser feito");
            }

            return this;
        }

        public Validations()
        {
        }
    }
}
