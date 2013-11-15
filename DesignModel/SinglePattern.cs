
namespace DesignModel
{
    //自我总结：有且只有一个实例


    //Singleton模式要求一个类有且仅有一个实例，并且提供了一个全局的访问点。
    //这就提出了一个问题：如何绕过常规的构造器，提供一种机制来保证一个类只有一个实例？
    //客户程序在调用某一个类时，它是不会考虑这个类是否只能有一个实例等问题的，所以，这应该是类设计者的责任，而不是类使用者的责任。 
    //从另一个角度来说，Singleton模式其实也是一种职责型模式。因为我们创建了一个对象，这个对象扮演了独一无二的角色，
    //在这个单独的对象实例中，它集中了它所属类的所有权力，同时它也肩负了行使这种权力的职责
    class SinglePattern
    {
        private static readonly object Lock = new object();
        private static SinglePattern singlePattern;
        public static SinglePattern GetInstance()
        {
            if (singlePattern == null)
            {
                lock (Lock)
                {
                    if (singlePattern == null)
                    {
                        singlePattern = new SinglePattern();
                    }
                }
            }
            return singlePattern ;
        }





    }
}


