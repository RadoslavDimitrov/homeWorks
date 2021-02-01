﻿using System;
using SoftUni.Data;
using SoftUni.Models;
using System.Collections.Generic;

namespace SoftUni.Models
{
    public partial class EmployeesProjects
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Projects Project { get; set; }
    }
}
