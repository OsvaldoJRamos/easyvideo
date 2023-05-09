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
                var nomeNovoCorte = txbNomeNovoCorte.Text.Replace(" ", "_");

                if (!string.IsNullOrEmpty(inicioNovoCorte.Replace(":", "").Trim()) && !string.IsNullOrEmpty(fimNovoCorte.Replace(":", "").Trim()))
                {
                    string descricaoExibicaoTela = $"{nomeNovoCorte} - {inicioNovoCorte} até {fimNovoCorte}";

                    var valorConvertido = ConversorTempoCorte.ConverterValorTela(descricaoExibicaoTela);
                    if (valorConvertido.DuracaoEmSegundos < TimeSpan.FromSeconds(1))
                    {
                        MessageBox.Show("O tempo fim deve ser maior que o tempo início");
                        return;
                    }

                    lbxTemposCortes.Items.Add(descricaoExibicaoTela + $" (total {valorConvertido.DuracaoEmSegundos}s)");

                    LimparCamposCriacaoNovoCorte();
                }

                this.ActiveControl = txbInicioNovoCorte;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar tempos. Verifique os valores digitados e tente novamente.");
                return;
            }
        }

        private void LimparCamposCriacaoNovoCorte()
        {
            txbInicioNovoCorte.Clear();
            txbFimNovoCorte.Clear();
            txbNomeNovoCorte.Clear();
        }

        private async void btnGerarCortes_Click(object sender, EventArgs e)
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

                BloquearOuDesbloquearTodosCampos();

                var totalCortes = temposCortesString.Count;
                ExibirProgressBar(quantidadeRegistros: totalCortes);

                await ProcessarCortes(temposCortesString, caminhoVideoOriginal, caminhoVideosCortados);

                MessageBox.Show($"Fim do processamento dos cortes. Confira se todos foram gerados com sucesso.");
                _logger.LogInformation($"Fim processamento de todos os cortes");

                BloquearOuDesbloquearTodosCampos();
                LimparCamposAposTerminarCortes();
                OcultarProgressBar();
            }
            catch (Exception ex)
            {
                BloquearOuDesbloquearTodosCampos();

                _logger.LogCritical($"Erro ao gerar cortes. Mensagem: {ex.Message}");
                MessageBox.Show($"Houve um erro ao gerar um ou mais. Verifique se eles foram gerados. Mensagem de erro: {ex.Message}");
            }
        }

        private void BloquearOuDesbloquearTodosCampos()
        {
            foreach (Control control in this.Controls)
            {
                control.Enabled = !control.Enabled;
            }

            lblBarraProgresso.Enabled = true;
            lblBarraProgresso.Enabled = true;
        }

        private void ExibirProgressBar(int quantidadeRegistros)
        {
            progressBar1.Maximum = quantidadeRegistros;
            progressBar1.Visible = true;
            lblBarraProgresso.Visible = true;
        }

        private void OcultarProgressBar()
        {
            progressBar1.Value = 0;
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

        private async Task<int> ProcessarCortes(ListBox.ObjectCollection temposCortesString, string caminhoVideoOriginal, string caminhoVideosCortados, CancellationToken cancellationToken = default(CancellationToken))
        {
            int numeroCorte = 0;
            foreach (var tempoCorteString in temposCortesString)
            {
                numeroCorte++;

                _logger.LogInformation($"Inicio processamento corte {numeroCorte}");

                var tempoCorte = ConversorTempoCorte.ConverterValorTela(tempoCorteString.ToString());

                var caminhoVideoCortadoTamanhoOriginal = caminhoVideosCortados + $"\\{tempoCorte.Nome ?? numeroCorte.ToString()}.mp4";
                await _gerenciadorCortes.CortarTamanhoOriginal(tempoCorte.TempoInicio, tempoCorte.DuracaoEmSegundos, caminhoVideoOriginal, caminhoVideoCortadoTamanhoOriginal, cancellationToken);

                if (cbxGerarNaResolucao9por16.Checked)
                    await _gerenciadorCortes.CortarTamanho9Por16(caminhoVideoCortadoTamanhoOriginal, caminhoVideoCortadoTamanhoOriginal.Replace(".mp4", "") + "-9por16.mp4", cbxMarcaDagua.Checked, cancellationToken);

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
            if (lbxTemposCortes.Items.Count > 0)
                lbxTemposCortes.Items.RemoveAt(lbxTemposCortes.Items.Count - 1);
        }
    }
}