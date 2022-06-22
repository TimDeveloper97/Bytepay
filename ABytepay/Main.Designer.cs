
namespace ABytepay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rEdge = new System.Windows.Forms.RadioButton();
            this.rFirefox = new System.Windows.Forms.RadioButton();
            this.rChrome = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nAmount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lvItems = new System.Windows.Forms.ListView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbItem = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tbnRandom = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.cbRepeat = new System.Windows.Forms.CheckBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.rInternet = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnPaymentIgnore = new System.Windows.Forms.Button();
            this.btnTransactionIgnore = new System.Windows.Forms.Button();
            this.btnLoginIgnore = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnPaymentNormal = new System.Windows.Forms.Button();
            this.btnTransactionNormal = new System.Windows.Forms.Button();
            this.btnLoginNormal = new System.Windows.Forms.Button();
            this.cbVip = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAmount)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbUsername);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(67, 45);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(131, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(67, 19);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(131, 20);
            this.tbUsername.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rEdge);
            this.groupBox3.Controls.Add(this.rFirefox);
            this.groupBox3.Controls.Add(this.rChrome);
            this.groupBox3.Location = new System.Drawing.Point(12, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 47);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "IDE";
            // 
            // rEdge
            // 
            this.rEdge.AutoSize = true;
            this.rEdge.Checked = true;
            this.rEdge.Location = new System.Drawing.Point(138, 19);
            this.rEdge.Name = "rEdge";
            this.rEdge.Size = new System.Drawing.Size(50, 17);
            this.rEdge.TabIndex = 2;
            this.rEdge.TabStop = true;
            this.rEdge.Text = "Edge";
            this.rEdge.UseVisualStyleBackColor = true;
            this.rEdge.CheckedChanged += new System.EventHandler(this.rEdge_CheckedChanged);
            // 
            // rFirefox
            // 
            this.rFirefox.AutoSize = true;
            this.rFirefox.Location = new System.Drawing.Point(76, 19);
            this.rFirefox.Name = "rFirefox";
            this.rFirefox.Size = new System.Drawing.Size(56, 17);
            this.rFirefox.TabIndex = 1;
            this.rFirefox.Text = "Firefox";
            this.rFirefox.UseVisualStyleBackColor = true;
            this.rFirefox.CheckedChanged += new System.EventHandler(this.rFirefox_CheckedChanged);
            // 
            // rChrome
            // 
            this.rChrome.AutoSize = true;
            this.rChrome.Location = new System.Drawing.Point(9, 19);
            this.rChrome.Name = "rChrome";
            this.rChrome.Size = new System.Drawing.Size(61, 17);
            this.rChrome.TabIndex = 0;
            this.rChrome.Text = "Chrome";
            this.rChrome.UseVisualStyleBackColor = true;
            this.rChrome.CheckedChanged += new System.EventHandler(this.rChrome_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.nAmount);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.lvItems);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.tbItem);
            this.groupBox4.Location = new System.Drawing.Point(222, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(408, 215);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Items";
            // 
            // nAmount
            // 
            this.nAmount.Location = new System.Drawing.Point(274, 15);
            this.nAmount.Name = "nAmount";
            this.nAmount.Size = new System.Drawing.Size(32, 20);
            this.nAmount.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Amount:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Name:";
            // 
            // lvItems
            // 
            this.lvItems.FullRowSelect = true;
            this.lvItems.HideSelection = false;
            this.lvItems.Location = new System.Drawing.Point(6, 49);
            this.lvItems.MultiSelect = false;
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(393, 160);
            this.lvItems.TabIndex = 2;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            this.lvItems.DoubleClick += new System.EventHandler(this.lvItems_DoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(324, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbItem
            // 
            this.tbItem.Location = new System.Drawing.Point(50, 15);
            this.tbItem.Name = "tbItem";
            this.tbItem.Size = new System.Drawing.Size(166, 20);
            this.tbItem.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbnRandom);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.tbEmail);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.tbAddress);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.tbPhone);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.tbName);
            this.groupBox5.Location = new System.Drawing.Point(12, 144);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(204, 150);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Receiver";
            // 
            // tbnRandom
            // 
            this.tbnRandom.Location = new System.Drawing.Point(123, 12);
            this.tbnRandom.Name = "tbnRandom";
            this.tbnRandom.Size = new System.Drawing.Size(75, 23);
            this.tbnRandom.TabIndex = 10;
            this.tbnRandom.Text = "Random";
            this.tbnRandom.UseVisualStyleBackColor = true;
            this.tbnRandom.Click += new System.EventHandler(this.tbnRandom_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Email:";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(67, 119);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(131, 20);
            this.tbEmail.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Address:";
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(67, 93);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(131, 20);
            this.tbAddress.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Phone:";
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(67, 67);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(131, 20);
            this.tbPhone.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Name:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(67, 41);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(131, 20);
            this.tbName.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbVip);
            this.groupBox6.Controls.Add(this.btnStop);
            this.groupBox6.Controls.Add(this.cbRepeat);
            this.groupBox6.Controls.Add(this.btnAuto);
            this.groupBox6.Location = new System.Drawing.Point(222, 234);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(408, 60);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Auto";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(286, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(105, 32);
            this.btnStop.TabIndex = 3;
            this.btnStop.Tag = "false";
            this.btnStop.Text = "[End] Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cbRepeat
            // 
            this.cbRepeat.AutoSize = true;
            this.cbRepeat.Checked = true;
            this.cbRepeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRepeat.Location = new System.Drawing.Point(9, 28);
            this.cbRepeat.Name = "cbRepeat";
            this.cbRepeat.Size = new System.Drawing.Size(70, 17);
            this.cbRepeat.TabIndex = 8;
            this.cbRepeat.Text = "Incognito";
            this.cbRepeat.UseVisualStyleBackColor = true;
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(157, 19);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(111, 32);
            this.btnAuto.TabIndex = 1;
            this.btnAuto.Tag = "false";
            this.btnAuto.Text = "[Start] Auto";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // rInternet
            // 
            this.rInternet.AutoSize = true;
            this.rInternet.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rInternet.Checked = true;
            this.rInternet.ForeColor = System.Drawing.Color.Black;
            this.rInternet.Location = new System.Drawing.Point(12, 390);
            this.rInternet.Name = "rInternet";
            this.rInternet.Size = new System.Drawing.Size(85, 17);
            this.rInternet.TabIndex = 6;
            this.rInternet.TabStop = true;
            this.rInternet.Text = "radioButton1";
            this.rInternet.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox8);
            this.groupBox2.Controls.Add(this.groupBox7);
            this.groupBox2.Location = new System.Drawing.Point(12, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(618, 84);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnPaymentIgnore);
            this.groupBox8.Controls.Add(this.btnTransactionIgnore);
            this.groupBox8.Controls.Add(this.btnLoginIgnore);
            this.groupBox8.Location = new System.Drawing.Point(312, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(297, 57);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Ignore";
            // 
            // btnPaymentIgnore
            // 
            this.btnPaymentIgnore.Location = new System.Drawing.Point(207, 19);
            this.btnPaymentIgnore.Name = "btnPaymentIgnore";
            this.btnPaymentIgnore.Size = new System.Drawing.Size(82, 32);
            this.btnPaymentIgnore.TabIndex = 4;
            this.btnPaymentIgnore.Tag = "false";
            this.btnPaymentIgnore.Text = "[3] Payment";
            this.btnPaymentIgnore.UseVisualStyleBackColor = true;
            this.btnPaymentIgnore.Click += new System.EventHandler(this.btnPaymentIgnore_Click);
            // 
            // btnTransactionIgnore
            // 
            this.btnTransactionIgnore.Location = new System.Drawing.Point(94, 19);
            this.btnTransactionIgnore.Name = "btnTransactionIgnore";
            this.btnTransactionIgnore.Size = new System.Drawing.Size(101, 32);
            this.btnTransactionIgnore.TabIndex = 3;
            this.btnTransactionIgnore.Tag = "false";
            this.btnTransactionIgnore.Text = "[2] Transaction";
            this.btnTransactionIgnore.UseVisualStyleBackColor = true;
            this.btnTransactionIgnore.Click += new System.EventHandler(this.btnTransactionIgnore_Click);
            // 
            // btnLoginIgnore
            // 
            this.btnLoginIgnore.Location = new System.Drawing.Point(6, 19);
            this.btnLoginIgnore.Name = "btnLoginIgnore";
            this.btnLoginIgnore.Size = new System.Drawing.Size(82, 32);
            this.btnLoginIgnore.TabIndex = 2;
            this.btnLoginIgnore.Tag = "false";
            this.btnLoginIgnore.Text = "[1] Login";
            this.btnLoginIgnore.UseVisualStyleBackColor = true;
            this.btnLoginIgnore.Click += new System.EventHandler(this.btnLoginIgnore_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnPaymentNormal);
            this.groupBox7.Controls.Add(this.btnTransactionNormal);
            this.groupBox7.Controls.Add(this.btnLoginNormal);
            this.groupBox7.Location = new System.Drawing.Point(9, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(297, 57);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Normal";
            // 
            // btnPaymentNormal
            // 
            this.btnPaymentNormal.Location = new System.Drawing.Point(207, 19);
            this.btnPaymentNormal.Name = "btnPaymentNormal";
            this.btnPaymentNormal.Size = new System.Drawing.Size(82, 32);
            this.btnPaymentNormal.TabIndex = 4;
            this.btnPaymentNormal.Tag = "false";
            this.btnPaymentNormal.Text = "[3] Payment";
            this.btnPaymentNormal.UseVisualStyleBackColor = true;
            this.btnPaymentNormal.Click += new System.EventHandler(this.btnPaymentNormal_Click);
            // 
            // btnTransactionNormal
            // 
            this.btnTransactionNormal.Location = new System.Drawing.Point(94, 19);
            this.btnTransactionNormal.Name = "btnTransactionNormal";
            this.btnTransactionNormal.Size = new System.Drawing.Size(101, 32);
            this.btnTransactionNormal.TabIndex = 3;
            this.btnTransactionNormal.Tag = "false";
            this.btnTransactionNormal.Text = "[2] Transaction";
            this.btnTransactionNormal.UseVisualStyleBackColor = true;
            this.btnTransactionNormal.Click += new System.EventHandler(this.btnTransactionNormal_Click);
            // 
            // btnLoginNormal
            // 
            this.btnLoginNormal.Location = new System.Drawing.Point(6, 19);
            this.btnLoginNormal.Name = "btnLoginNormal";
            this.btnLoginNormal.Size = new System.Drawing.Size(82, 32);
            this.btnLoginNormal.TabIndex = 2;
            this.btnLoginNormal.Tag = "false";
            this.btnLoginNormal.Text = "[1] Login";
            this.btnLoginNormal.UseVisualStyleBackColor = true;
            this.btnLoginNormal.Click += new System.EventHandler(this.btnLoginNormal_Click);
            // 
            // cbVip
            // 
            this.cbVip.AutoSize = true;
            this.cbVip.Checked = true;
            this.cbVip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbVip.Location = new System.Drawing.Point(85, 28);
            this.cbVip.Name = "cbVip";
            this.cbVip.Size = new System.Drawing.Size(41, 17);
            this.cbVip.TabIndex = 9;
            this.cbVip.Text = "Vip";
            this.cbVip.UseVisualStyleBackColor = true;
            this.cbVip.CheckedChanged += new System.EventHandler(this.cbVip_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 413);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.rInternet);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABytepay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nAmount)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rEdge;
        private System.Windows.Forms.RadioButton rFirefox;
        private System.Windows.Forms.RadioButton rChrome;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox tbItem;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button tbnRandom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.CheckBox cbRepeat;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RadioButton rInternet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnPaymentIgnore;
        private System.Windows.Forms.Button btnTransactionIgnore;
        private System.Windows.Forms.Button btnLoginIgnore;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnPaymentNormal;
        private System.Windows.Forms.Button btnTransactionNormal;
        private System.Windows.Forms.Button btnLoginNormal;
        private System.Windows.Forms.CheckBox cbVip;
    }
}

