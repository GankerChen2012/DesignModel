using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignModel
{
    //自我总结：就是用一个集合来存放需要保存的内容。


    //备忘录（Memento）模式又称标记（Token）模式。
    //GOF给备忘录模式的定义为：在不破坏封装性的前提下，捕获一个对象的内部状态，并在该对象之外保存这个状态。
    //这样以后就可将该对象恢复到原先保存的状态。


    //  来看下“月光宝盒”备忘录模式的组成部分：
    //1) 备忘录（Memento）角色：备忘录角色存储“备忘发起角色”的内部状态。
    //  “备忘发起角色”根据需要决定备忘录角色存储“备忘发起角色”的哪些内部状态。
    //   为了防止“备忘发起角色”以外的其他对象访问备忘录。
    //   备忘录实际上有两个接口，“备忘录管理者角色”只能看到备忘录提供的窄接口——对于备忘录角色中存放的属性是不可见的。
    //  “备忘发起角色”则能够看到一个宽接口——能够得到自己放入备忘录角色中属性。 
    //2) 备忘发起（Originator）角色：“备忘发起角色”创建一个备忘录，用以记录当前时刻它的内部状态。在需要时使用备忘录恢复内部状态。
    //3) 备忘录管理者（Caretaker）角色：负责保存好备忘录。不能对备忘录的内容进行操作或检查。
    internal class MementoPattern
    {
        public MementoPattern()
        {
            Caretaker caretaker = new Caretaker();
           
            Document document = new Document();
            
            document.Content = "dsa";
            caretaker.AddDocumentVersion(document.CreateMemento());

            document.Content = "332211";
            caretaker.AddDocumentVersion(document.CreateMemento());

            var ver = caretaker.GetDocumentVersion(1);
            Console.WriteLine(ver.Content);

            ver = caretaker.GetDocumentVersion(2);
            Console.WriteLine(ver.Content);
        }
    }

    //Originator
    public class Document
    {
        public string Content { get; set; }

        public DocumentVersion CreateMemento()
        {
            return new DocumentVersion(Content);
        }

        public void SetMemento(DocumentVersion documentVersion)
        {
            Content = documentVersion.Content;
        }

    }

    //Memento
    public class DocumentVersion
    {
        public string Content { get; set; }
        public DocumentVersion(string content)
        {
            Content = content;
        }
    }

    //Caretaker
    public partial class Caretaker
    {
        private readonly Dictionary<int,DocumentVersion> mementoList=new Dictionary<int, DocumentVersion>();

        public DocumentVersion GetDocumentVersion(int versionId)
        {
            return mementoList[versionId];
        }

        public void AddDocumentVersion(DocumentVersion documentVersion)
        {
            int maxVersionId = mementoList.Keys.Count == 0 ? 0 : mementoList.Keys.Max();
            mementoList.Add(maxVersionId+1,documentVersion);
        }
    }


}

