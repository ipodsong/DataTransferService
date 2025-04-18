namespace DataTransferService.Configs
{
    public class AppSettings
    {
        #region Fields
        private static string? _applicationMode;
        #endregion

        #region Properties
        public static string? ApplicationMode { get => _applicationMode; }
        #endregion

        public AppSettings(IConfigurationRoot config)
        {
            _applicationMode = config.GetSection("ApplicationMode").Get<string>();
        }
    }
}
