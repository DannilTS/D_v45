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
        private int RowId;
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

        private void button2_Click(object sender, EventArgs e)  //изменение записи в таблице аудитории
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                int number = Convert.ToInt32(textBox1.Text);
                string corp = (string)comboBox1.SelectedItem;
                bool lecture = radioButton1.Checked;
                bool practice = radioButton2.Checked;
                bool proverka = false;
                if (number != 0)  //проверка на пустоту в textBox1
                {
                    if (corp != null)
                    {
                        if (lecture ^ practice)//проверка на пустоту в radioButtons
                        {
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {

                                if (corp == (string)dataGridView1[1, i].Value)    //проверка на дубликаты
                                {
                                    DialogResult res = MessageBox.Show("Такая запись уже есть!", "Внимание", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                    proverka = true;         //если дубликат есть, меняем значение на true
                                }
                            }
                            if (proverka == false)
                            {
                                Audit_edit(number, lecture, practice, corp);       //вызываем метод 
                                auditTableAdapter.Fill(diplom2DataSet.Audit);      //вносим изменения в датасет
                                dataGridView1.Refresh();    //обновляем dataGridView1
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
                    DialogResult res = MessageBox.Show("Введите предмет!", "Внимание!", MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Выберите строку для редактирования!", "Ошибка!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button3_Click(object sender, EventArgs e)  //удаление записи в таблице Audit
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Audit_dell();
                        auditTableAdapter.Fill(diplom2DataSet.Audit);
                        dataGridView1.Refresh();
                    }
                    catch
                    { }
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Выберите строку для редактирования!", "Ошибка!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) //получение id выбранной строки
        {
            try
            {
                RowId = e.RowIndex;
                textBox1.Text = diplom2DataSet.Audit.Rows[RowId]["Nomer"].ToString();
                radioButton1.Checked = Convert.ToBoolean(diplom2DataSet.Audit.Rows[RowId]["Lecture"].ToString());
                radioButton2.Checked = Convert.ToBoolean(diplom2DataSet.Audit.Rows[RowId]["Practice"].ToString());
                comboBox1.SelectedItem= diplom2DataSet.Audit.Rows[RowId]["Corps"].ToString();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Audit_edit(int number, bool lec, bool prac, string corp)    //метод для внесения изменений в номерах аудиторий 
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Audit", con); //получаем таблицу с номерами аудиторий
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Audit"); // работаем с таблицей аудиторий
            ds.Tables["Audit"].Rows[RowId]["Nomer"] = number; //вносим номер в новую строку
            ds.Tables["Audit"].Rows[RowId]["Lecture"] = lec; //вносим тип заняти в новую строку
            ds.Tables["Audit"].Rows[RowId]["Practice"] = prac; //вносим тип занятия в новую строку
            ds.Tables["Audit"].Rows[RowId]["Corps"] = corp; //вносим корпус в новую строку
            da.Update(ds, "Audit"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }

        private void Audit_dell()                 //метод для удаления записей в таблице Audit
        {
            diplom2DataSet.Audit.Rows[RowId].Delete();

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Audit", con); //получаем таблицу с номерами аудиторий
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            da.Update(diplom2DataSet, "Audit"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)  //ввод только чисел
        {
            char number = e.KeyChar;
            if(!Char.IsDigit(number) && number!= '\b')
            {
                e.Handled = true;
            }
        }
    }
 }
