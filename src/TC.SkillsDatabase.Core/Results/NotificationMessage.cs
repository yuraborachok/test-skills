namespace TC.SkillsDatabase.Core.Results
{
    using System;

    public class NotificationMessage
    {
        public NotificationMessage()
        {
        }

        public NotificationMessage(string propertyName, string message)
        {
            this.PropertyName = propertyName;
            this.Message = message;
        }

        public string PropertyName { get; set; }

        public string Message { get; set; }
    }
}
