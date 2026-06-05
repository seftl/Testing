using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LR1
{
    internal class OrderManager
    {
        public List<Order> Orders { get; private set; }
        private const string FilePath = "orders.txt";

        public OrderManager()
        {
            Orders = new List<Order>();
            LoadOrders();
        }

        public void AddOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            Orders.Add(order);
            SaveOrders();
        }

        public void RemoveOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            Orders.Remove(order);
            SaveOrders();
        }

        public void UpdateOrderStatus(Order order, OrderStatus newStatus)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            order.UpdateStatus(newStatus);
            SaveOrders();
        }

        public void SaveOrders()
        {
            var lines = Orders.Select(o =>
                $"{o.CustomerName}|{o.Description}|{o.CreationDate:yyyy-MM-dd}|{o.Status}");
            File.WriteAllLines(FilePath, lines); // UTF-8 без BOM по умолчанию
        }

        public void LoadOrders()
        {
            if (!File.Exists(FilePath)) return;
            foreach (var line in File.ReadAllLines(FilePath))
            {
                var parts = line.Split('|');
                if (parts.Length != 4) continue; // некорректные строки игнорируем
                var order = new Order(parts[0], parts[1], DateTime.Parse(parts[2]));
                order.UpdateStatus((OrderStatus)Enum.Parse(typeof(OrderStatus), parts[3]));
                Orders.Add(order);
            }
        }
    }
}