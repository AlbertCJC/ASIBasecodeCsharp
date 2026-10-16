using ASI.Basecode.Data;
using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;

namespace ASI.Basecode.Services.ViewModelClasses
{
    public class PolicyFormViewModel
    {
        public long PolicyVersionId { get; set; }
        public string VersionNumber { get; set; }
        public DateTime EffectiveFromUtc { get; set; } = DateTime.UtcNow;
        public int MaxBorrowDurationDays { get; set; } = 7;
        public int MaxActiveBorrowingUnits { get; set; } = 3;
        public int OverdueGracePeriodMinutes { get; set; } = 0;
        public int LowStockThreshold { get; set; } = 5;
        public bool BlockReservationWhenOverdue { get; set; } = true;
    }

    public class SettingsIndexViewModel
    {
        public PolicyFormViewModel Policy { get; set; }
        public List<BorrowingPolicyVersion> PolicyHistory { get; set; } = new();
        public List<EquipmentCategory> Categories { get; set; } = new();
        public List<Location> Locations { get; set; } = new();
    }

    public class NotificationRowViewModel
    {
        public long NotificationId { get; set; }
        public string NotificationType { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }

    public class NotificationsIndexViewModel
    {
        public List<NotificationRowViewModel> Notifications { get; set; } = new();
        public int UnreadCount { get; set; }
    }
}
