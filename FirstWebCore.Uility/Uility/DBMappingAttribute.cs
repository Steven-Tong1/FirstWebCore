using System;
using System.Collections.Generic;
using System.Text;

namespace FirstWebCore.Framework.Uility
{
  public class DBMappingAttribute:Attribute
  {
    private string _MappingName = null;

    public DBMappingAttribute(string mappingName)
    {
      //构造函数注入进来
      this._MappingName = mappingName;
    }
    public string GetMappingName()
    {
      return this._MappingName;
    }
  }
}
