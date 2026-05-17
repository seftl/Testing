using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR1;

namespace LR1.Tests;

[TestClass]
public class OrderTests
{
    [TestMethod]
    public void Constructor_SetsCustomerName()
    {
        // Arrange & Act
        var order = new Order("Seftl", "ручка", DateTime.Now);

        // Assert
        Assert.AreEqual("Seftl", order.CustomerName);
    }

    [TestMethod]
    public void Constructor_SetsDescription()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        Assert.AreEqual("ручка", order.Description);
    }

    [TestMethod]
    public void Constructor_SetsCreationDate()
    {
        var date = new DateTime(2024, 1, 15);
        var order = new Order("Seftl", "ручка", date);
        Assert.AreEqual(date, order.CreationDate);
    }

    [TestMethod]
    public void Constructor_DefaultStatus_IsНовый()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        Assert.AreEqual(OrderStatus.Новый, order.Status);
    }

    [TestMethod]
    public void UpdateStatus_ChangesStatus()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        order.UpdateStatus(OrderStatus.Завершён);
        Assert.AreEqual(OrderStatus.Завершён, order.Status);
    }
}