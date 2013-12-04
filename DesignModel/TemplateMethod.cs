using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{



    //模板方法模式是非常简单的一个模式。任何时候只要你编写了一个父类，并在类中留有一个或者多个方法给派生类实现，实质上就是在使用模板模式。
    //模板模式形式化了在类中定义算法但是把算法的实现细节留个子类实现这一想法。
    
    //模板方法模式有四种可能在派生类中使用的方法：
    //1. 完整的方法，这些方法可以被派生类继承。
    //2. 完全没有填写的方法，方法主体为空。
    //3. 包含某些操作的默认实现的方法，但是有可能在派生类中被重写。这种方法被称为钩子。
    //4. 其本身使用了抽象，钩子和具体方法的任意组合的方法。


    internal class TemplateMethod
    {
        public TemplateMethod()
        {
            AbstractClass concrete = new Concrete();
            concrete.TemplateMathod();
        }
    }


    public abstract class AbstractClass
    {
        public void TemplateMathod()
        {
            TemplateMathod1();
        }

        public void TemplateMathod1()
        {
            Console.WriteLine("sds");
        }
    }

    public class Concrete : AbstractClass
    {

    }



}
