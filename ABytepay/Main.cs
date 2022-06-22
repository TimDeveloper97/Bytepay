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
        IBaseController @base, @baseIgnore;
        IWebDriver @web, @webIgnore;
        List<Product> @products;
        public static bool IsStart = false;
        static string _link = "";
        static string _linkIgnore = "";
        public static bool IsError = false;
        static bool IsInternet = true;
        BaseFirebase _firebase;
        static FirebaseObject<User> _user;
        public static bool IsClose = false;
        public static bool IsVip = false;
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

            nAmount.Value = 1;
            tbnRandom_Click(null, null);
            cbRepeat.Checked = true;

            lvItems.Columns.Add("Name", 240);
            lvItems.Columns.Add("Amount");

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
                    lvItems.Items.Add(new ListViewItem(new string[]
                    {
                    item.Name,
                    item.Amount
                    }));
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

            // Actions
            // Normal tab
            var thread = new System.Threading.Thread(() =>
            {
                try
                {
                    @web.FindElement(By.XPath("//*[@id='root']/div[1]/div[2]/div/div[2]/div/div/div/div/h2"), 15);

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
                }
                catch (Exception)
                {

                }

            });
            thread.IsBackground = true;
            thread.Start();

            // Ignore tab
            if (cbRepeat.CheckState == CheckState.Checked)
            {
                var threadIgnore = new System.Threading.Thread(() =>
                {
                    @webIgnore.FindElement(By.XPath("//*[@id='root']/div[1]/div[2]/div/div[2]/div/div/div/div/h2"), 15);

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
                    //btnStop_Click(null, null);

                });
                threadIgnore.IsBackground = true;
                threadIgnore.Start();
            }
            //else
            //{
            //    //btnStop_Click(null, null);
            //}

            CRUDHelper.Serialize(new Account { Email = Login.Email, Key = Login.Key, Password = tbPassword.Text, Username = tbUsername.Text });
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

        void PaymentController(IWebDriver w)
        {
            this.Invoke(new Action(() => _link = Clipboard.GetText()));
            new PaymentController(
                w,
                _link,
                IsVip == true ? 24 : 23, false).Execute();
        }

        void TransactionController(IWebDriver w)
        {
            new TransactionController(
                        w,
                        @products,
                        new Receiver
                        {
                            Name = tbName.Text,
                            Address = tbAddress.Text,
                            Email = tbEmail.Text,
                            Phone = tbPhone.Text
                        }, false).Execute();
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
                var item = tbItem.Text;
                var amount = nAmount.Value;
                if (amount > 0 && !string.IsNullOrEmpty(item))
                {
                    lvItems.Items.Add(new ListViewItem(new string[] { item, amount.ToString() }));
                    @products.Add(new Product { Name = item, Amount = amount.ToString() });
                    tbItem.Text = "";

                    if (_user.Object.Products == null)
                        _user.Object.Products = new List<Product>();

                    _user.Object.Products.Add(new Product { Name = item, Amount = amount.ToString() });
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
                @products.RemoveAt(lvItems.SelectedItems[0].Index);
                lvItems.Items.RemoveAt(lvItems.SelectedItems[0].Index);

                _user.Object.Products.RemoveAt(lvItems.SelectedItems[0].Index);
                await _firebase._firebaseDatabase.Child("Users").Child(_user.Key).PutAsync(_user.Object);
            }
            catch (Exception)
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

        private void btnLoginNormal_Click(object sender, EventArgs e) => LoginController(@web);

        private void btnTransactionNormal_Click(object sender, EventArgs e) => TransactionController(@web);

        private void btnPaymentNormal_Click(object sender, EventArgs e)
        {
            for (int i = @web.WindowHandles.Count - 1; i > 0; i--)
            {
                @web = @web?.SwitchTo().Window(@web?.WindowHandles[i]);
                @web.Close();
            }

            PaymentController(@web);
        }

        private void btnLoginIgnore_Click(object sender, EventArgs e) => LoginController(@webIgnore);

        private void btnTransactionIgnore_Click(object sender, EventArgs e) => TransactionController(@webIgnore);

        private void btnPaymentIgnore_Click(object sender, EventArgs e)
        {
            for (int i = webIgnore.WindowHandles.Count - 1; i > 0; i--)
            {
                webIgnore = webIgnore?.SwitchTo().Window(webIgnore?.WindowHandles[i]);
                webIgnore.Close();
            }

            PaymentController(@webIgnore);
        }
        #endregion


    }
}
