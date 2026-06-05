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
        internal CheckBox notifyProcessingCheckBox;
        internal CheckBox notifyCompletedCheckBox;

        public Form1()
        {
            InitializeComponent();

            Text = "Управление заказами";
            ClientSize = new System.Drawing.Size(800, 450);
            StartPosition = FormStartPosition.CenterScreen;

            // Менеджер создаём до чекбоксов — они читают его настройки уведомлений
            orderManager = new OrderManager();

            var customerNameLabel = new Label { Text = "Имя клиента", Location = new System.Drawing.Point(10, 10), Width = 150 };
            var descriptionLabel = new Label { Text = "Описание", Location = new System.Drawing.Point(170, 10), Width = 200 };
            var dateLabel = new Label { Text = "Дата создания", Location = new System.Drawing.Point(380, 10), Width = 190 };

            customerNameTextBox = new TextBox { Location = new System.Drawing.Point(10, 32), Width = 150 };
            descriptionTextBox = new TextBox { Location = new System.Drawing.Point(170, 32), Width = 200 };
            creationDatePicker = new DateTimePicker { Location = new System.Drawing.Point(380, 32), Width = 190 };

            addOrderButton = new Button { Location = new System.Drawing.Point(10, 64), Text = "Добавить", Width = 100 };
            removeOrderButton = new Button { Location = new System.Drawing.Point(120, 64), Text = "Удалить", Width = 100 };
            updateStatusButton = new Button { Location = new System.Drawing.Point(230, 64), Text = "Обновить статус", Width = 130 };

            statusComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(370, 64),
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

            // Настройки уведомлений (ЛР5)
            notifyProcessingCheckBox = new CheckBox
            {
                Text = "Уведомлять: В обработке",
                Location = new System.Drawing.Point(10, 96),
                Width = 180,
                Checked = orderManager.IsNotificationEnabled(OrderStatus.В_обработке)
            };
            notifyProcessingCheckBox.CheckedChanged += (s, e) =>
                orderManager.SetNotificationEnabled(OrderStatus.В_обработке, notifyProcessingCheckBox.Checked);

            notifyCompletedCheckBox = new CheckBox
            {
                Text = "Уведомлять: Завершён",
                Location = new System.Drawing.Point(200, 96),
                Width = 180,
                Checked = orderManager.IsNotificationEnabled(OrderStatus.Завершён)
            };
            notifyCompletedCheckBox.CheckedChanged += (s, e) =>
                orderManager.SetNotificationEnabled(OrderStatus.Завершён, notifyCompletedCheckBox.Checked);

            ordersListBox = new ListBox { Location = new System.Drawing.Point(10, 124), Width = 760, Height = 280 };

            addOrderButton.Click += addOrderButton_Click;
            removeOrderButton.Click += removeOrderButton_Click;
            updateStatusButton.Click += updateStatusButton_Click;

            Controls.Add(customerNameLabel);
            Controls.Add(descriptionLabel);
            Controls.Add(dateLabel);
            Controls.Add(customerNameTextBox);
            Controls.Add(descriptionTextBox);
            Controls.Add(creationDatePicker);
            Controls.Add(addOrderButton);
            Controls.Add(removeOrderButton);
            Controls.Add(updateStatusButton);
            Controls.Add(statusComboBox);
            Controls.Add(notifyProcessingCheckBox);
            Controls.Add(notifyCompletedCheckBox);
            Controls.Add(ordersListBox);

            RefreshOrdersList();
        }

        internal void addOrderButton_Click(object sender, EventArgs e)
        {
            string customerName = customerNameTextBox.Text.Trim();
            string description = descriptionTextBox.Text;

            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Введите имя клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (customerName.Contains("|"))
            {
                MessageBox.Show("Введите корректное имя клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (description.Contains("|"))
            {
                MessageBox.Show("Введите корректное описание", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var order = new Order(customerName, description, creationDatePicker.Value);
            orderManager.AddOrder(order);
            RefreshOrdersList();

            customerNameTextBox.Clear();
            descriptionTextBox.Clear();
        }

        internal void removeOrderButton_Click(object sender, EventArgs e)
        {
            if (ordersListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var order = orderManager.Orders[ordersListBox.SelectedIndex];
            orderManager.RemoveOrder(order);
            RefreshOrdersList();
        }

        internal void updateStatusButton_Click(object sender, EventArgs e)
        {
            if (ordersListBox.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var order = orderManager.Orders[ordersListBox.SelectedIndex];
            var newStatus = (OrderStatus)statusComboBox.SelectedItem;
            orderManager.UpdateOrderStatus(order, newStatus);

            // уведомление о статусе
            string notification = orderManager.GetStatusNotification(order, newStatus);
            if (notification != null)
            {
                MessageBox.Show(notification, "Уведомление о статусе заказа",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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