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
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if(dlgAbrirArquivo.ShowDialog() == DialogResult.OK)
            {
                string nomeArq = dlgAbrirArquivo.FileName;
                labirinto = new Labirinto(nomeArq);
                labirinto.exibirLabirinto(dgvLabirinto);
            }            
        }

        private void btnFindWays_Click(object sender, EventArgs e)
        {
            labirinto.acharCaminhos(dgvLabirinto, dgvCaminhosEncontrados);
        }
    }
}
