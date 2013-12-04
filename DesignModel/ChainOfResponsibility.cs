
using System;
using System.Collections;
using System.Collections.Generic;

namespace DesignModel
{
    //自我总结： 职责链 就是把问题一级一级的传递，直到有能处理它的方法或者传到最底层。


    //职责链模式的数据结构非常像数据结构里的单链表，请求信息一直沿着链表传递，直到有能处理信息的类为止。
    //在职责链模式中，链的组织方式是从最具体的情形到最一般的情形；并不保证所有的请求都会产生反映。
    //职责链是一个很好的模式例子，有助于隔离程序中的每个对象所能完成的工作内容，降低了对象之间的耦合度。
    //职责链模式经常和组合模式一起使用，在这种情况下父 组件可以作为其在职责链上的后继。

    public class ChainOfResponsibility
    {
        public ChainOfResponsibility()
        {
            //Handler h1=new ConcreateHandler1();
            //Handler h2 = new ConcreateHandler2();
            //Handler h3 = new ConcreateHandler3();
            //Handler h4 = new ConcreateHandler4();
            //h1.SetSuccessor(h2);
            //h2.SetSuccessor(h3);
            //h3.SetSuccessor(h4);
            //int[] requests = {2, 4, 12, 34, 23, 54};

            //foreach (var request in requests)
            //{
            //    h1.HandRequest(request);
            //}

            //四则运算 职责链
            ChainOfResponsibilityTest chainOfResponsibilityTest = new ChainOfResponsibilityTest();
        }

    }

    public abstract class Handler
    {
        protected Handler Successor;
        public void SetSuccessor(Handler successor)
        {
            Successor = successor;
        }
        public abstract void HandRequest(int request);
    }
    public class ConcreateHandler1 : Handler
    {
        public override void HandRequest(int request)
        {
            if (request > 0 && request < 10)
            {
                Console.WriteLine("{0}处理请求：{1}", GetType().Name, request);
            }
            else if(Successor!=null)
            {
                Successor.HandRequest(request);
            }
        }
    }
    public class ConcreateHandler2 : Handler
    {
        public override void HandRequest(int request)
        {
            if (request > 10 && request < 20)
            {
                Console.WriteLine("{0}处理请求：{1}", GetType().Name, request);
            }
            else if (Successor != null)
            {
                Successor.HandRequest(request);
            }
        }
    }
    public class ConcreateHandler3 : Handler
    {
        public override void HandRequest(int request)
        {
            if (request > 20 && request < 30)
            {
                Console.WriteLine("{0}处理请求：{1}", GetType().Name, request);
            }
            else if (Successor != null)
            {
                Successor.HandRequest(request);
            }
        }
    }
    public class ConcreateHandler4 : Handler
    {
        public override void HandRequest(int request)
        {
            Console.WriteLine("{0}处理未知请求：{1}", GetType().Name, request);
        }
    }

    #region 四则运算的职责链
    
    class ChainOfResponsibilityTest
    {
        public ChainOfResponsibilityTest()
        {
            Operation com1 = new AddToken();
            Operation com2 = new SubtractionToken();
            Operation com3 = new MultipToken();
            Operation com4 = new DivisionToken();

            com1.Next = com2;
            com2.Next = com3;
            com3.Next = com4;

            const string s = "2+3*4-7/7+5*6";
            char[] chs = s.ToCharArray();

            var listNum=new List<double>();
            var listSign=new List<string>();

            for (var i = 0; i < chs.Length; i++)
            {
                var ch = chs[i].ToString();
                double num;
                var bo = double.TryParse(ch, out num);

                if (bo)
                    listNum.Add(num);
                else
                    listSign.Add(ch);
            }

            var sum = Calculate(listNum, listSign, com1);
            Console.WriteLine(sum);
        }

        //只进行2级运算
        public double Calculate(List<double> listNum, List<string> listSign, Operation com)
        {
            for (var i = 0; i < listSign.Count;i++ )
            {
                var sign = listSign[i];
                var num1 = listNum[i];
                var num2 = listNum[i + 1];
                if (sign == "*" || sign == "/")
                {
                    var num = com.Action(sign, num1, num2);
                    listNum.RemoveAt(i);
                    listNum.RemoveAt(i);
                    listNum.Insert(i, num);
                    listSign.RemoveAt(i);
                    Console.WriteLine("{0}{1}{2}={3}", num1, sign, num2, num);
                }
            }

            return  AddCut(listNum, listSign, com);
        }

        //只进行1级运算
        public double AddCut(List<double> listNum, List<string> listSign, Operation com)
        {
            double sum = 0;
            for (var i = 0; i < listSign.Count; i++)
            {
                if (i == 0)
                {
                    sum = listNum[0];
                }
                var sign = listSign[i];
                var num2 = listNum[i + 1];
                var num = com.Action(sign, sum, num2);
                sum = num;
            }
            return sum;
        }

    }
    public abstract class Operation
    {
        protected string Token = "";
        protected int Level;
        public Operation Next { get; set; }

        public double Action(string s, double a, double b)
        {
            if (s == Token)
            {
                return Com(a, b);
            }
            return Next.Action(s, a, b);
        }
        protected abstract double Com(double a, double b);
    }
    public class AddToken : Operation
    {
        public AddToken()
        {
            Token = "+";
        }
        protected override double Com(double a, double b)
        {
            return a + b;
        }
    }
    public class SubtractionToken : Operation
    {
        public SubtractionToken()
        {
            Token = "-";
        }
        protected override double Com(double a, double b)
        {
            return a - b;
        }
    }
    public class MultipToken : Operation
    {
        public MultipToken()
        {
            Token = "*";
        }
        protected override double Com(double a, double b)
        {
            return a * b;
        }
    }
    public class DivisionToken : Operation
    {
        public DivisionToken()
        {
            Token = "/";
        }
        protected override double Com(double a, double b)
        {
            return a / b;
        }
    }
  





    #endregion


}

