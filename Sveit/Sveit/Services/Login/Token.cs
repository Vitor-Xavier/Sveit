﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sveit.Services.Login
{
    public class Token
    {
        public string Access_token { get; set; }

        public string Token_type { get; set; }

        public int Expires_in { get; set; }
    }
}
