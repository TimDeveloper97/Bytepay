using ABytepay.Domain;
using ABytepay.Helpers;
using ABytepay.Models;
using Firebase.Database.Query;
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
    public partial class Login : Form
    {
        static BaseFirebase _firebase;
        public static string Key = "";

        public Login()
        {
            InitializeComponent();

            _firebase = new BaseFirebase();
        }

        #region ======================= Actions ==========================
        private async void btnCheckKey_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbKey.Text))
            {
                var key = (await _firebase._firebaseDatabase.Child("Keys").OnceAsync<LicenseKey>())
                    .FirstOrDefault(x => x.Object.Key == tbKey.Text);

                if (key == null)
                    System.Windows.Forms.MessageBox.Show("License key doesn't exist", "Error");
                else if (key.Object.IsUse)
                    System.Windows.Forms.MessageBox.Show("License key is already used", "Error");
                else if (key.Object.End < DateTime.Now)
                    System.Windows.Forms.MessageBox.Show("License key is out of date", "Error");
                else
                    System.Windows.Forms.MessageBox.Show("License key ready to use", "Success");
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbKey.Text) && !string.IsNullOrEmpty(tbEmail.Text))
            {
                var key = (await _firebase._firebaseDatabase.Child("Keys").OnceAsync<LicenseKey>())
                    .FirstOrDefault(x => x.Object.Key == tbKey.Text);

                if (key == null)
                    System.Windows.Forms.MessageBox.Show("License key doesn't exist", "Error");
                //else if (key.Object.IsUse)
                //    System.Windows.Forms.MessageBox.Show("License key is already used", "Error");
                else if (key.Object.End < DateTime.Now)
                    System.Windows.Forms.MessageBox.Show("License key is out of date", "Error");
                else
                {
                    var user = (await _firebase._firebaseDatabase.Child("Users").OnceAsync<User>())
                                .FirstOrDefault(x => x.Object.Email == tbEmail.Text);

                    var mdevice = ComputerHelper.GetDeviceId();

                    if (user == null)
                    {
                        System.Windows.Forms.MessageBox.Show("Email doesn't exist", "Error");
                        var confirmResult = MessageBox.Show("Do you want create account with this email?",
                                     "Information",
                                     MessageBoxButtons.YesNo);

                        if (confirmResult == DialogResult.Yes)
                        {
                            var u = new User
                            {
                                ComputerId = mdevice,
                                Email = tbEmail.Text,
                                Keys = new List<string>(),
                                Products = new List<Product>(),
                            };
                            await _firebase._firebaseDatabase.Child("Users").PostAsync(u);
                        }
                    }    
                    else if(mdevice != user.Object.ComputerId)
                        System.Windows.Forms.MessageBox.Show("Email is use in another machine", "Error");
                    else
                    {
                        var isKeyExist = user.Object.Keys?.Any(x => x == tbKey.Text);
                        if(isKeyExist == null || isKeyExist == false)
                        {
                            var confirmResult = MessageBox.Show("Do you want add key to this email?",
                                     "Information",
                                     MessageBoxButtons.YesNo);

                            if (confirmResult == DialogResult.Yes)
                            {
                                if(user.Object.Keys == null)
                                    user.Object.Keys = new List<string>();
                                user.Object.Keys.Add(tbKey.Text);

                                key.Object.IsUse = true;

                                await _firebase._firebaseDatabase.Child("Keys").Child(key.Key).PutAsync(key.Object);
                                await _firebase._firebaseDatabase.Child("Users").Child(user.Key).PutAsync(user.Object);
                            }
                        }                        
                        else if(isKeyExist == true)
                        {
                            Key = tbKey.Text;
                            var main = new Main();
                            main.Show();
                            this.Hide();
                        }    
                    } 
                        
                }


            }
        }
        #endregion


    }
}
