﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public static class GlobalController
    {
        public static ChestionarController Controller { get; } = new ChestionarController();
    }
}

