using FirstWebCore.DAL;
using System;
using Zhaoxi.CustomORMDemo.Model;

namespace FirstWebCore.UI
{
  class Program
  {
    static void Main(string[] args)
    {
      SqlHelper helper = new SqlHelper();
      Console.WriteLine("**************ORM**************");
      {
        //实现泛型查询 orm
        var company = helper.Find<CompanyModel>(1);
        //orm 在映射、还有各种高阶特性缓存，延迟，事务等
        var user = helper.Find<UserModel>(1);
      }
      {
        
        var company = helper.Find<CompanyModel>(1);
        company.Name = "TestName";
        company.LastModifyTime = DateTime.Now;
        helper.Insert<CompanyModel>(company);

        //orm 在映射、还有各种高阶特性缓存，延迟，事务等
        var user = helper.Find<UserModel>(1);
        user.Name = "TestName";
        user.LastModifyTime = DateTime.Now;
        helper.Insert<UserModel>(user);

      }
    }
  }
}
