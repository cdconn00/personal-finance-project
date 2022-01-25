namespace pfp_api
{
    public class AppSettings
    {
        private static AppSettings? _appSettings;

        public string ConnectString { get; set; }

        public AppSettings(IConfiguration config)
        {
            this.ConnectString = config.GetValue<string>("ConnectString");

            // Now set Current
            _appSettings = this;
        }

        public static AppSettings Current
        {
            get
            {
                if (_appSettings == null)
                {
                    _appSettings = GetCurrentSettings();
                }

                return _appSettings;
            }
        }

        public static AppSettings GetCurrentSettings()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddEnvironmentVariables();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                builder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
            }
            else
            {
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            }

            IConfigurationRoot configuration = builder.Build();

            var settings = new AppSettings(configuration);

            return settings;
        }
    }
}
