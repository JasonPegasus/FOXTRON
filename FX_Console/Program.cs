using FX_Console;
using FX_Core;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;


internal class Program
{
    private static void Main(string[] args)
    {
        ConsUtils.print("[ F O X T R O N ]", ConsoleColor.DarkRed);
        ConsUtils.empty();
        ConsUtils.print("Welcome to the console application for FOXTRON.", ConsUtils.titleColor);
        ConsUtils.print("You can always type 'help' to get some useful info.", ConsUtils.subtitleColor);
        awaitCommand();
    }

    static void awaitCommand()
    {
        while (true)
        {
            try
            {
                switch (Console.ReadLine().ToLower())
                {
                    case "help": help(); break;
                    case "attach": tryAttach(); break;
                    case "detach": tryDetach(); break;
                    case "find": findCommand(); break;
                    default: break;
                }
            }
            catch (Exception e) { ConsUtils.print(e.Message, ConsUtils.programError); }
        }
    }

    static void findCommand()
    {
        foreach (IntPtr ptr in Core.scanner.Find360())
        {
            ConsUtils.print($"Ptr: {ptr} | Value: {Core.scanner.Memory().ReadFloat(ptr)}");
        }
    }

    static void help()
    {
        ConsUtils.print("A little Help: ", ConsUtils.infoTitleColor);
        ConsUtils.print("Type 'Attach' to get a list of available processes to attach to.", ConsUtils.infoColor);
    }

    static void tryAttach()
    {
        if (Core.isAttached()) 
        { ConsUtils.print("A process is already attached! Try typing 'Detach' before attaching something else", ConsUtils.userError); return; }

        try
        {
            Process proc = Core.Attach(processSelection()).Process();
            ConsUtils.print("Succesfully Attached!", ConsUtils.successColor);
            ConsUtils.print($"   {proc.ProcessName} ({proc.Id})", ConsUtils.successSubColor);
        }
        catch (Exception e)
        { throw new Exception("- Error on Program.tryAttach():\r\n" + e.Message); }
    }

    static void tryDetach()
    {
        if (Core.Detach())
        {
            ConsUtils.print("Succesfully Detached!", ConsUtils.successColor);
        }
        else
        {
            ConsUtils.print("Process was already detached.", ConsUtils.userError);
        }
    }

    static Process processSelection()
    {
        Console.Clear();
        Process[] processList = printProcessList();
        ConsUtils.print("Type the index of the process you want to attach to...", ConsUtils.infoTitleColor);

        int index = ConsUtils.askIntRanged(0, processList.Length-1);
        Process proc = processList[index];

        if (proc == null) 
        {
            ConsUtils.print("Process no longer exists! Try again.");
            return null;
        }
        
        return proc;
    }


    static Process[] printProcessList()
    {
        ConsUtils.print("Process List:", ConsUtils.titleColor);
        Process[] processList = ProcessManager.getUserProcesses();

        for (int i = 0; i < processList.Length; i++)
        {
            Process proc = processList[i];
            ConsUtils.print($"{i,3}  | PID {proc.Id,-6} | {proc.ProcessName}", ConsoleColor.DarkGray);
        }

        return processList;
    }
}