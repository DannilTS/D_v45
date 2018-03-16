using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;

namespace Diplom_v._0._36
{
    public partial class Audit_form : Form
    {
        public Audit_form()
        {
            InitializeComponent();
        }

        private void Audit_form_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplom2DataSet.Audit". При необходимости она может быть перемещена или удалена.
            this.auditTableAdapter.Fill(this.diplom2DataSet.Audit);
        }

        private void button1_Click(object sender, EventArgs e)  //  добавление аудитории
        {
            int number = Convert.ToInt32(textBox1.Text);
            string corp = (string)comboBox1.SelectedItem;
            bool lecture = radioButton1.Checked;
            bool practice = radioButton2.Checked;
            bool proverka = false;
            if (number != 0)
            {
                if (corp != null)
                {
                    if (lecture ^ practice)//проверка на пустоту в radioButtons
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {

                            if (number == (int)dataGridView1[1, i].Value)    //проверка на дубликаты
                            {
                                DialogResult res = MessageBox.Show("Такая запись уже есть!", "Внимание", MessageBoxButtons.OK,
                                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                proverka = true;                                        //если дубликат есть, меняем значение на true
                            }
                        }
                        if (proverka == false)
                        {
                            Audit_add(number, lecture, practice,corp);
                            auditTableAdapter.Fill(diplom2DataSet.Audit);
                            dataGridView1.Refresh();
                        }
                    }
                    else
                    {
                        DialogResult res = MessageBox.Show("Выберите тип занятия!", "Внимание", MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
                else
                {
                    DialogResult res = MessageBox.Show("Выберите корпус!", "Внимание", MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Введите номер аудитории!", "Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void Audit_add(int number, bool lec, bool prac, string corp)    //метод для внесения номеров аудиторий в таблицу
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Audit", con); //получаем таблицу с номерами аудиторий
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Audit"); // работаем с таблицей аудиторий
            ds.Tables["Audit"].Rows.Add(); //создаем новую строку в таблице
            int last = ds.Tables["Audit"].Rows.Count - 1; //берем айди новой строки
            ds.Tables["Audit"].Rows[last]["Nomer"] = number; //вносим номер в новую строку
            ds.Tables["Audit"].Rows[last]["Lecture"] = lec; //вносим тип заняти в новую строку
            ds.Tables["Audit"].Rows[last]["Practice"] = prac; //вносим тип занятия в новую строку
            ds.Tables["Audit"].Rows[last]["Corps"] = corp; //вносим корпус в новую строку
            da.Update(ds, "Audit"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.auditTableAdapter.FillBy(this.diplom2DataSet.Audit);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        }
    }
