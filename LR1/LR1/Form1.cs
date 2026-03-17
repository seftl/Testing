using System;
using System.Windows.Forms;

namespace LR1
{
    public partial class Form1 : Form
    {
        private OrderManager orderManager;

        private TextBox customerNameTextBox;
        private TextBox descriptionTextBox;
        private DateTimePicker creationDatePicker;
        private ComboBox statusComboBox;
        private Button addOrderButton;
        private Button removeOrderButton;
        private Button updateStatusButton;
        private ListBox ordersListBox;

        public Form1()
        {
            InitializeComponent();

            Text = "Управление заказами";
            Width = 600;
            Height = 450;
            StartPosition = FormStartPosition.CenterScreen;

            customerNameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 150
            };

            descriptionTextBox = new TextBox
            {
                Location = new System.Drawing.Point(170, 10),
                Width = 200
            };

            creationDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(380, 10),
                Width = 190
            };

            addOrderButton = new Button
            {
                Location = new System.Drawing.Point(10, 40),
                Text = "Добавить",
                Width = 100
            };
            addOrderButton.Click += AddOrderButton_Click;

            removeOrderButton = new Button
            {
                Location = new System.Drawing.Point(120, 40),
                Text = "Удалить",
                Width = 100
            };
            removeOrderButton.Click += RemoveOrderButton_Click;

            updateStatusButton = new Button
            {
                Location = new System.Drawing.Point(230, 40),
                Text = "Обновить статус",
                Width = 130
            };
            updateStatusButton.Click += UpdateStatusButton_Click;

            statusComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(370, 40),
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            statusComboBox.Items.AddRange(new object[]
            {
                OrderStatus.Новый,
                OrderStatus.В_обработке,
                OrderStatus.Завершён
            });
            statusComboBox.SelectedIndex = 0;

            ordersListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 80),
                Width = 560,
                Height = 300
            };

            Controls.Add(customerNameTextBox);
            Controls.Add(descriptionTextBox);
            Controls.Add(creationDatePicker);
            Controls.Add(addOrderButton);
            Controls.Add(removeOrderButton);
            Controls.Add(updateStatusButton);
            Controls.Add(statusComboBox);
            Controls.Add(ordersListBox);

            orderManager = new OrderManager();
            UpdateOrdersList();
        }

        private void UpdateOrdersList()
        {
            ordersListBox.Items.Clear();

            foreach (var order in orderManager.Orders)
            {
                ordersListBox.Items.Add(
                    $"{order.CustomerName} - {order.Description} ({order.Status}) [{order.CreationDate:dd.MM.yyyy}]");
            }
        }

        private void AddOrderButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(customerNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(descriptionTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            DateTime creationDate = creationDatePicker.Value;
            Order newOrder = new Order(
                customerNameTextBox.Text.Trim(),
                descriptionTextBox.Text.Trim(),
                creationDate
            );
            try
            {
                orderManager.AddOrder(newOrder);
                customerNameTextBox.Clear();
                descriptionTextBox.Clear();
                UpdateOrdersList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveOrderButton_Click(object sender, EventArgs e)
{
    if (ordersListBox.SelectedIndex == -1)
    {
        MessageBox.Show("Выберите заказ для удаления!");
        return;
    }

    var orderToRemove = orderManager.Orders[ordersListBox.SelectedIndex];

    try
    {
        orderManager.RemoveOrder(orderToRemove);
        UpdateOrdersList();
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
}

private void UpdateStatusButton_Click(object sender, EventArgs e)
{
    if (ordersListBox.SelectedIndex == -1)
    {
        MessageBox.Show("Выберите заказ для обновления статуса!");
        return;
    }

    if (statusComboBox.SelectedItem == null)
    {
        MessageBox.Show("Выберите новый статус!");
        return;
    }

    var orderToUpdate = orderManager.Orders[ordersListBox.SelectedIndex];
    OrderStatus newStatus = (OrderStatus)statusComboBox.SelectedItem;

    try
    {
        orderManager.UpdateOrderStatus(orderToUpdate, newStatus);
        UpdateOrdersList();
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
}
    }
}