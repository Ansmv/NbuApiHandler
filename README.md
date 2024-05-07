# Currency Data API Handler

The Currency Data API Handler is a windows service designed to retrieve currency prices from a [National Bank of Ukraine API](https://bank.gov.ua/en/open-data/api-dev). 
It retrieves prices every specified amount of seconds and saves it to the file in a desired format (csv, xml or json).

## Installation

To install the Currency Data API Handler, follow these steps:

0. Make sure you are using Windows and .NET Core 8 SDK is installed. If not, you could install it [here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
1. Clone the repository to your local machine.
2. Open the solution in your preferred development environment (e.g., Visual Studio).
3. Build the solution to ensure all dependencies are resolved.
4. Optionally, configure the `appsettings.json` file according to your preferences.
5. Open power shell with administrator rights, and add our service with the following command:  
`sc.exe create NbuHandler binpath= \PUT_YOUR_PATH_HERE\CurrencyDataWorkerService.exe start= auto`  
Should return the following message: `[SC] CreateService SUCCESS`.
6. Press Windows + R, type services.msc in Run dialog, and hit Enter key to open it. Find NbuHandler and click start.

### Delete service

1. First stop the service by navigating to services and clicking stop.
2. Open power shell with administrator rights, and add our service with the following command:  
`sc.exe delete NbuHandler`  
Should return the following message: `[SC] DeleteService SUCCESS`.

## Configuration

The Currency Data API Handler uses a configuration file (`appsettings.json`) to customize its behavior. Here are the available configuration options:

- **AppSettings**: General application settings.
  - `DelayInSeconds`: The time, in seconds, between two calls to the api and therefore saving to the file are done.
  - `DataFormat`: The desired format for saving the currency data (csv, json, xml).
  - `DataStoragePath`: You can chose both relative to the exe path as well as an absolute path.
If the folder does not exist the app will create it if it has the rights.
  - "DataSaverType": "SingleFile" - to create new file every fetch or  
                     "MultipleFiles" - to update existing one

- **CurrencyApiSettings**: Configuration related to the external Currency API.
  - `ApiUrl`: The base URL of the Currency API (Currently has a support for only the NBU API).

Ensure that you provide valid values for these settings before running the program.

## Contributing

I do not welcome contributions to the Currency Data API Handler! If you'd like to contribute, please dont:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and ensure that tests pass.
4. Submit a pull request with a clear description of your changes.
5. Do not do this, I do not welcome contributors.

## License

This project is licensed under the [MIT License](LICENSE), which means you are free to use, modify, and distribute the code as you see fit. See the LICENSE file for more details.

## Contact

If you have any questions, feedback, or issues with the Currency Data API Handler, please don't hesitate to contact me at [ansmv125@gamil.com](mailto:ansmv125@gamil.com).
