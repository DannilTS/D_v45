using System;
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
    public partial class Main_form : Form
    {
        public Main_form()
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
            Load_form LD = new Load_form();
            LD.Owner = this;
            LD.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int CountI = 0, CountJ = 0;                             //переменные для формирования матрицы смежности
            var g = new AdjacencyGraph<uzel, Edge<uzel>>();

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Diplom2.mdb");

            con.Open();

            OleDbDataAdapter da = new OleDbDataAdapter("SELECT t.FIO as Teacher, s.Subject as Subject, l.Lecture as Lecture, l.Practice as Practice, l.Groups as Groups, l.Hours FROM Load l, Teachers t, Subjects s WHERE l.Teacher=t.Key and l.Subject=s.Key", con); //вытаскиваем нагрузку
            OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                uzel u1 = new uzel();                                                //узел графа
                u1.teacher = dt.Rows[i]["Teacher"].ToString();
                u1.subject = dt.Rows[i]["Subject"].ToString();
                u1.leacture = Convert.ToBoolean(dt.Rows[i]["Lecture"].ToString());
                u1.practice = Convert.ToBoolean(dt.Rows[i]["Practice"].ToString());
                u1.group = dt.Rows[i]["Groups"].ToString();
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
                    for (int i = CountI; i < uz.Count;)
                    {
                        for (int j = CountJ; j < uz.Count;)
                        {
                            if (grps || u.teacher == uu.teacher)        //если значение группы или имена преподавателей совпадают, то заносим 1 и зеркалим
                            {
                                matrix[i, j] = 1;
                                matrix[j, i] = matrix[i, j];
                                //dataGridView2.Rows[j].Cells[i].Value = matrix[i, j];     для проверки работы нужна dataGridView2
                                //dataGridView2.Rows[i].Cells[j].Value = matrix[j, i];
                            }
                            else        //иначе заносим в массив 0 и зеркалим
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
                for (int i = ii; i < g.VertexCount;)
                {                                   //обход матрицы смежности matrix
                    for (int j = jj; j < g.VertexCount;)
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
            uzel[] practice = new uzel[g.VertexCount];
            foreach (uzel col in g.Vertices)
            {
                col.color = 73;                 //присваиваем всем вершинам цвет 73
                practice[col.number] = col;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////
            //Алгоритм по присвоению цветов 
            int t = 0, x = -1;
            int versh = g.VertexCount;                    //количество вершин в графе (для передачи в метод)
            int[] colors = new int[73];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = i + 1;
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            //Доработка для практик////
            //////////////////////////////////////////////////////////////////////////////////////////////


            foreach (uzel col in practice)
            {

                if (col.color == 73)
                {
                    x++;
                    col.color = colors[x];              //Присваиваем новый цвет    
                    int j = col.number;
                    if (practice[j].practice && practice[j + 1].practice && 
                        practice[j].group == practice[j + 1].group && 
                        practice[j].subject == practice[j + 1].subject)     //если данный узел практика  и следующий узел практика,
                        practice[j + 1].color = colors[x];                  //а также совпадают предметы, то присваиваем следующему
                }                                                           //узлу значение цвета текущего           
                else
                {
                    continue;
                }
                t = col.number;         //Запоминаем номер исследуемой вершины
                ArrayList al = new ArrayList();     //список закрашенных узлов текущим цветом
                for (int j = 0; j < g.VertexCount; j++)     //Идем по строке матрицы по номеру
                {
                    //Блок условий проверки на возможность "раскраски" вершины. Под раскраской - подразумевается присваивание номера
                    if (matrix[t, j] == 0 && t != j && practice[j].color==73)     //Если 0 - можно назначить цвет, что и у вершины
                    {
                        bool allow = true;
                        foreach (int p in al)
                        {
                            if (matrix[j, p] != 0)       //проверка на связи с раскрашенными узлами текщего цвета
                            {
                                allow = false;           //можно = false
                            }
                        }
                        if (!allow)                     //если нет 0
                            continue;
                        if(practice[j].practice)        //если элемент j массива practice= практика
                        {
                            if(j<practice.Length-1 && practice[j+1].practice && practice[j].group== practice[j+1].group && practice[j].subject == practice[j + 1].subject)     //если предыдущий элемент ArrayList равен текущему то переходим к присвоению цвета
                            {
                                bool access = true;
                                foreach (int p in al)               //проходим ArrayList
                                {
                                    if (matrix[j+1, p] != 0)       //проверка на связи с раскрашенными узлами текщего цвета
                                    {
                                        access = false;           //можно = false
                                    }
                                }
                                if (!access)                     //если нет 0
                                    continue;
                                practice[j].color = colors[x];
                                practice[j + 1].color = colors[x];
                            }
                            if (j > 0 && practice[j - 1].practice && practice[j].group == practice[j - 1].group && practice[j].subject == practice[j - 1].subject)  //если предыдущий элемент ArrayList равен текущему то переходим к присвоению цвета
                            {
                                continue;
                            }
                            practice[j].color = colors[x];
                        }
                        else
                        {
                            practice[j].color = colors[x];      
                        }
                        al.Add(j);          //заносим в ArrayList значение цвета узла
                    }
                }
            }
            foreach(uzel col in g.Vertices)
            {
                col.color = practice[col.number].color;     //переносим значения цвета узлов из массива в граф
            }
            
            //////////////////////////////////////////////////////////////////////////////////////////////

            var graphViz = new GraphvizAlgorithm<uzel, Edge<uzel>>(g, @".\", QuickGraph.Graphviz.Dot.GraphvizImageType.Png);
            // render
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            string output = graphViz.Generate(new FileDotEngine(), "graph.dot");
        }

        //Возвращает ноль, если все цвета уже присвоены
        //static bool NoFullColor(int versh, AdjacencyGraph<uzel, Edge<uzel>> g)
        //{
        //    int flag = 0;
        //    foreach (uzel col in g.Vertices)    //Цикл проверяет - все ли цвета посещены
        //    {
        //        if (col.color <= versh)
        //            flag++;
        //    }
        //    if (flag == versh)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}
        
        private static void FormatVertex(object sender, FormatVertexEventArgs<uzel> e)
        {
            string para;
            if (e.Vertex.leacture)
                para = "Лекция";
            else
                para = "Практика";
            e.VertexFormatter.Label = e.Vertex.group + "\n" + e.Vertex.subject + "\n" + e.Vertex.teacher + "\n" + para + "\n\n" + e.Vertex.color.ToString();
            e.VertexFormatter.Shape = GraphvizVertexShape.Circle;
            //e.VertexFormatter.BottomLabel = e.Vertex.subject;
           


            e.VertexFormatter.StrokeColor = GraphvizColor.Black;
            e.VertexFormatter.Font = new GraphvizFont("Calibri", 16);
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

        private void Main_form_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "diplom2DataSet.Load". При необходимости она может быть перемещена или удалена.
            this.loadTableAdapter1.Fill(this.diplom2DataSet1.Load);
        }
    }
}
