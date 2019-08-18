using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Магистр
{
    public partial class Form1 : Form
    {
        DrawGraph G;
        List<Vertex> V;
        List<Edge> E;
        int[,] AMatrix; //матрица смежности
        int[,] IMatrix; //матрица инцидентности

        int selected1; //выбранные вершины, для соединения линиями
        int selected2;
        public Form1()
        {
            InitializeComponent();

            V = new List<Vertex>();
            G = new DrawGraph(pictureBox.Width, pictureBox.Height);
            E = new List<Edge>();
            pictureBox.Image = G.GetBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(КоличествоВершинГрафа.SelectedItem != null)
            {
                int ЧислоВершин = Convert.ToInt32(КоличествоВершинГрафа.SelectedItem);

                СкрытьВершины();
                УстановитьВидимостьВершин(ЧислоВершин);
                button2.Visible = true;
            }
            else
            {
                СкрытьВершины();
            }
        }


        private void VertexKeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void УстановитьВидимостьВершин(int ЧислоВершин)
        {
            if(ЧислоВершин == 1)
            {
                Text_a1.Visible = true;
                a1.Visible = true;
            }
            if (ЧислоВершин == 2)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;
            }
            if (ЧислоВершин == 3)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;

                Text_a3.Visible = true;
                a3.Visible = true;
            }
            if (ЧислоВершин == 4)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;

                Text_a3.Visible = true;     Text_a4.Visible = true;
                a3.Visible = true;          a4.Visible = true;
            }
            if (ЧислоВершин == 5)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;

                Text_a3.Visible = true;     Text_a4.Visible = true;
                a3.Visible = true;          a4.Visible = true;

                Text_a5.Visible = true;
                a5.Visible = true;
            }
            if (ЧислоВершин == 6)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;

                Text_a3.Visible = true;     Text_a4.Visible = true;
                a3.Visible = true;          a4.Visible = true;

                Text_a5.Visible = true;     Text_a6.Visible = true;
                a5.Visible = true;          a6.Visible = true;
            }
            if (ЧислоВершин == 7)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;

                Text_a3.Visible = true;     Text_a4.Visible = true;
                a3.Visible = true;          a4.Visible = true;

                Text_a5.Visible = true;     Text_a6.Visible = true;
                a5.Visible = true;          a6.Visible = true;

                Text_a7.Visible = true;
                a7.Visible = true;
            }
            if (ЧислоВершин == 8)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;

                Text_a3.Visible = true;     Text_a4.Visible = true;
                a3.Visible = true;          a4.Visible = true;

                Text_a5.Visible = true;     Text_a6.Visible = true;
                a5.Visible = true;          a6.Visible = true;

                Text_a7.Visible = true;     Text_a8.Visible = true;
                a7.Visible = true;          a8.Visible = true;
            }
            if (ЧислоВершин == 9)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;

                Text_a3.Visible = true;     Text_a4.Visible = true;
                a3.Visible = true;          a4.Visible = true;

                Text_a5.Visible = true;     Text_a6.Visible = true;
                a5.Visible = true;          a6.Visible = true;

                Text_a7.Visible = true;     Text_a8.Visible = true;
                a7.Visible = true;          a8.Visible = true;

                Text_a9.Visible = true;
                a9.Visible = true;
            }
            if (ЧислоВершин == 10)
            {
                Text_a1.Visible = true;     Text_a2.Visible = true;
                a1.Visible = true;          a2.Visible = true;

                Text_a3.Visible = true;     Text_a4.Visible = true;
                a3.Visible = true;          a4.Visible = true;

                Text_a5.Visible = true;     Text_a6.Visible = true;
                a5.Visible = true;          a6.Visible = true;

                Text_a7.Visible = true;     Text_a8.Visible = true;
                a7.Visible = true;          a8.Visible = true;

                Text_a9.Visible = true;     Text_a10.Visible = true;
                a9.Visible = true;          a10.Visible = true;
            }
        }

        private void СкрытьВершины()
        {
            Text_a1.Visible = false;    Text_a2.Visible = false;
            a1.Visible = false;         a2.Visible = false;

            Text_a3.Visible = false;    Text_a4.Visible = false;
            a3.Visible = false;         a4.Visible = false;

            Text_a5.Visible = false;    Text_a6.Visible = false;
            a5.Visible = false;         a6.Visible = false;

            Text_a7.Visible = false;    Text_a8.Visible = false;
            a7.Visible = false;         a8.Visible = false;

            Text_a9.Visible = false;    Text_a10.Visible = false;
            a9.Visible = false;         a10.Visible = false;
        }

        private void ВидимостьОбъектов(bool Видимость)
        {
            НадписьУкажитеКоличество.Visible = !Видимость;
            КоличествоВершинГрафа.Visible = !Видимость;

            button1.Visible = !Видимость;   button2.Visible = !Видимость;

            Text_a1.Visible = !Видимость;   a1.Visible = !Видимость;
            Text_a2.Visible = !Видимость;   a2.Visible = !Видимость;
            Text_a3.Visible = !Видимость;   a3.Visible = !Видимость;
            Text_a4.Visible = !Видимость;   a4.Visible = !Видимость;
            Text_a5.Visible = !Видимость;   a5.Visible = !Видимость;
            Text_a6.Visible = !Видимость;   a6.Visible = !Видимость;
            Text_a7.Visible = !Видимость;   a7.Visible = !Видимость;
            Text_a8.Visible = !Видимость;   a8.Visible = !Видимость;
            Text_a9.Visible = !Видимость;   a9.Visible = !Видимость;
            Text_a10.Visible = !Видимость;  a10.Visible = !Видимость;

            pictureBox.Visible = Видимость;
            button3.Visible = Видимость;    button4.Visible = Видимость; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool Видимость = true;

            ВидимостьОбъектов(Видимость);

            G.clearSheet();
            G.drawALLGraph(V, E);
            pictureBox.Image = G.GetBitmap();

        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            int ЧислоВершин = Convert.ToInt32(КоличествоВершинГрафа.SelectedItem);

            //нажата кнопка "рисовать вершину"
            if (button3.Enabled == false)
            {
                if(e.Button == MouseButtons.Right)
                {
                    bool flag = false; //удалили ли что-нибудь по ЭТОМУ клику
                                       //ищем, возможно была нажата вершина
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                        {
                            for (int j = 0; j < E.Count; j++)
                            {
                                if ((E[j].v1 == i) || (E[j].v2 == i))
                                {
                                    E.RemoveAt(j);
                                    j--;
                                }
                                else
                                {
                                    if (E[j].v1 > i) E[j].v1--;
                                    if (E[j].v2 > i) E[j].v2--;
                                }
                            }
                            V.RemoveAt(i);
                            flag = true;
                            break;
                        }
                    }
                    //если что-то было удалено, то обновляем граф на экране
                    if (flag)
                    {
                        G.clearSheet();
                        G.drawALLGraph(V, E);
                        pictureBox.Image = G.GetBitmap();
                    }

                }
                else if (e.Button == MouseButtons.Left)
                {
                    int КоличествоОтрисованныхВершин = V.Count;
                    if (КоличествоОтрисованныхВершин < ЧислоВершин)
                    {
                        V.Add(new Vertex(e.X, e.Y));
                        G.drawVertex(e.X, e.Y, V.Count.ToString());
                        pictureBox.Image = G.GetBitmap();

                    }
                } 
            }
                //нажата кнопка "рисовать ребро"
                if (button4.Enabled == false)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        for (int i = 0; i < V.Count; i++)
                        {
                            if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                            {
                                if (selected1 == -1)
                                {
                                    G.drawSelectedVertex(V[i].x, V[i].y);
                                    selected1 = i;
                                    pictureBox.Image = G.GetBitmap();
                                    break;
                                }
                                if (selected2 == -1)
                                {
                                    G.drawSelectedVertex(V[i].x, V[i].y);
                                    selected2 = i;
                                    E.Add(new Edge(selected1, selected2));
                                    G.drawEdge(V[selected1], V[selected2], E[E.Count - 1], E.Count - 1);
                                    selected1 = -1;
                                    selected2 = -1;
                                    pictureBox.Image = G.GetBitmap();
                                    break;
                                }
                            }
                        }
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                    //if ((selected1 != -1) &&
                    //    (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= G.R * G.R))
                    //{
                    //    G.drawVertex(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
                    //    selected1 = -1;
                    //    pictureBox.Image = G.GetBitmap();
                    //}

                    bool flag = false;

                    //ищем, возможно было нажато ребро
                    if (!flag)
                    {
                        for (int i = 0; i < E.Count; i++)
                        {
                            if (E[i].v1 == E[i].v2) //если это петля
                            {
                                if ((Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                    (Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2))))
                                {
                                    E.RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                            else //не петля
                            {
                                if (((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) <= (e.Y + 4) &&
                                    ((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) >= (e.Y - 4))
                                {
                                    if ((V[E[i].v1].x <= V[E[i].v2].x && V[E[i].v1].x <= e.X && e.X <= V[E[i].v2].x) ||
                                        (V[E[i].v1].x >= V[E[i].v2].x && V[E[i].v1].x >= e.X && e.X >= V[E[i].v2].x))
                                    {
                                        E.RemoveAt(i);
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //если что-то было удалено, то обновляем граф на экране
                    if (flag)
                    {
                        G.clearSheet();
                        G.drawALLGraph(V, E);
                        pictureBox.Image = G.GetBitmap();
                    }
                }
                }
            }

        //построение вершин
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            pictureBox.Image = G.GetBitmap();
        }

        //создание ребер
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button3.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, E);
            pictureBox.Image = G.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }
    }
}
