using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FirstWebCore.Framework.Uility.Validate
{
    /// <summary>
    /// 通用校验是否正常的方法只需要使用一个就行
    /// </summary>
    public static class DataValidateExtend
    {
        public static bool ValidateData<T>(this T t) where T : new()
        {
            //先获取对应的类型
            Type type = typeof(T);

            //判断是否包含某个特性

            //校验包含特性那就直接判断
            foreach (var prop in type.GetProperties())
            {
                //判断对应的属性的特性
                if (prop.IsDefined(typeof(BaseValidateAttribute), true))
                {
                    //判断每一个特性然后先后去对应的值
                    object oValue = prop.GetValue(t);
                    //获取到传入的值之后需要批量校验构造函数
                    var attributList = prop.GetCustomAttributes<BaseValidateAttribute>();
                    foreach (var attr in attributList)
                    {
                        if (!attr.ValidateData(oValue))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
