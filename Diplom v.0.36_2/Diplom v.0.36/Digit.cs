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

namespace Diplom_v._0._36
{
    class Digit
    {
        private string FIO;
        private string subject;
        public bool dg;
        public bool dt;
        public Digit(string FIO, string subject, bool dg, bool dt)
        {
            this.FIO = FIO;
            this.subject = subject;
            this.dg = dg;
            this.dt = dt;
        }

        public void Digit_t()
        {
            for (int i = 0; i < FIO.Length; i++)
            {
                if (char.IsDigit(FIO[i]))
                {
                    DialogResult res = MessageBox.Show("Проверьте данные преподавателя!", "Внимание", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    dg = true;
                    break;
                }
            }
            for (int i = 0; i < subject.Length; i++)
            {
                if (char.IsDigit(subject[i]))
                {
                    DialogResult res = MessageBox.Show("Проверьте наименование предмета!", "Внимание", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    dt=true;
                    break;
                }
            }
            
        }
    }
     
}
