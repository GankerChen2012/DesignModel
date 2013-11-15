using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DesignModel
{
    //自我总结：原型模式就是加强版建造者模式 只不过原型模式更灵活，可以自由建造。最重要的是不用新建实例，直接克隆（浅克隆或深克隆）
    //

    //用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新的对象。
    //引入原型模式的本质在于利用已有的一个原型对象，快速的生成和原型对象一样的实例。
    //有一个A的实例a:A a = new A();现在想生成和a一样的一个实例b，按照原型模式，应该是这样：A b = a.Clone();而不是重新再new一个A对象。
    //通过上面这句话就可以得到一个和a一样的实例，确切的说，应该是它们的数据成员是一样的。

    //依赖倒置原则：上面的例子，原型管理器（ColorManager）仅仅依赖于抽象部分（ColorTool），
    //而具体实现细节（MyColor）则依赖与抽象部分（ColorTool），所以 原型模式 很好的满足了依赖倒置原则。

    //使用原型管理器，体现在一个系统中原型数目不固定时，可以动态的创建和销毁，如举的调色板的例子。
    //实现克隆操作，在.NET中可以使用Object类的MemberwiseClone()方法来实现对象的浅表拷贝或通过序列化的方式来实现深拷贝。
    //原型模式同样用于隔离类对象的使用者和具体类型（易变类）之间的耦合关系，它同样要求这些“易变类”拥有稳定的接口。 

    //原型模式是克隆一个原型而不是请求工厂方法创建一个，所以它不需要一个与具体产品类平行的Creater类层次。

    class ProtypePattern
    {
        public ProtypePattern()
        {
            ColorManager colormanager = new ColorManager();
            //初始化颜色
            colormanager["red"] = new MyColor(255, 0, 0);
            colormanager["green"] = new MyColor(0, 255, 0);


            string colorName = "red";
            var c1 = (MyColor)colormanager[colorName].Clone();
            c1.Display(colorName);

            colorName = "green";
            var c2 = (MyColor)colormanager[colorName].Clone();
            c2.Display(colorName);

        }

    }

    abstract class ColorTool
    {
        public abstract ColorTool Clone();
    }

    class MyColor:ColorTool
    {
        private readonly int red;
        private readonly int green;
        private readonly int blue;

        public MyColor(int red, int green, int blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        public override ColorTool Clone()
        {
            return (ColorTool)MemberwiseClone();

            //return this as object; //引用同一个对象
            //return this.MemberwiseClone(); //浅复制
            //return new MyColor() as object;//深复制
        }
        public void Display(string colorname)
        {
            Console.WriteLine("{0}'s RGB Values are: {1},{2},{3}", colorname, red, green, blue);
        }

    }
   
    class ColorManager
    {
        readonly Hashtable colors = new Hashtable();
        public ColorTool this[string name]
        {
            get
            {
                return (ColorTool)colors[name]; 
            }
            set
            {
                colors.Add(name, value);
            }
        }
    }



    //浅拷贝：是指将对象中的所有字段逐字复杂到一个新对象,
    //对值类型字段只是简单的拷贝一个副本到目标对象，改变目标对象中值类型字段的值,不会反映到原始对象中，因为拷贝的是副本
    //对引用型字段则是指拷贝他的一个引用到目标对象。改变目标对象中引用类型字段的值它将反映到原始对象中，因为拷贝的是指向堆是上的一个地址

    //深拷贝：深拷贝与浅拷贝不同的是对于引用字段的处理，
    //深拷贝将会在新对象中创建一个新的对象和原始对象中对应字段相同（内容相同）的字段，
    //也就是说这个引用和原始对象的引用是不同，我们改变新对象中这个字段的时候是不会影响到原始对象中对应字段的内容。

    //浅复制：实现浅复制需要使用Object类的MemberwiseClone方法用于创建一个浅表副本
    //深复制：须实现ICloneable接口中的Clone方法，且需要需要克隆的对象加上[Serializable]特性

    //自我总结： 浅拷贝引用类型值变都变，值类型个人变个人的。 深拷贝 是新建实例。
    //浅拷贝是拷贝指针，深拷贝是拷贝指针指向的数据。


    //通过序列化实现深拷贝
    class DeepCopy
    {

        public ColorTool Deep(ColorTool colorTool)
        {

            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, this);
            memoryStream.Position = 0;

            colorTool = (ColorTool)formatter.Deserialize(memoryStream);
            return colorTool; 
        }

        public int[] DeepNum(int[] numbers)
        {
            int[] num = new int[numbers.Length];
            numbers.CopyTo(num, 0);
            return num;
        }

    }


}
