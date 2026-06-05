using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LR1.Tests
{
    [TestClass]
    public class OrderManagerTests
    {
        private Order MakeOrder() =>
            new Order("Каримов", "Ноутбук", new DateTime(2026, 5, 25));

        [TestMethod]
        public void AddOrder_AddsToCollection()
        {
            var manager = new OrderManager();
            manager.Orders.Clear();
            manager.AddOrder(MakeOrder());
            Assert.AreEqual(1, manager.Orders.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddOrder_Null_Throws()
        {
            var manager = new OrderManager();
            manager.AddOrder(null);
        }

        [TestMethod]
        public void RemoveOrder_RemovesExisting()
        {
            var manager = new OrderManager();
            manager.Orders.Clear();
            var order = MakeOrder();
            manager.AddOrder(order);
            manager.RemoveOrder(order);
            Assert.AreEqual(0, manager.Orders.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveOrder_Null_Throws()
        {
            var manager = new OrderManager();
            manager.RemoveOrder(null);
        }

        [TestMethod]
        public void UpdateOrderStatus_ChangesStatus()
        {
            var manager = new OrderManager();
            manager.Orders.Clear();
            var order = MakeOrder();
            manager.AddOrder(order);
            manager.UpdateOrderStatus(order, OrderStatus.В_обработке);
            Assert.AreEqual(OrderStatus.В_обработке, order.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateOrderStatus_Null_Throws()
        {
            var manager = new OrderManager();
            manager.UpdateOrderStatus(null, OrderStatus.Завершён);
        }

        [TestMethod]
        public void SaveAndLoad_PersistsOrders()
        {
            var manager = new OrderManager();
            manager.Orders.Clear();
            manager.AddOrder(MakeOrder());  

            var reloaded = new OrderManager();  
            Assert.AreEqual(1, reloaded.Orders.Count);
            Assert.AreEqual("Каримов", reloaded.Orders[0].CustomerName);
        }




        // добавил новые 9 тестов (5 работа

        [TestMethod]
        public void Notify_Processing_EnabledByDefault()
        {
            Assert.IsTrue(new OrderManager().IsNotificationEnabled(OrderStatus.В_обработке));
        }

        [TestMethod]
        public void Notify_Completed_EnabledByDefault()
        {
            Assert.IsTrue(new OrderManager().IsNotificationEnabled(OrderStatus.Завершён));
        }

        [TestMethod]
        public void Notify_New_DisabledByDefault()
        {
            Assert.IsFalse(new OrderManager().IsNotificationEnabled(OrderStatus.Новый));
        }

        [TestMethod]
        public void SetNotificationEnabled_False_Disables()
        {
            var m = new OrderManager();
            m.SetNotificationEnabled(OrderStatus.Завершён, false);
            Assert.IsFalse(m.IsNotificationEnabled(OrderStatus.Завершён));
        }

        [TestMethod]
        public void GetStatusNotification_Completed_ReturnsText()
        {
            var m = new OrderManager();
            var order = MakeOrder();
            Assert.IsFalse(string.IsNullOrEmpty(m.GetStatusNotification(order, OrderStatus.Завершён)));
        }

        [TestMethod]
        public void GetStatusNotification_New_ReturnsNull()
        {
            var m = new OrderManager();
            Assert.IsNull(m.GetStatusNotification(MakeOrder(), OrderStatus.Новый));
        }

        [TestMethod]
        public void GetStatusNotification_Disabled_ReturnsNull()
        {
            var m = new OrderManager();
            m.SetNotificationEnabled(OrderStatus.Завершён, false);
            Assert.IsNull(m.GetStatusNotification(MakeOrder(), OrderStatus.Завершён));
        }

        [TestMethod]
        public void GetStatusNotification_ContainsNameAndStatus()
        {
            var m = new OrderManager();
            string msg = m.GetStatusNotification(MakeOrder(), OrderStatus.Завершён);
            StringAssert.Contains(msg, "Каримов");
            StringAssert.Contains(msg, "Завершён");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetStatusNotification_NullOrder_Throws()
        {
            new OrderManager().GetStatusNotification(null, OrderStatus.Завершён);
        }
    }
}