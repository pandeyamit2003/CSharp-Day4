using System;
using System.Collections.Generic;

// Client class
class Client
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Client(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

// Purchase class
class Purchase
{
    public int PurchaseId { get; set; }
    public int ClientId { get; set; }
    public string Category { get; set; }

    public Stack<string> StatusTrack { get; set; }

    public Purchase(int pid, int cid, string category)
    {
        PurchaseId = pid;
        ClientId = cid;
        Category = category;
        StatusTrack = new Stack<string>();
    }

    public void AddStatus(string status)
    {
        StatusTrack.Push(status);
    }
}

class Program
{
    static void Main()
    {
        // Collections
        List<Purchase> purchases = new List<Purchase>();
        Dictionary<int, Client> clients = new Dictionary<int, Client>();
        HashSet<string> categories = new HashSet<string>();
        Queue<Purchase> queue = new Queue<Purchase>();

        // Add clients
        clients.Add(1, new Client(1, "Arjun"));
        clients.Add(2, new Client(2, "Ravi"));

        // Add purchases
        Purchase p1 = new Purchase(101, 1, "Electronics");
        p1.AddStatus("Created");

        Purchase p2 = new Purchase(102, 2, "Clothing");
        p2.AddStatus("Created");

        purchases.Add(p1);
        purchases.Add(p2);

        // Add categories
        foreach (var p in purchases)
        {
            categories.Add(p.Category);
        }

        // Update purchase
        p2.AddStatus("Packed");

        // Remove purchase
        purchases.Remove(p1);

        // Add to queue
        foreach (var p in purchases)
        {
            queue.Enqueue(p);
        }

        // Process purchases
        Console.WriteLine("Processing Purchases:");
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            current.AddStatus("Processed");

            Console.WriteLine($"Purchase {current.PurchaseId} processed for {clients[current.ClientId].Name}");
        }

        // Show status
        Console.WriteLine("\nStatus History:");
        foreach (var p in purchases)
        {
            Console.WriteLine($"Purchase {p.PurchaseId}:");
            foreach (var s in p.StatusTrack)
            {
                Console.WriteLine($" - {s}");
            }
        }

        // Show categories
        Console.WriteLine("\nUnique Categories:");
        foreach (var c in categories)
        {
            Console.WriteLine(c);
        }
    }
}
