namespace MontgomeryAPI.Models
{
    public partial class Agency
    {
        //this model may be used to update agency head and name possibly can utilize this model for agency and subagency since they follow the exact same pattern
        public int AgencyId { get; set; }
        public string AgencyName {get; set;} = "";
        public int AgencyHeadUserId { get; set; }
    }
}