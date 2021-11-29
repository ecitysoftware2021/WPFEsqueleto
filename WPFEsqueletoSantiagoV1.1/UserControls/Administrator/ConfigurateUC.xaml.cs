using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFEsqueletoSantiagoV1._1.Classes;
using WPFEsqueletoSantiagoV1._1.Resources;
using WPFEsqueletoSantiagoV1._1.Services;
using WPFEsqueletoSantiagoV1._1.Services.Object;

namespace WPFEsqueletoSantiagoV1._1.UserControls.Administrator
{
    /// <summary>
    /// Lógica de interacción para ConfigurateUserControl.xaml
    /// </summary>
    public partial class ConfigurateUC : UserControl
    {
        #region "Referencias"
        private AdminPayPlus init;
        private ApiIntegration apiIntegration;
        #endregion

        #region "Constructor"
        public ConfigurateUC()
        {
            try
            {
                InitializeComponent();

                if (init == null)
                {
                    init = new AdminPayPlus();
                }

                if (apiIntegration == null)
                {
                    apiIntegration = new ApiIntegration();
                }

                txtMs.DataContext = init;

                Initial();
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }
        #endregion

        #region "Métodos"
        private async void Initial()
        {
            try
            {
                init.callbackResult = result =>
                {
                    ProccesResult(result);
                };

                init.Start();
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }

        private async void ProccesResult(bool result)
        {
            try
            {
                if (AdminPayPlus.DataPayPlus.StateUpdate)
                {
                    Utilities.ShowModal(MessageResource.UpdateAplication, EModalType.Error, true);
                    Utilities.UpdateApp();
                }
                else if (AdminPayPlus.DataPayPlus.StateBalanece)
                {
                    Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, null, MessageResource.ModoAdministrativo);
                    //Utilities.navigator.Navigate(UserControlView.Login, false, ETypeAdministrator.Balancing);
                }
                else if (AdminPayPlus.DataPayPlus.StateUpload)
                {
                    Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, null, MessageResource.ModoAdministrativo);
                    //Utilities.navigator.Navigate(UserControlView.Login, false, ETypeAdministrator.Upload);
                }
                else
                {
                    Finish(result);
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
                Utilities.ShowModal(MessageResource.NoService, EModalType.Error, false);
                Initial();
            }
        }

        private void Finish(bool state)
        {
            try
            {
              
                if (state)
                {

                    AdminPayPlus.SaveLog(new RequestLog
                    {
                        Reference = "",
                        Description = MessageResource.YesGoInitial,
                        State = 1,
                        Date = DateTime.Now
                    }, ELogType.General);

                    Utilities.navigator.Navigate(UserControlView.Main);
                }
                else
                {
                    if (!string.IsNullOrEmpty(AdminPayPlus.DataPayPlus.Message))
                    {
                        AdminPayPlus.SaveLog(new RequestLog
                        {
                            Reference = "",
                            Description = $"{MessageResource.NoGoInitial}, {AdminPayPlus.DataPayPlus.Message}",
                            State = 6,
                            Date = DateTime.Now
                        }, ELogType.General);

                        Utilities.ShowModal(MessageResource.NoService + " " + MessageResource.NoMoneyKiosco, EModalType.Error, false);
                        Initial();
                    }
                    else
                    {
                        AdminPayPlus.SaveLog(new RequestLog
                        {
                            Reference = "",
                            Description = $"{MessageResource.NoGoInitial}, {init.DescriptionStatusPayPlus}",
                            State = 2,
                            Date = DateTime.Now
                        }, ELogType.General);

                        Utilities.ShowModal(MessageResource.NoService + " " + init.DescriptionStatusPayPlus, EModalType.Error, false);
                        Initial();
                    }
                }
               
            }
            catch (Exception ex)
            {
                Utilities.ShowModal(MessageResource.NoService, EModalType.Error, false);
                Initial();
            }
        }
        #endregion
    }
}
