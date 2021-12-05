using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FirstWebCore.Framework.Uility
{
  public static class DBExtendMapping
  {
    //需要判断TableNameAttribute 特性标识对应的表名
    public static string GetMappingTalbeName(this Type type) //使用扩展方法后直接可以用type.使用
    {
      //首先需要判断是否有包含特性 TableNameAttribute
      if (type.IsDefined(typeof(TableNameAttribute), true))
      {
        //GetCustomAttribut 是需要引用命名空间 System.Reflection
        var attribute = type.GetCustomAttribute<TableNameAttribute>();
        return attribute.GetMappingName();
      }
      else
      {
        return type.Name;
      }
    }

    /// <summary>
    /// 通过 ColumnNameAttribute 特性来处理获取列名
    /// </summary>
    /// <param name="prop"></param>
    /// <returns></returns>
    public static string GetMappingColumnName(this PropertyInfo prop) //使用扩展方法后直接可以用type.使用
    {
      //首先需要判断是否有包含特性 TableNameAttribute
      if (prop.IsDefined(typeof(ColumnNameAttribute), true))
      {
        //GetCustomAttribut 是需要引用命名空间 System.Reflection
        var attribute = prop.GetCustomAttribute<ColumnNameAttribute>();
        return attribute.GetMappingName();
      }
      else
      {
        return prop.Name;
      }
    }

    /// <summary>
    /// 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public static string GetMappingName<T>(this T t) where T:MemberInfo
    {
      if (t.IsDefined(typeof(DBMappingAttribute), true))
      {
        //GetCustomAttribut 是需要引用命名空间 System.Reflection
        var attribute = t.GetCustomAttribute<DBMappingAttribute>();
        return attribute.GetMappingName();
      }
      else
      {
        return t.Name;
      }
    }
  }
}
