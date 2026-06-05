using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR1;

namespace LR1.Tests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void Constructor_SetsCustomerName()
        {
            var order = new Order("Каримов", "Ноутбук", new DateTime(2026, 5, 25));
            Assert.AreEqual("Каримов", order.CustomerName);
        }

        [TestMethod]
        public void Constructor_SetsDescription()
        {
            var order = new Order("Каримов", "Ноутбук", new DateTime(2026, 5, 25));
            Assert.AreEqual("Ноутбук", order.Description);
        }

        [TestMethod]
        public void Constructor_SetsCreationDate()
        {
            var date = new DateTime(2026, 5, 25);
            var order = new Order("Каримов", "Ноутбук", date);
            Assert.AreEqual(date, order.CreationDate);
        }

        [TestMethod]
        public void Constructor_DefaultStatusIsNew()
        {
            var order = new Order("Каримов", "Ноутбук", new DateTime(2026, 5, 25));
            Assert.AreEqual(OrderStatus.Новый, order.Status);
        }

        [TestMethod]
        public void UpdateStatus_ChangesStatus()
        {
            var order = new Order("Каримов", "Ноутбук", new DateTime(2026, 5, 25));
            order.UpdateStatus(OrderStatus.Завершён);
            Assert.AreEqual(OrderStatus.Завершён, order.Status);
        }
    }
}