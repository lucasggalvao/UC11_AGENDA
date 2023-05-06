using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UC11_AGENDA
{
    public partial class Form1 : Form
    {
        string servidor;
        MySqlConnection conexao;
        MySqlCommand comando;

        public Form1()
        {
            InitializeComponent();
            servidor = "Server=localhost;Database=agenda_bd;Uid=root;Pwd=";
            conexao = new MySqlConnection(servidor);
            comando = conexao.CreateCommand();
        }

        private void buttonCADASTRAR_Click(object sender, EventArgs e)
        {
            bool duplicado = false;

            try
            {
                conexao.Open();
                comando.CommandText = " SELECT nome, sobrenome FROM tbl_contatos WHERE nome, sobrenome  = '" + textBoxNOME.Text + "'"+ textBoxSOBRENOME.Text +"'";
                MySqlDataReader resultado = comando.ExecuteReader();

                if (resultado.Read())
                {
                    duplicado = true;
                    MessageBox.Show("Contato ja cadastrado!");
                }
            }
            catch (Exception erro_mysql)
            {
                
            }
            finally
            {
                conexao.Close();
            }

            if (duplicado == false)
            {
                try
                {
                    conexao.Open();
                    comando.CommandText = "INSERT INTO tbl_contatos(nome, sobrenome) VALUES ('" + textBoxNOME.Text + "', '" + textBoxSOBRENOME.Text + "');";
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Contato cadastrado com sucesso!");
                }
                catch (Exception erro_mysql)
                {
                    MessageBox.Show(erro_mysql.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxNOME.Text = dataGridViewCONTATO.CurrentRow.Cells[2].Value.ToString();
            textBoxSOBRENOME.Text = dataGridViewCONTATO.CurrentRow.Cells[3].Value.ToString();
            textBoxCELULAR.Text = dataGridViewCONTATO.CurrentRow.Cells[5].Value.ToString();
            
        }

        private void buttonPESQUISAR_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "SELECT * FROM tbl_contatos";

                MySqlDataAdapter adaptadorcontatos = new MySqlDataAdapter(comando);

                DataTable tabelacontatos = new DataTable();
                adaptadorcontatos.Fill(tabelacontatos);

                dataGridViewCONTATO.DataSource = tabelacontatos;
                dataGridViewCONTATO.Columns["nome"].HeaderText = "Nome";
                dataGridViewCONTATO.Columns["sobrenome"].HeaderText = "Sobrenome";
                dataGridViewCONTATO.Columns["celular"].HeaderText = "Celular";
                
            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void buttonALTERAR_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                comando.CommandText = "UPDATE tbl_contatos SET nome = '" + textBoxNOME.Text + "', sobrenome = '" + textBoxSOBRENOME.Text + "', telefone = " + textBoxTELEFONE.Text + "', celular = " +textBoxCELULAR.Text + "', email = " +textBoxEMAIL.Text + "', linkedin = " +textBoxLINKEDIN.Text + "' WHERE id = " + textBoxID.Text + ";";
                comando.ExecuteNonQuery();
                MessageBox.Show("Resgistro alterado com sucesso!!");

            }
            catch (Exception erro_mysql)
            {
                MessageBox.Show(erro_mysql.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void buttonEXCLUIR_Click(object sender, EventArgs e)
        {
            try
            {

                conexao.Open();
                comando.CommandText = "DELETE FROM tbl_contatos WHERE id = '" + textBoxID.Text + "';";
                comando.ExecuteNonQuery();
                MessageBox.Show("contato excluido com sucesso!");


            }
            catch (Exception erro)
            {
                
                MessageBox.Show("Erro ao excluir contato. Tente Novamente");
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
