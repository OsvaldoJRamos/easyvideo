using FFmpeg.NET;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CortadorVideo.Services
{

    public class GerenciadorCortes : IGerenciadorCortes
    {
        private readonly ILogger<GerenciadorCortes> _logger;

        public GerenciadorCortes(ILogger<GerenciadorCortes> logger)
        {
            _logger = logger;
        }

        public void CortarTamanhoOriginal(TimeSpan tempoInicioTimeSpan, TimeSpan tempoDuracaoCorte, string caminhoVideoOriginal, string caminhoNovoVideo)
        {
            try
            {
                var ffmpeg = new Engine(Constants.caminhoCompletoffmpeg);
                var options = new ConversionOptions();

                var inputFile = new InputFile(caminhoVideoOriginal);
                var outputFile = new OutputFile(caminhoNovoVideo);

                options.CutMedia(tempoInicioTimeSpan, tempoDuracaoCorte);
                var result = ffmpeg.ConvertAsync(inputFile, outputFile, options, CancellationToken.None).Result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Erro ao cortar video em tamanho original. caminhoNovoVideo: {caminhoNovoVideo} tempoInicioTimeSpan: {tempoInicioTimeSpan} tempoDuracaoCorte: {tempoDuracaoCorte} erro: {ex.Message}");
            }
        }

        public void GerarComMarcaDagua(string caminhoVideoOriginal, string nomeNovoArquivo)
        {
            try
            {
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.WorkingDirectory = Constants.caminhoffmpeg;
                info.FileName = "cmd.exe";
                info.RedirectStandardInput = true;
                info.UseShellExecute = false;

                p.StartInfo = info;
                p.Start();

                using (StreamWriter sw = p.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine($"ffmpeg -i {caminhoVideoOriginal} -i {Constants.caminhoImagemMarcaDagua} -filter_complex \"overlay=main_w-overlay_w:main_h-overlay_h\" -c:a copy {nomeNovoArquivo}");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"Erro ao gerar com marca dagua. nomeNovoArquivo: {nomeNovoArquivo} erro: {ex.Message}");
            }
        }

        public void CortarTamanho9Por16(string caminhoVideoOriginal, string nomeNovoArquivo, bool gerarMarcaDagua)
        {
            try
            {
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.WorkingDirectory = Constants.caminhoffmpeg;
                info.FileName = "cmd.exe";
                info.RedirectStandardInput = true;
                info.UseShellExecute = false;

                p.StartInfo = info;
                p.Start();

                using (StreamWriter sw = p.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        if (gerarMarcaDagua)
                            sw.WriteLine($"ffmpeg -i {caminhoVideoOriginal} -vf crop=ih*(9/16):ih {nomeNovoArquivo} && ffmpeg -i {nomeNovoArquivo} -i C:\\Users\\osval\\Downloads\\200SemFundo.png -filter_complex overlay=main_w-overlay_w:main_h-overlay_h -c:a copy {nomeNovoArquivo.Replace(".mp4", "")}-marcaDagua.mp4");
                        else
                            sw.WriteLine($"ffmpeg -i {caminhoVideoOriginal} -vf crop=ih*(9/16):ih {nomeNovoArquivo}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Erro ao cortar tamanho 9 por 16. nomeNovoArquivo: {nomeNovoArquivo} gerarMarcaDagua: {gerarMarcaDagua} erro: {ex.Message}");
            }
        }
    }
}
