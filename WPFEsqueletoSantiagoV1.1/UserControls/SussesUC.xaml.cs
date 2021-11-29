using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFEsqueletoSantiagoV1._1.Classes;
using WPFEsqueletoSantiagoV1._1.Models;
using WPFEsqueletoSantiagoV1._1.Resources;
using WPFEsqueletoSantiagoV1._1.Services.Object;

namespace WPFEsqueletoSantiagoV1._1.UserControls
{
    /// <summary>
    /// Lógica de interacción para SussesUserControl.xaml
    /// </summary>
    public partial class SussesUC : UserControl
    {
        #region "Referencias"
        private Transaction transaction;
        #endregion

        #region "Constructor"
        public SussesUC(Transaction transaction)
        {
            InitializeComponent();

            this.transaction = transaction;

            FinishTrnsaction();
        }
        #endregion

        #region "Métodos"
        private void FinishTrnsaction()
        {
            try
            {
                if (transaction.State == ETransactionState.Success)
                {
                    AdminPayPlus.SaveLog(new RequestLog
                    {
                        Description = MessageResource.SussesTransaction,
                        Reference = transaction.reference
                    }, ELogType.General);
                }
                else
                {
                    AdminPayPlus.SaveLog(new RequestLog
                    {
                        Description = MessageResource.NoveltyTransation,
                        Reference = transaction.reference
                    }, ELogType.General);
                }

                GC.Collect();

                Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(transaction.Observation))
                    {
                        AdminPayPlus.SaveErrorControl(transaction.Observation, "", EError.Device, ELevelError.Medium);
                    }

                    AdminPayPlus.UpdateTransaction(this.transaction);

                    transaction.StatePay = "Aprobado";

                    Utilities.PrintVoucher(this.transaction);

                    Thread.Sleep(5000);

                    Dispatcher.BeginInvoke((Action)delegate
                    {
                        if (transaction.State == ETransactionState.Error)
                        {
                            Utilities.RestartApp();
                        }
                        else
                        {
                            Utilities.navigator.Navigate(UserControlView.Main);
                        }
                    });
                    GC.Collect();
                });
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }
        #endregion
    }
}