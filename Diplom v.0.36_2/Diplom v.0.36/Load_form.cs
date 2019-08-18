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
    public partial class Load_form : Form
    {
        public Load_form()
        {
            InitializeComponent();
            //teachersBindingSource.Sort = "FIO ASC";
        }
        private int RowId;
        private void button1_Click(object sender, EventArgs e) //добавление
        {   
            int FIO = (int)comboBox1.SelectedValue;                             //Ф.И.О. преподавателей
            int subject = (int)comboBox2.SelectedValue;                         //предметы
            string group = textBox3.Text;                                       //группы
            group = Group_Replace(group);                                       //вызов метода удаления пробелов
            bool lecture = radioButton1.Checked;                                //лекция это
            bool practice = radioButton2.Checked;                               //или практика
            bool proverka = false;                                              //для проверки дубликатов
            int number = Convert.ToInt32(textBox1.Text);                        //количество часов
            if (FIO != 0) //проверка на пустоту в comboBox1
            {
                if (subject != 0)//проверка на пустоту в comboBox2
                {                 
                        if (group != "")//проверка на пустоту в текстбокс
                        {
                        if (lecture ^ practice)//проверка на пустоту в radioButtons
                        {
                            if (number != 0)    //проверка на пустоту в textBox1
                            {
                                for (int i = 0; i < dataGridView1.RowCount; i++)
                                {

                                    if (FIO == (int)dataGridView1[1, i].Value && subject == (int)dataGridView1[2, i].Value && group == (string)dataGridView1[5, i].Value 
                                        && lecture == (bool)dataGridView1[3, i].Value && practice == (bool)dataGridView1[4, i].Value && number==(int)dataGridView1[6,i].Value)    //проверка на дубликаты
                                    {
                                        DialogResult res = MessageBox.Show("Такая запись уже есть!", "Внимание", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                        proverka = true;                                        //если дубликат есть, меняем значение на true
                                    }
                                }
                                if (proverka == false)
                                {
                                    Load_add sub = new Load_add(FIO, subject, group, lecture, practice, number);
                                    sub.Add_tch();
                                    Main_form F = Owner as Main_form;
                                    if (F != null)
                                    {
                                        F.kol = dataGridView1.RowCount;
                                    }
                                    loadTableAdapter.Fill(diplom2DataSet.Load);
                                    dataGridView1.Refresh();
                                }
                            }
                            else
                            {
                                DialogResult res = MessageBox.Show("Введите количество часов!", "Внимание!", MessageBoxButtons.OK,
                                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            }
                        }
                        else
                        {
                            DialogResult res = MessageBox.Show("Выберите тип занятия!", "Внимание!", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                        }
                        else
                        {
                            DialogResult res = MessageBox.Show("Введите группу!", "Внимание!", MessageBoxButtons.OK,
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
                    DialogResult res = MessageBox.Show("Введите преподавателя!", "Внимание!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            }
        
        private void Load_Load(object sender, EventArgs e)
        {
           

            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplom2DataSet.Subjects". При необходимости она может быть перемещена или удалена.
            subjectsTableAdapter.Fill(diplom2DataSet.Subjects);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplom2DataSet.Teachers". При необходимости она может быть перемещена или удалена.
            teachersTableAdapter.Fill(diplom2DataSet.Teachers);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplom2DataSet.Load". При необходимости она может быть перемещена или удалена.
            loadTableAdapter.Fill(diplom2DataSet.Load);

        }

        private void button2_Click(object sender, EventArgs e) //удаление
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        diplom2DataSet.Load.Rows[RowId].Delete();
                        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

                        con.Open();

                        OleDbDataAdapter da = new OleDbDataAdapter("select * from Load", con); //вытаскиваем нагрузку
                        OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                        da.Update(diplom2DataSet, "Load");
                        con.Close();
                        loadTableAdapter.Fill(diplom2DataSet.Load);
                        dataGridView1.Refresh();
                    }
                    catch
                    {}
                    
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления", "Ошибка");
                }
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) //выгрузка данных из dataGridView на формы заполнения
        {
            try
            {
                RowId = e.RowIndex;
                comboBox1.SelectedValue = Convert.ToInt32(diplom2DataSet.Load.Rows[RowId]["Teacher"]);
                comboBox2.SelectedValue = diplom2DataSet.Load.Rows[RowId]["Subject"].ToString();
                radioButton1.Checked = Convert.ToBoolean(diplom2DataSet.Load.Rows[RowId]["Lecture"].ToString());
                radioButton2.Checked = Convert.ToBoolean(diplom2DataSet.Load.Rows[RowId]["Practice"].ToString());
                textBox3.Text = diplom2DataSet.Load.Rows[RowId]["Groups"].ToString();
                textBox1.Text = diplom2DataSet.Load.Rows[RowId]["Hours"].ToString();
            }
            catch(Exception)
            { }
        }

        private void button3_Click(object sender, EventArgs e) //редактирование
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                int FIO = (int)comboBox1.SelectedValue;
                int subject = (int)comboBox2.SelectedValue;
                string group = textBox3.Text;
                bool lecture = radioButton1.Checked;
                bool practice = radioButton2.Checked;
                int number = Convert.ToInt32(textBox1.Text);
                bool proverka = false;
                if (FIO != 0) //проверка на пустоту в текстбокс
                {
                    if (subject != 0)
                    {
                        if (group != "")
                        {
                            if (lecture ^ practice)
                            {
                                if (number != 0)    //проверка на пустоту в textBox1
                                {
                                    for (int i = 0; i < dataGridView1.RowCount; i++)
                                    {

                                        if (FIO == (int)dataGridView1[1, i].Value && subject == (int)dataGridView1[2, i].Value && group == (string)dataGridView1[5, i].Value && 
                                            lecture == (bool)dataGridView1[3, i].Value && practice == (bool)dataGridView1[4, i].Value && number == (int)dataGridView1[6, i].Value)    //проверка на дубликаты
                                        {
                                            DialogResult res = MessageBox.Show("Такая запись уже есть!", "Внимание!", MessageBoxButtons.OK,
                                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                            proverka = true;                                        //если дубликат есть, меняем значение на true
                                        }
                                    }
                                    if (proverka == false)
                                    {
                                        Load_add sub = new Load_add(FIO, subject, group, lecture, practice,number);
                                        sub.Edit(RowId);
                                        loadTableAdapter.Fill(diplom2DataSet.Load);
                                        dataGridView1.Refresh();
                                    }
                                }
                                else
                                {
                                    DialogResult res = MessageBox.Show("Введите количество часов!", "Внимание!", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                }
                            }
                            else
                            {
                                DialogResult res = MessageBox.Show("Выберите тип занятия!", "Внимание!", MessageBoxButtons.OK,
                                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            }
                        }
                        else
                        {
                            DialogResult res = MessageBox.Show("Введите группу!", "Внимание!", MessageBoxButtons.OK,
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
        Teachers_form Th = null;
        private void button4_Click(object sender, EventArgs e)  //переход на форму преподаватели
        {

            if (Th == null || Th.Text == "")
            {
                Th = new Teachers_form();
                Th.Dock = DockStyle.Fill;
                Th.Show();
            }
            else if (CheckOpened(Th.Text))
            {
                Th.WindowState = FormWindowState.Normal;
                Th.Dock = DockStyle.Fill;
                Th.Show();
                Th.Focus();
            }    
        }
        Subjects_form Sb = null;
        private void button5_Click(object sender, EventArgs e)  //переход на форму предметы
        {
            if (Sb == null || Sb.Text == "")
            {
                Sb = new Subjects_form();
                Sb.Dock = DockStyle.Fill;
                Sb.Show();
            }
            else if (CheckOpened(Sb.Text))
            {
                Sb.WindowState = FormWindowState.Normal;
                Sb.Dock = DockStyle.Fill;
                Sb.Show();
                Sb.Focus();
            }            
        }

        private bool CheckOpened(string name)   //проверка на открытие или закрытие формы
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }
        Audit_form Ad = null;
        private void button6_Click(object sender, EventArgs e)  //открытие формы аудиторий
        {
            if (Ad == null || Ad.Text == "")
            {
                Ad = new Audit_form();
                Ad.Dock = DockStyle.Fill;
                Ad.Show();
            }
            else if (CheckOpened(Ad.Text))
            {
                Ad.WindowState = FormWindowState.Normal;
                Ad.Dock = DockStyle.Fill;
                Ad.Show();
                Ad.Focus();
            }  
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)  //разрешение на ввод определенных символов в поле "Кол-во часов"
        {
            char number = e.KeyChar;
            if (!char.IsNumber(e.KeyChar) && number != '\b')
            {
                e.Handled = true;
            }
        }

        public string Group_Replace(string group)   //удаление пробелов в строке группа
        {
            group = group.Trim();
            return group = Regex.Replace(group, @"\s+", " ");
        }
    }
}