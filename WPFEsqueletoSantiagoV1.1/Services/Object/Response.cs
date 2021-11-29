using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFEsqueletoSantiagoV1._1.Classes;
using WPFEsqueletoSantiagoV1._1.Models;

namespace WPFEsqueletoSantiagoV1._1.Services.Object
{
    public class Response
    {
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
