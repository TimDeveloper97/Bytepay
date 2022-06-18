using ABytepay.Controllers;
using ABytepay.Domain;
using ABytepay.Helpers;
using ABytepay.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABytepay
{
    public partial class Main : Form
    {
        IBaseController @base;
        IWebDriver @web;
        List<Product> @products;

        public Main()
        {
            InitializeComponent();
            Init();
        }

        #region =================== Init =====================

        void Init()
        {
            @base = new BaseController();
            @products = new List<Product>();
            @base.InitEdge();
            @web = @base.GetEdge();

            nAmount.Value = 1;
            tbnRandom_Click(null, null);

            lvItems.Columns.Add("Index");
            lvItems.Columns.Add("Name", 210);
            lvItems.Columns.Add("Amount");


            MockData();
        }

        void MockData()
        {
            int index = 0;
            
            tbUsername.Text = "db05111997@gmail.com";
            tbPassword.Text = "123456789";

            @products.Add(new Product { Amount = "2", Name = "Bia tiger lon thùng 24" });
            @products.Add(new Product { Amount = "2", Name = "1 thùng nước Sting lon" });
            @products.Add(new Product { Amount = "2", Name = "Sữa rữa mặt cho da khô Beaty Med" });

            foreach (var item in @products)
            {
                lvItems.Items.Add(new ListViewItem(new string[]
                {
                    (index + 1).ToString(),
                    item.Name,
                    item.Amount
                }));
            }
        }

        #endregion

        #region =================== Actions =====================

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new LoginController(
                @web,
                "https://bytepay.vn/login",
                tbUsername.Text,
                tbPassword.Text).Execute();
        }
        private void btnTransaction_Click(object sender, EventArgs e)
        {
            new TransactionController(
                @web,
                @products, 
                new Receiver 
                {
                    Name = tbName.Text, 
                    Address = tbAddress.Text,
                    Email = tbEmail.Text,
                    Phone = tbPhone.Text
                }).Execute();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            new PaymentController(
                @web,
                Clipboard.GetText()).Execute();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            btnLogin_Click(null, null);
            @web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
            btnTransaction_Click(null, null);
            btnPayment_Click(null, null);
        }

        private void cbRepeat_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region =================== Event & Method =====================

        private void rChrome_CheckedChanged(object sender, EventArgs e)
        {
            @base.InitChrome();
            @web = @base.GetChrome();
        }

        private void rFirefox_CheckedChanged(object sender, EventArgs e)
        {
            @base.InitFirefox();
            @web = @base.GetFirefox();
        }

        private void rEdge_CheckedChanged(object sender, EventArgs e)
        {
            @base.InitEdge();
            @web = @base.GetEdge();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            @web.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var item = tbItem.Text;
            var amount = nAmount.Value;
            if (amount > 0 && !string.IsNullOrEmpty(item))
            {
                lvItems.Items.Add(new ListViewItem(new string[] { "1", item, amount.ToString() }));
                @products.Add(new Product { Name = item, Amount = amount.ToString() });
                tbItem.Text = "";
            }
        }

        private void lvItems_DoubleClick(object sender, EventArgs e)
        {
            @products.RemoveAt(lvItems.SelectedItems[0].Index);
            lvItems.Items.RemoveAt(lvItems.SelectedItems[0].Index);
        }

        private void tbnRandom_Click(object sender, EventArgs e)
        {
            tbAddress.Text = RandomHelper.GetAddress();
            tbName.Text = RandomHelper.GetName();
            tbEmail.Text = RandomHelper.GetEmail(tbName.Text);
            tbPhone.Text = RandomHelper.GetPhone();
        }

        #endregion
    }
}
