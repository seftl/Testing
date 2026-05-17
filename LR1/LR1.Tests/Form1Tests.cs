using Microsoft.VisualStudio.TestTools.UnitTesting;
using LR1;

namespace LR1.Tests;

[TestClass]
public class Form1Tests
{
    private Form1 _form;

    [TestInitialize]
    public void SetUp()
    {
        _form = new Form1();
        _form.orderManager.Orders.Clear();
        _form.Show();
    }

    [TestCleanup]
    public void TearDown()
    {
        _form.Dispose();
    }

    [TestMethod]
    public void CustomerNameTextBox_IsVisibleAndEnabled()
    {
        Assert.IsTrue(_form.customerNameTextBox.Visible);
        Assert.IsTrue(_form.customerNameTextBox.Enabled);
    }

    [TestMethod]
    public void AddOrderButton_IsVisibleAndEnabled()
    {
        Assert.IsTrue(_form.addOrderButton.Visible);
        Assert.IsTrue(_form.addOrderButton.Enabled);
    }

    [TestMethod]
    public void OrdersListBox_IsVisibleAndEnabled()
    {
        Assert.IsTrue(_form.ordersListBox.Visible);
        Assert.IsTrue(_form.ordersListBox.Enabled);
    }

    [TestMethod]
    public void AddOrderButton_Click_ValidData_AddsOrder()
    {
        _form.customerNameTextBox.Text = "Seftl";
        _form.descriptionTextBox.Text = "ручка";
        _form.addOrderButton.PerformClick();
        Assert.AreEqual(1, _form.orderManager.Orders.Count);
    }

    [TestMethod]
    public void RemoveOrderButton_Click_SelectedOrder_RemovesOrder()
    {
        var order = new Order("Seftl", "ручка", DateTime.Now);
        _form.orderManager.AddOrder(order);
        _form.RefreshOrdersList();
        _form.ordersListBox.SelectedIndex = 0;
        _form.removeOrderButton.PerformClick();
        Assert.AreEqual(0, _form.orderManager.Orders.Count);
    }
}