using FirstWebCore.DAL;
using System;
using System.Linq.Expressions;
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

                //var company = helper.Find<CompanyModel>(1);
                //company.Name = "TestName";
                //company.CreateTime = DateTime.Now;
                //company.LastModifyTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //helper.Insert<CompanyModel>(company);

                ////orm 在映射、还有各种高阶特性缓存，延迟，事务等
                //var user = helper.Find<UserModel>(1);
                //user.Name = "TestName";
                //user.CreateTime = DateTime.Now;
                //user.LastModifyTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //helper.Insert<UserModel>(user);

                
            }
            {
                //Console.WriteLine("*********Update*********");
                //var user = helper.Find<UserModel>(6);
                //user.Name = " ";
                //var isOk = helper.Update<UserModel>(user);
                
                //Console.WriteLine($"*********Update {(isOk? "成功":" 失败")}*********");
            }
            {
                //Console.WriteLine("*********Delete*********");
                //var company = new CompanyModel();
                //company.Id = 11;
                //var isOk = helper.Delete<CompanyModel>(company);

                //Console.WriteLine($"*********Delete {(isOk ? "成功" : " 失败")}*********");
            }
            {
                //Expression<Func<CompanyModel, bool>> expression = a => a.Id == 10 && (a.Name.Contains("11") || a.Name != "");
                //helper.DeleteCondition<CompanyModel>(expression);

                Expression<Func<CompanyModel,bool>> expressionFind = a => a.Id == 1 && (a.Name.Contains("11") || a.Name != "");
                var company = helper.FindCondition<CompanyModel>(expressionFind);
            }
        }
    }
}
