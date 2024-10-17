using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace UtilityLibrary
{
    public static class DBPropertyUtil
    {
        private static IConfigurationRoot _configuration;

        static DBPropertyUtil()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("C:\\Users\\vkaks\\source\\repos\\Career Hub Coding Challenge\\Utility\\appsettings.json", optional: false, reloadOnChange: true);

                _configuration = builder.Build();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                throw; // Rethrow the exception to indicate failure
            }
        }

        public static string ReturnCn(string key)
        {
            return _configuration.GetConnectionString(key);
        }
    }
}
