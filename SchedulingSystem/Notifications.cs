using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulingSystem
{
    public class Notifications
    {
        public string notificationID;
        public string notificationType;
        public int messageLength;
        public string messageBody;

        public void sendMessage(string message)
        {
            messageBody = message;
        }
    }

  
}