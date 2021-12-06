using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace FirstWebCore.DAL
{
    public class ExpressionToSqlVisitor:ExpressionVisitor
    {
        //需要引用 System.Linq.Expression
        /*
         * Visit 解析入口--会识别当前传入的目录树的类型
         * 如果是lambda表达式 会再次把body属性交给Visit 然后进行递归
         * 如果是其他的类型 会调用对应的Visit方法 如：VisitBinary 二元 等方法
         * 特点：先进后出
         */

        /// <summary>
        /// Visit入口
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override Expression Visit(Expression node)
        {
            Console.WriteLine("");
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Console.WriteLine($"VisitBinary {node}");
            base.Visit(node.Right);
            Console.WriteLine($"VisitBinary {node} NodeType {node.NodeType}");
            base.Visit(node.Left);
            return node;
        }
    }
}
