﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IStudent
    {
       string Name { get; set; }

       string Email { get; set; }

       DateTime StartDate { get; set; }

       DateTime EndDate { get; set; }
    }
}
