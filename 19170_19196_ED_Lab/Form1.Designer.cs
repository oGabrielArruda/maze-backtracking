namespace _19170_19196_ED_Lab
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvLabirinto = new System.Windows.Forms.DataGridView();
            this.dgvCaminhosEncontrados = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFindWays = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.dlgAbrirArquivo = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabirinto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaminhosEncontrados)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLabirinto
            // 
            this.dgvLabirinto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLabirinto.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLabirinto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabirinto.Location = new System.Drawing.Point(12, 95);
            this.dgvLabirinto.Name = "dgvLabirinto";
            this.dgvLabirinto.Size = new System.Drawing.Size(404, 390);
            this.dgvLabirinto.TabIndex = 0;
            // 
            // dgvCaminhosEncontrados
            // 
            this.dgvCaminhosEncontrados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCaminhosEncontrados.Location = new System.Drawing.Point(422, 95);
            this.dgvCaminhosEncontrados.Name = "dgvCaminhosEncontrados";
            this.dgvCaminhosEncontrados.Size = new System.Drawing.Size(577, 390);
            this.dgvCaminhosEncontrados.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Labirinto:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(419, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Caminhos encontrados:";
            // 
            // btnFindWays
            // 
            this.btnFindWays.Location = new System.Drawing.Point(927, 13);
            this.btnFindWays.Name = "btnFindWays";
            this.btnFindWays.Size = new System.Drawing.Size(75, 76);
            this.btnFindWays.TabIndex = 4;
            this.btnFindWays.Text = "Encontrar Caminhos";
            this.btnFindWays.UseVisualStyleBackColor = true;
            this.btnFindWays.Click += new System.EventHandler(this.btnFindWays_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(846, 12);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 76);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "Abrir Arquivo";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // dlgAbrirArquivo
            // 
            this.dlgAbrirArquivo.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 487);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnFindWays);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCaminhosEncontrados);
            this.Controls.Add(this.dgvLabirinto);
            this.Name = "Form1";
            this.Text = "Caminhos em labirinto";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabirinto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaminhosEncontrados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLabirinto;
        private System.Windows.Forms.DataGridView dgvCaminhosEncontrados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFindWays;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog dlgAbrirArquivo;
    }
}

