using MauiAppProjeto2.Helper;

namespace MauiAppProjeto2
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper database;

        
        public static SQLiteDatabaseHelper Database
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteDatabaseHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "MauiAppProjeto2.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.LoginPage());
        }
    }
}
