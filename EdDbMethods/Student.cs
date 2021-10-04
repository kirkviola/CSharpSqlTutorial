﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdDbLib
{
    public class Student
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string StateCode { get; set; }
        public int? SAT { get; set; }
        public decimal GPA { get; set; }
        public int? MajorId { get; set; }

        public override string ToString()
        {
            return $"{this.Id} | {this.Firstname} {this.Lastname} | {this.StateCode} | " +
                     $"{(this.SAT != null ? SAT : "NULL")} | {this.GPA}";
        }

    }
}
