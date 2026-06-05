using System;

namespace LR1
{
    public class OrderNotifier
    {
        private readonly NotificationSettings _settings;

        public OrderNotifier(NotificationSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        // Текст уведомления, либо null — если уведомлять не нужно
        public string GetNotification(Order order, OrderStatus newStatus)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            if (newStatus != OrderStatus.В_обработке && newStatus != OrderStatus.Завершён)
                return null;
            if (!_settings.IsEnabled(newStatus))
                return null;

            return $"Заказ «{order.CustomerName} — {order.Description}» перешёл в статус «{newStatus}».";
        }
    }
}