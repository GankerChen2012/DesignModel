using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结：指挥家就是一个老总，建造者就是一个总监，具体建造者就是各个部门，产品角色就是整个的产品配件
    //老总想要一台苹果电脑，他给总监说，然后总监就及找具体部门，然后这个部门就的产品配件里面去组装一台电脑，然后给老总。


    //4个角色：指挥者(Director)，建造者(Builder)，具体建造者(ConcreteBuilder)，产品(Product)
    //产品：需要创建的对象产品
    //具体建造者：创建产品的实例并且实现建造者多个方法对产品进行装配
    //建造者：本质为抽象类，里面的抽象方法供具体建造者重写，声明产品的引用
    //指挥者：调用建造者抽象类以及其方法
    class BuilderPattern
    {
        static BuilderPattern()
        {
            CreateDirector createDirector=new CreateDirector();
            
            AppleBulid appleBulid=new AppleBulid();
            createDirector.CreateComputer(appleBulid);

            IbmBulid ibmBulid = new IbmBulid();
            createDirector.CreateComputer(ibmBulid);
        }
    }

    //产品角色
    public class ProductRole
    {
        public string Computer { get; set; }
        public string Size { get; set; }
        public string Style { get; set; }

        public void ShowComputerInfo()
        {
            Console.WriteLine("电脑名字：" + Computer);
            Console.WriteLine("电脑大小：" + Size);
            Console.WriteLine("电脑类型：" + Style);
        }
    }

    //建造者角色
    public abstract class BulidRole
    {
        public ProductRole ComputerInstance { get; set; }
        abstract public void CreateCompName();
        abstract public void CreateCompSize();
        abstract public void CreateCompStyle();
    }

    //具体建造者角色
    //具体建造者创建Apple电脑
    public class AppleBulid : BulidRole
    {
        public override void CreateCompName()
        {
            ComputerInstance = new ProductRole();
            ComputerInstance.Computer = "apple";
        }
        public override void CreateCompSize()
        {
            ComputerInstance.Size = "121";
        }
        public override void CreateCompStyle()
        {
            ComputerInstance.Style = "ew1122q";
            ComputerInstance.ShowComputerInfo();
        }
    }
    //具体建造者创建Ibm电脑
    public class IbmBulid : BulidRole
    {
        public override void CreateCompName()
        {
            ComputerInstance = new ProductRole();
            ComputerInstance.Computer = "Ibm";
        }
        public override void CreateCompSize()
        {
            ComputerInstance.Size = "123";
        }
        public override void CreateCompStyle()
        {
            ComputerInstance.Style = "ewq";
            ComputerInstance.ShowComputerInfo();
        }
    }
   
    //指挥者
    class CreateDirector
    {
        public void CreateComputer(BulidRole compBuilder)
        {
            compBuilder.CreateCompName();
            compBuilder.CreateCompSize();
            compBuilder.CreateCompStyle();
        }
    }



}
