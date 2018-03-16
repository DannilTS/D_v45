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
    public partial class Subjects_form : Form
    {
        public Subjects_form()
        {
            InitializeComponent();
        }
        private int RowId;
        private void Subjects_form_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplom2DataSet.Subjects". При необходимости она может быть перемещена или удалена.
            this.subjectsTableAdapter.Fill(this.diplom2DataSet.Subjects);
        }

        private void button1_Click(object sender, EventArgs e)  //добавление предметов в список
        {
            string subject = textBox1.Text;
            bool proverka = false;                                              //для проверки дубликатов
            subject = subject.Replace("Ё", "Е").Replace("ё", "е");                       //замена Ё на Е и ё на е
            if (subject != "") //проверка на пустоту в текстбокс
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    if (subject == (string)dataGridView1[1, i].Value)    //проверка на дубликаты
                    {
                        DialogResult res = MessageBox.Show("Такой предмет уже в списке!", "Внимание", MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        proverka = true;         //если дубликат есть, меняем значение на true
                    }
                }
                if (proverka == false)
                {
                    Subject_add(subject);       //вызываем метод 
                    subjectsTableAdapter.Fill(diplom2DataSet.Subjects);
                    dataGridView1.Refresh();
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Введите предмет!", "Внимание", MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button2_Click(object sender, EventArgs e)  //изменение предмета в списке
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                string subject = textBox1.Text;
                bool proverka = false;
                if (subject != "")  //проверка на пустоту в textBox1
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {

                        if (subject == (string)dataGridView1[1, i].Value)    //проверка на дубликаты
                        {
                            DialogResult res = MessageBox.Show("Такая запись уже есть!", "Внимание", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            proverka = true;         //если дубликат есть, меняем значение на true
                        }
                    }
                    if (proverka == false)
                    {
                        Subject_edit(subject);       //вызываем метод 
                        subjectsTableAdapter.Fill(diplom2DataSet.Subjects);
                        dataGridView1.Refresh();    //обновляем dataGridView1
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

        private void button3_Click(object sender, EventArgs e)  //удаление предмета из списка
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Subject_dell();
                        subjectsTableAdapter.Fill(diplom2DataSet.Subjects);
                        dataGridView1.Refresh();
                    }
                    catch
                    { }
                }
            }
        }

        private void Subject_add(string subject)    //метод для внесения предметов в таблицу
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Subjects", con); //получаем таблицу с преподавателями
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Subjects"); // работаем с таблицей предметов
            ds.Tables["Subjects"].Rows.Add(); //создаем новую строку в таблице
            int last = ds.Tables["Subjects"].Rows.Count - 1; //берем айди новой строки
            ds.Tables["Subjects"].Rows[last]["Subject"] = subject; //вносим название в новую строку
            da.Update(ds, "Subjects"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                RowId = e.RowIndex;
                textBox1.Text = diplom2DataSet.Subjects.Rows[RowId]["Subject"].ToString();
            }
            catch (Exception)
            { }
        }//получение id выбранной строки

        private void Subject_edit(string subject)    //метод для редактирования предмета в таблице
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Subjects", con); //получаем таблицу с преподавателями
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Subjects"); // работаем с таблицей предметов
            ds.Tables["Subjects"].Rows.Add(); //создаем новую строку в таблице
            int last = ds.Tables["Subjects"].Rows.Count - 1; //берем айди новой строки
            ds.Tables["Subjects"].Rows[last]["Subject"] = subject; //вносим название в новую строку
            da.Update(ds, "Subjects"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }

        private void Subject_dell() //метод для удаления предмета из списка
        {
            diplom2DataSet.Subjects.Rows[RowId].Delete();
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Subjects", con);  //получаем таблицу с предметами
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            da.Update(diplom2DataSet, "Subjects");
            con.Close();
        }

       

    }
}
