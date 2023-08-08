namespace MontgomeryAPI.Models
{
    public partial class TrafficViolation
    {
        public string SeqId { get; set; } = "";
        public DateOnly DateOfStop {get; set;}

        public TimeOnly TimeOfStop {get; set;}

        public string Agency { get; set; } = "";
        public string SubAgency { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool Accident { get; set; }
        public bool Belts { get; set; }
        public bool PersonalInjury {get; set;}
        public bool PropertyDamage {get; set;}
        public bool Fatal {get; set;}
        public bool CommercialLicense {get; set;}
        public bool Hazmat {get; set;}
        public bool CommercialVehicle {get; set;}
        public bool Alcohol {get; set;}
        public bool WorkZone {get; set;}
        public bool SearchConducted {get; set;}
        public string SearchDisposition { get; set; } = "";
        public string SearchOutcome { get; set; } = "";
        public string SearchReason { get; set; } = "";
        public string SearchReasonForStop { get; set; } = "";
        public string SearchType { get; set; } = "";
        public string SearchArrestReason { get; set; } = "";
        public string State { get; set; } = "";
        public string VehicleType { get; set; } = "";
        public int Year { get; set; }
        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public string Color { get; set; } = "";
        public string ViolationType { get; set; } = "";
        public string Charge { get; set; } = "";
        public string Article { get; set; } = "";
        public bool ContributedToAccident { get; set; }
        public string Race { get; set; } = "";
        public string Gender { get; set; } = "";
        public string DriverCity { get; set; } = "";
        public string DriverState { get; set; } = "";
        public string DLState { get; set; } = "";
        public string ArrestType { get; set; } = "";
        public string Geolocations { get; set; } = "";



    }
}