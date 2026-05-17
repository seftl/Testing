using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR1;

namespace LR1.Tests;

[TestClass]
public class OrderStatusTests
{
    [TestMethod]
    public void OrderStatus_HasExactlyThreeValues()
    {
        var values = Enum.GetValues(typeof(OrderStatus));
        Assert.AreEqual(3, values.Length);
    }

    [TestMethod]
    public void OrderStatus_ContainsНовый()
    {
        Assert.IsTrue(Enum.IsDefined(typeof(OrderStatus), OrderStatus.Новый));
    }

    [TestMethod]
    public void OrderStatus_ContainsВОбработке()
    {
        Assert.IsTrue(Enum.IsDefined(typeof(OrderStatus), OrderStatus.В_обработке));
    }

    [TestMethod]
    public void OrderStatus_ContainsЗавершён()
    {
        Assert.IsTrue(Enum.IsDefined(typeof(OrderStatus), OrderStatus.Завершён));
    }
}