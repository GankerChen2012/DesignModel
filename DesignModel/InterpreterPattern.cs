using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
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
