using System;
using System.Collections.Generic;
using System.Text;

namespace FirstWebCore.Framework.Uility.Validate
{
    /// <summary>
    /// 定义抽象类，用来专门校验数据信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true,Inherited = true)]
    public abstract class BaseValidateAttribute:Attribute
    {
        public abstract bool ValidateData(object oValue);
    }
}
