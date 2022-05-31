using Python.Runtime;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Urls.Add("http://localhost:3000");

app.MapGet("/", () => "Hello World!");
app.MapGet("/yahoo/{ticker}", (string ticker) => PythonAccess.Instance.GetDataFromTicker(ticker));

app.Run();

public class PythonAccess
{
    private static PythonAccess? instance;
    public static PythonAccess Instance => instance ??= new PythonAccess();

    public PythonAccess()
    {
        string path = "C:\\Python310";

        Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("PYTHONHOME", path, EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("PYTHONPATH", $"{path}\\Lib\\site-packages;{path}\\Lib;{path}\\DLLs;{Environment.CurrentDirectory}", EnvironmentVariableTarget.Process);

        string dll = path + "\\python310";
        Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", dll);
        Runtime.PythonDLL = dll;

        PythonEngine.PythonPath = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);
        PythonEngine.Initialize();

        PythonEngine.BeginAllowThreads();

        PythonEngine.PythonHome = path;
    }

    public string GetDataFromTicker(string ticker)
    {
        string result = "err";

        using var _ = Py.GIL();

        dynamic htmlfilescraper = Py.Import("htmlfilescraper");

        Func<string, string> formatter = (ticker) => $"https://finance.yahoo.com/quote/{ticker}?p={ticker}";
        dynamic scraper = htmlfilescraper.get_scraper(formatter);

        dynamic data = scraper.get_data_from_ticker(ticker);

        result = data[0].to_csv();

        return result;
    }
}