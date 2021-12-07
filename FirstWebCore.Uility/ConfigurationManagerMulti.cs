using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace FirstWebCore.Framework
{
    public class ConfigurationManagerMulti
    {
        //1.需要读取配置文件的json链接sql的链接字符串
        //固定读取根目录下的appsetting.json
        //写库（主库）链接
        private static string _SqlConnectionStringCustomWrite = null;

        //读库（从库）链接
        private static string [] _SqlConnectionStringCustomRead = null;

        /*
         * 需要注意添加三个程序包
         * 1.Microsoft.Extensions.Configuration
         * 2.Microsoft.Extensions.Configuration.FileExtensions
         * 3.Microsoft.Extensions.Configuration.Json
         * 
         * 后面才能使用SetBasePath、以及AddJsonFiel()方法
         * 然后通过IConfigurationRoot 获取对应的builder.Build() ,这样就可以获取配置文件里面的根目录节点
         * 然后通过单例进行获取链接字符串
         */
        static ConfigurationManagerMulti()
        {
            //使用静态的构造函数进行缓存
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            _SqlConnectionStringCustomWrite = configuration["ConnectionString:Write"];
            _SqlConnectionStringCustomRead = configuration.GetSection("ConnectionString").GetSection("Read").GetChildren().Select(x=>x.Value).ToArray(); //获取多个节点中的数组
        }

        public static string SqlConnectionStringCustomWrite { get { return _SqlConnectionStringCustomWrite; } }

        public static string [] SqlConnectionStringCustomRead { get { return _SqlConnectionStringCustomRead; } }
    }
}
