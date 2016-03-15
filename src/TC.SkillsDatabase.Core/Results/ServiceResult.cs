namespace TC.SkillsDatabase.Core.Results
{
    using System;
    using System.Collections.Generic;

    public class ServiceResult<T> : IServiceResult<T>
    {
        public ServiceResult()
        {
            this.Errors = new List<NotificationMessage>();
            this.Warnings = new List<NotificationMessage>();
            this.Messages = new List<NotificationMessage>();
        }

        public bool IsValid
        {
            get { return this.Errors == null || this.Errors.Count == 0; }
        }

        public IList<NotificationMessage> Errors { get; set; }

        public IList<NotificationMessage> Warnings { get; set; }

        public IList<NotificationMessage> Messages { get; set; }

        public T Entity { get; set; }
    }
}
