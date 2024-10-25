using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    private static int threadCounter = 1;
    private static Semaphore semaphore;
    private static int semaphoreCount = 3;
    private static List<Thread> threads = new List<Thread>();

    static void Main(string[] args)
    {
        semaphore = new Semaphore(semaphoreCount, semaphoreCount);
        string command = "";

        Console.WriteLine("Semaphore Control Console\nCommands:");
        Console.WriteLine("'create' - Create and start a new thread");
        Console.WriteLine("'increase' - Increase the semaphore capacity");
        Console.WriteLine("'decrease' - Decrease the semaphore capacity");
        Console.WriteLine("'exit' - Exit the application\n");

        while (command != "exit")
        {
            Console.Write("Enter command: ");
            command = Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "create":
                    CreateThread();
                    break;
                case "increase":
                    IncreaseSemaphore();
                    break;
                case "decrease":
                    DecreaseSemaphore();
                    break;
                case "exit":
                    Console.WriteLine("Exiting the application...");
                    break;
                default:
                    Console.WriteLine("Invalid command! Use 'create', 'increase', 'decrease', or 'exit'.");
                    break;
            }
        }
    }

    private static void CreateThread()
    {
        Thread thread = new Thread(new ThreadStart(ThreadTask))
        {
            Name = $"Thread {threadCounter++}"
        };
        threads.Add(thread);
        Console.WriteLine($"{thread.Name} created and starting.");
        thread.Start();
    }

    private static void ThreadTask()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} is waiting to work...");
        semaphore.WaitOne();

        Console.WriteLine($"{Thread.CurrentThread.Name} has started working.");
        Thread.Sleep(2000); // Work duration example (2 seconds)

        Console.WriteLine($"{Thread.CurrentThread.Name} has completed work.");
        semaphore.Release();
    }

    private static void IncreaseSemaphore()
    {
        semaphoreCount++;
        semaphore = new Semaphore(semaphoreCount, semaphoreCount);
        Console.WriteLine($"Semaphore capacity increased. New capacity: {semaphoreCount}");
    }

    private static void DecreaseSemaphore()
    {
        if (semaphoreCount > 1)
        {
            semaphoreCount--;
            semaphore = new Semaphore(semaphoreCount, semaphoreCount);
            Console.WriteLine($"Semaphore capacity decreased. New capacity: {semaphoreCount}");
        }
        else
        {
            Console.WriteLine("Semaphore capacity must be greater than 1.");
        }
    }
}
