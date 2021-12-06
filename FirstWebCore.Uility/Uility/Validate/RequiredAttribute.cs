using System;
using System.Collections.Generic;
using System.Text;

namespace FirstWebCore.Framework.Uility.Validate
{
    public class RequiredAttribute:BaseValidateAttribute
    {
        /// <summary>
        /// 校验不能为空或者Null
        /// </summary>
        /// <param name="oValue"></param>
        /// <returns></returns>
        public override bool ValidateData(object oValue)
        {
            return oValue != null && !string.IsNullOrWhiteSpace(oValue.ToString());
        }
    }
}
