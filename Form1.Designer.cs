namespace CortadorVideo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txbCaminhoVideoOriginal = new System.Windows.Forms.TextBox();
            this.txbCaminhoVideosCortados = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdicionarNovoCorte = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbxTemposCortes = new System.Windows.Forms.ListBox();
            this.btnGerarCortes = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbNomeNovoCorte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbFimNovoCorte = new System.Windows.Forms.MaskedTextBox();
            this.txbInicioNovoCorte = new System.Windows.Forms.MaskedTextBox();
            this.btnNavegarCaminhoOriginal = new System.Windows.Forms.Button();
            this.btnNavegarCaminhoVideosCortados = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblBarraProgresso = new System.Windows.Forms.Label();
            this.btnRemoverUltimo = new System.Windows.Forms.Button();
            this.cbxGerarNaResolucao9por16 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxMarcaDagua = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Caminho do Vídeo Original";
            // 
            // txbCaminhoVideoOriginal
            // 
            this.txbCaminhoVideoOriginal.Location = new System.Drawing.Point(217, 17);
            this.txbCaminhoVideoOriginal.Name = "txbCaminhoVideoOriginal";
            this.txbCaminhoVideoOriginal.Size = new System.Drawing.Size(439, 23);
            this.txbCaminhoVideoOriginal.TabIndex = 1;
            // 
            // txbCaminhoVideosCortados
            // 
            this.txbCaminhoVideosCortados.Location = new System.Drawing.Point(217, 55);
            this.txbCaminhoVideosCortados.Name = "txbCaminhoVideosCortados";
            this.txbCaminhoVideosCortados.Size = new System.Drawing.Size(439, 23);
            this.txbCaminhoVideosCortados.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Caminho Para Criar Vídeos Cortados";
            // 
            // btnAdicionarNovoCorte
            // 
            this.btnAdicionarNovoCorte.Location = new System.Drawing.Point(28, 120);
            this.btnAdicionarNovoCorte.Name = "btnAdicionarNovoCorte";
            this.btnAdicionarNovoCorte.Size = new System.Drawing.Size(285, 48);
            this.btnAdicionarNovoCorte.TabIndex = 18;
            this.btnAdicionarNovoCorte.Text = "Adicionar Novo Corte";
            this.btnAdicionarNovoCorte.UseVisualStyleBackColor = true;
            this.btnAdicionarNovoCorte.Click += new System.EventHandler(this.btnAdicionarNovoCorte_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Início";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Fim";
            // 
            // lbxTemposCortes
            // 
            this.lbxTemposCortes.FormattingEnabled = true;
            this.lbxTemposCortes.ItemHeight = 15;
            this.lbxTemposCortes.Location = new System.Drawing.Point(20, 24);
            this.lbxTemposCortes.Name = "lbxTemposCortes";
            this.lbxTemposCortes.Size = new System.Drawing.Size(365, 184);
            this.lbxTemposCortes.TabIndex = 12;
            // 
            // btnGerarCortes
            // 
            this.btnGerarCortes.Location = new System.Drawing.Point(26, 314);
            this.btnGerarCortes.Name = "btnGerarCortes";
            this.btnGerarCortes.Size = new System.Drawing.Size(352, 47);
            this.btnGerarCortes.TabIndex = 13;
            this.btnGerarCortes.Text = "Gerar Cortes";
            this.btnGerarCortes.UseVisualStyleBackColor = true;
            this.btnGerarCortes.Click += new System.EventHandler(this.btnGerarCortes_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txbNomeNovoCorte);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txbFimNovoCorte);
            this.groupBox1.Controls.Add(this.txbInicioNovoCorte);
            this.groupBox1.Controls.Add(this.btnAdicionarNovoCorte);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 186);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cadastrar Novos Cortes";
            // 
            // txbNomeNovoCorte
            // 
            this.txbNomeNovoCorte.Location = new System.Drawing.Point(48, 67);
            this.txbNomeNovoCorte.Name = "txbNomeNovoCorte";
            this.txbNomeNovoCorte.Size = new System.Drawing.Size(265, 23);
            this.txbNomeNovoCorte.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Nome";
            // 
            // txbFimNovoCorte
            // 
            this.txbFimNovoCorte.Location = new System.Drawing.Point(216, 24);
            this.txbFimNovoCorte.Mask = "##:##:##";
            this.txbFimNovoCorte.Name = "txbFimNovoCorte";
            this.txbFimNovoCorte.Size = new System.Drawing.Size(55, 23);
            this.txbFimNovoCorte.TabIndex = 16;
            // 
            // txbInicioNovoCorte
            // 
            this.txbInicioNovoCorte.Location = new System.Drawing.Point(110, 24);
            this.txbInicioNovoCorte.Mask = "##:##:##";
            this.txbInicioNovoCorte.Name = "txbInicioNovoCorte";
            this.txbInicioNovoCorte.Size = new System.Drawing.Size(55, 23);
            this.txbInicioNovoCorte.TabIndex = 15;
            // 
            // btnNavegarCaminhoOriginal
            // 
            this.btnNavegarCaminhoOriginal.Location = new System.Drawing.Point(662, 17);
            this.btnNavegarCaminhoOriginal.Name = "btnNavegarCaminhoOriginal";
            this.btnNavegarCaminhoOriginal.Size = new System.Drawing.Size(65, 23);
            this.btnNavegarCaminhoOriginal.TabIndex = 18;
            this.btnNavegarCaminhoOriginal.Text = "Navegar";
            this.btnNavegarCaminhoOriginal.UseVisualStyleBackColor = true;
            this.btnNavegarCaminhoOriginal.Click += new System.EventHandler(this.btnNavegarCaminhoOriginal_Click);
            // 
            // btnNavegarCaminhoVideosCortados
            // 
            this.btnNavegarCaminhoVideosCortados.Location = new System.Drawing.Point(662, 55);
            this.btnNavegarCaminhoVideosCortados.Name = "btnNavegarCaminhoVideosCortados";
            this.btnNavegarCaminhoVideosCortados.Size = new System.Drawing.Size(65, 23);
            this.btnNavegarCaminhoVideosCortados.TabIndex = 17;
            this.btnNavegarCaminhoVideosCortados.Text = "Navegar";
            this.btnNavegarCaminhoVideosCortados.UseVisualStyleBackColor = true;
            this.btnNavegarCaminhoVideosCortados.Click += new System.EventHandler(this.btnNavegarCaminhoVideosCortados_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.IndianRed;
            this.progressBar1.Location = new System.Drawing.Point(44, 448);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(269, 23);
            this.progressBar1.TabIndex = 18;
            // 
            // lblBarraProgresso
            // 
            this.lblBarraProgresso.AutoSize = true;
            this.lblBarraProgresso.Location = new System.Drawing.Point(122, 430);
            this.lblBarraProgresso.Name = "lblBarraProgresso";
            this.lblBarraProgresso.Size = new System.Drawing.Size(105, 15);
            this.lblBarraProgresso.TabIndex = 19;
            this.lblBarraProgresso.Text = "Barra de Progresso";
            // 
            // btnRemoverUltimo
            // 
            this.btnRemoverUltimo.Location = new System.Drawing.Point(279, 214);
            this.btnRemoverUltimo.Name = "btnRemoverUltimo";
            this.btnRemoverUltimo.Size = new System.Drawing.Size(106, 28);
            this.btnRemoverUltimo.TabIndex = 20;
            this.btnRemoverUltimo.Text = "Remover Último";
            this.btnRemoverUltimo.UseVisualStyleBackColor = true;
            this.btnRemoverUltimo.Click += new System.EventHandler(this.btnRemoverUltimo_Click);
            // 
            // cbxGerarNaResolucao9por16
            // 
            this.cbxGerarNaResolucao9por16.AutoSize = true;
            this.cbxGerarNaResolucao9por16.Location = new System.Drawing.Point(20, 214);
            this.cbxGerarNaResolucao9por16.Name = "cbxGerarNaResolucao9por16";
            this.cbxGerarNaResolucao9por16.Size = new System.Drawing.Size(148, 19);
            this.cbxGerarNaResolucao9por16.TabIndex = 21;
            this.cbxGerarNaResolucao9por16.Text = "Gerar na resolução 9:16";
            this.cbxGerarNaResolucao9por16.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxMarcaDagua);
            this.groupBox2.Controls.Add(this.lbxTemposCortes);
            this.groupBox2.Controls.Add(this.cbxGerarNaResolucao9por16);
            this.groupBox2.Controls.Add(this.btnRemoverUltimo);
            this.groupBox2.Controls.Add(this.btnGerarCortes);
            this.groupBox2.Location = new System.Drawing.Point(362, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(403, 382);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cortes Já Cadastrados";
            // 
            // cbxMarcaDagua
            // 
            this.cbxMarcaDagua.AutoSize = true;
            this.cbxMarcaDagua.Location = new System.Drawing.Point(20, 239);
            this.cbxMarcaDagua.Name = "cbxMarcaDagua";
            this.cbxMarcaDagua.Size = new System.Drawing.Size(253, 19);
            this.cbxMarcaDagua.TabIndex = 22;
            this.cbxMarcaDagua.Text = "Gerar na resolução 9:16 com Marca D\'Água";
            this.cbxMarcaDagua.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 542);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblBarraProgresso);
            this.Controls.Add(this.btnNavegarCaminhoVideosCortados);
            this.Controls.Add(this.btnNavegarCaminhoOriginal);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txbCaminhoVideosCortados);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbCaminhoVideoOriginal);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Eazy Video";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox txbCaminhoVideoOriginal;
        private TextBox txbCaminhoVideosCortados;
        private Label label2;
        private Button btnAdicionarNovoCorte;
        private Label label4;
        private Label label5;
        private ListBox lbxTemposCortes;
        private Button btnGerarCortes;
        private GroupBox groupBox1;
        private MaskedTextBox txbFimNovoCorte;
        private MaskedTextBox txbInicioNovoCorte;
        private Button btnNavegarCaminhoOriginal;
        private Button btnNavegarCaminhoVideosCortados;
        private ProgressBar progressBar1;
        private Label lblBarraProgresso;
        private Button btnRemoverUltimo;
        private CheckBox cbxGerarNaResolucao9por16;
        private GroupBox groupBox2;
        private CheckBox cbxMarcaDagua;
        private Label label3;
        private TextBox txbNomeNovoCorte;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}