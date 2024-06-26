using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class SweetAlert
    {
        public enum NotificationType
        {
            error,
            success,
            warning,
            info
        }

        public static String Show(String title, String message, NotificationType notificationType)
        {
            string alertType = "";

            switch (notificationType) {
                case NotificationType.error:
                    alertType = "error";
                    break;
                case NotificationType.success:
                    alertType = "success";
                    break;
                case NotificationType.warning:
                    alertType = "warning";
                    break;
                case NotificationType.info:
                    alertType = "info";
                    break;
                default:
                    alertType = "";
                    break;
            }

            SweetAlertResponse objResponse = new SweetAlertResponse()
            {
                isSweetAlert = true,
                alertType = alertType,
                message = message,
            };

            return JsonConvert.SerializeObject(objResponse);
        }

    }

    public class SweetAlertResponse
    {
        public bool isSweetAlert { get; set; }
        public string alertType { get; set; }
        public string message { get; set; }

    }

}
