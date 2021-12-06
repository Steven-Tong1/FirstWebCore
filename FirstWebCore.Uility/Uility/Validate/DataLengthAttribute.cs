using System;
using System.Collections.Generic;
using System.Text;

namespace FirstWebCore.Framework.Uility.Validate
{
    public class DataLengthAttribute:BaseValidateAttribute
    {
        /// <summary>
        /// 最小长度
        /// </summary>
        public int _Min { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int _Max { get; set; }

        /// <summary>
        /// 构造函数然后将参数注入进来
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public DataLengthAttribute(int min,int max)
        {
            this._Min = min;
            this._Max = max;
        }

        /// <summary>
        /// 继承后可以进行重写然后判断各自的校验
        /// </summary>
        /// <returns></returns>
        public override bool ValidateData(object oValue)
        {
            return oValue !=null && String.IsNullOrWhiteSpace(oValue.ToString()) && oValue.ToString().Length >= _Min && oValue.ToString().Length < _Max;
        }
    }
}
