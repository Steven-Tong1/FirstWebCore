using System;
using System.Collections.Generic;
using System.Text;

namespace FirstWebCore.Framework.Uility
{
  ///// <summary>
  ///// 表明的属性(特性必须继承Attribute)
  ///// </summary>
  //[AttributeUsage(AttributeTargets.Class)]//指定作用只能是类才能使用
  //public class TableNameAttribute:Attribute
  //{
  //  private string _TableName = null;

  //  public TableNameAttribute(string tableName)
  //  {
  //    //构造函数注入进来
  //    this._TableName = tableName;
  //  }
  //  public string GetMappingName()
  //  {
  //    return this._TableName;
  //  }
  //}
  /// <summary>
  /// 表明的属性(特性必须继承Attribute)
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]//指定作用只能是类才能使用
  public class TableNameAttribute : DBMappingAttribute
  {
    public TableNameAttribute(string tableName):base(tableName)
    {
      
    }
  }
}
