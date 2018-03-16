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
    class Add_teachers
    {
        private string FIO;
        private string subject;
        private string group;
        bool lec;
        bool prac;
        public Add_teachers(string FIO, string subject, string group, bool lec, bool prac)
        {
            this.FIO = FIO;
            this.subject = subject;
            this.group = group;
            this.lec = lec;
            this.prac = prac;
        }

        public void Add_tch()
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");


            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Load", con); //вытаскиваем нагрузку
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Load"); // работаем с нагрузкой
            ds.Tables["Load"].Rows.Add(); //создаем новую строку в таблице
            int last = ds.Tables["Load"].Rows.Count - 1; //берем айди новой строки
            ds.Tables["Load"].Rows[last]["Teacher"] = FIO; //вносим имя в новую строку
            ds.Tables["Load"].Rows[last]["Subject"] = subject; //вносим предмет в новую строку
            ds.Tables["Load"].Rows[last]["Groups"] = group; //вносим предмет в новую строку
            ds.Tables["Load"].Rows[last]["Lecture"] = lec;
            ds.Tables["Load"].Rows[last]["Practice"] = prac; 
            da.Update(ds, "Load"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }

        public void Edit(int last)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");


            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Load", con); //вытаскиваем нагрузку
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Load"); // работаем с нагрузкой
            ds.Tables["Load"].Rows[last]["Teacher"] = FIO; //вносим имя в новую строку
            ds.Tables["Load"].Rows[last]["Subject"] = subject; //вносим предмет в новую строку
            ds.Tables["Load"].Rows[last]["Groups"] = group; //вносим предмет в новую строку
            ds.Tables["Load"].Rows[last]["Lecture"] = lec;
            ds.Tables["Load"].Rows[last]["Practice"] = prac;
            da.Update(ds, "Load"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }
    }
}
