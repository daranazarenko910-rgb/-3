using System;
using System.IO;
using System.Text.Json;

namespace Shop
{
    class Order
    {
        private readonly int id;
        private double amount;
        private string status;

        private static int count = 0;

        public double Amount
        {
            get { return amount; }
        }

        public Order()
        {
            count++;
            id = count;
            amount = 0;
            status = "Нове";
        }

        public Order(double amount)
        {
            count++;
            id = count;
            this.amount = amount;
            status = "Нове";
        }

        public void ChangeStatus(string newStatus)
        {
            status = newStatus;
        }

        public bool IsCompleted()
        {
            return status == "Завершене";
        }

        public static int GetOrderCount()
        {
            return count;
        }

        public void SaveToJson(string path)
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(path, json);
        }

        public static Order LoadFromJson(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Order>(json);
        }

        public override string ToString()
        {
            return $"Замовлення №{id}, Сума: {amount}, Статус: {status}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Order order1 = new Order();
            Order order2 = new Order(500);

            order1.ChangeStatus("В обробці");
            order2.ChangeStatus("Завершене");

            order2.SaveToJson("order.json");

            Order order3 = Order.LoadFromJson("order.json");

            Console.WriteLine(order1);
            Console.WriteLine(order2);
            Console.WriteLine(order3);

            Console.WriteLine("Кількість замовлень: " + Order.GetOrderCount());
        }
    }
}