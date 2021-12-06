using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstWebCore.DAL.ExpressionExtend
{
    /// <summary>
    /// Visit入口--表达式目录树传递进去--解析判断类型然后调用对应的visit方法--方法里面继续visit--判断类型再去调用对应的visit方法---就是一个递归式的解析---二叉树深度无限必须得递归的
    /// </summary>
    public  class CustomExpressionVisitor : ExpressionVisitor
    {
        private Stack<string> ConditionStack = new Stack<string>();

        public string GetWhere()
        {
            string where = string.Concat(this.ConditionStack.ToArray());
            this.ConditionStack.Clear();
            return where;
        }


        public override Expression Visit(Expression node)
        {
            Console.WriteLine($"Visit入口：{node.NodeType} {node.Type} {node.ToString()}");
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Console.WriteLine($"VisitBinary：{node.NodeType} {node.Type} {node.ToString()}");
            this.ConditionStack.Push(" ) ");
            base.Visit(node.Right);//5
            this.ConditionStack.Push(node.NodeType.ToSqlOperator()); //翻译成>
            base.Visit(node.Left);//Age
            this.ConditionStack.Push(" ( ");
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            Console.WriteLine($"VisitConstant：{node.NodeType} {node.Type} {node.ToString()}");
            //node.Value;
            this.ConditionStack.Push($"'{node.Value.ToString()}'");
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Console.WriteLine($"VisitMember：{node.NodeType} {node.Type} {node.ToString()}");
            this.ConditionStack.Push($"{node.Member.Name}");
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m == null) throw new ArgumentNullException("MethodCallExpression");

            string format;
            switch (m.Method.Name)
            {
                case "StartsWith":
                    format = "({0} LIKE {1}+'%')";
                    break;

                case "Contains":
                    format = "({0} LIKE '%'+{1}+'%')";
                    break;

                case "EndsWith":
                    format = "({0} LIKE '%'+{1})";
                    break;

                default:
                    throw new NotSupportedException(m.NodeType + " is not supported!");
            }
            this.Visit(m.Object);
            this.Visit(m.Arguments[0]);
            string right = this.ConditionStack.Pop();
            string left = this.ConditionStack.Pop();
            this.ConditionStack.Push(String.Format(format, left, right));

            return m;
        }
    }
}
