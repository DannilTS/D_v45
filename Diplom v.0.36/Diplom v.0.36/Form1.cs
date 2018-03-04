﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Graphviz;
using QuickGraph.Collections;
using QuickGraph.Graphviz.Dot;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;

namespace Diplom_v._0._36
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        class uzel
        {
            public int number;          //номер вершины
            public string group;        //имя группы
            public string teacher;      //имя преподавателя
            public string subject;      //наименование предмета
            public bool leacture;       //это лекция
            public bool practice;       //это практика
            public int color;           //цвет вершины 
            public int stepeni;         //степень вершины(количество связей)
            
        }   

        ArrayList uz = new ArrayList();     
        public int kol;
        private void button1_Click(object sender, EventArgs e) //открытие формы "Нагрузка"
        {
            Load LD = new Load();
            LD.Owner = this;
            LD.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int CountI = 0, CountJ = 0;                             //переменные для формирования матрицы смежности
            this.loadTableAdapter1.Fill(this.diplom2DataSet1.Load); 
            var g = new AdjacencyGraph<uzel, Edge<uzel>>();
            for (int i = 0; i < diplom2DataSet1.Load.Count; i++)
            {
                uzel u1 = new uzel();                                                //узел графа
                u1.teacher = diplom2DataSet1.Load.Rows[i]["Teacher"].ToString();
                u1.subject = diplom2DataSet1.Load.Rows[i]["Subject"].ToString();
                u1.leacture = Convert.ToBoolean(diplom2DataSet1.Load.Rows[i]["Lecture"].ToString());
                u1.practice = Convert.ToBoolean(diplom2DataSet1.Load.Rows[i]["Practice"].ToString());
                u1.group = diplom2DataSet1.Load.Rows[i]["Groups"].ToString();
                uz.Add(u1);
            }
            //dataGridView2.RowCount = Convert.ToInt32(uz.Count);       //для проверки работы нужна dataGridView2
            //dataGridView2.ColumnCount = Convert.ToInt32(uz.Count);
            int[,] matrix = new int[uz.Count, uz.Count];                // создание матрицы смежности      
            foreach (uzel u in uz)                      //выбираем значение узла из листа uz
            {
                g.AddVertex(u);                         //создаем вершину с содержимым узла
                int ch = 1;          
                foreach (uzel uu in g.Vertices)         //выбираем занчение узлов из графа
                {
                    if (ch == g.VertexCount)            //если ch = количеству узлов, прерываем работу
                    {
                        matrix[CountI, CountJ] = 0;
                        //dataGridView2.Rows[CountI].Cells[CountJ].Value = matrix[CountI, CountJ];   //для проверки работы нужна dataGridView2
                        CountI++;
                        CountJ = 0;
                        break;
                    }
                    int check = 0;
                    if (u.group.Contains(','))          //если в 1 узле в значении группы есть запятая,
                    {                                   //то check=1
                        check += 1;
                    }
                    if (uu.group.Contains(','))         //если во 2 узле в значении группы есть запятая,
                    {                                   //то check=2
                        check += 2;
                    }
                    bool grps = false;
                    switch (check)
                    {
                        case 0:                                       //если значение check=0, то в узлах по одной группе,
                            grps = (u.group == uu.group);             //следовательно значение grps=true 
                            if (grps)
                                grps = !(u.practice && uu.practice && (u.subject == uu.subject));   //если значение практика и предмет в узлах равно, 
                            break;                                                                  //то значение grps=false

                        case 1:                                                                     //если значение check=1, то в 1 узле несколько групп,  
                            grps = u.group.Contains(uu.group);                                      //а во 2 узле 1 группа, следовательно значение grps=true 
                            break;

                        case 2:                                                                     //если значение check=2, то во 2 узле несколько групп,                                                     
                            grps = uu.group.Contains(u.group);                                      //а в 1 узле 1 группа, следовательно значение grps=true                                      
                            break;

                        case 3:                                                                     //если значение check=3, то в обоих узлах несколько групп,
                            string temp = u.group.ToString();                                       //удаляем пробелы в группах, после чего разбиваем на несколько значений
                            temp = temp.Replace(" ", "");                                           // по запятым и заносим в массив temp1, далее сравниваем гуруппы с 
                            string[] temp1 = temp.Split(',');                                       // с группами во 2 узле и если такие есть grps=true
                            foreach (string gx in temp1)
                                if (uu.group.Contains(gx))
                                    grps = true;
                            break;
                    }
                    if (grps || u.teacher == uu.teacher)                                            //если значение grps=true или преподаватель в первом узле равен
                    {                                                                               //преподавателю во втором, то создаем между ними связь
                        var e1 = new Edge<uzel>(u, uu);
                        g.AddEdge(e1);
                    }
                    //заполнение матрицы смежности\\
                    for (int i = CountI; i < uz.Count; )
                    {
                        for (int j = CountJ; j < uz.Count; )
                        {
                            if (grps || u.teacher == uu.teacher)
                            {
                                matrix[i, j] = 1;
                                matrix[j, i] = matrix[i, j];
                                //dataGridView2.Rows[j].Cells[i].Value = matrix[i, j];     для проверки работы нужна dataGridView2
                                //dataGridView2.Rows[i].Cells[j].Value = matrix[j, i];
                            }
                            else
                            {
                                matrix[i, j] = 0;
                                matrix[j, i] = matrix[i, j];
                                //dataGridView2.Rows[j].Cells[i].Value = matrix[i, j];      для проверки работы нужна dataGridView2
                                //dataGridView2.Rows[i].Cells[j].Value = matrix[j, i];
                            }
                            CountJ++;
                            break;
                        }
                        if (CountJ == uz.Count)
                        {
                            CountJ = 0;
                            CountI++;
                        }
                        break;
                    }
                    //заполнение матрицы смежности//
                        ch++;
                    }
                }

            //Находим степени вершин по матрице смежности (вынести в метод)
            int number;
            int ii = 0, jj = 0;                 //переменные для подсчета, чтобы использовать foreach
            foreach (uzel stp in g.Vertices)
            {
                number = ii;
                stp.stepeni = 0;
                stp.number = number;
                for (int i = ii; i < g.VertexCount; )
                {                                   //обход матрицы смежности matrix
                    for (int j = jj; j < g.VertexCount; )
                    {
                        if (matrix[i, j] == 1)      //если находим в строке 1 увеличиваем степень этой вершины соответственно
                        {
                            stp.stepeni++;
                            jj++;
                            break;
                        }

                        jj++;
                        break;
                    }
                    if (jj == g.VertexCount)                  //выходим из цикла после обхода строки матрицы
                    {
                        jj = 0;
                        ii++;
                        break;
                    }

                }
            }

            foreach (uzel col in g.Vertices)    
            {
                col.color = 73;                 //присваиваем всем вершинам цвет 73
            }

            ////////////////////////////////////////////////////////////////////////////////////////////
            //Алгоритм по присвоению цветов 
            int t = 0, number_x = 0, number_i = 0;
            int versh=g.VertexCount;                    //количество вершин в графе (для передачи в метод)
            int[] colors = new int[73];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = i + 1;
            }

            foreach (uzel col in g.Vertices)
            {
                for (int x = number_x; NoFullColor(versh, g); )//Цикл по цветам. Условие выхода - все цвета окрашены! Пока не раскрашено( НеВсёЗакрашено) - работает
                {

                    if (col.color == 73)
                    {
                        col.color = colors[x];              //Присваиваем новый цвет
                    }
                    else
                    {
                        number_x--;
                    }
                    // продолжаем дальше;
                    for (int i = number_i; i < g.VertexCount; )   //цикл, чтобы не только одни вершины раскрасить, а ВСЕ НЕ смежные
                    {
                        t = col.number;         //Запоминаем номер исследуемой вершины
                        for (int j = 0; j < g.VertexCount; j++)     //Идем по строке матрицы по номеру
                        {
                            //Блок условий проверки на возможность "раскраски" вершины. Под раскраской - подразумевается присваивание номера
                            if ((matrix[t, j] == 0 && t != j))      //Если 0 - можно назначить цвет, что и у вершины
                            {
                                foreach (uzel num in g.Vertices)     //Перебираем все вершины
                                {
                                    if (num.number == j && t < j && num.color == 73)   //Если условие выполняется, то присваиваем тот же цвет, что и исследуемой вершины
                                    {
                                        num.color = colors[x];
                                    }
                                }
                            }
                        }
                        number_i++;
                        break;
                    }
                    number_x++;
                    break;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////////////

            var graphViz = new GraphvizAlgorithm<uzel, Edge<uzel>>(g, @".\", QuickGraph.Graphviz.Dot.GraphvizImageType.Png);
            // render
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            string output = graphViz.Generate(new FileDotEngine(), "graph.dot");
        }

        //Возвращает ноль, если все цвета уже присвоены
        static bool NoFullColor(int versh, AdjacencyGraph<uzel, Edge<uzel>> g)
        {
            int flag = 0;
            foreach (uzel col in g.Vertices)    //Цикл проверяет - все ли цвета посещены
            {
                if (col.color <= versh)
                    flag++;
            }
            if (flag == versh)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private static void FormatVertex(object sender, FormatVertexEventArgs<uzel> e)
        {
            string para;
            if (e.Vertex.leacture)
                para = "Лекция";
            else
                para = "Практика";
            e.VertexFormatter.Label = e.Vertex.group + "\n" + e.Vertex.subject + "\n" + e.Vertex.teacher + "\n" + para;
            e.VertexFormatter.Shape = GraphvizVertexShape.Circle;

            //e.VertexFormatter.BottomLabel = e.Vertex.subject;


            e.VertexFormatter.StrokeColor = GraphvizColor.Black;
            e.VertexFormatter.Font = new GraphvizFont("Calibri", 11);
        }
        private static void FormatEdge(object sender, FormatEdgeEventArgs<uzel, Edge<uzel>> e)
        {
            //e.EdgeFormatter.Head.Label = e.Edge.Source.group;
            //e.EdgeFormatter.Tail.Label = e.Edge.Source.subject;
            e.EdgeFormatter.Font = new GraphvizFont("Calibri", 8);
            e.EdgeFormatter.FontGraphvizColor = GraphvizColor.Black;
            e.EdgeFormatter.StrokeGraphvizColor = GraphvizColor.Black;
        }

        public sealed class FileDotEngine : IDotEngine
        {
            public string Run(GraphvizImageType imageType, string dot, string outputFileName)
            {
                string output = outputFileName;
                System.IO.File.WriteAllText(output, dot);


                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe";
                startInfo.Arguments = @"dot -Tgif graph.dot -o graph.png";



                Process.Start(startInfo);
                return output;
            }
        }
    }
}
