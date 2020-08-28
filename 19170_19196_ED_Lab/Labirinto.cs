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
        List<PilhaLista<Coordenada>> caminhosPossiveis;
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

        public void exibirCaminho(DataGridView dgv, int indexCaminho)
        {
            PilhaLista<Coordenada> caminho = caminhosPossiveis[indexCaminho].Clone();
            for (int i = 0; i < matriz.GetLength(0); i++)
                for (int j = 0; j < matriz.GetLength(1); j++)
                    dgv.Rows[i].Cells[j].Style.BackColor = Color.White;

            while (!caminho.EstaVazia)
            {
                Coordenada coord = caminho.Desempilhar();
                dgv.Rows[coord.Linha].Cells[coord.Coluna].Style.BackColor = Color.Green;
            }

            Application.DoEvents();
        }

        public void acharCaminhos(DataGridView dgvLabirinto, DataGridView dgvCaminhos)
        {            
            caminhosPossiveis = listaCaminhos(dgvLabirinto);
            ajustarDgv(dgvCaminhos);
            int qtdCaminhos = 0;
            foreach(PilhaLista<Coordenada> caminho in caminhosPossiveis)                                       
                exibirDadosCaminho(dgvCaminhos, caminho.Clone(), qtdCaminhos++);                        
        }

        private List<PilhaLista<Coordenada>> listaCaminhos(DataGridView dgv)
        {
            int linhaAtual = 1, colunaAtual = 1;
            List<PilhaLista<Coordenada>> caminhosEncontrados = new List<PilhaLista<Coordenada>>();
            PilhaLista<Coordenada> pilhaMovimentos = new PilhaLista<Coordenada>();
            pilhaMovimentos.Empilhar(new Coordenada(1, 1));
            exibirMovimento(dgv, 1, 1, true);

            

            int[] movimentoLinha = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] movimentoColuna = { 0, 1, 1, 1, 0, -1, -1, -1 };
            bool temCaminhoPossivel = true;

            while(temCaminhoPossivel)
            {
                bool seMoveu = false;
                for (int i = 0; i < movimentoLinha.Length && !seMoveu; i++)
                {
                    int possivelLinha = linhaAtual + movimentoLinha[i];
                    int possivelColuna = colunaAtual + movimentoColuna[i];

                    char valorPosicao = matriz[possivelLinha, possivelColuna];
                    if (estaVazio(valorPosicao))
                        mover(possivelLinha, possivelColuna, ref linhaAtual, ref colunaAtual, dgv, pilhaMovimentos, ref seMoveu);
                    else if (ehSaida(valorPosicao))
                        salvarCaminho(caminhosEncontrados, pilhaMovimentos, possivelLinha, possivelColuna, dgv);
                }

                if(!seMoveu)
                {
                    Coordenada coordAtual = pilhaMovimentos.Desempilhar();
                    exibirMovimento(dgv, coordAtual.Linha, coordAtual.Coluna, false);

                    if (pilhaMovimentos.EstaVazia)
                        temCaminhoPossivel = false;
                    else
                    {
                        Coordenada coordAntiga = pilhaMovimentos.OTopo();
                        linhaAtual = coordAntiga.Linha;
                        colunaAtual = coordAntiga.Coluna;
                    }
                } 
            }

            return caminhosEncontrados;
        }

        private bool estaVazio(char c) => c == ' ';
        private bool ehSaida(char c) => c == 'S';
        private void mover(int possivelLinha, int possivelColuna, ref int linhaAtual, ref int colunaAtual, DataGridView dgv, PilhaLista<Coordenada> pilhaMovimentos, ref bool seMoveu)
        {
            linhaAtual = possivelLinha;
            colunaAtual = possivelColuna;
            pilhaMovimentos.Empilhar(new Coordenada(linhaAtual, colunaAtual));
            seMoveu = true;
            matriz[linhaAtual, colunaAtual] = 'o';
            exibirMovimento(dgv, linhaAtual, colunaAtual, true);
        }

        private void salvarCaminho(List<PilhaLista<Coordenada>> listaCaminhos, PilhaLista<Coordenada> pilhaMovimentos, int linhaFinal, int colunaFinal, DataGridView dgv)
        {
            pilhaMovimentos.Empilhar(new Coordenada(linhaFinal, colunaFinal));
            listaCaminhos.Add(pilhaMovimentos.Clone());
            exibirMovimento(dgv, linhaFinal, colunaFinal, true);
            exibirMovimento(dgv, linhaFinal, colunaFinal, false);
            pilhaMovimentos.Desempilhar();
        }

        private void exibirMovimento(DataGridView dgv, int linhaAtual, int colunaAtual, bool avanco)
        {
            if (avanco)
                dgv.Rows[linhaAtual].Cells[colunaAtual].Style.BackColor = Color.Green;
            else
                dgv.Rows[linhaAtual].Cells[colunaAtual].Style.BackColor = Color.White;
            Thread.Sleep(10);
            Application.DoEvents();
        }

        private void exibirDadosCaminho(DataGridView dgv, PilhaLista<Coordenada> umCaminho, int linha)
        {
            int t = umCaminho.Tamanho;
            dgv.ColumnCount = 100;
            for (int i = t - 1; i >= 0; i--)
            {
                Coordenada coord = umCaminho.Desempilhar();
                dgv.Rows[linha].Cells[i].Value = "Linha: " + coord.Linha + "  Coluna: " + coord.Coluna;
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
