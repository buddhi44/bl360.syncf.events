using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.shared.Constants.Storage
{
    public static class StorageConstants
    {
        public static class Local
        {
            public static string Preference = "clientPreference";
            public static string Auth = "Auth";//"Auth"
            public static string AuthToken = "authToken";
            public static string RefreshToken = "refreshToken";
            public static string UserImageURL = "userImageURL";
            public static string CompanyName = "companyName";
            public static string SelectedShift = "SelectedShift";
            public static string ShiftStartDate = "ShiftStartDate";
            public static string _SHOULD_SELECT_SHIFT_ = "_SHOULD_SELECT_SHIFT_";
            public static string _SHOULD_GOTO_COMPANY_SELECTION_ = "_SHOULD_GOTO_COMPANY_SELECTION_";

            //user info
            public static string UID = "UID";
            public static string CID = "CID";

            //Printer Related Constants
            public static string PRINTER_BRAND = "printer_brand";
            public static string PRINTER_MODEL = "printer_model";
            public static string CONNECTION_MODE = "connection_mode";
            public static string PRINTER_ADDRESS = "printer_address";
            public static string IS_USE_INTERNAL_APP_PRINTING = "is_use_internal_app_printing";

            //workstation
            public static string WORK_STATION_SAVED = "WrkStnSaved";

            //language
            public static string LANGUAGE_CULTURE = "language_culture";
        }

        public static class Server
        {
            public static string Preference = "serverPreference";

            //TODO - add
        }
    }
}
