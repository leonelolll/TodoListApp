using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: PackageRenamer <OldPackageName> <NewPackageName>");
            return;
        }

        string oldPackageName = args[0];
        string newPackageName = args[1];

        string basePath = "android/app/src/main/java";
        string oldPackagePath = Path.Combine(basePath, oldPackageName.Replace('.', Path.DirectorySeparatorChar));

        // Dynamically generate the paths for MainActivity and MainApplication based on the old package name
        string mainActivityPath = Path.Combine(oldPackagePath, "MainActivity.kt");
        string mainApplicationPath = Path.Combine(oldPackagePath, "MainApplication.kt");

        string[] filesToUpdate = {
            "android/app/src/main/AndroidManifest.xml",
            "android/app/build.gradle",
            mainActivityPath,
            mainApplicationPath
            // Add more file paths as needed
        };

        foreach (string filePath in filesToUpdate)
        {
            UpdateFileContent(filePath, oldPackageName, newPackageName);
        }

        string buildGradlePath = "android/app/build.gradle";
        if (File.Exists(buildGradlePath))
        {
            UpdateFileContent(buildGradlePath, $"applicationId \"{oldPackageName}\"", $"applicationId \"{newPackageName}\"");
        }

        string buckFilePath = "android/app/BUCK";
        if (File.Exists(buckFilePath))
        {
            UpdateFileContent(buckFilePath, $"package = \"{oldPackageName}\"", $"package = \"{newPackageName}\"");
        }

        string newPackagePath = Path.Combine("android/app/src/main/java", newPackageName.Replace('.', Path.DirectorySeparatorChar));

        CreateDirectories(newPackagePath);

        MoveFiles(oldPackagePath, newPackagePath);

        DeleteDirectory(oldPackagePath);
    }

    static void UpdateFileContent(string filePath, string oldContent, string newContent)
    {
        if (File.Exists(filePath))
        {
            string fileContent = File.ReadAllText(filePath);
            string updatedContent = fileContent.Replace(oldContent, newContent);
            File.WriteAllText(filePath, updatedContent);
            Console.WriteLine($"Updated content in {filePath}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }
    }

    static void CreateDirectories(string dirPath)
    {
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
            Console.WriteLine($"Created directory {dirPath}");
        }
    }

    static void MoveFiles(string oldPath, string newPath)
    {
        if (Directory.Exists(oldPath))
        {
            Directory.CreateDirectory(newPath);
            foreach (var file in Directory.GetFiles(oldPath))
            {
                string fileName = Path.GetFileName(file);
                string newFilePath = Path.Combine(newPath, fileName);
                File.Move(file, newFilePath);
                Console.WriteLine($"Moved {file} to {newFilePath}");
            }
        }
        else
        {
            Console.WriteLine($"Old path not found: {oldPath}");
        }
    }

    static void DeleteDirectory(string dirPath)
    {
        if (Directory.Exists(dirPath))
        {
            foreach (var file in Directory.GetFiles(dirPath))
            {
                File.Delete(file);
            }
            foreach (var directory in Directory.GetDirectories(dirPath))
            {
                DeleteDirectory(directory);
            }
            Directory.Delete(dirPath);
            Console.WriteLine($"Deleted directory {dirPath}");
        }
        else
        {
            Console.WriteLine($"Directory not found: {dirPath}");
        }
    }
}
