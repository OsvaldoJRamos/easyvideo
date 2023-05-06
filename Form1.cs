using CortadorVideo.Log;
using CortadorVideo.Services;
using FFmpeg.NET;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text;

namespace CortadorVideo
{
    public partial class Form1 : Form
    {
        private readonly ILogger<Form1> _logger;
        private readonly IGerenciadorCortes _gerenciadorCortes;

        public Form1(
            ILoggerFactory loggerFactory,
            ILogger<Form1> logger,
            IGerenciadorCortes gerenciadorCortes)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logs"));
            _logger = logger;
            _gerenciadorCortes = gerenciadorCortes;

            InitializeComponent();

            progressBar1.Visible = false;
            lblBarraProgresso.Visible = false;
        }

        private void btnAdicionarNovoCorte_Click(object sender, EventArgs e)
        {
            try
            {
                var inicioNovoCorte = txbInicioNovoCorte.Text;
                var fimNovoCorte = txbFimNovoCorte.Text;

                if (!string.IsNullOrEmpty(inicioNovoCorte.Replace(":", "").Trim()) && !string.IsNullOrEmpty(fimNovoCorte.Replace(":", "").Trim()))
                {
                    string temposJuntos = $"{inicioNovoCorte} - {fimNovoCorte}";

                    var valorConvertido = ConversorTempoCorte.Converter(temposJuntos);
                    if (valorConvertido.DuracaoEmSegundos < TimeSpan.FromSeconds(1))
                    {
                        MessageBox.Show("O tempo fim deve ser maior que o tempo início");
                        return;
                    }

                    lbxTemposCortes.Items.Add(temposJuntos);

                    txbInicioNovoCorte.Clear();
                    txbFimNovoCorte.Clear();
                }

                this.ActiveControl = txbInicioNovoCorte;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar tempos. Verifique os valores digitados e tente novamente.");
                return;
            }
        }

        private void btnGerarCortes_Click(object sender, EventArgs e)
        {
            try
            {
                var temposCortesString = lbxTemposCortes.Items;

                _logger.LogInformation($"Começo geração cortes. Total: {temposCortesString.Count}");

                string caminhoVideoOriginal = txbCaminhoVideoOriginal.Text;
                string caminhoVideosCortados = txbCaminhoVideosCortados.Text;

                var validations = new Validations().
                                      ValidarSeTemOffmpeg().
                                      ValidarCaminhoVideoOriginal(caminhoVideoOriginal).
                                      ValidarCaminhoVideosCortados(caminhoVideosCortados).
                                      ValidarSeTemCortesParaProcessar(temposCortesString);

                if (validations.TemErro)
                {
                    MessageBox.Show(string.Join("\n", validations.Erros));
                    return;
                }

                var totalCortes = temposCortesString.Count;
                ExibirProgressBar(quantidadeRegistros: totalCortes);

                var numeroCortes = ProcessarCortes(temposCortesString, caminhoVideoOriginal, caminhoVideosCortados);

                MessageBox.Show($"Fim do processamento dos cortes. Confira se todos foram gerados com sucesso.");
                _logger.LogInformation($"Fim processamento de todos os cortes");

                LimparCamposAposTerminarCortes();
                OcultarProgressBar();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Erro ao gerar cortes. Mensagem: {ex.Message}");
                MessageBox.Show($"Houve um erro ao gerar um ou mais. Verifique se eles foram gerados. Mensagem de erro: {ex.Message}");
            }
        }

        private void ExibirProgressBar(int quantidadeRegistros)
        {
            progressBar1.Visible = true;
            progressBar1.Maximum = quantidadeRegistros;
            lblBarraProgresso.Visible = true;
        }

        private void OcultarProgressBar()
        {
            progressBar1.Maximum = 100;
            progressBar1.Visible = false;
            lblBarraProgresso.Visible = false;
        }

        private void LimparCamposAposTerminarCortes()
        {
            lbxTemposCortes.Items.Clear();
            txbCaminhoVideoOriginal.Clear();
            txbCaminhoVideosCortados.Clear();
        }

        private int ProcessarCortes(ListBox.ObjectCollection temposCortesString, string caminhoVideoOriginal, string caminhoVideosCortados)
        {
            int numeroCorte = 0;
            foreach (var tempoCorteString in temposCortesString)
            {
                numeroCorte++;

                _logger.LogInformation($"Inicio processamento corte {numeroCorte}");

                var tempoCorte = ConversorTempoCorte.Converter(tempoCorteString.ToString());
                var tempoInicio = tempoCorte.TempoInicio;
                var tempoDuracao = tempoCorte.DuracaoEmSegundos;

                var caminhoVideoCortadoTamanhoOriginal = caminhoVideosCortados + $"\\{numeroCorte}.mp4";
                _gerenciadorCortes.CortarTamanhoOriginal(tempoInicio, tempoDuracao, caminhoVideoOriginal, caminhoVideoCortadoTamanhoOriginal);

                if (cbxMarcaDagua.Checked)
                    _gerenciadorCortes.GerarComMarcaDagua(caminhoVideoCortadoTamanhoOriginal, caminhoVideoCortadoTamanhoOriginal.Replace(".mp4", "") + "-marcaDagua.mp4");

                if (cbxGerarNaResolucao9por16.Checked)
                    _gerenciadorCortes.CortarTamanho9Por16(caminhoVideoCortadoTamanhoOriginal, caminhoVideoCortadoTamanhoOriginal.Replace(".mp4", "") + "-9por16.mp4", cbxMarcaDagua.Checked);

                IncrementarProgressBar();

                _logger.LogInformation($"Fim processamento corte {numeroCorte}");
            }

            return numeroCorte;
        }

        private void IncrementarProgressBar()
        {
            progressBar1.Value += 1;
            lblBarraProgresso.Text = "Cortes Prontos = " + progressBar1.Value.ToString() + " de " + progressBar1.Maximum;
        }

        private void btnNavegarCaminhoOriginal_Click(object sender, EventArgs e)
        {
            var v1 = new OpenFileDialog();
            v1.Filter = "Video files (*.mp4)|*.mp4|All files (*.*)|*.*";

            if (v1.ShowDialog() == DialogResult.OK)
            {
                txbCaminhoVideoOriginal.Text = v1.FileName;

            }
        }

        private void btnNavegarCaminhoVideosCortados_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txbCaminhoVideosCortados.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnRemoverUltimo_Click(object sender, EventArgs e)
        {
            lbxTemposCortes.Items.RemoveAt(lbxTemposCortes.Items.Count - 1);
        }
    }
}