﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Shift
    {
        public byte ShiftId { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; }
    }
}
