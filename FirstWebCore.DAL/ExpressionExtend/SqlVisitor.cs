using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FirstWebCore.DAL.ExpressionExtend
{
    /// <summary>
    /// 解读表达式目录树
    /// </summary>
    public class SqlVisitor : ExpressionVisitor
    {
        private Stack<string> _ConditionStack = new Stack<string>();

        public string GetWhere()
        {
            string where = string.Join(" ", _ConditionStack);
            this._ConditionStack.Clear();
            return where;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var right = node.Right;
            this.Visit(right);


            if (node.NodeType == ExpressionType.GreaterThan)
            {
                string tag = ">";
                this._ConditionStack.Push(tag);
            }

            var left = node.Left;
            this.Visit(left);
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            string prop = node.Member.Name;
            this._ConditionStack.Push(prop);
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            object value = node.Value;
            this._ConditionStack.Push(value.ToString());
            return node;
        }

    }
}
