using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WPFEsqueletoSantiagoV1._1.Classes;
using WPFEsqueletoSantiagoV1._1.Models;
using WPFEsqueletoSantiagoV1._1.Resources;
using WPFEsqueletoSantiagoV1._1.Services.Object;

namespace WPFEsqueletoSantiagoV1._1.UserControls
{
    /// <summary>
    /// Lógica de interacción para MainUserControl.xaml
    /// </summary>
    public partial class MainUC : UserControl
    {
        #region "Referencias"
        private ImageSleader _imageSleader;
        private bool _validatePaypad;
        #endregion

        #region "Constructor"
        public MainUC(bool validatePaypad = true)
        {
            InitializeComponent();

            _validatePaypad = validatePaypad;

            InitValidation();

            ConfiguratePublish();
        }
        #endregion

        #region "Métodos"
        private void InitValidation()
        {
            try
            {
                Task.Run(() =>
                {
                    while (_validatePaypad)
                    {
                        AdminPayPlus.ValidatePaypad();

                        Thread.Sleep(int.Parse(Utilities.GetConfiguration("DurationAlert")));
                    }
                });
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }

        private void ValidateStatus()
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
                else if (AdminPayPlus.DataPayPlus.State && AdminPayPlus.DataPayPlus.StateAceptance && AdminPayPlus.DataPayPlus.StateDispenser)
                {
                    int response = AdminPayPlus.PrintService.StatusPrint();

                    if (response != 0)
                    {
                        if (response == 7 || response == 8)
                        {
                            AdminPayPlus.SaveErrorControl(AdminPayPlus.PrintService.MessageStatus(response), MessageResource.InformationError, EError.Nopapper, ELevelError.Medium);
                        }
                        else
                        {
                            AdminPayPlus.SaveErrorControl(AdminPayPlus.PrintService.MessageStatus(response), MessageResource.InformationError, EError.Printer, ELevelError.Medium);
                        }

                        if (response != 8)
                        {
                            AdminPayPlus.SaveLog(new RequestLog
                            {
                                Reference = "",
                                Description = MessageResource.PrinterNoPapper,
                                State = 2,
                                Date = DateTime.Now
                            }, ELogType.General);

                            Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, null, MessageResource.PrinterNoPapper);

                            if (Utilities.ShowModal(MessageResource.ErrorNoPaper, EModalType.Information))
                            {
                                Redirect(true);
                            }
                        }
                        else
                        {
                            Redirect(true);
                        }
                    }
                    else
                    {
                        Redirect(true);
                    }
                }
                else
                {
                    Redirect(false);
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }

        private void Redirect(bool state)
        {
            try
            {
                if (state)
                {
                    _imageSleader.Stop();

                    _validatePaypad = false;
                    Utilities.navigator.Navigate(UserControlView.Menu);
                }
                else
                {
                    Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, null, AdminPayPlus.DataPayPlus.Message);
                    Utilities.ShowModal(MessageResource.NoService + " " + MessageResource.NoMoneyKiosco, EModalType.Error, false);
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }

        private void ConfiguratePublish()
        {
            try
            {
                if (_imageSleader == null)
                {
                    string folder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Images", "Publish");

                    _imageSleader = new ImageSleader(null, folder);

                    this.DataContext = _imageSleader.imageModel;

                    _imageSleader.time = int.Parse(Utilities.GetConfiguration("TimerPublicity"));

                    _imageSleader.isRotate = true;

                    _imageSleader.Start();
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, this.GetType().Name, ex, ex.ToString());
            }
        }


        #endregion

        #region "Eventos"
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ValidateStatus();
        }
        #endregion
    }
}