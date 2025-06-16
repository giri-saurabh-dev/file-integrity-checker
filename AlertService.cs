using System;

public class AlertService
{
    public static void ShowAlert(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[ALERT] " + message);
        Console.ResetColor();
    }

    public static void ShowInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[INFO] " + message);
        Console.ResetColor();
    }
}
