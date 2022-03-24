using System;
using System.ComponentModel.DataAnnotations;

namespace DentalApps.Models
{
	public class Treatment
	{
        public Treatment()
        {
            SubTreatments = new List<SubTreatment>();
        }

        [Key]
        public int TreatmentID { get; set; }
        public string PatientID { get; set; } = string.Empty;
        public string EmployeeID { get; set; } = string.Empty;
        public string Anamnesis { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string Recipe { get; set; } = string.Empty;
        public DateTime TreatmentDate { get; set; }

        public Patient Patient { get; set; } = new();
        public Employee Employee { get; set; } = new();

        public ICollection<SubTreatment> SubTreatments { get; set; }
    }
}

