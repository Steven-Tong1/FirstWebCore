using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace FirstWebCore.Framework.Uility
{
  public static class DBFiliterExtendAttribute
  {
    public static IEnumerable<PropertyInfo> GetPropWithNoKey(this Type type)
    {
      return type.GetProperties().Where(x=>!x.IsDefined(typeof(TableKeyAttribute),true));
    }
  }
}
