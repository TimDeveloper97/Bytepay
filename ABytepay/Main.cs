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
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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

        public Main()
        {
            InitializeComponent();
            Init();
        }

        #region =================== Init =====================

        void Init()
        {
            @base = new BaseController();
            _firebase = new BaseFirebase();
            @baseIgnore = new BaseIgnoreController();
            @products = new List<Product>();

            if(rFirefox.Checked)
            {
                @base.InitFirefox();
                @baseIgnore.InitFirefox();
                @web = @base.GetFirefox();
                @webIgnore = @baseIgnore.GetFirefox();
            }    
            else if(rChrome.Checked)
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
                if(account != null)
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
            if(lvItems.Items.Count == 0)
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
            new System.Threading.Thread(() =>
            {
                try
                {
                    @web.FindElement(By.XPath("//*[@id='root']/div[1]/div[2]/div/div[2]/div/div/div/div/h2"), 15);

                    if (IsStart && !IsError)
                    {
                        new LoginController(
                        @web,
                        "https://bytepay.vn/login",
                        tbUsername.Text,
                        tbPassword.Text, false).Execute();
                    }
                    else { btnStop_Click(null, null); return; }

                    @web.FindElement(By.XPath("//*[@id='root']/div[1]/header/div[2]/div[1]/div[1]"), 15);
                    @web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                    if (IsStart && !IsError)
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
                        }, false).Execute();
                    }
                    else { btnStop_Click(null, null); return; }

                    @web.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                    if (IsStart && !IsError)
                    {
                        this.Invoke(new Action(() => _link = Clipboard.GetText()));
                        new PaymentController(
                            @web,
                            _link,
                            23, false).Execute();
                    }
                    else { btnStop_Click(null, null); return; }
                }
                catch (Exception)
                {

                }

            }).Start();

            // Ignore tab
            if (cbRepeat.CheckState == CheckState.Checked)
                new System.Threading.Thread(() =>
                {
                    @webIgnore.FindElement(By.XPath("//*[@id='root']/div[1]/div[2]/div/div[2]/div/div/div/div/h2"), 15);

                    if (IsStart && !IsError)
                    {
                        new LoginController(
                        @webIgnore,
                        "https://bytepay.vn/login",
                        tbUsername.Text,
                        tbPassword.Text, true).Execute();
                    }
                    else { btnStop_Click(null, null); return; }

                    @webIgnore.FindElement(By.XPath("//*[@id='root']/div[1]/header/div[2]/div[1]/div[1]"), 15);
                    @webIgnore.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                    if (IsStart && !IsError)
                    {
                        new TransactionController(
                        @webIgnore,
                        @products,
                        new Receiver
                        {
                            Name = tbName.Text,
                            Address = tbAddress.Text,
                            Email = tbEmail.Text,
                            Phone = tbPhone.Text
                        }, true).Execute();
                    }
                    else { btnStop_Click(null, null); return; }

                    @webIgnore.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

                    if (IsStart && !IsError)
                    {
                        this.Invoke(new Action(() => _linkIgnore = Clipboard.GetText()));
                        new PaymentController(
                            @webIgnore,
                            _linkIgnore,
                            23, true).Execute();
                    }

                    //btnStop_Click(null, null);

                }).Start();
            else
            {
                //btnStop_Click(null, null);
            }

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

                if (cbRepeat.CheckState == CheckState.Checked)
                {
                    for (int i = webIgnore.WindowHandles.Count - 1; i > 0; i--)
                    {
                        webIgnore = webIgnore?.SwitchTo().Window(webIgnore?.WindowHandles[i]);
                        webIgnore.Close();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region =================== Event & Method =====================
        private void rChrome_CheckedChanged(object sender, EventArgs e)
        {
            @base.InitChrome();
            @web = @base.GetChrome();

            @baseIgnore.InitChrome();
            @webIgnore = @baseIgnore.GetChrome();
        }

        private void rFirefox_CheckedChanged(object sender, EventArgs e)
        {
            @base.InitFirefox();
            @web = @base.GetFirefox();

            @baseIgnore.InitFirefox();
            @webIgnore = @baseIgnore.GetFirefox();
        }

        private void rEdge_CheckedChanged(object sender, EventArgs e)
        {
            @base.InitEdge();
            @web = @base.GetEdge();

            @baseIgnore.InitEdge();
            @webIgnore = @baseIgnore.GetEdge();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                @web.Close();
                @webIgnore.Close();
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
        #endregion


    }
}
