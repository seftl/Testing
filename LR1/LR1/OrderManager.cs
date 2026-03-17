using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LR1
{
    public class OrderManager
    {
        public List<Order> Orders { get; private set; }

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

        private void SaveOrders()
        {
            File.WriteAllLines(
                "orders.txt",
                Orders.Select(o =>
                    $"{o.CustomerName}|{o.Description}|{(int)o.Status}|{o.CreationDate:yyyy-MM-dd HH:mm:ss}")
            );
        }

        private void LoadOrders()
        {
            if (!File.Exists("orders.txt"))
                return;

            var lines = File.ReadAllLines("orders.txt");

            foreach (var line in lines)
            {
                var parts = line.Split('|');

                if (parts.Length == 4)
                {
                    if (int.TryParse(parts[2], out int statusValue) &&
                        Enum.IsDefined(typeof(OrderStatus), statusValue) &&
                        DateTime.TryParse(parts[3], out DateTime date))
                    {
                        var order = new Order(parts[0], parts[1], date);
                        order.Status = (OrderStatus)statusValue;
                        Orders.Add(order);
                    }
                }
            }
        }
    }
}