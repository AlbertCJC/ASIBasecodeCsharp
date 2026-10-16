using ASI.Basecode.Services.ViewModelClasses;
using System.Collections.Generic;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IMonitoringService
    {
        DashboardViewModel GetDashboard();

        TransactionMonitoringViewModel GetTransactions(string search, string tab, int page, int pageSize);

        AuditLogsViewModel GetAuditLogs(string search, string entityType, int page, int pageSize);

        string ExportAuditLogsCsv(string search, string entityType);

        string ExportMovementsCsv(string search);
    }
}
