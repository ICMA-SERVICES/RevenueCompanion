namespace RevenueCompanion.Application.Enums
{
    public enum Status
    {
        INSERT = 1,
        UPDATE = 2,
        DELETE = 3,
        GETALL = 4,
        GETBYID = 5,
        GETMENUBYUSERID = 6,
        GETMENUBYROLEID = 7,
        GETSUBID = 8,
        GetMenuAssignedToUser = 9,
        GETBYPHONE = 10,
        Enable = 11,
        Disable = 12,
        Filter = 13
    }

    public enum SummaryReportStatus
    {
        SummaryReport = 1,
        SummaryDetails = 2,
        DefaultersHistory = 3,
        PayerList = 4,
    }
}
