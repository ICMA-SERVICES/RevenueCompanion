namespace RevenueCompanion.Presentation.Constants
{
    public class ApplicationContants
    {
        public static string Authenticate = "account/authenticate";
        public static string GetUserCount = "account/GetUserCount/";
        public static string confirm_email = "account/confirm-email/";
        public static string reset_password = "account/reset-password";
        public static string forgot_Password = "account/forgot-password";
        public static string GetRoles = "account/get-roles";
        public static string LogOut = "account/logout";
        public static string GetStateUrlSetup = "v1.0/Menu/getStateUrlSetup";
        public static string GetApprovalCount = "v1.0/ApprovalSetting/count-all";
        public static string GetMenusWithApprovalSettingsSetAsTrue = "v1.0/Menu/get-approval-menus";
        public static string GetMenuSetup = "v1.0/Menu/get-user-pages/";
        public static string GetCreditNotRequestByUserId = "v1.0/CreditNote/get-all-by-userId/";
        public static string GetCreditNotRequestByUser = "v1.0/CreditNote/get-creditnote-by-userId/";
        public static string GetNotesNotAttendedToByUserId = "v1.0/CreditNote/get-credit-note-list-unattended/";
        public static string GetCreditNotRequestDetails = "v1.0/CreditNote/get-approval-details/";



        public static bool ValidatePassword(string passWord)
        {
            if (passWord.Length < 8)
                return false;
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }

            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions < 3) return false;
            char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' }; // or whatever    
            if (passWord.IndexOfAny(special) == -1)
            {

            }
            else
            {
                validConditions++;
            }
            if (validConditions >= 4)
            {
                return true;
            }
            return false;
        }

    }
}
