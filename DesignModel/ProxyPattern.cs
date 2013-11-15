using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结： coder只负责最终实现。 代理去实现各种判断。


    //代理模式应用场景:
    //1.安全代理，在代理类里面加一些权限的判断和控制
    //2.远程代理，.net引用webservice,会生成一些代理文件
    //3.虚拟代理,提供一个占位符，但是没有直接显示图片，qq图片采用
    //最后总结一下，代理模式就是找一个人干活，但是并不直接找干活的人，找一个中间人,找干活的人并不关心中间人(代理)去怎么做，只是关心最后活干完就行了!
    //中间人负责接活，但是并不完成具体的工作任务，他会把找他干活人的任务分配给其他人去完成！

    class ProxyPattern
    {
        public ProxyPattern()
        {
            AbstactCoder coder = new Coder();
            Proxy proxy = new Proxy(coder);
            proxy.Write();

        }
    }

    public abstract class AbstactCoder
    {
        public abstract void Write();
    }

    public class Coder : AbstactCoder
    {
        
        public override void Write()
        {
            
            Console.WriteLine("写代码！");
        }
    }

    public class Proxy : AbstactCoder
    {
        protected AbstactCoder AbstactCoder;
        public Proxy(AbstactCoder abstactCoder)
        {
            AbstactCoder = abstactCoder;
        }

        public override void Write()
        {
            AbstactCoder.Write();
        }

    }



}
