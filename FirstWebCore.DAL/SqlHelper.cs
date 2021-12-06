using FirstWebCore.DAL.ExpressionExtend;
using FirstWebCore.Framework;
using FirstWebCore.Framework.Uility;
using FirstWebCore.Framework.Uility.Validate;
using FirstWebCore.Model;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace FirstWebCore.DAL
{
    public class SqlHelper
    {
        private static string connectionString = ConfigurationManager.SqlConnectionStringCustom;

        private static CustomExpressionVisitor visitor = null;

        public static object _Object = null;
        //先写一个查询的通过反射进行动态拼接字符串实现查找
        //1.where T:BaseModel 基于泛型约束

        public SqlHelper()
        {
            //lock (_Object)
            //{
                if (visitor == null)
                {
                    visitor = new CustomExpressionVisitor();
                }
            //}
        }


        #region 代码坏的味道，一段代码中多个重复，可以想办法进行封装

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public T Find<T>(int id) where T : BaseModel, new()
        //{
        //    //反射三部曲
        //    Type type = typeof(T);
        //    var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.select);
        //    SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Id", id) };
        //    //ado操作数据库
        //    //1.创建链接
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {

        //            //2.创建command 传入 sql跟链接conn
        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.Parameters.AddRange(parameters);
        //            //3.打开链接
        //            conn.Open();
        //            var read = cmd.ExecuteReader();
        //            if (read.Read())
        //            {
        //                //如果获取到了对应的数据
        //                T t = Activator.CreateInstance<T>();

        //                //需要给对应的对象进行赋值？
        //                //通过反射然后去循环赋值
        //                foreach (var prop in type.GetProperties())
        //                {
        //                    //循环遍历所有的属性
        //                    //反射属性赋值 SetValue 给对象赋值
        //                    //var propName = prop.GetMappingColumnName();

        //                    prop.SetValue(t, read[prop.Name] is DBNull ? null : read[prop.Name]); //思考如果是数据库中有数据为NULL，需要判断null，   
        //                }

        //                //最后输出
        //                return t;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            //异常就返回null
        //            return null;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }

        //    return null;
        //}


        //public bool Insert<T>(T t) where T : BaseModel, new()
        //{
        //    if (!t.ValidateData())
        //        throw new Exception("参数校验异常");

        //    Type type = typeof(T);
        //    var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.insert);//注意调用的时候，调用顺序 静态字段>构造函数>静态方法
        //    var parameters = type.GetPropWithNoKey().Select(p => new SqlParameter($"@{p.GetMappingName()}", $"{p.GetValue(t) ?? DBNull.Value}")).ToArray();
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.Parameters.AddRange(parameters);
        //            conn.Open();
        //            int row = cmd.ExecuteNonQuery();
        //            return row > 0;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //            return false;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 根据主键id更新
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public bool Update<T>(T t) where T : BaseModel, new()
        //{
        //    //加入特性校验

        //    if (!t.ValidateData())
        //        throw new Exception("参数校验异常");

        //    Type type = typeof(T);
        //    var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.update);//注意调用的时候，调用顺序 静态字段>构造函数>静态方法
        //    var parameters = type.GetPropWithNoKey().Select(p => new SqlParameter($"@{p.GetMappingName()}", $"{p.GetValue(t) ?? DBNull.Value}")).Append(new SqlParameter(@"id", t.Id)).ToArray();
        //    parameters.Append(new SqlParameter(@"id", t.Id));
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.Parameters.AddRange(parameters);
        //            conn.Open();
        //            int row = cmd.ExecuteNonQuery();
        //            return row > 0;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //            return false;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 根据主键id删除
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public bool Delete<T>(T t) where T : BaseModel, new()
        //{
        //    Type type = typeof(T);
        //    var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.delete);//注意调用的时候，调用顺序 静态字段>构造函数>静态方法
        //    SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Id", t.Id) };
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.Parameters.AddRange(parameters);
        //            conn.Open();
        //            int row = cmd.ExecuteNonQuery();
        //            return row > 0;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //            return false;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}

        #endregion

        #region 升级版

        /// <summary>
        /// 根据id返回1条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : BaseModel, new()
        {
            //反射三部曲
            Type type = typeof(T);
            var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.select);
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Id", id) };

            var data = this.ExcuteSql<T>(sql, parameters, cmd =>
            {
                var read = cmd.ExecuteReader();
                if (read.Read())
                {
                    //如果获取到了对应的数据
                    T t = Activator.CreateInstance<T>();

                    //需要给对应的对象进行赋值？
                    //通过反射然后去循环赋值
                    foreach (var prop in type.GetProperties())
                    {
                        //循环遍历所有的属性
                        //反射属性赋值 SetValue 给对象赋值
                        //var propName = prop.GetMappingColumnName();
                        prop.SetValue(t, read[prop.Name] is DBNull ? null : read[prop.Name]); //思考如果是数据库中有数据为NULL，需要判断null，   
                    }
                    return t;
                }
                return default(T);
            });
            
            return data;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert<T>(T t) where T : BaseModel, new()
        {
            if (!t.ValidateData())
                throw new Exception("参数校验异常");

            Type type = typeof(T);
            var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.insert);//注意调用的时候，调用顺序 静态字段>构造函数>静态方法
            var parameters = type.GetPropWithNoKey().Select(p => new SqlParameter($"@{p.GetMappingName()}", $"{p.GetValue(t) ?? DBNull.Value}")).ToArray();
            return this.ExcuteSql<bool>(sql, parameters, cmd => cmd.ExecuteNonQuery() > 0);
        }

        /// <summary>
        /// 根据主键id更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<T>(T t) where T : BaseModel, new()
        {
            //加入特性校验

            if (!t.ValidateData())
                throw new Exception("参数校验异常");

            Type type = typeof(T);
            var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.update);//注意调用的时候，调用顺序 静态字段>构造函数>静态方法
            var parameters = type.GetPropWithNoKey().Select(p => new SqlParameter($"@{p.GetMappingName()}", $"{p.GetValue(t) ?? DBNull.Value}")).Append(new SqlParameter(@"id", t.Id)).ToArray();
            parameters.Append(new SqlParameter(@"id", t.Id));
            return this.ExcuteSql<bool>(sql, parameters, cmd => cmd.ExecuteNonQuery() > 0);
        }

        /// <summary>
        /// 根据主键id删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Delete<T>(T t) where T : BaseModel, new()
        {
            Type type = typeof(T);
            var sql = SqlCachBulider<T>.GetSql(SqlTypeEnum.delete);//注意调用的时候，调用顺序 静态字段>构造函数>静态方法
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Id", t.Id) };
            return this.ExcuteSql<bool>(sql, parameters, cmd => cmd.ExecuteNonQuery() > 0);
        }

        /// <summary>
        /// 1.代码服用，方便升级
        /// 2.集中管理，Ado.net 操作集中起来然后一起管理
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private S ExcuteSql<S>(string sql, SqlParameter[] parameters, Func<SqlCommand, S> func)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if(parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return func.Invoke(cmd);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return default(S);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 根据条件进行删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool DeleteCondition<T>(Expression<Func<T,bool>> expression)
        {
            Type type = typeof(T);
            visitor.Visit(expression);
            var strWhere = visitor.GetWhere();
            var sql = $" Delete From [{type.GetMappingName()}] Where {strWhere}";
            
            return this.ExcuteSql<bool>(sql, null, cmd => cmd.ExecuteNonQuery() > 0);
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool UpdateCondition<T>(Expression<Func<T, bool>> expression) where T : BaseModel, new()
        {
            //加入特性校验
            Type type = typeof(T);
            T t = new T();//Activator.CreateInstance(type);

            if (!t.ValidateData())
                return false;

            visitor.Visit(expression);
            var strWhere = visitor.GetWhere();
            var updateColunmString = string.Join(",", type.GetPropWithNoKey().Select(a => $"[{a.GetMappingName()}]=@{a.GetMappingName()}"));
            var sql = $" Update [{type.GetMappingName()}] set {updateColunmString} where {strWhere}";
            
            return this.ExcuteSql<bool>(sql, null, cmd => cmd.ExecuteNonQuery() > 0);
        }

        /// <summary>
        /// 根据条件返回数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindCondition<T>(Expression<Func<T,bool>> expression) where T : BaseModel, new()
        {
            //反射三部曲
            Type type = typeof(T);
            visitor.Visit(expression);
            var strWhere = visitor.GetWhere();
            var selectColunmString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetMappingName()}] as [{p.Name}]"));
            var sql = $@"select {selectColunmString} from [{type.GetMappingName()}] where {strWhere}";
            
            var data = this.ExcuteSql<T>(sql, null, cmd =>
            {
                var read = cmd.ExecuteReader();
                if (read.Read())
                {
                    //如果获取到了对应的数据
                    T t = Activator.CreateInstance<T>();

                    //需要给对应的对象进行赋值？
                    //通过反射然后去循环赋值
                    foreach (var prop in type.GetProperties())
                    {
                        //循环遍历所有的属性
                        //反射属性赋值 SetValue 给对象赋值
                        //var propName = prop.GetMappingColumnName();
                        prop.SetValue(t, read[prop.Name] is DBNull ? null : read[prop.Name]); //思考如果是数据库中有数据为NULL，需要判断null，   
                    }
                    return t;
                }
                return default(T);
            });

            return data;
        }

        #endregion
    }
}
