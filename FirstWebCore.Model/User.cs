using FirstWebCore.Model;
using System;
using FirstWebCore.Framework.Uility;
using FirstWebCore.Framework.Uility.Validate;

namespace Zhaoxi.CustomORMDemo.Model
{
    [TableName("User")]
    public class UserModel : BaseModel
    {
        [Required]
        [DataLength(2,10)]
        public string Name { get; set; }

        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        //[ZhaoxiTable("State")]//不好，代码坏的味道
        [ColumnName("State")]
        public int Status { get; set; }
        public int UserType { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int LastModifierId { get; set; }
        public DateTime LastModifyTime { get; set; }
    }
}
