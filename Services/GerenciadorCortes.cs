using FFmpeg.NET;
using System.Diagnostics;

namespace CortadorVideo.Services
{
    public static class GerenciadorCortes
    {
        public static void CortarTamanhoOriginal(TimeSpan tempoInicioTimeSpan, TimeSpan tempoDuracaoCorte, string caminhoVideoOriginal, string caminhoNovoVideo)
        {
            var ffmpeg = new Engine(Constants.caminhoCompletoffmpeg);
            var options = new ConversionOptions();

            var inputFile = new InputFile(caminhoVideoOriginal);
            var outputFile = new OutputFile(caminhoNovoVideo);

            options.CutMedia(tempoInicioTimeSpan, tempoDuracaoCorte);
            var result = ffmpeg.ConvertAsync(inputFile, outputFile, options, CancellationToken.None).Result;
        }

        public static void GerarComMarcaDagua(string caminhoVideoOriginal, string nomeNovoArquivo)
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

        public static void CortarTamanho9Por16(string caminhoVideoOriginal, string nomeNovoArquivo, bool gerarMarcaDagua)
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
    }
}
