using FirstWebCore.Framework;
using FirstWebCore.Framework.Uility;
using FirstWebCore.Model;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace FirstWebCore.DAL
{
  public class SqlHelper
  {
    private static string connectionString = ConfigurationManager.SqlConnectionStringCustom;

    //先写一个查询的通过反射进行动态拼接字符串实现查找
    //1.where T:BaseModel 基于泛型约束

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public T Find<T>(int id) where T : BaseModel, new()
    {
      //反射三部曲
      Type type = typeof(T);
      var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.insert);
      SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Id", id) };
      //ado操作数据库
      //1.创建链接
      using (SqlConnection conn = new SqlConnection(connectionString))
      {
        try
        {
          //2.创建command 传入 sql跟链接conn
          SqlCommand cmd = new SqlCommand(sql,conn);
          cmd.Parameters.AddRange(parameters);
          //3.打开链接
          conn.Open();
          var read = cmd.ExecuteReader();
          if (read.Read())
          {
            //如果获取到了对应的数据
            T t = Activator.CreateInstance<T>();

            //需要给对应的对象进行赋值？
            //通过反射然后去循环赋值
            foreach (var prop in type.GetProperties())
            {
              //循环遍历所有的属性
              //反射属性赋值 SetValue 给对象赋值
              //var propName = prop.GetMappingColumnName();

              prop.SetValue(t,read[prop.Name] is DBNull? null:read[prop.Name]); //思考如果是数据库中有数据为NULL，需要判断null，   
            }

            //最后输出
            return t;
          }
        }
        catch (Exception e)
        {
          //异常就返回null
          return null;
        }
        finally
        {
          conn.Close();
        }
      }

      return null;
    }

    public bool Insert<T>(T t) where T:BaseModel,new()
    {
      Type type = typeof(T);
      var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.insert);//注意调用的时候，调用顺序 静态字段>构造函数>静态方法
      var parameters = type.GetPropWithNoKey().Select(p => new SqlParameter($"@{p.GetMappingName()}", $"'{p.GetValue(t) ?? DBNull.Value}'")).ToArray();
      using (SqlConnection conn = new SqlConnection(connectionString))
      {
        try
        {
          SqlCommand cmd = new SqlCommand(sql, conn);
          cmd.Parameters.AddRange(parameters);
          conn.Open();
          int row = cmd.ExecuteNonQuery();
          return row > 0;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
          return false;
        }
        finally
        {
          conn.Close();
        }
      }
    }
  }
}
