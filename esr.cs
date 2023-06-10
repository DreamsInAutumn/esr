using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

[Comment:main](https://marketsplash.com/tutorials/html/html-anchor/#link1)

class Program {
    static void Main() {
        Console.WriteLine("Autumn's Epic Loader...\n");
        string configFileName = "config.txt";
        string appName = ReadConfigurationValue(configFileName, "AppTitle");
        string executableName = ReadConfigurationValue(configFileName, "ExecutableName");
        string workingDirectory = ReadConfigurationValue(configFileName, "WorkingDirectory");        
        string commandLineArgument = ReadConfigurationValue(configFileName, "CommandLineArgument");

        Console.WriteLine($"\nSetting up for App: {appName}\n");

        SetApplicationTitle(appName);
        ChangeWorkingDirectory(workingDirectory);

        ExecuteExternalExecutable(executableName, commandLineArgument);

        Console.WriteLine("Press any key to exit, ** After you finish playing **");
        Console.ReadKey();

    }

static string ReadConfigurationValue(string fileName, string key) {
    string line;
    using (StreamReader file = new StreamReader(fileName)) {
        while ((line = file.ReadLine()) != null) {
            string[] parts = line.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && parts[0].Trim().Equals(key, StringComparison.OrdinalIgnoreCase)) {
                string value = parts[1].Trim();
                Console.WriteLine($"Found value for {key}: {value}");
                return value;
            }
        }
    }
    return null;
}

    static int ReadConfigurationValueAsInt(string fileName, string key) {
        int value;
        string stringValue = ReadConfigurationValue(fileName, key);
        int.TryParse(stringValue, out value);
        return value;
    }

    static void SetApplicationTitle(string title) {
        Console.Title = title;
    }

    static void ChangeWorkingDirectory(string directory) {
        Directory.SetCurrentDirectory(directory);
    }

    static void ExecuteExternalExecutable(string executableName, string commandLineArgument) {
        Console.WriteLine($"Command line argument: {commandLineArgument}");

        if (string.IsNullOrEmpty(commandLineArgument)) {
            Console.WriteLine("Warning: Command line argument is empty.");
        }

        ProcessStartInfo startInfo = new ProcessStartInfo {
            FileName = executableName,
            Arguments = commandLineArgument,
            UseShellExecute = false,
            RedirectStandardInput = true
        };

        Process process = new Process();
        process.StartInfo = startInfo;
        process.EnableRaisingEvents = true;
        process.Start();
    }
}
