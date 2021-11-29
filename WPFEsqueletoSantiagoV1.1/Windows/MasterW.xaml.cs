using System;
using System.Reflection;
using System.Windows;
using WPFEsqueletoSantiagoV1._1.Classes;
using WPFEsqueletoSantiagoV1._1.Models;

namespace WPFEsqueletoSantiagoV1._1.Windows
{
    /// <summary>
    /// Lógica de interacción para MasterWindow.xaml
    /// </summary>
    public partial class MasterW : Window
    {
        #region "Constructor"
        public MasterW()
        {
            InitializeComponent();

            SetUserControl();
        }
        #endregion

        #region "Métodos"
        private void SetUserControl()
        {
            try
            {
                if (Utilities.navigator == null)
                {
                    Utilities.navigator = new Navigation();
                }

                string a = Encryptor.Encrypt("usrapli", "WPFEsqueletoSantiagoV1.1");
                string b = Encryptor.Encrypt("1Cero12019$/*", "WPFEsqueletoSantiagoV1.1");
                string c = Encryptor.Encrypt("Pay+ Michael", "WPFEsqueletoSantiagoV1.1");
                string d = Encryptor.Encrypt("EcitySoftware2021%", "WPFEsqueletoSantiagoV1.1");
                string e = Encryptor.Encrypt("https://e-ecity.online/", "WPFEsqueletoSantiagoV1.1");

                WPKeyboard.Keyboard.ConsttrucKeyyboard(WPKeyboard.Keyboard.EStyle.style_2);

                Utilities.navigator.Navigate(UserControlView.Config);

                DataContext = Utilities.navigator;
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }
        #endregion
    }
}
