using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // Models
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<string> Products { get; set; } = new List<string>();
        public string CurrentStatus { get; set; }
        public Stack<string> StatusHistory { get; set; } = new Stack<string>();
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }

    // Collections
    static List<Order> orders = new List<Order>();
    static Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
    static HashSet<string> categories = new HashSet<string>();
    static Queue<Order> orderQueue = new Queue<Order>();

    static void Main()
    {
        // Sample Customers
        customers[1] = new Customer { CustomerId = 1, Name = "Amit" };
        customers[2] = new Customer { CustomerId = 2, Name = "Pandey" };

        // Add Categories (HashSet ensures uniqueness)
        AddCategory("Electronics");
        AddCategory("Books");
 

        // Add Orders
        AddOrder(new Order { OrderId = 101, CustomerId = 1, Products = new List<string> { "Laptop" } });
        AddOrder(new Order { OrderId = 102, CustomerId = 2, Products = new List<string> { "Book" } });

        // Update Order
        UpdateOrderStatus(101, "Shipped");

        // Process Orders (FIFO)
        ProcessOrders();

        // Print Status History
        PrintOrderHistory(101);

        // Remove Order
        RemoveOrder(102);
    }

    // Add Order
    static void AddOrder(Order order)
    {
        order.CurrentStatus = "Created";
        order.StatusHistory.Push("Created");

        orders.Add(order);
        orderQueue.Enqueue(order);

        Console.WriteLine($"Order {order.OrderId} added.");
    }

    // Update Order Status
    static void UpdateOrderStatus(int orderId, string newStatus)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            order.CurrentStatus = newStatus;
            order.StatusHistory.Push(newStatus);

            Console.WriteLine($"Order {orderId} updated to {newStatus}.");
        }
    }

    // Remove Order
    static void RemoveOrder(int orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            orders.Remove(order);
            Console.WriteLine($"Order {orderId} removed.");
        }
    }

    // Process Orders (FIFO)
    static void ProcessOrders()
    {
        Console.WriteLine("\nProcessing Orders:");
        while (orderQueue.Count > 0)
        {
            var order = orderQueue.Dequeue();
            Console.WriteLine($"Processing Order {order.OrderId}");

            UpdateOrderStatus(order.OrderId, "Processed");
        }
    }

    // Track Status History (LIFO)
    static void PrintOrderHistory(int orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);

        if (order != null)
        {
            Console.WriteLine($"\nOrder {orderId} Status History:");
            foreach (var status in order.StatusHistory)
            {
                Console.WriteLine(status);
            }
        }
    }

    // Add Category (HashSet ensures uniqueness)
    static void AddCategory(string category)
    {
        if (categories.Add(category))
            Console.WriteLine($"Category '{category}' added.");
    }
}
