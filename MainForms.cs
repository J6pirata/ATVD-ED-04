using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class MainForms : Form
    {
        private List<Contato> contatos;

        public MainForms()
        {
            InitializeComponent();
            contatos = new List<Contato>();
        }

        private void NovoButton_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void SalvarButton_Click(object sender, EventArgs e)
        {
            string nome = nomeTextBox.Text;
            string email = emailTextBox.Text;

            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(email))
            {
                DateTime dataNascimento;
                if (DateTime.TryParse(dataNascimentoTextBox.Text, out dataNascimento))
                {
                    Contato novoContato = new Contato(nome, email, dataNascimento);
                    contatos.Add(novoContato);
                    AtualizarListaContatos();
                    LimparCampos();
                }
                else
                {
                    MessageBox.Show("Formato de data inválido. Utilize o formato dd/mm/aaaa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nome e email são campos obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesquisarButton_Click(object sender, EventArgs e)
        {
            string emailPesquisa = emailTextBox.Text;
            Contato contatoEncontrado = contatos.Find(c => c.Email == emailPesquisa);

            if (contatoEncontrado != null)
            {
                nomeTextBox.Text = contatoEncontrado.Nome;
                dataNascimentoTextBox.Text = contatoEncontrado.DataNascimento.ToString("dd/MM/yyyy");
            }
            else
            {
                MessageBox.Show("Contato não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RemoverButton_Click(object sender, EventArgs e)
        {
            string emailRemover = emailTextBox.Text;
            Contato contatoRemover = contatos.Find(c => c.Email == emailRemover);

            if (contatoRemover != null)
            {
                contatos.Remove(contatoRemover);
                AtualizarListaContatos();
                LimparCampos();
            }
            else
            {
                MessageBox.Show("Contato não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AtualizarListaContatos()
        {
            listaContatosTextBox.Text = "Lista de Contatos:\r\n";
            foreach (Contato contato in contatos)
            {
                listaContatosTextBox.Text += $"{contato.Nome} - {contato.Email} - {contato.DataNascimento:dd/MM/yyyy}\r\n";
            }
        }

        private void LimparCampos()
        {
            nomeTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            dataNascimentoTextBox.Text = string.Empty;
        }
    }
}