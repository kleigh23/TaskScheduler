using System;
using System.Collections.Generic;
using System.Timers;

class TaskScheduler
{
    private static System.Timers.Timer timer = null!;
    private static List<ScheduledTask> tasks = new List<ScheduledTask>();

    static void Main()
    {
        timer = new System.Timers.Timer(60000); // Check every minute
        timer.Elapsed += CheckTasks;
        timer.Start();

        Console.WriteLine("Task Scheduler Started. Type 'exit' to quit.");
        while (true)
        {
            Console.Write("Enter task description (or 'exit' to quit): ");
            string? description = Console.ReadLine();
            if (description?.ToLower() == "exit") break;

            if (!string.IsNullOrWhiteSpace(description))
            {
                Console.Write("Enter task date and time (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime taskTime))
                {
                    tasks.Add(new ScheduledTask(description, taskTime));
                    Console.WriteLine("Task added successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }
        }
    }

    private static void CheckTasks(object? sender, ElapsedEventArgs e)
    {
        DateTime now = DateTime.Now;
        foreach (var task in tasks)
        {
            if (!task.Notified && task.TaskTime <= now)
            {
                Console.WriteLine($"Reminder: {task.Description} at {task.TaskTime}");
                task.Notified = true;
            }
        }
    }
}

class ScheduledTask
{
    public string Description { get; }
    public DateTime TaskTime { get; }
    public bool Notified { get; set; }

    public ScheduledTask(string description, DateTime taskTime)
    {
        Description = description;
        TaskTime = taskTime;
        Notified = false;
    }
}
