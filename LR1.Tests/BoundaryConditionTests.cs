using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LR1.Tests
{
    [TestClass]
    public class BoundaryConditionTests
    {
        [TestMethod]
        public void Order_EmptyCustomerName_IsStored()
        {
            var order = new Order("", "Описание", new DateTime(2026, 5, 25));
            Assert.AreEqual("", order.CustomerName);
        }

        [TestMethod]
        public void Order_VeryLongDescription_IsStored()
        {
            string longDesc = new string('a', 10000);
            var order = new Order("Каримов", longDesc, new DateTime(2026, 5, 25));
            Assert.AreEqual(10000, order.Description.Length);
        }

        [TestMethod]
        public void Order_MinDate_IsStored()
        {
            var order = new Order("Каримов", "Описание", DateTime.MinValue);
            Assert.AreEqual(DateTime.MinValue, order.CreationDate);
        }

        [TestMethod]
        public void Order_FutureDate_IsStored()
        {
            var future = DateTime.Now.AddYears(1);
            var order = new Order("Каримов", "Описание", future);
            Assert.IsTrue(order.CreationDate > DateTime.Now);
        }

        [TestMethod]
        public void RemoveOrder_NonExistent_DoesNotThrow()
        {
            var manager = new OrderManager();
            manager.Orders.Clear();
            var order = new Order("Каримов", "Описание", new DateTime(2026, 5, 25));
            manager.RemoveOrder(order);
            Assert.AreEqual(0, manager.Orders.Count);
        }
    }
}