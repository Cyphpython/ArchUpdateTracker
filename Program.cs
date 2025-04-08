using System;
using System.Diagnostics;
using System.Linq;

class ArchTracker 
{
    static void Main(string[] args)
    {
        Console.WriteLine("Checking update for installed applications...\n");

        string updateCommand = "pacman -Qu";
        string output = RunCommand(updateCommand);

        if (string.IsNullOrEmpty(output))
        {
            Console.WriteLine("No update Found.");
            return;
        }

        Console.WriteLine("Here all the available update : \n");
        Console.WriteLine(output);

        Console.WriteLine("\nDo you want to update this/those app ? (O/n)");
        string answer = Console.ReadLine()?.ToLower(); //Check if not null and lower it in case of uppercase

        if (answer == "o")
        {
            Console.WriteLine("Updating...");
            string upgradeCommand = "sudo pacman -Syu";
            RunCommand(upgradeCommand);
            Console.WriteLine("Update finished !");
        }
        else
        {
            Console.WriteLine("Update canceled");
        }
    }

    static string RunCommand(string command)
    {
        ProcessStartInfo PSI = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{command}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(PSI))
        {
            using (System.IO.StreamReader reader = process.StandardOutput)
            {
                return reader.ReadToEnd();
            }
        } 
    }
}