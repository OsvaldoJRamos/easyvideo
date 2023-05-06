using CortadorVideo.Services;
using FFmpeg.NET;
using System.Diagnostics;

namespace CortadorVideo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            progressBar1.Visible = false;
            lblBarraProgresso.Visible = false;
        }

        private void btnAdicionarNovoCorte_Click(object sender, EventArgs e)
        {
            var inicioNovoCorte = txbInicioNovoCorte.Text;
            var fimNovoCorte = txbFimNovoCorte.Text;

            if (!string.IsNullOrEmpty(inicioNovoCorte.Replace(":", "").Trim()) && !string.IsNullOrEmpty(fimNovoCorte.Replace(":", "").Trim()))
            {

                lbxTemposCortes.Items.Add($"{inicioNovoCorte} - {fimNovoCorte}");

                txbInicioNovoCorte.Clear();
                txbFimNovoCorte.Clear();
            }

            this.ActiveControl = txbInicioNovoCorte;
        }

        private void btnGerarCortes_Click(object sender, EventArgs e)
        {
            try
            {
                var temposCortesString = lbxTemposCortes.Items;

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

                MessageBox.Show($"Foram feitos {numeroCortes} de {totalCortes} cortes. Caminho de criação: {caminhoVideosCortados}");

                LimparCamposAposTerminarCortes();
                OcultarProgressBar();
            }
            catch (Exception ex)
            {
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

                var tempoCorte = ConversorTempoCorte.Converter(tempoCorteString.ToString());
                var tempoInicio = tempoCorte.TempoInicio;
                var tempoDuracao = tempoCorte.DuracaoEmSegundos;

                var caminhoVideoCortadoTamanhoOriginal = caminhoVideosCortados + $"\\{numeroCorte}.mp4";
                GerenciadorCortes.CortarTamanhoOriginal(tempoInicio, tempoDuracao, caminhoVideoOriginal, caminhoVideoCortadoTamanhoOriginal);

                if (cbxMarcaDagua.Checked)
                    GerenciadorCortes.GerarComMarcaDagua(caminhoVideoCortadoTamanhoOriginal, caminhoVideoCortadoTamanhoOriginal.Replace(".mp4", "") + "-marcaDagua.mp4");

                if (cbxGerarNaResolucao9por16.Checked)
                    GerenciadorCortes.CortarTamanho9Por16(caminhoVideoCortadoTamanhoOriginal, caminhoVideoCortadoTamanhoOriginal.Replace(".mp4", "") + "-9por16.mp4", cbxMarcaDagua.Checked);

                IncrementarProgressBar();
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