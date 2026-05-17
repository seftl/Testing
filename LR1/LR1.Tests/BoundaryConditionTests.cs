using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR1;

namespace LR1.Tests;

[TestClass]
public class BoundaryConditionTests
{
    private OrderManager _manager;

    [TestInitialize]
    public void SetUp()
    {
        _manager = new OrderManager();
        _manager.Orders.Clear();
    }

    [TestMethod]
    public void Order_EmptyCustomerName_IsAllowed()
    {
        var order = new Order("", "ручка", DateTime.Now);
        Assert.AreEqual("", order.CustomerName);
    }

    [TestMethod]
    public void Order_VeryLongDescription_IsAllowed()
    {
        var longDesc = new string('A', 10000);
        var order = new Order("Seftl", longDesc, DateTime.Now);
        Assert.AreEqual(10000, order.Description.Length);
    }

    [TestMethod]
    public void Order_MinDateTime_IsAllowed()
    {
        var order = new Order("Seftl", "ручка", DateTime.MinValue);
        Assert.AreEqual(DateTime.MinValue, order.CreationDate);
    }

    [TestMethod]
    public void Order_FutureDate_IsAllowed()
    {
        var future = new DateTime(2099, 12, 31);
        var order = new Order("Seftl", "ручка", future);
        Assert.AreEqual(future, order.CreationDate);
    }

    [TestMethod]
    public void RemoveOrder_NonExistingOrder_DoesNotThrow()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        _manager.RemoveOrder(order);
        Assert.AreEqual(0, _manager.Orders.Count);
    }
}