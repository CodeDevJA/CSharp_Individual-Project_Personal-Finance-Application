using System;
using System.IO;

namespace CSharp_Individual_Project_Personal_Finance_Application_main;

public class FileManager
{
  public void SaveToFile(string filePath, string data)
  {
    File.WriteAllText(filePath, data);
  }

  public string ReadFromFile(string filePath)
  {
    if (File.Exists(filePath))
    {
      return File.ReadAllText(filePath);
    }

    return string.Empty;
  }
}
