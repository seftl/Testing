using System.Collections.Generic;

namespace LR1
{
    public class NotificationSettings
    {
        private readonly HashSet<OrderStatus> _enabled;

        public NotificationSettings()
        {
            // По умолчанию уведомляем о "В обработке" и "Завершён"
            _enabled = new HashSet<OrderStatus>
            {
                OrderStatus.В_обработке,
                OrderStatus.Завершён
            };
        }

        public bool IsEnabled(OrderStatus status) => _enabled.Contains(status);

        public void SetEnabled(OrderStatus status, bool enabled)
        {
            if (enabled) _enabled.Add(status);
            else _enabled.Remove(status);
        }
    }
}