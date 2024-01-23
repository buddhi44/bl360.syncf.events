using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bl360.clientInfrastructure.Routes
{
    public class BaseEndpoint
    {
        //private static string ProductionBaseURL = "https://bl360x.com//BLECOMTEST/api/";
        private static string ProductionBaseURL = "https://blcoreapi.azurewebsites.net/api/"; 
        
        /*"https://bluelotusx.co/CoreAPI/api/";*/

        private static string ReportDevURL = "https://localhost:7036/api/";
		private static string DevURL = "https://localhost:7036/api/";


        private static bool IsDevMode = false;
        public static string BaseURL
        {
            get
            {
                if (IsDevMode)
                {
                    return DevURL;

                }
                    return ProductionBaseURL; 
            }
        }

        public static string ReportURL
        {
            get
            {
                if (IsDevMode)
                {
                    return DevURL + "telerikreports";

                }
                //return "https://bl360x.com//BLECOMTEST/api/telerikreports";
                return ProductionBaseURL + "telerikreports";
            }
        }

        public static string ReportDesignerURL
        {
            get
            {
                if (IsDevMode)
                {
                    return ReportDevURL + "telerikreports";

                }
                return "https://bluelotus360.co/CoreApi/api/reportdesigner";
            }
        }

        public static string ProductImageURL
		{
			get
			{
				return "https://bl360x.com/BLECOMTEST/images/product_images";
			}
		}

        public static string SendEmailBLURL
        {
            get
            {
                //return "https://localhost:44377/Home/TransactionReminderSendEmailAsync/";
                return "https://bl360x.com/SendGridAlert/Home/TransactionReminderSendEmailAsync/";
            }
        }
    }
}
