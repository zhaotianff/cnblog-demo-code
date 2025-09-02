using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerkeleyDB;

namespace BDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BTreeDatabaseConfig bTreeDatabaseConfig = new BTreeDatabaseConfig();
            //文件不存在则创建
            bTreeDatabaseConfig.Creation = CreatePolicy.IF_NEEDED;
            //页大小
            bTreeDatabaseConfig.PageSize = 512;
            //缓存大小
            bTreeDatabaseConfig.CacheSize = new CacheInfo(0, 64 * 1024, 1);
            BTreeDatabase bTreeDatabase = BTreeDatabase.Open("demo.db", bTreeDatabaseConfig);
            string content = "HelloWorld";
            DatabaseEntry key = new DatabaseEntry(BitConverter.GetBytes(1));
            DatabaseEntry value = new DatabaseEntry(Encoding.ASCII.GetBytes(content));          
            bTreeDatabase.Put(key, value);
            Console.WriteLine("写入成功");
            KeyValuePair<DatabaseEntry, DatabaseEntry> pair = bTreeDatabase.Get(key);
            Console.WriteLine("读取写入");
            Console.WriteLine(Encoding.ASCII.GetString(pair.Value.Data));
            bTreeDatabase.Close();
        }
    }
}
