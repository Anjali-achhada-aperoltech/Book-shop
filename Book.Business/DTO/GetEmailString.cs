﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Business.DTO
{
    public class GetEmailString
    {
        public string key {  get; set; }
        public string From {  get; set; }
        public string SmtpServer {  get; set; }
        public int Port {  get; set; }
        public bool EnableSSL {  get; set; }
    }
}
