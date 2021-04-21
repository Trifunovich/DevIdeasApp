using LoggingLibrary.DiResolver;

namespace LoggingLibrary
{
  public static class LoggingManager
  {
    public static void CreateNewLoggingFile(string path)
    {
      ServiceCollectionExtension.AddNewFileToTheCurrentLogger(path);
    }
  }
}