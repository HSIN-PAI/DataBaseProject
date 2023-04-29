using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicServiceSystem.Models
{
    public class Clinic
    {
        public decimal ClinicId { get; set; }
        public string ClinicName { get; set; }
        public int ClinicType { get; set; }
        public int ServiceType { get; set; }
        public int OutPatientType { get; set; }
        public int CoopType { get; set; }
        public int BusinessHour { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Rmark { get; set; }
        public string DepartmentName { get; set; }
        public int? CountyId { get; set; }
    }
}