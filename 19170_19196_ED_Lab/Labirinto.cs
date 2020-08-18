using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        private void ajustarDgv(DataGridView dgv)
        {
            dgv.RowCount = matriz.GetLength(0);
            dgv.ColumnCount = matriz.GetLength(1);
            dgv.ColumnHeadersVisible = false;
            dgv.RowHeadersVisible = false;
        }
        public void exibirLabirinto(DataGridView dgv)
        {
            ajustarDgv(dgv);
            for (int i = 0; i < matriz.GetLength(0); i++)
                for (int j = 0; j < matriz.GetLength(1); j++)
                    dgv.Rows[i].Cells[j].Value = matriz[i, j];
        }

        public void acharCaminhos(DataGridView dgv)
        {
            ajustarDgv(dgv);
            PilhaLista<Movimento> umCaminho = acharUmCaminho();
            exibirCaminho(dgv, umCaminho);
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
        private PilhaLista<Movimento> acharUmCaminho()
        {
            int[] movimentoLinha =  { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] movimentoColuna = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int linhaAtual = 1;
            int colunaAtual = 1;
            bool possivelSaida = true;


            PilhaLista<Movimento> pilhaMov = new PilhaLista<Movimento>();

            while(matriz[linhaAtual, colunaAtual] != 'S' && possivelSaida)
            {
                bool seMoveu = false;
                for(int i = 0; i < 8 && !seMoveu; i++)
                {
                    int possivelLinha = linhaAtual + movimentoLinha[i];
                    int possivelColuna = colunaAtual + movimentoColuna[i];

                    if(matriz[possivelLinha, possivelColuna] == ' ' || matriz[possivelLinha, possivelColuna] == 'S')
                    {
                        Movimento mov = new Movimento(linhaAtual, colunaAtual, possivelLinha, possivelColuna);
                        pilhaMov.Empilhar(mov);

                        if(matriz[possivelLinha, possivelColuna] != 'S')
                            matriz[possivelLinha, possivelColuna] = 'o';

                        linhaAtual = possivelLinha;
                        colunaAtual = possivelColuna;
                                                
                        seMoveu = true;
                    }
                }

                if (!seMoveu) 
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
    }
}
