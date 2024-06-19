using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OutOfOffice.MVC.Models;

namespace OutOfOffice.MVC.Extensions
{
    public static class ControllerExtensions
    {
        // Extension to pass the notification to the view
        public static void SetNotification(this Controller controller, string type, string message)
        {
            var notification = new Notification(type, message);
            controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
        }
    }
}
