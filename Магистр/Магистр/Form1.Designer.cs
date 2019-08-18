namespace Магистр
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.НадписьУкажитеКоличество = new System.Windows.Forms.Label();
            this.КоличествоВершинГрафа = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.a1 = new System.Windows.Forms.TextBox();
            this.a2 = new System.Windows.Forms.TextBox();
            this.a3 = new System.Windows.Forms.TextBox();
            this.a4 = new System.Windows.Forms.TextBox();
            this.a5 = new System.Windows.Forms.TextBox();
            this.a6 = new System.Windows.Forms.TextBox();
            this.a7 = new System.Windows.Forms.TextBox();
            this.a8 = new System.Windows.Forms.TextBox();
            this.a9 = new System.Windows.Forms.TextBox();
            this.a10 = new System.Windows.Forms.TextBox();
            this.Text_a1 = new System.Windows.Forms.Label();
            this.Text_a2 = new System.Windows.Forms.Label();
            this.Text_a3 = new System.Windows.Forms.Label();
            this.Text_a4 = new System.Windows.Forms.Label();
            this.Text_a5 = new System.Windows.Forms.Label();
            this.Text_a6 = new System.Windows.Forms.Label();
            this.Text_a7 = new System.Windows.Forms.Label();
            this.Text_a8 = new System.Windows.Forms.Label();
            this.Text_a9 = new System.Windows.Forms.Label();
            this.Text_a10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // НадписьУкажитеКоличество
            // 
            this.НадписьУкажитеКоличество.AutoSize = true;
            this.НадписьУкажитеКоличество.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.НадписьУкажитеКоличество.Location = new System.Drawing.Point(3, 13);
            this.НадписьУкажитеКоличество.Name = "НадписьУкажитеКоличество";
            this.НадписьУкажитеКоличество.Size = new System.Drawing.Size(282, 20);
            this.НадписьУкажитеКоличество.TabIndex = 0;
            this.НадписьУкажитеКоличество.Text = "Укажите количество вершин графа";
            // 
            // КоличествоВершинГрафа
            // 
            this.КоличествоВершинГрафа.FormattingEnabled = true;
            this.КоличествоВершинГрафа.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.КоличествоВершинГрафа.Location = new System.Drawing.Point(7, 36);
            this.КоличествоВершинГрафа.Name = "КоличествоВершинГрафа";
            this.КоличествоВершинГрафа.Size = new System.Drawing.Size(149, 21);
            this.КоличествоВершинГрафа.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Подтвердить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // a1
            // 
            this.a1.Location = new System.Drawing.Point(31, 63);
            this.a1.Name = "a1";
            this.a1.Size = new System.Drawing.Size(100, 20);
            this.a1.TabIndex = 3;
            this.a1.Visible = false;
            this.a1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a2
            // 
            this.a2.Location = new System.Drawing.Point(31, 89);
            this.a2.Name = "a2";
            this.a2.Size = new System.Drawing.Size(100, 20);
            this.a2.TabIndex = 4;
            this.a2.Visible = false;
            this.a2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a3
            // 
            this.a3.Location = new System.Drawing.Point(31, 115);
            this.a3.Name = "a3";
            this.a3.Size = new System.Drawing.Size(100, 20);
            this.a3.TabIndex = 5;
            this.a3.Visible = false;
            this.a3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a4
            // 
            this.a4.Location = new System.Drawing.Point(31, 141);
            this.a4.Name = "a4";
            this.a4.Size = new System.Drawing.Size(100, 20);
            this.a4.TabIndex = 6;
            this.a4.Visible = false;
            this.a4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a5
            // 
            this.a5.Location = new System.Drawing.Point(31, 167);
            this.a5.Name = "a5";
            this.a5.Size = new System.Drawing.Size(100, 20);
            this.a5.TabIndex = 7;
            this.a5.Visible = false;
            this.a5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a6
            // 
            this.a6.Location = new System.Drawing.Point(31, 193);
            this.a6.Name = "a6";
            this.a6.Size = new System.Drawing.Size(100, 20);
            this.a6.TabIndex = 8;
            this.a6.Visible = false;
            this.a6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a7
            // 
            this.a7.Location = new System.Drawing.Point(31, 219);
            this.a7.Name = "a7";
            this.a7.Size = new System.Drawing.Size(100, 20);
            this.a7.TabIndex = 9;
            this.a7.Visible = false;
            this.a7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a8
            // 
            this.a8.Location = new System.Drawing.Point(31, 245);
            this.a8.Name = "a8";
            this.a8.Size = new System.Drawing.Size(100, 20);
            this.a8.TabIndex = 10;
            this.a8.Visible = false;
            this.a8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a9
            // 
            this.a9.Location = new System.Drawing.Point(31, 271);
            this.a9.Name = "a9";
            this.a9.Size = new System.Drawing.Size(100, 20);
            this.a9.TabIndex = 11;
            this.a9.Visible = false;
            this.a9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // a10
            // 
            this.a10.Location = new System.Drawing.Point(31, 297);
            this.a10.Name = "a10";
            this.a10.Size = new System.Drawing.Size(100, 20);
            this.a10.TabIndex = 12;
            this.a10.Visible = false;
            this.a10.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VertexKeyPress);
            // 
            // Text_a1
            // 
            this.Text_a1.AutoSize = true;
            this.Text_a1.Location = new System.Drawing.Point(6, 66);
            this.Text_a1.Name = "Text_a1";
            this.Text_a1.Size = new System.Drawing.Size(19, 13);
            this.Text_a1.TabIndex = 13;
            this.Text_a1.Text = "a1";
            this.Text_a1.Visible = false;
            // 
            // Text_a2
            // 
            this.Text_a2.AutoSize = true;
            this.Text_a2.Location = new System.Drawing.Point(6, 92);
            this.Text_a2.Name = "Text_a2";
            this.Text_a2.Size = new System.Drawing.Size(19, 13);
            this.Text_a2.TabIndex = 14;
            this.Text_a2.Text = "a2";
            this.Text_a2.Visible = false;
            // 
            // Text_a3
            // 
            this.Text_a3.AutoSize = true;
            this.Text_a3.Location = new System.Drawing.Point(6, 118);
            this.Text_a3.Name = "Text_a3";
            this.Text_a3.Size = new System.Drawing.Size(19, 13);
            this.Text_a3.TabIndex = 15;
            this.Text_a3.Text = "a3";
            this.Text_a3.Visible = false;
            // 
            // Text_a4
            // 
            this.Text_a4.AutoSize = true;
            this.Text_a4.Location = new System.Drawing.Point(6, 144);
            this.Text_a4.Name = "Text_a4";
            this.Text_a4.Size = new System.Drawing.Size(19, 13);
            this.Text_a4.TabIndex = 16;
            this.Text_a4.Text = "a4";
            this.Text_a4.Visible = false;
            // 
            // Text_a5
            // 
            this.Text_a5.AutoSize = true;
            this.Text_a5.Location = new System.Drawing.Point(6, 170);
            this.Text_a5.Name = "Text_a5";
            this.Text_a5.Size = new System.Drawing.Size(19, 13);
            this.Text_a5.TabIndex = 17;
            this.Text_a5.Text = "a5";
            this.Text_a5.Visible = false;
            // 
            // Text_a6
            // 
            this.Text_a6.AutoSize = true;
            this.Text_a6.Location = new System.Drawing.Point(6, 196);
            this.Text_a6.Name = "Text_a6";
            this.Text_a6.Size = new System.Drawing.Size(19, 13);
            this.Text_a6.TabIndex = 18;
            this.Text_a6.Text = "a6";
            this.Text_a6.Visible = false;
            // 
            // Text_a7
            // 
            this.Text_a7.AutoSize = true;
            this.Text_a7.Location = new System.Drawing.Point(6, 222);
            this.Text_a7.Name = "Text_a7";
            this.Text_a7.Size = new System.Drawing.Size(19, 13);
            this.Text_a7.TabIndex = 19;
            this.Text_a7.Text = "a7";
            this.Text_a7.Visible = false;
            // 
            // Text_a8
            // 
            this.Text_a8.AutoSize = true;
            this.Text_a8.Location = new System.Drawing.Point(6, 248);
            this.Text_a8.Name = "Text_a8";
            this.Text_a8.Size = new System.Drawing.Size(19, 13);
            this.Text_a8.TabIndex = 20;
            this.Text_a8.Text = "a8";
            this.Text_a8.Visible = false;
            // 
            // Text_a9
            // 
            this.Text_a9.AutoSize = true;
            this.Text_a9.Location = new System.Drawing.Point(6, 274);
            this.Text_a9.Name = "Text_a9";
            this.Text_a9.Size = new System.Drawing.Size(19, 13);
            this.Text_a9.TabIndex = 21;
            this.Text_a9.Text = "a9";
            this.Text_a9.Visible = false;
            // 
            // Text_a10
            // 
            this.Text_a10.AutoSize = true;
            this.Text_a10.Location = new System.Drawing.Point(6, 300);
            this.Text_a10.Name = "Text_a10";
            this.Text_a10.Size = new System.Drawing.Size(25, 13);
            this.Text_a10.TabIndex = 22;
            this.Text_a10.Text = "a10";
            this.Text_a10.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(163, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Построение графа";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.Text_a10);
            this.panel1.Controls.Add(this.Text_a9);
            this.panel1.Controls.Add(this.Text_a8);
            this.panel1.Controls.Add(this.Text_a7);
            this.panel1.Controls.Add(this.Text_a6);
            this.panel1.Controls.Add(this.Text_a5);
            this.panel1.Controls.Add(this.Text_a4);
            this.panel1.Controls.Add(this.Text_a3);
            this.panel1.Controls.Add(this.Text_a2);
            this.panel1.Controls.Add(this.Text_a1);
            this.panel1.Controls.Add(this.a10);
            this.panel1.Controls.Add(this.a9);
            this.panel1.Controls.Add(this.a8);
            this.panel1.Controls.Add(this.a7);
            this.panel1.Controls.Add(this.a6);
            this.panel1.Controls.Add(this.a5);
            this.panel1.Controls.Add(this.a4);
            this.panel1.Controls.Add(this.a3);
            this.panel1.Controls.Add(this.a2);
            this.panel1.Controls.Add(this.a1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.КоличествоВершинГрафа);
            this.panel1.Controls.Add(this.НадписьУкажитеКоличество);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 429);
            this.panel1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(7, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 69);
            this.button3.TabIndex = 24;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox.Location = new System.Drawing.Point(82, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(773, 423);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            // 
            // button4
            // 
            this.button4.Image = global::Магистр.Properties.Resources.Ребро;
            this.button4.Location = new System.Drawing.Point(9, 82);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(67, 59);
            this.button4.TabIndex = 25;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 429);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Алгоритм 1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label НадписьУкажитеКоличество;
        private System.Windows.Forms.ComboBox КоличествоВершинГрафа;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox a1;
        private System.Windows.Forms.TextBox a2;
        private System.Windows.Forms.TextBox a3;
        private System.Windows.Forms.TextBox a4;
        private System.Windows.Forms.TextBox a5;
        private System.Windows.Forms.TextBox a6;
        private System.Windows.Forms.TextBox a7;
        private System.Windows.Forms.TextBox a8;
        private System.Windows.Forms.TextBox a9;
        private System.Windows.Forms.TextBox a10;
        private System.Windows.Forms.Label Text_a1;
        private System.Windows.Forms.Label Text_a2;
        private System.Windows.Forms.Label Text_a3;
        private System.Windows.Forms.Label Text_a4;
        private System.Windows.Forms.Label Text_a5;
        private System.Windows.Forms.Label Text_a6;
        private System.Windows.Forms.Label Text_a7;
        private System.Windows.Forms.Label Text_a8;
        private System.Windows.Forms.Label Text_a9;
        private System.Windows.Forms.Label Text_a10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

