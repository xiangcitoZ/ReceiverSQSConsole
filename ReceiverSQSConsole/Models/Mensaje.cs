﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverSQSConsole.Models
{
    public class Mensaje
    {
        public string Asunto { get; set; }
        public string Email { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }

        public string ReceiptHandle { get; set; }

    }
}
