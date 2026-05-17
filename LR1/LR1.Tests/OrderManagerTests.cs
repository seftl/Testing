using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR1;

namespace LR1.Tests;

[TestClass]
public class OrderManagerTests
{
    private OrderManager _manager;

    [TestInitialize]
    public void SetUp()
    {
        _manager = new OrderManager();
        _manager.Orders.Clear();
    }

    [TestMethod]
    public void AddOrder_ValidOrder_AddsToList()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        _manager.AddOrder(order);
        Assert.AreEqual(1, _manager.Orders.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddOrder_NullOrder_ThrowsArgumentNullException()
    {
        _manager.AddOrder(null);
    }

    [TestMethod]
    public void RemoveOrder_ExistingOrder_RemovesFromList()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        _manager.AddOrder(order);
        _manager.RemoveOrder(order);
        Assert.AreEqual(0, _manager.Orders.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RemoveOrder_NullOrder_ThrowsArgumentNullException()
    {
        _manager.RemoveOrder(null);
    }

    [TestMethod]
    public void UpdateOrderStatus_ChangesStatus()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        _manager.AddOrder(order);
        _manager.UpdateOrderStatus(order, OrderStatus.Завершён);
        Assert.AreEqual(OrderStatus.Завершён, order.Status);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateOrderStatus_NullOrder_ThrowsArgumentNullException()
    {
        _manager.UpdateOrderStatus(null, OrderStatus.Завершён);
    }

    [TestMethod]
    public void SaveAndLoad_OrdersAreRestored()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        _manager.AddOrder(order);

        var manager2 = new OrderManager();
        Assert.AreEqual(1, manager2.Orders.Count);
        Assert.AreEqual("Seftl", manager2.Orders[0].CustomerName);
    }
}