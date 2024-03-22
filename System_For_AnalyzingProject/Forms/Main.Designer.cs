namespace System_For_AnalyzingProject
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TableInterface = new System.Windows.Forms.TableLayoutPanel();
            this.Label_Create = new System.Windows.Forms.Label();
            this.PictureBox_Create_1 = new System.Windows.Forms.PictureBox();
            this.PictureBox_Create_2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Create_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Create_2)).BeginInit();
            this.SuspendLayout();
            // 
            // TableInterface
            // 
            this.TableInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableInterface.AutoScroll = true;
            this.TableInterface.ColumnCount = 1;
            this.TableInterface.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableInterface.Location = new System.Drawing.Point(14, 14);
            this.TableInterface.Margin = new System.Windows.Forms.Padding(5, 5, 5, 60);
            this.TableInterface.Name = "TableInterface";
            this.TableInterface.RowCount = 1;
            this.TableInterface.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableInterface.Size = new System.Drawing.Size(772, 367);
            this.TableInterface.TabIndex = 0;
            // 
            // Label_Create
            // 
            this.Label_Create.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Label_Create.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Create.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label_Create.Location = new System.Drawing.Point(247, 392);
            this.Label_Create.Name = "Label_Create";
            this.Label_Create.Size = new System.Drawing.Size(307, 50);
            this.Label_Create.TabIndex = 2;
            this.Label_Create.Text = "Создать документ по списку кейсов";
            this.Label_Create.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Create.Click += new System.EventHandler(this.Button_Create_Click);
            // 
            // PictureBox_Create_1
            // 
            this.PictureBox_Create_1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PictureBox_Create_1.BackgroundImage = global::System_For_AnalyzingProject.Properties.Resources.Hammer;
            this.PictureBox_Create_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox_Create_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox_Create_1.Location = new System.Drawing.Point(192, 392);
            this.PictureBox_Create_1.Margin = new System.Windows.Forms.Padding(2, 10, 2, 10);
            this.PictureBox_Create_1.Name = "PictureBox_Create_1";
            this.PictureBox_Create_1.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Create_1.TabIndex = 1;
            this.PictureBox_Create_1.TabStop = false;
            this.PictureBox_Create_1.Click += new System.EventHandler(this.Button_Create_Click);
            // 
            // PictureBox_Create_2
            // 
            this.PictureBox_Create_2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PictureBox_Create_2.BackgroundImage = global::System_For_AnalyzingProject.Properties.Resources.StackOfPaper;
            this.PictureBox_Create_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox_Create_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox_Create_2.Location = new System.Drawing.Point(559, 392);
            this.PictureBox_Create_2.Margin = new System.Windows.Forms.Padding(2, 10, 2, 10);
            this.PictureBox_Create_2.Name = "PictureBox_Create_2";
            this.PictureBox_Create_2.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Create_2.TabIndex = 3;
            this.PictureBox_Create_2.TabStop = false;
            this.PictureBox_Create_2.Click += new System.EventHandler(this.Button_Create_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PictureBox_Create_2);
            this.Controls.Add(this.Label_Create);
            this.Controls.Add(this.PictureBox_Create_1);
            this.Controls.Add(this.TableInterface);
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Create_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Create_2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableInterface;
        private System.Windows.Forms.PictureBox PictureBox_Create_1;
        private System.Windows.Forms.Label Label_Create;
        private System.Windows.Forms.PictureBox PictureBox_Create_2;
    }
}