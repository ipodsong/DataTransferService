namespace DataTransferService.Configs
{
    /// <summary>
    /// Provides access to application-wide settings loaded from configuration.
    /// </summary>
    public class AppSettings
    {
        #region Fields

        /// <summary>
        /// Holds the current application mode (e.g., Dev, Prod).
        /// </summary>
        private static string? _applicationMode;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the application mode set in configuration.
        /// </summary>
        public static string? ApplicationMode => _applicationMode;

        #endregion

        /// <summary>
        /// Initializes the AppSettings class and loads configuration values.
        /// </summary>
        /// <param name="config">The root configuration from which to retrieve settings.</param>
        public AppSettings(IConfigurationRoot config)
        {
            _applicationMode = config.GetSection("ApplicationMode").Get<string>();
        }
    }
}
