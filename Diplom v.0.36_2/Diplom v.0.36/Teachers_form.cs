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
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace Diplom_v._0._36
{
    public partial class Teachers_form : Form
    {
        public Teachers_form()
        {
            InitializeComponent();
        }

        private void Teachers_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplom2DataSet.Teachers". При необходимости она может быть перемещена или удалена.
            this.teachersTableAdapter.Fill(this.diplom2DataSet.Teachers);
        }
         private int RowId;
        private void button1_Click(object sender, EventArgs e) //добавление преподавателя
        {
            string FIO = textBox1.Text;
            bool proverka = false;                                              //для проверки дубликатов
            FIO = FIO_Replace(FIO);                                                                
            if (FIO != "") //проверка на пустоту в текстбокс
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    if (FIO == (string)dataGridView1[1, i].Value)    //проверка на дубликаты
                    {
                        DialogResult res = MessageBox.Show("Такой преподаватель уже в списке!", "Внимание", MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        proverka = true;         //если дубликат есть, меняем значение на true
                    }
                }
                if (proverka == false)
                {
                    Teacher_add(FIO);       //вызываем метод 
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Введите преподавателя!", "Внимание", MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button2_Click(object sender, EventArgs e)  //изменение в таблице преподавателей
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                string FIO = textBox1.Text;
                bool proverka = false;
                FIO = FIO_Replace(FIO);
                if (FIO != "")  //проверка на пустоту в textBox1
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {

                        if (FIO == (string)dataGridView1[1, i].Value)    //проверка на дубликаты
                        {
                            DialogResult res = MessageBox.Show("Такая запись уже есть!", "Внимание", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            proverka = true;         //если дубликат есть, меняем значение на true
                        }                        
                    }
                    if (proverka == false)
                    {
                        Teacher_edit(FIO);       //вызываем метод  
                    }
                }
                else
                {
                    DialogResult res = MessageBox.Show("Введите преподавателя!", "Внимание!", MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("Выберите строку для редактирования!", "Ошибка!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button3_Click(object sender, EventArgs e)  //удаление преподавателей из таблицы
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Teacher_dell();
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

        private void Teacher_add(string FIO)    //метод для внесения преподавателей в таблицу
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Teachers", con); //получаем таблицу с преподавателями
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Teachers"); // работаем с таблицей преподавателей
            ds.Tables["Teachers"].Rows.Add(); //создаем новую строку в таблице
            int last = ds.Tables["Teachers"].Rows.Count - 1; //берем айди новой строки
            ds.Tables["Teachers"].Rows[last]["FIO"] = FIO; //вносим имя в новую строку
            da.Update(ds, "Teachers"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект

            teachersTableAdapter.Fill(diplom2DataSet.Teachers);
            dataGridView1.Refresh();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) //выгрузка данных из dataGridView на формы заполнения
        {
            try
            {
                RowId = e.RowIndex;
                textBox1.Text = diplom2DataSet.Teachers.Rows[RowId]["FIO"].ToString();
            }
            catch (Exception)
            { }
        }

        private void Teacher_edit(string FIO)    //метод для внесения изменений в таблицу
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Teachers", con); //получаем таблицу с преподавателями
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Teachers"); // работаем с таблицей преподавателей
            ds.Tables["Teachers"].Rows[RowId]["FIO"] = FIO; //вносим имя в cтроку
            da.Update(ds, "Teachers"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект

            teachersTableAdapter.Fill(diplom2DataSet.Teachers);
            dataGridView1.Refresh();
        }

        private void Teacher_dell() //метод для удаления преподавателя из списка
        {
            diplom2DataSet.Teachers.Rows[RowId].Delete();
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Teachers", con); //получаем таблицу с преподавателями
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            da.Update(diplom2DataSet, "Teachers");
            con.Close();

            teachersTableAdapter.Fill(diplom2DataSet.Teachers);
            dataGridView1.Refresh();
            textBox1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)  //разрешение на ввод определенных символов
        {
            char word = e.KeyChar;
            if ((word < 'А' || word > 'я') && word != '\b' && word!=' ')
            {
                e.Handled = true;
            }
        }

        public string FIO_Replace(string FIO)   //удаление пробелов
        {
            FIO = FIO.Trim();
            return FIO = Regex.Replace(FIO, @"\s+", " "); 
        }
    }
}
