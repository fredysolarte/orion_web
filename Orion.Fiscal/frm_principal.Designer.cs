
namespace Orion.Fiscal
{
    partial class frm_principal
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
            this.txt_puerto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_imprimir = new System.Windows.Forms.Button();
            this.btn_reportez = new System.Windows.Forms.Button();
            this.btn_reportex = new System.Windows.Forms.Button();
            this.txt_rif = new System.Windows.Forms.TextBox();
            this.txt_compania = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_direccion = new System.Windows.Forms.TextBox();
            this.txt_municipio = new System.Windows.Forms.TextBox();
            this.txt_telefonos = new System.Windows.Forms.TextBox();
            this.Direccion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rt_log = new System.Windows.Forms.RichTextBox();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.rc_tipofac = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_nrofac = new System.Windows.Forms.TextBox();
            this.rg_datos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.rg_datos)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_puerto
            // 
            this.txt_puerto.Location = new System.Drawing.Point(111, 28);
            this.txt_puerto.Name = "txt_puerto";
            this.txt_puerto.Size = new System.Drawing.Size(100, 31);
            this.txt_puerto.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Puerto";
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.Location = new System.Drawing.Point(11, 592);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Size = new System.Drawing.Size(116, 59);
            this.btn_imprimir.TabIndex = 2;
            this.btn_imprimir.Text = "Imprimir";
            this.btn_imprimir.UseVisualStyleBackColor = true;
            this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
            // 
            // btn_reportez
            // 
            this.btn_reportez.Location = new System.Drawing.Point(133, 592);
            this.btn_reportez.Name = "btn_reportez";
            this.btn_reportez.Size = new System.Drawing.Size(135, 59);
            this.btn_reportez.TabIndex = 3;
            this.btn_reportez.Text = "Reporte Z";
            this.btn_reportez.UseVisualStyleBackColor = true;
            this.btn_reportez.Click += new System.EventHandler(this.btn_reportez_Click);
            // 
            // btn_reportex
            // 
            this.btn_reportex.Location = new System.Drawing.Point(274, 592);
            this.btn_reportex.Name = "btn_reportex";
            this.btn_reportex.Size = new System.Drawing.Size(137, 59);
            this.btn_reportex.TabIndex = 4;
            this.btn_reportex.Text = "Reporte X";
            this.btn_reportex.UseVisualStyleBackColor = true;
            this.btn_reportex.Click += new System.EventHandler(this.btn_reportex_Click);
            // 
            // txt_rif
            // 
            this.txt_rif.Location = new System.Drawing.Point(111, 65);
            this.txt_rif.Name = "txt_rif";
            this.txt_rif.Size = new System.Drawing.Size(212, 31);
            this.txt_rif.TabIndex = 5;
            // 
            // txt_compania
            // 
            this.txt_compania.Location = new System.Drawing.Point(336, 65);
            this.txt_compania.Name = "txt_compania";
            this.txt_compania.Size = new System.Drawing.Size(360, 31);
            this.txt_compania.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Compañia";
            // 
            // txt_direccion
            // 
            this.txt_direccion.Location = new System.Drawing.Point(111, 102);
            this.txt_direccion.Name = "txt_direccion";
            this.txt_direccion.Size = new System.Drawing.Size(707, 31);
            this.txt_direccion.TabIndex = 8;
            // 
            // txt_municipio
            // 
            this.txt_municipio.Location = new System.Drawing.Point(111, 139);
            this.txt_municipio.Name = "txt_municipio";
            this.txt_municipio.Size = new System.Drawing.Size(707, 31);
            this.txt_municipio.TabIndex = 9;
            // 
            // txt_telefonos
            // 
            this.txt_telefonos.Location = new System.Drawing.Point(111, 176);
            this.txt_telefonos.Name = "txt_telefonos";
            this.txt_telefonos.Size = new System.Drawing.Size(707, 31);
            this.txt_telefonos.TabIndex = 10;
            // 
            // Direccion
            // 
            this.Direccion.AutoSize = true;
            this.Direccion.Location = new System.Drawing.Point(3, 105);
            this.Direccion.Name = "Direccion";
            this.Direccion.Size = new System.Drawing.Size(102, 25);
            this.Direccion.TabIndex = 11;
            this.Direccion.Text = "Direccion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Telefono";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Municipio";
            // 
            // rt_log
            // 
            this.rt_log.Location = new System.Drawing.Point(824, 12);
            this.rt_log.Name = "rt_log";
            this.rt_log.Size = new System.Drawing.Size(441, 639);
            this.rt_log.TabIndex = 14;
            this.rt_log.Text = "";
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(683, 215);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(135, 59);
            this.btn_buscar.TabIndex = 15;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // rc_tipofac
            // 
            this.rc_tipofac.FormattingEnabled = true;
            this.rc_tipofac.Location = new System.Drawing.Point(136, 232);
            this.rc_tipofac.Name = "rc_tipofac";
            this.rc_tipofac.Size = new System.Drawing.Size(306, 33);
            this.rc_tipofac.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "Documento";
            // 
            // txt_nrofac
            // 
            this.txt_nrofac.Location = new System.Drawing.Point(448, 234);
            this.txt_nrofac.Name = "txt_nrofac";
            this.txt_nrofac.Size = new System.Drawing.Size(216, 31);
            this.txt_nrofac.TabIndex = 18;
            // 
            // rg_datos
            // 
            this.rg_datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rg_datos.Location = new System.Drawing.Point(8, 280);
            this.rg_datos.Name = "rg_datos";
            this.rg_datos.RowHeadersWidth = 82;
            this.rg_datos.RowTemplate.Height = 33;
            this.rg_datos.Size = new System.Drawing.Size(810, 294);
            this.rg_datos.TabIndex = 19;
            // 
            // frm_principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 670);
            this.Controls.Add(this.rg_datos);
            this.Controls.Add(this.txt_nrofac);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rc_tipofac);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.rt_log);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Direccion);
            this.Controls.Add(this.txt_telefonos);
            this.Controls.Add(this.txt_municipio);
            this.Controls.Add(this.txt_direccion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_compania);
            this.Controls.Add(this.txt_rif);
            this.Controls.Add(this.btn_reportex);
            this.Controls.Add(this.btn_reportez);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_puerto);
            this.Name = "frm_principal";
            this.Text = "Impresion Facturas";
            ((System.ComponentModel.ISupportInitialize)(this.rg_datos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_puerto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_imprimir;
        private System.Windows.Forms.Button btn_reportez;
        private System.Windows.Forms.Button btn_reportex;
        private System.Windows.Forms.TextBox txt_rif;
        private System.Windows.Forms.TextBox txt_compania;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_direccion;
        private System.Windows.Forms.TextBox txt_municipio;
        private System.Windows.Forms.TextBox txt_telefonos;
        private System.Windows.Forms.Label Direccion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rt_log;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.ComboBox rc_tipofac;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_nrofac;
        private System.Windows.Forms.DataGridView rg_datos;
    }
}

