namespace TC.SkillsDatabase.Core.Results
{
    using System;
    using System.Collections.Generic;

    public interface IServiceResult
    {
        bool IsValid { get; }

        IList<NotificationMessage> Errors { get; set; }

        IList<NotificationMessage> Warnings { get; set; }

        IList<NotificationMessage> Messages { get; set; }
    }

    public interface IServiceResult<T> : IServiceResult
    {
        T Entity { get; set; }
    }
}
