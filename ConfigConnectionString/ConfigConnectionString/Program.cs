using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;

namespace ConfigConnectionString
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //输出 连接字符串

            var con1 = ConfigurationManager.ConnectionStrings["con1"].ConnectionString;
            var con2 = ConfigurationManager.ConnectionStrings["con2"].ConnectionString;

            Console.WriteLine(con1);
            Console.WriteLine(con2);

            MySqlConnectionStringBuilder mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder(con1);

            //输出
            Console.WriteLine(mySqlConnectionStringBuilder.Server); 
            Console.WriteLine(mySqlConnectionStringBuilder.Port);
            Console.WriteLine(mySqlConnectionStringBuilder.Database);
            Console.WriteLine(mySqlConnectionStringBuilder.UserID);
            Console.WriteLine(mySqlConnectionStringBuilder.Password);

            //更新
            mySqlConnectionStringBuilder.Server = "localhost";
            mySqlConnectionStringBuilder.Port = 10250;
            mySqlConnectionStringBuilder.Database = "ttt";
            mySqlConnectionStringBuilder.UserID = "testuser";
            mySqlConnectionStringBuilder.Password = "abc";

            //获取更新后的连接字符串
            var newCon = mySqlConnectionStringBuilder.ConnectionString;
            Console.WriteLine(newCon);

            //保存到当前程序配置文件
            //con1
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["con1"].ConnectionString = newCon;
            config.Save(ConfigurationSaveMode.Modified, false);
            ConfigurationManager.RefreshSection("connectionStrings");

            //con2
            EntityConnectionStringBuilder efb = new EntityConnectionStringBuilder(con2);
            efb.ProviderConnectionString = mySqlConnectionStringBuilder.ConnectionString;
            config.ConnectionStrings.ConnectionStrings["con2"].ConnectionString = efb.ConnectionString;
            config.Save(ConfigurationSaveMode.Modified, false);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}