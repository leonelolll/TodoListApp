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
        string oldPackagePath = basePath + "/" + oldPackageName.Replace('.', '/');

        // Dynamically generate the paths for MainActivity and MainApplication based on the old package name
        string mainActivityPath = oldPackagePath + "/" + "MainActivity.kt";
        string mainApplicationPath = oldPackagePath + "/" + "MainApplication.kt";

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

        string buckFilePath = "android/app/BUCK";
        if (File.Exists(buckFilePath))
        {
            UpdateFileContent(buckFilePath, $"package = \"{oldPackageName}\"", $"package = \"{newPackageName}\"");
        }

        string newPackagePath = "android/app/src/main/java" + "/" + newPackageName.Replace('.', '/');

        CreateDirectories(newPackagePath);

        MoveFiles(oldPackagePath, newPackagePath);

        DeleteDirectory(oldPackagePath);

        DeleteEmptyDirectories(basePath);
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
                string newFilePath = newPath + "/" + fileName;
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

    static void DeleteEmptyDirectories(string dirPath)
    {
        // Ensure the directory exists before attempting to process it
        if (Directory.Exists(dirPath))
        {
            // Get all subdirectories in the current directory
            var subdirectories = Directory.GetDirectories(dirPath);

            foreach (var subdirectory in subdirectories)
            {
                // Recursively delete empty subdirectories in each subdirectory
                DeleteEmptyDirectories(subdirectory);

                // Check if the current subdirectory is empty
                if (Directory.GetFiles(subdirectory).Length == 0 && 
                    Directory.GetDirectories(subdirectory).Length == 0)
                {
                    try
                    {
                        // Delete the empty subdirectory
                        Directory.Delete(subdirectory);
                        Console.WriteLine($"Deleted empty directory: {subdirectory}");
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions, such as permission issues or file locks
                        Console.WriteLine($"Error deleting directory: {subdirectory}. {ex.Message}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"Directory not found: {dirPath}");
        }
    }
}
