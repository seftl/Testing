using System;

namespace LR1
{
    public class Order
    {
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }

        public Order(string customerName, string description, DateTime creationDate)
        {
            CustomerName = customerName;
            Description = description;
            Status = OrderStatus.Новый;
            CreationDate = creationDate;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }
    }
}