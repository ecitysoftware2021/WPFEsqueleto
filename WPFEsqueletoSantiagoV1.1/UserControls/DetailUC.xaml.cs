using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFEsqueletoSantiagoV1._1.Classes;
using WPFEsqueletoSantiagoV1._1.Classes.UseFull;
using WPFEsqueletoSantiagoV1._1.Models;
using WPFEsqueletoSantiagoV1._1.Resources;

namespace WPFEsqueletoSantiagoV1._1.UserControls
{
    /// <summary>
    /// Interaction logic for DetailUC.xaml
    /// </summary>
    public partial class DetailUC : UserControl
    {
        #region "Referencias"
        private Transaction transaction;
        private TimerGeneric timer;
        #endregion

        #region "Constructor"
        public DetailUC()
        {
            InitializeComponent();
        }
        #endregion

        #region "Métodos"
        private async void SendData()
        {
            try
            {
                if (transaction.Amount > 0)
                {
                    Task.Run(async () =>
                    {
                        await AdminPayPlus.SaveTransaction(this.transaction);

                        Utilities.CloseModal();

                        if (this.transaction.IdTransactionAPi == 0)
                        {
                            Utilities.ShowModal("", EModalType.Error);
                            Utilities.navigator.Navigate(UserControlView.Main);
                        }
                        else
                        {
                            //el navigate recibe 3 parametros, uno que es la vista, otro que es la data a enviar y el último es data adicional a enviar (si es que hay una data adicional)
                            Utilities.navigator.Navigate(UserControlView.Pay, transaction);
                        }
                    });
                    Utilities.ShowModal(MessageResource.LoadInformation, EModalType.Preload);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }
        #endregion
    }
}
