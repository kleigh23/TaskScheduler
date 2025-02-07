using System;
using System.Collections.Generic;
using System.Timers;

class TaskScheduler
{
    // This is the time.
    private static System.Timers.Timer timer = null!;
    // This is the list that is remembering all the tasks.
    private static List<ScheduledTask> tasks = new List<ScheduledTask>();

    static void Main()
    {
        timer = new System.Timers.Timer(60000); // Check every minute.
        timer.Elapsed += CheckTasks;
        timer.Start();

        // This is what  is being sent to the command line/user and is the beginning of the loop.
        Console.WriteLine("Task Scheduler Started. Type 'exit' to quit.");
        while (true)
        {   
            // This is the prompt that is given at the beginning or after task has been entered.
            Console.Write("Enter task description (or 'exit' to quit): ");

            // This is checking to read what the user has entered and to check if they typed exit if not what to do next
            string? description = Console.ReadLine();
            if (description?.ToLower() == "exit") break;

            if (!string.IsNullOrWhiteSpace(description))
            {
                // This shows them how to enter their reminder for their task
                Console.Write("Enter task date and time (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime taskTime))
                {
                    // Telling the user its been added
                    tasks.Add(new ScheduledTask(description, taskTime));
                    Console.WriteLine("Task added successfully!");
                }
                else
                {
                    // Telling the user that what they entered wasn't right 
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            }
        }
    }

    // This is the function that checks to see if the time has come and if so to send the reminder to the user/commandline
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

// This is creating a class that holds the data the user entered.
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
