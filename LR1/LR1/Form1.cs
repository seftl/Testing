using System;
using System.Windows.Forms;

namespace LR1
{
    public partial class Form1 : Form
    {
        internal OrderManager orderManager;

        internal TextBox customerNameTextBox;
        internal TextBox descriptionTextBox;
        internal DateTimePicker creationDatePicker;
        internal ComboBox statusComboBox;
        internal Button addOrderButton;
        internal Button removeOrderButton;
        internal Button updateStatusButton;
        internal ListBox ordersListBox;

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

            removeOrderButton = new Button
            {
                Location = new System.Drawing.Point(120, 40),
                Text = "Удалить",
                Width = 100
            };

            updateStatusButton = new Button
            {
                Location = new System.Drawing.Point(230, 40),
                Text = "Обновить статус",
                Width = 130
            };

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

            addOrderButton.Click += AddOrderButton_Click;
            removeOrderButton.Click += RemoveOrderButton_Click;
            updateStatusButton.Click += UpdateStatusButton_Click;

            RefreshOrdersList();
        }

        private void AddOrderButton_Click(object sender, EventArgs e)
        {
            var customerName = customerNameTextBox.Text.Trim();
            var description = descriptionTextBox.Text.Trim();
            var creationDate = creationDatePicker.Value;

            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Введите имя клиента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var order = new Order(customerName, description, creationDate);
            orderManager.AddOrder(order);
            RefreshOrdersList();
        }

        private void RemoveOrderButton_Click(object sender, EventArgs e)
        {
            if (ordersListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите заказ", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = orderManager.Orders[ordersListBox.SelectedIndex];
            orderManager.RemoveOrder(selected);
            RefreshOrdersList();
        }

        private void UpdateStatusButton_Click(object sender, EventArgs e)
        {
            if (ordersListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите заказ", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = orderManager.Orders[ordersListBox.SelectedIndex];
            var newStatus = (OrderStatus)statusComboBox.SelectedItem;
            orderManager.UpdateOrderStatus(selected, newStatus);
            RefreshOrdersList();
        }

        internal void RefreshOrdersList()
        {
            ordersListBox.Items.Clear();
            foreach (var order in orderManager.Orders)
                ordersListBox.Items.Add(order.ToString());
        }
    }
}