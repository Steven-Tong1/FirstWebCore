using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FirstWebCore.Framework
{
  public class ConfigurationManager
  {
    //1.需要读取配置文件的json链接sql的链接字符串
    //固定读取根目录下的appsetting.json
    private static string _SqlConnectionStringCustom = null;

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
    static ConfigurationManager()
    {
      //使用静态的构造函数进行缓存
      var builder = new ConfigurationBuilder();
      builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
      IConfigurationRoot configuration = builder.Build();
      _SqlConnectionStringCustom = configuration["ConnectionStrings:Customers"];
    }

    public static string SqlConnectionStringCustom { get { return _SqlConnectionStringCustom; } }
  }
}
