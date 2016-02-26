using System;
using System.IO;
using Windows.Storage;
using MyChecklists.Infra;

namespace MyChecklists.StartUp
{
    public class DbConfig
    {
        public const String DB_NAME = "MyCheckliststest3.sqlite";

        public static String DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_NAME));

        public static void Init()
        {
            var dbHelper = new DatabaseHelperClass();
            if (!dbHelper.CreateIfNotExists(DB_PATH))
            {
                throw new InvalidDataException("Error on db create");
            }
        }
    }
}