namespace OrderDetailsMaintenance
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        // Opeyemi Obute
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            AppDomain.CurrentDomain.SetData("DataDirectory", AppContext.BaseDirectory);
            ApplicationConfiguration.Initialize();
            Application.Run(new frmCustomerMaintenance());
        }
    }
}
