using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace ConsoleApp9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            students.Add(new Student() { Name = "雍心慈", StudentID = "1" });
            students.Add(new Student() { Name = "雍心慈2", StudentID = "2" });
            students.Add(new Student() { Name = "乔宁夏", StudentID = "3" });
            students.Add(new Student() { Name = "乔宁夏2", StudentID = "4" });
            students.Add(new Student() { Name = "宓白夜", StudentID = "5" });
            students.Add(new Student() { Name = "仰灵萱", StudentID = "6" });
            students.Add(new Student() { Name = "冷杏儿", StudentID = "7" });

            #region 循环过滤
            var query = students.AsEnumerable();

            foreach (var rule in GetExpression("心", "1"))
            {
                query = query.Where(x => rule(x));
            }

            foreach (var item in query)
            {
                Console.WriteLine(item.StudentID);
                Console.WriteLine(item.Name);
            }
            #endregion

            #region 构建表达式树
            var result = students.AsQueryable().Where(GetExpressionTree("心", "1"));

            foreach (var item in result)
            {
                Console.WriteLine(item.StudentID);
                Console.WriteLine(item.Name);
            }
            #endregion
        }

        private static List<Func<Student, bool>> GetExpression(string name, string studentId)
        {
            List<Func<Student, bool>> list = new List<Func<Student, bool>>();

            if (!string.IsNullOrEmpty(studentId))
            {
                list.Add(x => x.StudentID.Contains(studentId));
            }

            if (!string.IsNullOrEmpty(name))
            {
                list.Add(x => x.Name.Contains(name));
            }
            return list;
        }

        private static Expression<Func<Student, bool>> GetExpressionTree(string name, string studentId)
        {
            Expression<Func<Student, bool>> expression = x => true;

            var paramExpression = Expression.Parameter(typeof(Student));

            if (!string.IsNullOrEmpty(name))
            {
                //名字条件
                Expression<Func<Student, bool>> nameExpression = x => x.Name.Contains(name);
                expression = ConcatExpression(expression, nameExpression, paramExpression);
            }

            if (!string.IsNullOrEmpty(studentId))
            {
                //Id条件
                Expression<Func<Student, bool>> patientIdExpression = x => x.StudentID.Contains(studentId);
                expression = ConcatExpression(expression, patientIdExpression, paramExpression);
            }  

            return expression;
        }

        /// <summary>
        /// 将表达式组合在一起
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="parameterExpression"></param>
        /// <returns></returns>
        private static Expression<Func<Student, bool>> ConcatExpression(Expression<Func<Student, bool>> left, Expression<Func<Student, bool>> right, ParameterExpression parameterExpression)
        {
            var expressionBody = Expression.AndAlso(left.Body, right.Body);
            expressionBody = (BinaryExpression)new ParameterReplacer(parameterExpression).Visit(expressionBody);
            return Expression.Lambda<Func<Student, bool>>(expressionBody, parameterExpression);
        }
    }

    /// <summary>
    /// 重写表达式树。
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter);
        }

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
    }
}