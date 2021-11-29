﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEsqueletoSantiagoV1._1.DataModel
{
    public partial class CONFIGURATION_PAYDAD
    {
        public int ID { get; set; }
        public string USER_API { get; set; }
        public string PASSWORD_API { get; set; }
        public string USER { get; set; }
        public string PASSWORD { get; set; }
        public Nullable<int> ID_SESSION { get; set; }
        public Nullable<int> ID_PAYPAD { get; set; }
        public string TOKEN_API { get; set; }
        public Nullable<int> TYPE { get; set; }
    }
}
