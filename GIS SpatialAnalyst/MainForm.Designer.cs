namespace GIS_SpatialAnalyst
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTIN = new System.Windows.Forms.Button();
            this.btnCutLine = new System.Windows.Forms.Button();
            this.btnSLWJ = new System.Windows.Forms.Button();
            this.btnYS = new System.Windows.Forms.Button();
            this.btnSX = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPolyline = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnPoint = new System.Windows.Forms.Button();
            this.picDP = new System.Windows.Forms.PictureBox();
            this.labCoor = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDP)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 632);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTIN);
            this.groupBox2.Controls.Add(this.btnCutLine);
            this.groupBox2.Controls.Add(this.btnSLWJ);
            this.groupBox2.Controls.Add(this.btnYS);
            this.groupBox2.Controls.Add(this.btnSX);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 322);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(204, 310);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "算法分析";
            // 
            // btnTIN
            // 
            this.btnTIN.Location = new System.Drawing.Point(10, 269);
            this.btnTIN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTIN.Name = "btnTIN";
            this.btnTIN.Size = new System.Drawing.Size(180, 37);
            this.btnTIN.TabIndex = 8;
            this.btnTIN.Text = "递归生长算法";
            this.btnTIN.UseVisualStyleBackColor = true;
            // 
            // btnCutLine
            // 
            this.btnCutLine.Location = new System.Drawing.Point(11, 212);
            this.btnCutLine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCutLine.Name = "btnCutLine";
            this.btnCutLine.Size = new System.Drawing.Size(180, 37);
            this.btnCutLine.TabIndex = 7;
            this.btnCutLine.Text = "多边形裁剪算法";
            this.btnCutLine.UseVisualStyleBackColor = true;
            // 
            // btnSLWJ
            // 
            this.btnSLWJ.Location = new System.Drawing.Point(11, 154);
            this.btnSLWJ.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSLWJ.Name = "btnSLWJ";
            this.btnSLWJ.Size = new System.Drawing.Size(180, 37);
            this.btnSLWJ.TabIndex = 6;
            this.btnSLWJ.Text = "矢量外积算法";
            this.btnSLWJ.UseVisualStyleBackColor = true;
            // 
            // btnYS
            // 
            this.btnYS.Location = new System.Drawing.Point(11, 92);
            this.btnYS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnYS.Name = "btnYS";
            this.btnYS.Size = new System.Drawing.Size(180, 37);
            this.btnYS.TabIndex = 5;
            this.btnYS.Text = "右手法则";
            this.btnYS.UseVisualStyleBackColor = true;
            // 
            // btnSX
            // 
            this.btnSX.Location = new System.Drawing.Point(11, 34);
            this.btnSX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSX.Name = "btnSX";
            this.btnSX.Size = new System.Drawing.Size(180, 37);
            this.btnSX.TabIndex = 4;
            this.btnSX.Text = "射线算法";
            this.btnSX.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnPolyline);
            this.groupBox1.Controls.Add(this.btnPolygon);
            this.groupBox1.Controls.Add(this.btnLine);
            this.groupBox1.Controls.Add(this.btnPoint);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(204, 322);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "画图工具";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(10, 247);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(180, 37);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnPolyline
            // 
            this.btnPolyline.Location = new System.Drawing.Point(11, 137);
            this.btnPolyline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPolyline.Name = "btnPolyline";
            this.btnPolyline.Size = new System.Drawing.Size(180, 37);
            this.btnPolyline.TabIndex = 3;
            this.btnPolyline.Text = "画复合线要素";
            this.btnPolyline.UseVisualStyleBackColor = true;
            // 
            // btnPolygon
            // 
            this.btnPolygon.Location = new System.Drawing.Point(11, 191);
            this.btnPolygon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(180, 37);
            this.btnPolygon.TabIndex = 2;
            this.btnPolygon.Text = "画多边形";
            this.btnPolygon.UseVisualStyleBackColor = true;
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(10, 83);
            this.btnLine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(180, 37);
            this.btnLine.TabIndex = 1;
            this.btnLine.Text = "画线段";
            this.btnLine.UseVisualStyleBackColor = true;
            // 
            // btnPoint
            // 
            this.btnPoint.Location = new System.Drawing.Point(11, 31);
            this.btnPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(180, 37);
            this.btnPoint.TabIndex = 0;
            this.btnPoint.Text = "画点";
            this.btnPoint.UseVisualStyleBackColor = true;
            // 
            // picDP
            // 
            this.picDP.BackColor = System.Drawing.Color.White;
            this.picDP.Cursor = System.Windows.Forms.Cursors.UpArrow;
            this.picDP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picDP.Location = new System.Drawing.Point(204, 0);
            this.picDP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.picDP.Name = "picDP";
            this.picDP.Size = new System.Drawing.Size(952, 632);
            this.picDP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDP.TabIndex = 1;
            this.picDP.TabStop = false;
            // 
            // labCoor
            // 
            this.labCoor.AutoSize = true;
            this.labCoor.BackColor = System.Drawing.Color.White;
            this.labCoor.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCoor.ForeColor = System.Drawing.Color.Blue;
            this.labCoor.Location = new System.Drawing.Point(557, 310);
            this.labCoor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labCoor.Name = "labCoor";
            this.labCoor.Size = new System.Drawing.Size(42, 13);
            this.labCoor.TabIndex = 5;
            this.labCoor.Text = "     ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 632);
            this.Controls.Add(this.labCoor);
            this.Controls.Add(this.picDP);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "GIS空间分析实验一：叠加分析相关算法";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPolyline;
        private System.Windows.Forms.Button btnPolygon;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnPoint;
        private System.Windows.Forms.Button btnCutLine;
        private System.Windows.Forms.Button btnSLWJ;
        private System.Windows.Forms.Button btnYS;
        private System.Windows.Forms.Button btnSX;
        private System.Windows.Forms.PictureBox picDP;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label labCoor;
        private System.Windows.Forms.Button btnTIN;
    }
}

