using System;
using System.Collections.Generic;
using System.Text;

namespace FirstWebCore.Framework.Uility
{
  ///// <summary>
  ///// 表明的属性(特性必须继承Attribute)
  ///// </summary>
  //[AttributeUsage(AttributeTargets.Property)]//指定作用只能是字段属性才能使用
  //public class ColumnNameAttribute : Attribute
  //{
  //  private string _ColumnName = null;

  //  public ColumnNameAttribute(string columnName)
  //  {
  //    //构造函数注入进来
  //    this._ColumnName = columnName;
  //  }
  //  public string GetMappingName()
  //  {
  //    return this._ColumnName;
  //  }
  //}
  /// <summary>
  /// 表明的属性(特性必须继承Attribute)
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]//指定作用只能是字段属性才能使用
  public class ColumnNameAttribute : DBMappingAttribute
  {
    
    public ColumnNameAttribute(string columnName):base(columnName)
    {

    }
   
  }
}
