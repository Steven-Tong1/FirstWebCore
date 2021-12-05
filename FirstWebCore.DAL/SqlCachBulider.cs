using FirstWebCore.Framework.Uility;
using FirstWebCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstWebCore.DAL
{
  /// <summary>
  /// 泛型缓存实例
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /*
   * 泛型类T参数不同时会动态生成一个新的类，所以所有的的静态字段还有构造函数会自动获取一次
   */
  public class SqlCachBulider<T> where T : new()
  {
    private static string _InsertSql = "";
    private static string _SelectSql = "";
    private static string _UpdateSql = "";
    private static string _DeleteSql = "";

    static SqlCachBulider()
    {
      Type type = typeof(T);

      //SelectSql

      var selectColunmString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}] as [{p.Name}]"));
      _SelectSql = $@"select {selectColunmString} from [{type.GetMappingName()}] where id=@id";


      //需要解决sql注入不然就完蛋
      //1.数据转换 2.参数化 3.orm
      var columnString = string.Join(",", type.GetPropWithNoKey().Select(x => $"[{x.GetMappingName()}]"));
      //var valueString = string.Join(",",type.GetPropWithNoKey().Select(p=>$"'{p.GetValue(t)}'"));
      var valueString = string.Join(",", type.GetPropWithNoKey().Select(p => $"@{p.GetMappingName()}"));

      _InsertSql = $@"Insert Into [{type.GetMappingName()}]({columnString}) Values ({valueString})";
    }

    /// <summary>
    /// 获取查询缓存
    /// </summary>
    /// <param name="sqlType"></param>
    /// <returns></returns>
    public static string GetSql(SqlTypeEnum sqlType)
    {
      switch (sqlType)
      {
        case SqlTypeEnum.select:
          return _SelectSql;
        case SqlTypeEnum.insert:
          return _InsertSql;
            case SqlTypeEnum.update:
          return _UpdateSql;
        case SqlTypeEnum.delete:
          return _DeleteSql;
        default:
          return "Unkkown";
      }
    }

  }
}
