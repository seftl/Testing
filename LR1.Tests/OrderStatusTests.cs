using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LR1.Tests
{
    [TestClass]
    public class OrderStatusTests
    {
        [TestMethod]
        public void Enum_HasExactlyThreeValues()
        {
            Assert.AreEqual(3, Enum.GetValues(typeof(OrderStatus)).Length);
        }

        [TestMethod]
        public void Enum_ContainsНовый()
        {
            Assert.IsTrue(Enum.IsDefined(typeof(OrderStatus), OrderStatus.Новый));
        }

        [TestMethod]
        public void Enum_ContainsВобработке()
        {
            Assert.IsTrue(Enum.IsDefined(typeof(OrderStatus), OrderStatus.В_обработке));
        }

        [TestMethod]
        public void Enum_ContainsЗавершён()
        {
            Assert.IsTrue(Enum.IsDefined(typeof(OrderStatus), OrderStatus.Завершён));
        }
    }
}