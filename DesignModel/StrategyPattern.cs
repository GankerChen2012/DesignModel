using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结：在适当的时候 替代if else，使代码层次更分明。 和 状态模式 差不多


    //我们来实现一个企业的工资系统，该企业中不同级别的员工工资算法都不相同，针对该问题，
    //最容易想到的莫过于在代码中堆积一大堆if…else…语句或者是switch…case…语句。
    //如果该企业中不同级别的员工过多，或是对级别的调整比较频繁，那该系统就会显得复杂而脆弱。
    //如何能将对象和算法解耦，从而使得在系统运行时能透明的改变对象的算法呢？这就到了策略模式大显身手的时候了。

    //Context代表需要改变算法的那个对象，它维护了一个对Strategy对象的引用，可以定义一个接口允许Strategy对象来访问它的数据。
    //Strategy定义了所支持算法的公共接口，Context通过这个接口来调用ConcreteStrategy定义的算法。
    //ConcreteStrategy实现了具体的算法。

    class StrategyPattern
    {
        public StrategyPattern()
        {
            ISalary salary = new EmployeeSalary();
            Employee employee = new Employee();
            Console.WriteLine(employee.GetSalary());

            employee.Salary=new ManagerSalary();
            Console.WriteLine(employee.GetSalary());

            employee.Salary = new EmployeeSalary();
            Console.WriteLine(employee.GetSalary());

        }
    }

    //Strategy
    public interface ISalary
    {
        int Caculator();
    }
    //ConcreteStrategy
    public class ManagerSalary : ISalary
    {
        public int Caculator()
        {
            return 1000;
        }
    }
    public class EmployeeSalary : ISalary
    {
        public int Caculator()
        {
            return 500;
        }
    }
    
    //Context
    public class Employee
    {
        public ISalary Salary { get; set; }
        public int GetSalary()
        {
            if(Salary!=null)
                return Salary.Caculator();
            return 0;
        }
    }


}
