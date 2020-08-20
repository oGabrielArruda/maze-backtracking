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
            PilhaLista<Coordenada> umCaminho = acharUmCaminho(dgvLabirinto);
            exibirCaminho(dgvCaminhos, umCaminho);
        }
        private PilhaLista<Coordenada> acharUmCaminho(DataGridView dgv)
        {
            int[] movimentoLinha = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] movimentoColuna = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int linhaAtual = 1;
            int colunaAtual = 1;
            bool possivelSaida = true;

            exibirMovimento(dgv, linhaAtual, colunaAtual, true);

            PilhaLista<Coordenada> pilhaMovimentos = new PilhaLista<Coordenada>();
            pilhaMovimentos.Empilhar(new Coordenada(1, 1));

            while (matriz[linhaAtual, colunaAtual] != 'S' && possivelSaida) // enquanto não encontrou a saída e há uma possível saída
            {
                bool seMoveu = false;
                for (int i = 0; i < movimentoLinha.Length && !seMoveu; i++) // enquanto há possíveis movimentos a serem feitos
                {
                    int possivelLinha = linhaAtual + movimentoLinha[i];
                    int possivelColuna = colunaAtual + movimentoColuna[i];

                    if (podeMover(possivelLinha, possivelColuna))
                    {
                        Coordenada coord = new Coordenada(possivelLinha, possivelColuna);
                        pilhaMovimentos.Empilhar(coord);
                        mover(ref linhaAtual, ref colunaAtual, coord);
                        exibirMovimento(dgv, linhaAtual, colunaAtual, true);
                        seMoveu = true;
                    }
                }

                if (!seMoveu) // se está preso em uma parte do labirinto, volta-se um movimento
                {
                    if (pilhaMovimentos.EstaVazia)
                        possivelSaida = false;
                    else
                    {
                        exibirMovimento(dgv, linhaAtual, colunaAtual, false);
                        pilhaMovimentos.Desempilhar();
                        Coordenada coord = pilhaMovimentos.OTopo();
                        linhaAtual = coord.Linha;
                        colunaAtual = coord.Coluna;
                    }
                }
            }
            return pilhaMovimentos;
        }

        bool podeMover(int possivelLinha, int possivelColuna)
        {
            return matriz[possivelLinha, possivelColuna] == ' ' || matriz[possivelLinha, possivelColuna] == 'S';
        }
        private void mover(ref int linhaAtual, ref int colunaAtual, Coordenada coord)
        {
            int possivelLinha = coord.Linha;
            int possivelColuna = coord.Coluna;

            if (matriz[possivelLinha, possivelColuna] != 'S')
                matriz[possivelLinha, possivelColuna] = 'o';

            linhaAtual = possivelLinha;
            colunaAtual = possivelColuna;
        }

        private void exibirMovimento(DataGridView dgv, int linhaAtual, int colunaAtual, bool avanco)
        {
            if(avanco)
                dgv.Rows[linhaAtual].Cells[colunaAtual].Style.BackColor = Color.Green;
            else
                dgv.Rows[linhaAtual].Cells[colunaAtual].Style.BackColor = Color.White;
            Thread.Sleep(300);
            Application.DoEvents();
        }

        private void exibirCaminho(DataGridView dgv, PilhaLista<Coordenada> umCaminho)
        {
            int t = umCaminho.Tamanho;
            dgv.ColumnCount = 100;
            for(int i = t-1; i >= 0; i--)
            {
                Coordenada coord = umCaminho.Desempilhar();
                dgv.Rows[0].Cells[i].Value = "Linha: " + coord.Linha + "  Coluna: " + coord.Coluna;
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
