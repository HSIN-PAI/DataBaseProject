using System.Web.Mvc;

namespace ClinicServiceSystem.Models
{
    public class Clinic : ViewModel
    {
        public string ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string ClinicType { get; set; }
        public string ServiceType { get; set; }
        public string OutPatientType { get; set; }
        public string CoopType { get; set; }
        public string BusinessHour { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public string DepartmentName { get; set; }
        public int? CountyId { get; set; }

        public SelectList ClinicTypeSelectList { get; set; }
        public SelectList ServiceTypeSelectList { get; set; }
        public SelectList OutPatientTypeSelectList { get; set; }
        public SelectList CoopTypeSelectList { get; set; }
        public SelectList BusinessHourSelectList { get; set; }
    }
}