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

                string a = Encryptor.Encrypt("usrapli", "WPFProyecto");
                string b = Encryptor.Encrypt("1Cero12019$/*", "WPFProyecto");
                string c = Encryptor.Encrypt("Ecity.Software", "WPFProyecto");
                string d = Encryptor.Encrypt("Ecitysoftware2019#", "WPFProyecto");
                string e = Encryptor.Encrypt("https://e-citypay.co/", "WPFProyecto");

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
