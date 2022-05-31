# PythonMinimalAPI
A proof-of-concept for Python to .NET Minimal API

Uses Python.NET to load a Python module and expose it as an API endpoint.

Dependent on [Python.NET](https://www.nuget.org/packages/pythonnet).

> **Warning**<br>
Current proof-of-concept has hard-coded values for Python installation. It is a single implementation to pull stock data from Yahoo Finance based on the ticker (Ex: http://localhost:3000/yahoo/MSFT). <br>
<br>Feel free to fork your own versions. The plan is to generalize this tool to work with any Python script as an interface, allowing a user to write as much or as little C# code as they desire. Example python script is dependent on [pandas](https://pypi.org/project/pandas) and [requests](https://pypi.org/project/requests).
