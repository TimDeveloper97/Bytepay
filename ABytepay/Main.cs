using ABytepay.Controllers;
using ABytepay.Domain;
using ABytepay.Helpers;
using ABytepay.Models;
using Firebase.Database;
using Firebase.Database.Query;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABytepay
{
    public partial class Main : Form
    {
        public static bool IsStart = false;
        public static bool IsClose = false;
        public static bool IsVip = false;
        public static bool IsError = false;

        static FirebaseObject<User> _user;
        static bool IsInternet = true;
        static int currentitem = 0;

        IBaseController @base, @baseIgnore;
        IWebDriver @web, @webIgnore;
        List<Product> @products;
        BaseFirebase _firebase;
        Login _login;

        public Main(Login login)
        {
            InitializeComponent();

            _login = login;
            Init();
        }

        #region =================== Init =====================

        void Init()
        {
            @base = new BaseController();
            _firebase = new BaseFirebase();
            @baseIgnore = new BaseIgnoreController();
            @products = new List<Product>();

            IsVip = false;
            IsClose = false;
            IsInternet = true;
            IsError = false;
            IsStart = false;
            currentitem = 0;

            if (rFirefox.Checked)
            {
                @base.InitFirefox();
                @baseIgnore.InitFirefox();
                @web = @base.GetFirefox();
                @webIgnore = @baseIgnore.GetFirefox();
            }
            else if (rChrome.Checked)
            {
                @base.InitChrome();
                @baseIgnore.InitChrome();
                @web = @base.GetChrome();
                @webIgnore = @baseIgnore.GetChrome();
            }
            else
            {
                @base.InitEdge();
                @baseIgnore.InitEdge();
                @web = @base.GetEdge();
                @webIgnore = @baseIgnore.GetEdge();
            }

            nAmount1.Value = 1;
            nAmount2.Value = 1;
            tbnRandom_Click(null, null);
            cbRepeat.Checked = true;

            lvItems.Columns.Add("Item 1", 200);
            lvItems.Columns.Add("Item 2", 200);

            UpdateStatusInternet();
            TimeoutKey();
            SyncData();

            MockData();
        }

        void MockData()
        {
            tbUsername.Text = "db05111997@gmail.com";
            tbPassword.Text = "123456789";

            //@products.Add(new Product { Amount = "2", Name = "Bia tiger lon thùng 24" });
            //@products.Add(new Product { Amount = "2", Name = "1 thùng nước Sting lon" });
            //@products.Add(new Product { Amount = "2", Name = "Sữa rữa mặt cho da khô Beaty Med" });
        }

        async void SyncData()
        {
            try
            {
                _user = (await _firebase._firebaseDatabase.Child("Users").OnceAsync<User>())
                                .FirstOrDefault(x => x.Object.Email == Login.Email);

                if (_user.Object.Products != null)
                {
                    foreach (var item in _user.Object.Products)
                    {
                        @products.Add(item);
                    }
                }

                foreach (var item in @products)
                {
                    lvItems.Items.Add(new ListViewItem(ProductToString(item)));
                }

                var account = CRUDHelper.Deserialize();
                if (account != null)
                {
                    //tbUsername.Text = account.Username;
                    //tbPassword.Text = account.Password;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region =================== Actions =====================
        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (lvItems.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Must add product to cart", "Error");
                return;
            }

            // write data
            CRUDHelper.Serialize(new Account { Email = Login.Email, Key = Login.Key, Password = tbPassword.Text, Username = tbUsername.Text });

            // update button state
            StartAction();

            // Actions
            // Normal tab
            var thread = new System.Threading.Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        if (IsStart && !IsError)
                            LoginController(@web);
                        else { btnStop_Click(null, null); return; }

                        @web.FindElement(By.XPath("//*[@id='root']/div[1]/header/div[2]/div[1]/div[1]"), 15);
                        @web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                        if (IsStart && !IsError)
                            TransactionController(@web);
                        else { btnStop_Click(null, null); return; }

                        @web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                        if (IsStart && !IsError)
                            PaymentController(@web);
                        else { btnStop_Click(null, null); return; }

                        for (int i = @web.WindowHandles.Count - 1; i > 0; i--)
                        {
                            @web = @web?.SwitchTo().Window(@web?.WindowHandles[i]);
                            @web.Close();
                        }

                        @web = @web?.SwitchTo().Window(@web?.WindowHandles.First());
                    }
                }
                catch (Exception)
                {}

            });
            thread.IsBackground = true;
            thread.Start();

            // Ignore tab
            if (cbRepeat.CheckState == CheckState.Checked)
            {
                var threadIgnore = new System.Threading.Thread(() =>
                {
                    try
                    {
                        while (true)
                        {
                            if (IsStart && !IsError)
                                LoginController(@webIgnore);
                            else { btnStop_Click(null, null); return; }

                            @webIgnore.FindElement(By.XPath("//*[@id='root']/div[1]/header/div[2]/div[1]/div[1]"), 15);
                            @webIgnore.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                            if (IsStart && !IsError)
                                TransactionController(@webIgnore);
                            else { btnStop_Click(null, null); return; }

                            @webIgnore.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                            if (IsStart && !IsError)
                                PaymentController(@webIgnore);
                            else { btnStop_Click(null, null); return; }

                            for (int i = webIgnore.WindowHandles.Count - 1; i > 0; i--)
                            {
                                webIgnore = webIgnore?.SwitchTo().Window(webIgnore?.WindowHandles[i]);
                                webIgnore.Close();
                            }
                            webIgnore = webIgnore?.SwitchTo().Window(webIgnore?.WindowHandles.First());
                        }
                    }
                    catch (Exception)
                    {}
                });

                threadIgnore.IsBackground = true;
                threadIgnore.Start();
            }
        }

        public void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                IsStart = false;
                //change state
                if (IsStart)
                {
                    this.Invoke(new Action(() =>
                    {
                        btnAuto.Enabled = false;
                        btnStop.Enabled = true;
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        btnAuto.Enabled = true;
                        btnStop.Enabled = false;
                    }));
                }

                for (int i = @web.WindowHandles.Count - 1; i > 0; i--)
                {
                    @web = @web?.SwitchTo().Window(@web?.WindowHandles[i]);
                    @web.Close();
                }

                @web = @web?.SwitchTo().Window(@web?.WindowHandles.First());

                if (cbRepeat.CheckState == CheckState.Checked)
                {
                    for (int i = webIgnore.WindowHandles.Count - 1; i > 0; i--)
                    {
                        webIgnore = webIgnore?.SwitchTo().Window(webIgnore?.WindowHandles[i]);
                        webIgnore.Close();
                    }
                }
                webIgnore = webIgnore?.SwitchTo().Window(webIgnore?.WindowHandles.First());
            }
            catch (Exception)
            {
            }
        }

        void LoginController(IWebDriver w)
        {
            new LoginController(
                        w,
                        "https://bytepay.vn/login",
                        tbUsername.Text,
                        tbPassword.Text, false).Execute();
        }

        void TransactionController(IWebDriver w)
        {
            if (currentitem >= @products.Count)
                currentitem = 0;

            new TransactionController(
                        w,
                        @products[currentitem++],
                        new Receiver
                        {
                            Name = tbName.Text,
                            Address = tbAddress.Text,
                            Email = tbEmail.Text,
                            Phone = tbPhone.Text
                        }, false).Execute();
        }

        void PaymentController(IWebDriver w)
        {
            string link = "";
            this.Invoke(new Action(() => link = Clipboard.GetText()));
            new PaymentController(
                w,
                link,
                IsVip == true ? 24 : 23, false).Execute();
        }

        #endregion

        #region =================== Event & Method =====================
        private void rChrome_CheckedChanged(object sender, EventArgs e)
        {
            if (rChrome.Checked == true)
            {
                CloseTab();

                try
                {
                    @base.InitChrome();
                    @web = @base.GetChrome();

                    @baseIgnore.InitChrome();
                    @webIgnore = @baseIgnore.GetChrome();
                }
                catch (Exception) { }
            }
        }

        private void rFirefox_CheckedChanged(object sender, EventArgs e)
        {
            if (rFirefox.Checked == true)
            {
                CloseTab();

                try
                {
                    @base.InitFirefox();
                    @web = @base.GetFirefox();

                    @baseIgnore.InitFirefox();
                    @webIgnore = @baseIgnore.GetFirefox();
                }
                catch (Exception)
                { }
            }
        }

        private void rEdge_CheckedChanged(object sender, EventArgs e)
        {
            if (rEdge.Checked == true)
            {
                CloseTab();

                try
                {
                    @base.InitEdge();
                    @web = @base.GetEdge();

                    @baseIgnore.InitEdge();
                    @webIgnore = @baseIgnore.GetEdge();
                }
                catch (Exception)
                { }
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception)
            {
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var item1 = tbItem1.Text;
                var amount1 = nAmount1.Value;
                var item2 = tbItem2.Text;
                var amount2 = nAmount2.Value;

                if (amount1 > 0 && !string.IsNullOrEmpty(item1) 
                    || amount2 > 0 && !string.IsNullOrEmpty(item2))
                {
                    var pro = new Product(item1, amount1.ToString(), item2, amount2.ToString());

                    //ipdate view
                    lvItems.Items.Add(new ListViewItem(
                        ProductToString(pro)));
                    @products.Add(pro);

                    tbItem1.Text = ""; nAmount1.Value = 1;
                    tbItem2.Text = ""; nAmount2.Value = 1;

                    if (_user.Object.Products == null)
                        _user.Object.Products = new List<Product>();

                    //update firebase
                    _user.Object.Products.Add(pro);
                    await _firebase._firebaseDatabase.Child("Users").Child(_user.Key).PutAsync(_user.Object);
                }
            }
            catch (Exception)
            {
            }
        }

        private async void lvItems_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int index = lvItems.SelectedItems[0].Index;
                
                _user.Object.Products.RemoveAt(index);
                await _firebase._firebaseDatabase.Child("Users").Child(_user.Key).PutAsync(_user.Object);

                @products.RemoveAt(index);
                lvItems.Items.RemoveAt(index);
            }
            catch (Exception ex)
            {
            }
        }

        private void tbnRandom_Click(object sender, EventArgs e)
        {
            tbAddress.Text = RandomHelper.GetAddress();
            tbName.Text = RandomHelper.GetName();
            tbEmail.Text = RandomHelper.GetEmail(tbName.Text);
            tbPhone.Text = RandomHelper.GetPhone();
        }

        private void UpdateStatusInternet()
        {
            var timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1 * 1000;

            timer.Tick += (s, e) =>
            {

                if (IsClose)
                {
                    this.Invoke(new Action(() =>
                    {
                        try
                        {
                            timer.Stop();
                            this.Close();
                        }
                        catch (Exception)
                        { }
                    }));
                }

                try
                {
                    IsInternet = CheckForInternetConnection();
                    this.Invoke(new Action(() =>
                    {
                        if (IsInternet)
                        {
                            rInternet.Text = "Internet connection";
                            rInternet.ForeColor = Color.Green;
                        }
                        else
                        {
                            rInternet.Text = "No internet connection";
                            rInternet.ForeColor = Color.Red;
                        }

                    }));
                }
                catch (Exception)
                {
                }
            };
            timer.Start();
        }

        private void TimeoutKey()
        {
            var timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 10 * 1000;

            timer.Tick += async (s, e) =>
            {
                try
                {
                    if (!IsInternet) return;

                    var key = (await _firebase._firebaseDatabase.Child("Keys").OnceAsync<LicenseKey>())
                    .FirstOrDefault(x => x.Object.Key == Login.Key);

                    if (IsClose)
                    {
                        this.Invoke(new Action(() =>
                        {
                            try
                            {
                                timer.Stop();
                                this.Close();
                            }
                            catch (Exception)
                            { }
                        }));
                    }


                    if (key == null)
                    {
                        System.Windows.Forms.MessageBox.Show("License key doesn't exist", "Error");
                        this.Invoke(new Action(() =>
                        {
                            timer.Stop();
                            this.Close();
                        }));
                    }
                    else if (key.Object.End < DateTime.Now)
                    {
                        System.Windows.Forms.MessageBox.Show("License key is out of date", "Error");
                        this.Invoke(new Action(() =>
                        {
                            timer.Stop();
                            this.Close();
                        }));
                    }
                }
                catch (Exception)
                {}
            };
            timer.Start();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                IsClose = true;
                @web?.Quit();
                @webIgnore?.Quit();
                @web = @webIgnore = null;
                _login.Close();

            }
            catch (Exception)
            {
            }
        }

        private void cbVip_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVip.CheckState == CheckState.Checked)
                IsVip = true;
            else IsVip = false;
        }

        bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void CloseTab()
        {
            try
            {
                int lweb = (int)(web?.WindowHandles.Count);
                for (int i = 0; i < lweb; i++)
                {
                    web = web?.SwitchTo().Window(web?.WindowHandles[i]);
                    web?.Close();
                }

                int lwebignore = (int)(webIgnore?.WindowHandles.Count);
                for (int i = 0; i < lwebignore; i++)
                {
                    webIgnore = webIgnore?.SwitchTo().Window(webIgnore?.WindowHandles[i]);
                    webIgnore?.Close();
                }
            }
            catch (Exception) { }
        }

        private void btnLoginNormal_Click(object sender, EventArgs e) { StartAction(); LoginController(@web); }

        private void btnTransactionNormal_Click(object sender, EventArgs e) { StartAction(); TransactionController(@web); }

        private void btnPaymentNormal_Click(object sender, EventArgs e)
        {
            StartAction();

            for (int i = @web.WindowHandles.Count - 1; i > 0; i--)
            {
                @web = @web?.SwitchTo().Window(@web?.WindowHandles[i]);
                @web.Close();
            }

            PaymentController(@web);
        }

        private void btnLoginIgnore_Click(object sender, EventArgs e) { StartAction(); LoginController(@webIgnore); }

        private void btnTransactionIgnore_Click(object sender, EventArgs e) { StartAction(); TransactionController(@webIgnore); }

        private void tbItem1_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbItem1.Text))
            {
                tbItem2.Enabled = true;
                nAmount2.Enabled = true;
            }
            else
            {
                tbItem2.Enabled = false;
                nAmount2.Enabled = false;
            } 
                
        }

        private void btnPaymentIgnore_Click(object sender, EventArgs e)
        {
            StartAction();

            for (int i = webIgnore.WindowHandles.Count - 1; i > 0; i--)
            {
                webIgnore = webIgnore?.SwitchTo().Window(webIgnore?.WindowHandles[i]);
                webIgnore.Close();
            }

            PaymentController(@webIgnore);
        }

        string[] ProductToString(Product product)
        {
            return new string[] 
            { 
                $"{product.Name1} [{product.Amount1}]", 
                $"{product.Name2} [{(string.IsNullOrEmpty(product.Name2) ? "" : product.Amount2)}]" 
            };
        }

        void StartAction()
        {
            IsStart = true;
            IsError = false;

            //change state
            if (IsStart)
            {
                btnAuto.Enabled = false;
                btnStop.Enabled = true;
            }
            else
            {
                btnAuto.Enabled = false;
                btnStop.Enabled = true;
            }

            if (!IsInternet) return;
        }
        #endregion


    }
}
