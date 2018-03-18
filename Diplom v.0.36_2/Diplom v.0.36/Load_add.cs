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
    class Load_add
    {
        private int FIO;
        private int subject;
        private string group;
        bool lec;
        bool prac;
        private int number;
        public Load_add(int FIO, int subject, string group, bool lec, bool prac, int number)
        {
            this.FIO = FIO;
            this.subject = subject;
            this.group = group;
            this.lec = lec;
            this.prac = prac;
            this.number = number;
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
            ds.Tables["Load"].Rows[last]["Hours"] = number; //вносим количество часов
            da.Update(ds, "Load"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }

        public void Edit(int RowId)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");


            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("select * from Load", con); //вытаскиваем нагрузку
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataSet ds = new DataSet(); //создаем датасет
            da.Fill(ds, "Load"); // работаем с нагрузкой
            ds.Tables["Load"].Rows[RowId]["Teacher"] = FIO; //вносим имя в новую строку
            ds.Tables["Load"].Rows[RowId]["Subject"] = subject; //вносим предмет в новую строку
            ds.Tables["Load"].Rows[RowId]["Groups"] = group; //вносим предмет в новую строку
            ds.Tables["Load"].Rows[RowId]["Lecture"] = lec;
            ds.Tables["Load"].Rows[RowId]["Practice"] = prac;
            ds.Tables["Load"].Rows[RowId]["Hours"] = number; //вносим количество часов
            da.Update(ds, "Load"); //закидываем апдейт в бд
            con.Close(); //закрываем коннект
        }
    }
}
