using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结：解释器模式 就是根据输入的数据返回相应的解释数据。


    //适用性：
    //1.当有一个语言需要解释执行，并且你可将该语言中的句子表示为一个抽象语法树时，可使用解释器模式。
    //而当存在以下情况时该模式效果最好：
    //2.该文法简单对于复杂的文法，文法的类层次变得庞大而无法管理。此时语法分析程序生成器这样的工具是更好的选择。
    //它们无需构建抽象语法树即可解释表达工，这样可以节省空间而且还可能节省时间。
    //3.效率不是一个关键问题最高效的解释器通常不是通过直接解释语法分析树实现的，而是首先将它们转换成另一种形式。
    //例如：正则表达式通常被转换成状态器。但即使在这种情况下，转换器仍可用解释器模式实现，该模式仍是有用的。

    //在以下三种情况下需要使用解释器模式：
    //1. 当需要一个命令解释权来解析用户的命令的时候。
    //2. 当程序需要解析代数符号串的时候，比如需要计算一个数学表达式的值。
    //3. 当程序需要产生各种不同的输出的时候。就比如不同的SQL语句对应完全不用的用户数据输出。

    class InterpreterPattern
    {
        public InterpreterPattern()
        {

            string roman = "五千四百三十二"; //5432
            Context context = new Context(roman);

            ArrayList arr=new ArrayList();
            arr.Add(new OneExpression());
            arr.Add(new TenExpression());
            arr.Add(new HundredExpression());
            arr.Add(new ThousandExpression());


            foreach (Expression exp in arr)
            {
                exp.Interpret(context);
            }

            Console.WriteLine("{0} = {1}", roman, context.Data);
        }
    }

    public class Context
    {
        public string Statement { get; set; }
        public int Data { get; set; }
        public Context(string statement)
        {
            Statement = statement;
        }
    }

    public abstract class Expression
    {
        protected Dictionary<string,int> table=new Dictionary<string, int>(9);
        public abstract string GetPostifix();
        public abstract int Multiplier();
        public virtual int GetLength()
        {
            return GetPostifix().Length + 1;
        }

        protected Expression()
        {
            table.Add("一", 1);
            table.Add("二", 2);
            table.Add("三", 3);
            table.Add("四", 4);
            table.Add("五", 5);
            table.Add("六", 6);
            table.Add("七", 7);
            table.Add("八", 8);
            table.Add("九", 9);
        }

        public virtual void Interpret(Context context)
        {
            if (context.Statement.Length == 0)
            {
                return;
            }

            foreach (var key in table.Keys)
            {
                int value = table[key];
                var state = context.Statement;

                if (state.EndsWith(key + GetPostifix()))
                {
                    context.Data += value*Multiplier();
                    context.Statement = state.Substring(0,state.Length- GetLength());
                    break;
                }
                if (state.EndsWith("零"))
                {
                    context.Statement = state.Substring(0, state.Length - 1);
                    break;
                }
                if (state.Length == 0)
                {
                    break;
                }
            }
        }

    }

    public class OneExpression : Expression
    {
        public override string GetPostifix()
        {
            return "";
        }
        public override int Multiplier()
        {
            return 1;
        }
        public override int GetLength()
        {
            return 1;
        }
    }

    public class TenExpression : Expression
    {
        public override string GetPostifix()
        {
            return "十";
        }
        public override int Multiplier()
        {
            return 10;
        }
        public override int GetLength()
        {
            return 2;
        }
    }

    public class HundredExpression : Expression
    {
        public override string GetPostifix()
        {
            return "百";
        }
        public override int Multiplier()
        {
            return 100;
        }
        public override int GetLength()
        {
            return 2;
        }
    }

    public class ThousandExpression : Expression
    {
        public override string GetPostifix()
        {
            return "千";
        }
        public override int Multiplier()
        {
            return 1000;
        }
        public override int GetLength()
        {
            return 2;
        }
    }
}
