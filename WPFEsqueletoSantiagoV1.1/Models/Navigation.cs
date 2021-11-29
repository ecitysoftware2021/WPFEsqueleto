using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFEsqueletoSantiagoV1._1.Classes;
using WPFEsqueletoSantiagoV1._1.Models;
using WPFEsqueletoSantiagoV1._1.Services.Object;
using WPFEsqueletoSantiagoV1._1.UserControls;
using WPFEsqueletoSantiagoV1._1.UserControls.Administrator;

namespace WPFEsqueletoSantiagoV1._1.Models
{
    public class Navigation : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private UserControl _view;

        public UserControl View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(View)));
            }
        }

        public void Navigate(UserControlView newWindow, object data = null, object complement = null) => Application.Current.Dispatcher.Invoke((Action)delegate
        {
            try
            {
                switch (newWindow)
                {
                    case UserControlView.Config:
                        View = new ConfigurateUC();
                        break;
                    case UserControlView.Main:
                        View = new MainUC();
                        break;
                    case UserControlView.Menu:
                        View = new MenuUC();
                        break;
                    case UserControlView.Pay:
                        View = new PaymentUC((Transaction)data);
                        break;
                    case UserControlView.PaySuccess:
                        View = new SussesUC((Transaction)data);
                        break;
                    case UserControlView.ReturnMony:
                        View = new ReturnMonyUC((Transaction)data);
                        break;
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Navigate", ex, ex.ToString());
            }
            GC.Collect();
        });
    }
}