using System;
using System.Collections.Generic;
using System.Text;

namespace RevenueCompanion.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
