using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR1;

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
            manager.AddOrder(MakeOrder());      // AddOrder внутри вызывает приватный SaveOrders

            var reloaded = new OrderManager();  // конструктор вызывает LoadOrders
            Assert.AreEqual(1, reloaded.Orders.Count);
            Assert.AreEqual("Каримов", reloaded.Orders[0].CustomerName);
        }
    }
}