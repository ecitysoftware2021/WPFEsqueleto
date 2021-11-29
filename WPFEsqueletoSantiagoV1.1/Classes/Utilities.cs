using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WPFEsqueletoSantiagoV1._1.Classes.Printer;
using WPFEsqueletoSantiagoV1._1.Models;
using WPFEsqueletoSantiagoV1._1.Resources;
using WPFEsqueletoSantiagoV1._1.Windows;

namespace WPFEsqueletoSantiagoV1._1.Classes
{
    public class Utilities
    {
        #region "Referencias"
        public static Navigation navigator { get; set; }

        private static SpeechSynthesizer speechSynthesizer;

        private static ModalW modal { get; set; }
        #endregion

        public static string GetConfiguration(string key, bool decodeString = false)
        {
            try
            {
                string value = "";
                AppSettingsReader reader = new AppSettingsReader();
                value = reader.GetValue(key, typeof(String)).ToString();
                if (decodeString)
                {
                    value = Encryptor.Decrypt(value);
                }
                return value;
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
                return string.Empty;
            }
        }

        public static bool ShowModal(string message, EModalType type, bool timer = false)
        {
            bool response = false;
            try
            {
                ModalModel model = new ModalModel
                {
                    Tittle = "Estimado Cliente: ",
                    Messaje = message,
                    Timer = timer,
                    TypeModal = type,
                    ImageModal = @"Images/Backgrounds/modal.png",
                };

                if (type == EModalType.Error)
                {
                    model.ImageModal = @"Images/Backgrounds/modal-error.png";
                }
                else if (type == EModalType.Information)
                {
                    model.ImageModal = @"Images/Backgrounds/modal-info.png";
                }
                else if (type == EModalType.NoPaper)
                {
                    model.ImageModal = @"Images/Backgrounds/modal-info.png";
                }
                else if (type == EModalType.Preload)
                {
                    model.ImageModal = @"Images/Backgrounds/modal-info.png";
                }

                Application.Current.Dispatcher.Invoke(delegate
                {
                    modal = new ModalW(model);
                    modal.ShowDialog();

                    if (modal.DialogResult.HasValue && modal.DialogResult.Value)
                    {
                        response = true;
                    }
                });
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
            GC.Collect();
            return response;
        }

        public static void CloseModal() => Application.Current.Dispatcher.Invoke((Action)delegate
        {
            try
            {
                if (modal != null)
                {
                    modal.Close();
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex);
            }
        });

        public static void RestartApp()
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Process pc = new Process();
                    Process pn = new Process();
                    ProcessStartInfo si = new ProcessStartInfo();
                    si.FileName = Path.Combine(Directory.GetCurrentDirectory(), GetConfiguration("NAME_APLICATION"));
                    pn.StartInfo = si;
                    pn.Start();
                    pc = Process.GetCurrentProcess();
                    pc.Kill();
                }));
                GC.Collect();
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
        }

        public static void UpdateApp()
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Process pc = new Process();
                    Process pn = new Process();
                    ProcessStartInfo si = new ProcessStartInfo();
                    si.FileName = GetConfiguration("APLICATION_UPDATE");
                    pn.StartInfo = si;
                    pn.Start();
                    pc = Process.GetCurrentProcess();
                    pc.Kill();
                }));
                GC.Collect();
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
        }

        public static void PrintVoucher(Transaction transaction)
        {
            try
            {
                if (transaction != null)
                {
                    SolidBrush color = new SolidBrush(Color.Black);
                    Font fontKey = new Font("Arial", 9, System.Drawing.FontStyle.Bold);
                    Font fontValue = new Font("Arial", 9, System.Drawing.FontStyle.Regular);
                    int y = 0;
                    int sum = 20;
                    int x = 200;
                    int xKey = 15;
                    int xMax = 270;
                    string formaPago = transaction.tipoPago;

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    Rectangle rect = new Rectangle(0, y += sum - 10, 280, 20);

                    int multiplier = (xMax / 45);

                    var data = new List<DataPrinter>()
                    {
                        //La imagen usada para el voucher es de 100 x 100
                        new DataPrinter{ image = GetConfiguration("ImageBoucher"), x = 90, y = y += 10 , direction = sf  },
                    };

                    rect = new Rectangle(0, y += 80, 270, 15);


                    //data.Add(new DataPrinter { brush = color, font = fontKey, value = "Comedal", x = 100, y = y += sum });
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = "Cajero", x = 25, y = y += 30 });
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = "Tran", x = 85, y = y });
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = "Fecha", x = 145, y = y });
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = "Hora", x = 218, y = y });

                    data.Add(new DataPrinter { brush = color, font = fontValue, value = AdminPayPlus.DataConfiguration.ID_PAYPAD.ToString() ?? string.Empty, x = 23, y = y += 15 });
                    data.Add(new DataPrinter { brush = color, font = fontValue, value = "1538" ?? string.Empty, x = 83, y = y }); //transaction.IdTransactionAPi.ToString()
                    data.Add(new DataPrinter { brush = color, font = fontValue, value = DateTime.Now.Date.ToString("yyyy-MM-dd") ?? string.Empty, x = 140, y = y });
                    data.Add(new DataPrinter { brush = color, font = fontValue, value = DateTime.Now.ToString("HH:mm:ss") ?? string.Empty, x = 208, y = y });

                    //Forma de pago
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = "Forma de Pago", x = xKey, y = y += 30 });
                    data.Add(new DataPrinter { brush = color, font = fontValue, value = formaPago ?? string.Empty, x = (xMax - formaPago.Length * multiplier), y = y }); //transaction.DatosAdicionales.FORPAG.Length

                    //Tipo de combustible                          
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = "Tipo de combustible", x = xKey, y = y += sum });
                    data.Add(new DataPrinter { brush = color, font = fontValue, value = "Corriente" ?? string.Empty, x = (xMax - 8 * multiplier), y = y });

                    //# galones
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = "Núm. Galones", x = xKey, y = y += sum });
                    data.Add(new DataPrinter { brush = color, font = fontValue, value = "8" ?? string.Empty, x = (xMax - 8 /*transaction.Galon.ToString().Length*/ * multiplier), y = y });

                    //Total dinero
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = "Costo de operación", x = xKey, y = y += 30 });
                    data.Add(new DataPrinter { brush = color, font = fontKey, value = string.Format("{0:C2}", transaction.Amount), x = (xMax - string.Format("{0:C2}", transaction.Amount).Length * multiplier) - 5, y = y });

                    if (formaPago == "Efectivo")
                    {

                        data.Add(new DataPrinter { brush = color, font = fontKey, value = "Total ingresado:", x = xKey, y = y += 30 });
                        data.Add(new DataPrinter { brush = color, font = fontValue, value = string.Format("{0:C2}", transaction.Payment.ValorIngresado), x = (xMax - string.Format("{0:C2}", transaction.Payment.ValorIngresado).Length * multiplier), y = y });

                        data.Add(new DataPrinter { brush = color, font = fontKey, value = "Valor devuelto:", x = xKey, y = y += sum });
                        data.Add(new DataPrinter { brush = color, font = fontValue, value = string.Format("{0:C2}", transaction.Payment.ValorDispensado), x = (xMax - string.Format("{0:C2}", transaction.Payment.ValorDispensado).Length * multiplier), y = y });
                    }


                    data.Add(new DataPrinter { brush = color, font = fontValue, value = "-------------------------------------------------------------------", x = 2, y = y += 30 });

                    data.Add(new DataPrinter { brush = color, font = fontValue, value = "¡ Transacción exitosa !", x = 80, y = y += 50 });

                    data.Add(new DataPrinter { brush = color, font = fontValue, value = MessageResource.PrintMs1, x = xKey, y = y += sum });
                    data.Add(new DataPrinter { brush = color, font = fontValue, value = MessageResource.PrintMs2, x = xKey, y = y += 20 });

                    data.Add(new DataPrinter { brush = color, font = fontValue, value = "E-city Software", x = 100, y = y += sum });

                    AdminPayPlus.PrintService.Start(data);
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "PrintVoucher", ex, ex.ToString());
            }
        }

        public static decimal RoundValue(decimal Total, bool arriba)
        {
            try
            {
                decimal roundTotal = 0;

                if (arriba)
                {
                    roundTotal = Math.Ceiling(Total / 100) * 100;
                }
                else
                {
                    roundTotal = Math.Floor(Total / 100) * 100;
                }

                return roundTotal;
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex);
                return Total;
            }
        }

        public static bool ValidateModule(decimal module, decimal amount)
        {
            try
            {
                var result = (amount % module);
                return result == 0 ? true : false;
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex);
                return false;
            }
        }

        public static T ConverJson<T>(string path)
        {
            T response = default(T);
            try
            {
                using (StreamReader file = new StreamReader(path, Encoding.UTF8))
                {
                    try
                    {
                        var json = file.ReadToEnd().ToString();
                        if (!string.IsNullOrEmpty(json))
                        {
                            response = JsonConvert.DeserializeObject<T>(json);
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
            return response;
        }

        public static bool IsValidEmailAddress(string email)
        {
            try
            {
                Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,8}$");
                return regex.IsMatch(email);
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
                return false;
            }
        }

        public static void Speak(string text)
        {
            try
            {
                if (GetConfiguration("ActivateSpeak").ToUpper() == "SI")
                {
                    if (speechSynthesizer == null)
                    {
                        speechSynthesizer = new SpeechSynthesizer();
                    }

                    speechSynthesizer.SpeakAsyncCancelAll();
                    speechSynthesizer.SelectVoice("Microsoft Sabina Desktop");
                    speechSynthesizer.SpeakAsync(text);
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
        }

        public static string[] ReadFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return File.ReadAllLines(path);
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
            return null;
        }

        public static string GetIpPublish()
        {
            try
            {
                using (var client = new WebClient())
                {
                    return client.DownloadString(GetConfiguration("UrlGetIp"));
                }
            }
            catch (Exception ex)
            {
                // Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
            return GetConfiguration("IpDefoult");
        }

        public static void OpenKeyboard(bool keyBoard_Numeric, TextBox textBox, object thisView, int x = 0, int y = 0)
        {
            try
            {
                WPKeyboard.Keyboard.InitKeyboard(new WPKeyboard.Keyboard.DataKey
                {
                    control = textBox,
                    userControl = thisView is UserControl ? thisView as UserControl : null,
                    eType = (keyBoard_Numeric == true) ? WPKeyboard.Keyboard.EType.Numeric : WPKeyboard.Keyboard.EType.Standar,
                    window = thisView is Window ? thisView as Window : null,
                    X = x,
                    Y = y
                });
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
        }

        public static string[] ErrorDevice()
        {
            try
            {
                string[] keys = Utilities.ReadFile(@"" + ConstantsResource.PathDevice);

                return keys;
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
                return null;
            }
        }

        public static bool IsMultiple(decimal value)
        {
            try
            {
                if (value % 100 != 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Error.SaveLogError(MethodBase.GetCurrentMethod().Name, "Utilities", ex, ex.ToString());
            }
            return true;
        }
    }
}
