using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19170_19196_ED_Lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Labirinto labirinto;

        /**
         * Click do botão que abre o arquivo.
         * Responsável por abrir o arquivo 
         * e instanciar a classe Labirinto.
         */
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            limparDgv(dgvCaminhosEncontrados);
            limparDgv(dgvLabirinto);

            if(dlgAbrirArquivo.ShowDialog() == DialogResult.OK)
            {
                string nomeArq = dlgAbrirArquivo.FileName;
                labirinto = new Labirinto(nomeArq);
                exibirLabirinto();
            }            
        }

        /**
         * Click do botão de achar caminho.
         * Chama a função responsável por achar os caminhos,
         * e exibe os passos no data grid view.
         */
        private void btnFindWays_Click(object sender, EventArgs e)
        {
            labirinto.buscarCaminhos(dgvLabirinto);

            int qtd = labirinto.CaminhosPossiveis.Count;
            MessageBox.Show($"Foram achados {qtd} caminhos!");

            if(qtd > 0)
                exibirDadosCaminhos();
        }


        /**
         * Click na linha do DGV de movimentos feitos.
         * Chama a função que exibe o caminho da linha clickada.
         */
        private void dgvCaminhosEncontrados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha = dgvCaminhosEncontrados.CurrentCell.RowIndex;
            exibirCaminho(linha);           
        }

        /**
         * Função que limpa o data grid view.
         */
        private void limparDgv(DataGridView dgv)
        {
            dgv.DataSource = null;
            dgv.Rows.Clear();
        }

        /**
        * Função que exibe a matriz do labirinto em um DataGridView        
        */
        public void exibirLabirinto()
        {
            char[,] matrizLabirinto = labirinto.Matriz;

            dgvLabirinto.RowCount = matrizLabirinto.GetLength(0);
            dgvLabirinto.ColumnCount = matrizLabirinto.GetLength(1);
            dgvLabirinto.ColumnHeadersVisible = false;
            dgvLabirinto.RowHeadersVisible = false;
        

            for (int i = 0; i < matrizLabirinto.GetLength(0); i++)
                for (int j = 0; j < matrizLabirinto.GetLength(1); j++)
                    dgvLabirinto.Rows[i].Cells[j].Value = matrizLabirinto[i, j];
        }


        /**
         * Função para exibir o caminho no labirinto 
         * Esse método é chamado no click da linha no data grid view.
         */
        private void exibirCaminho(int nmrCaminho)
        {
            var caminho = labirinto.CaminhosPossiveis[nmrCaminho];
            var matriz = labirinto.Matriz;

            for (int i = 0; i < matriz.GetLength(0); i++)
                for (int j = 0; j < matriz.GetLength(1); j++)
                    dgvLabirinto.Rows[i].Cells[j].Style.BackColor = Color.White;

            while (!caminho.EstaVazia)
            {
                Coordenada coord = caminho.Desempilhar();
                dgvLabirinto.Rows[coord.Linha].Cells[coord.Coluna].Style.BackColor = Color.Green;
            }

            Application.DoEvents();
        }

        
        /**
         * Função responsável por exibir todos os caminhos encontrados, passo a passo,
         * no data grid view.
         */
        private void exibirDadosCaminhos()
        {
            int qtdCaminhos = 0;
            var caminhosPossiveis = labirinto.CaminhosPossiveis;
            dgvCaminhosEncontrados.RowCount = caminhosPossiveis.Count;


            foreach (PilhaLista<Coordenada> caminho in caminhosPossiveis)
            {
                int t = caminho.Tamanho;
                dgvCaminhosEncontrados.ColumnCount = 100;
                for (int i = t - 1; i >= 0; i--)
                {
                    Coordenada coord = caminho.Desempilhar();
                    dgvCaminhosEncontrados.Rows[qtdCaminhos].Cells[i].Value = coord;
                }
                qtdCaminhos++;
            }                
        }
    }
}
