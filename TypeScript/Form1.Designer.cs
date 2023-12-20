namespace JSONtoTypeScript
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            richTextBox1 = new RichTextBox();
            button3 = new Button();
            label1 = new Label();
            richTextBox2 = new RichTextBox();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(179, 96);
            button1.Name = "button1";
            button1.Size = new Size(113, 29);
            button1.TabIndex = 1;
            button1.Text = "Open File";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(741, 96);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 2;
            button2.Text = "Translate";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(179, 131);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(1131, 27);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(192, 205);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(588, 481);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // button3
            // 
            button3.Location = new Point(1216, 96);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 5;
            button3.Text = "Export";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(596, 9);
            label1.Name = "label1";
            label1.Size = new Size(419, 46);
            label1.TabIndex = 6;
            label1.Text = "txtUMLJSON to TypeScript";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(815, 205);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(495, 481);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = "";
            richTextBox2.TextChanged += richTextBox2_TextChanged;
            // 
            // button4
            // 
            button4.Location = new Point(12, 212);
            button4.Name = "button4";
            button4.Size = new Size(145, 29);
            button4.TabIndex = 7;
            button4.Text = "Help";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(421, 703);
            button5.Name = "button5";
            button5.Size = new Size(124, 52);
            button5.TabIndex = 8;
            button5.Text = "Copy JSON";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(1022, 703);
            button6.Name = "button6";
            button6.Size = new Size(113, 52);
            button6.TabIndex = 9;
            button6.Text = "Copy Code";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(730, 704);
            button7.Name = "button7";
            button7.Size = new Size(115, 50);
            button7.TabIndex = 10;
            button7.Text = "Clear Box";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(12, 273);
            button8.Name = "button8";
            button8.Size = new Size(145, 29);
            button8.TabIndex = 11;
            button8.Text = "Parsing Code";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.Location = new Point(12, 333);
            button9.Name = "button9";
            button9.Size = new Size(145, 29);
            button9.TabIndex = 12;
            button9.Text = "VisualizeJSON";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1332, 903);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(richTextBox2);
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private RichTextBox richTextBox1;
        private Button button3;
        private Label label1;
        private RichTextBox richTextBox2;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
    }
}