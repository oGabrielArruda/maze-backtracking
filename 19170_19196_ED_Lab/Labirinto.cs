using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _19170_19196_ED_Lab
{
    class Labirinto
    {
        private char[ , ] matriz;
        public Labirinto(string nomeArquivo)
        {
            lerArquivo(nomeArquivo);                
        }

        private void lerArquivo(string nomeArquivo)
        {
            StreamReader sr = new StreamReader(nomeArquivo);
            int colunas = int.Parse(sr.ReadLine());
            int linhas = int.Parse(sr.ReadLine());

            matriz = new char[linhas, colunas];

            for (int i = 0; i < linhas; i++)
            {
                string linhaArquivo = sr.ReadLine();
                for (int j = 0; j < colunas; j++)
                {

                    matriz[i, j] = linhaArquivo[j];
                }
            }
        }

        public void exibirLabirinto(DataGridView dgv)
        {
            ajustarDgv(dgv);
            for (int i = 0; i < matriz.GetLength(0); i++)
                for (int j = 0; j < matriz.GetLength(1); j++)
                    dgv.Rows[i].Cells[j].Value = matriz[i, j];
        }

        public void acharCaminhos(DataGridView dgvLabirinto, DataGridView dgvCaminhos)
        {
            ajustarDgv(dgvCaminhos);
            PilhaLista<Movimento> umCaminho = acharUmCaminho(dgvLabirinto);
            exibirCaminho(dgvCaminhos, umCaminho);
        }
        private PilhaLista<Movimento> acharUmCaminho(DataGridView dgv)
        {
            int[] movimentoLinha = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] movimentoColuna = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int linhaAtual = 1;
            int colunaAtual = 1;
            bool possivelSaida = true;

            exibirMovimento(dgv, linhaAtual, colunaAtual);

            PilhaLista<Movimento> pilhaMov = new PilhaLista<Movimento>();

            while (matriz[linhaAtual, colunaAtual] != 'S' && possivelSaida) // enquanto não encontrou a saída e há uma possível saída
            {
                bool seMoveu = false;
                for (int i = 0; i < movimentoLinha.Length && !seMoveu; i++) // enquanto há possíveis movimentos a serem feitos
                {
                    int possivelLinha = linhaAtual + movimentoLinha[i];
                    int possivelColuna = colunaAtual + movimentoColuna[i];

                    if (podeMover(possivelLinha, possivelColuna))
                    {
                        Movimento mov = new Movimento(linhaAtual, colunaAtual, possivelLinha, possivelColuna);
                        pilhaMov.Empilhar(mov);
                        mover(ref linhaAtual, ref colunaAtual, mov);
                        exibirMovimento(dgv, linhaAtual, colunaAtual);
                        seMoveu = true;
                    }
                }

                if (!seMoveu) // se está preso em uma parte do labirinto, volta-se um movimento
                {
                    if (pilhaMov.EstaVazia)
                        possivelSaida = false;
                    else
                    {
                        Movimento mov = pilhaMov.Desempilhar();
                        linhaAtual = mov.LinhaOrigem;
                        colunaAtual = mov.ColunaOrigem;
                    }
                }
            }

            return pilhaMov;
        }

        bool podeMover(int possivelLinha, int possivelColuna)
        {
            return matriz[possivelLinha, possivelColuna] == ' ' || matriz[possivelLinha, possivelColuna] == 'S';
        }
        private void mover(ref int linhaAtual, ref int colunaAtual, Movimento mov)
        {
            int possivelLinha = mov.LinhaDestino;
            int possivelColuna = mov.ColunaDestino;

            if (matriz[possivelLinha, possivelColuna] != 'S')
                matriz[possivelLinha, possivelColuna] = 'o';

            linhaAtual = possivelLinha;
            colunaAtual = possivelColuna;
        }

        private void exibirMovimento(DataGridView dgv, int linhaAtual, int colunaAtual)
        {
            dgv.Rows[linhaAtual].Cells[colunaAtual].Style.BackColor = Color.Green;
            Thread.Sleep(1000);
            Application.DoEvents();
        }

        private void exibirCaminho(DataGridView dgv,PilhaLista<Movimento> umCaminho)
        {
            int t = umCaminho.Tamanho;
            for(int i = t-1; i >= 0; i--)
            {
                Movimento mov = umCaminho.Desempilhar();
                dgv.Rows[0].Cells[i].Value = mov.LinhaDestino + "  " + mov.ColunaDestino;
            }
        }


        private void ajustarDgv(DataGridView dgv)
        {
            dgv.RowCount = matriz.GetLength(0);
            dgv.ColumnCount = matriz.GetLength(1);
            dgv.ColumnHeadersVisible = false;
            dgv.RowHeadersVisible = false;
        }
    }
}
