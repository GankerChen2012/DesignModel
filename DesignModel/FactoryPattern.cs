using System;

namespace DesignModel
{
    //自我总结：工厂里每个类都是单独的，工厂类把他们整合在一起。
    //就像一个汉堡，里面的每样食物既可以单独存在（一个类），也可以一起组合成一个汉堡（工厂）。

    //工厂模式的工厂，实例化的对象只有一个，如果实例化的对象是多个，就成了抽象工厂模式，其实工厂模式和抽象工厂也就这点区别
    //在工厂方法模式中，工厂方法用来创建客户所需要的产品，同时还向客户隐藏了哪种具体产品类将被实例化这一细节。
    //工厂方法模式的核心是一个抽象工厂类，各种具体工厂类通过抽象工厂类将工厂方法继承下来。
    //如此使得客户可以只关心抽象产品和抽象工厂，完全不用理会返回的是哪一种具体产品，也不用关系它是如何被具体工厂创建的。
    public class FactoryPattern
    {
        static FactoryPattern()
        {
            //产品种类是变化的，如果发生变化，新增一个工厂就可以了,在调用的地方掉用新的方法
            //体现出对修改封闭，对扩展开放，新增新的功能对原来的没有影响
            Factory factory = new FactoryA();

            Product productA = factory.NewProduct();
            productA.Work();
            Car carA = factory.NewCar();
            carA.Bulid();


            Car carB = new CarB();
            carB.Bulid();

            Product productB = new ProductB();
            productB.Work();
        }

    }

    public abstract class Product
    {
        public abstract void Work();
    }
    public class ProductA : Product
    {
        public override void Work(){Console.WriteLine("ProductA");}
    }
    public class ProductB : Product
    {
        public override void Work(){Console.WriteLine("ProductB");}
    }

    public abstract class Car
    {
        public abstract void Bulid();
    }
    public class CarA : Car
    {
        public override void Bulid() { Console.WriteLine("CarA"); }
    }
    public class CarB : Car
    {
        public override void Bulid() { Console.WriteLine("CarB"); }
    }

    //对不同产品的实例化，由不同的工厂来具体实现,每一个工厂生产具体的商品
    public abstract class Factory
    {
        public abstract Car NewCar();
        public abstract Product NewProduct();
    }

    public class FactoryA : Factory//具体工厂
    {
        public override Product NewProduct()
        {
            return new ProductA();//实现具体的实例化
        }
        public override Car NewCar()
        {
            return new CarA();//实现具体的实例化
        }
    }

}
