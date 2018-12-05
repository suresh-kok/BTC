namespace Travel_Request_System_EF.Models.ViewModel
{
    public class Constants
    {
        public const string Employee = "Employee";
        public const string HR = "HR";
        public const string Manager = "Manager";
        public const string TravelCorordinator = "TravelCo";
        public const string Admin = "Admin";
    }

    public enum ProcessingStatus
    {
        NotProcessed,
        BeingProcessed,
        Processed
    }

    public enum ProcessingSections
    {
        AT = 1,
        HS,
        PC,
        ATHS,
        ATPC,
        HSPC,
        ATHSPC
    }

    public enum ApprovalLevels
    {
        ToBeApproved = 1,
        ApprovedByManager,
        RejectedByManager,
        ApprovedByHR,
        RejectedByHR,
        ApprovedbyTravelCo
    }
}