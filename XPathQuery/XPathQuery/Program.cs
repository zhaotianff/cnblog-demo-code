using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XPathQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("demo.xml");

            //获取根节点
            var root = doc.Root;

            //获取第一个子节点
            var firstNode = root.FirstNode;

            //获取全部子节点
            var allChildNode = root.Nodes();

            //获取指定名称的第一个子节点
            var headNode = root.Element("head");

            //获取指定名称的全部子节点
            var allNamedNode = root.Elements("head");

            //获取属性
            var attribute = root.Element("body").Element("div").Attribute("class");

            //获取节点的全部属性
            var allAttributes = root.Attributes();

            //XPath查询
            var xpathQeury = root.XPathSelectElement("body/div");

            //获取结点的名称
            var nodeName = root.Name;
            //获取节点的值
            var nodeValue = root.Value;

            //保存XML
            doc.Save("demo.xml");

            //Linq查询
            //不使用XPath
            //var queryResult = root.Element("body").Element("div").Elements("div").ElementAt(1).Elements("span").Where(x=>x.Attribute("class") != null && x.Attribute("class").Value == "price2").FirstOrDefault();
            //if(queryResult != null)
            //    Console.WriteLine(queryResult.Value);

            //使用XPath   
            var xpathQueryResult = root.XPathSelectElements("body/div/div[2]/span").Where(x => x.Attribute("class") != null && x.Attribute("class").Value == "price2").FirstOrDefault();
            if(xpathQueryResult != null)
                Console.WriteLine(xpathQueryResult.Value);

            //XPath语法
            //   /    从根节点开始选择
            var xpathRoot = doc.XPathSelectElement("/html");
            var xpathRoot2 = doc.XPathSelectElement("html");
            Console.WriteLine(xpathRoot.Name);
            Console.WriteLine(xpathRoot2.Name);

            //   //   获取文档中所有指定的结点，不管它的位置
            //如要获取xml文档中所有的div节点
            //以下获取的结果都是一样的
            var anyDivNode = root.Element("body").Element("div").Elements("div").ElementAt(1);
            var allDivNodeInBody = anyDivNode.XPathSelectElements("//div");
            var allDivNodeInDoc = doc.XPathSelectElements("//div");
            Console.WriteLine(allDivNodeInBody.Count());
            Console.WriteLine(allDivNodeInDoc.Count());

            //   .     当前节点
            var currentNode = root.XPathSelectElement(".");
            Console.WriteLine(currentNode.Name);

            //   ..    父节点
            var parentNode = root.Element("body").XPathSelectElement("..");
            Console.WriteLine(parentNode.Name);

            //   @     选取属性  不支持
            //var abc = root.XPathSelectElements("//@class");

            //通配
            //   *      选取全部节点
            var allNode = doc.XPathSelectElements("*");
            //   @*     选取全部属性 不支持
            //var allAttribute = doc.XPathSelectElements("@*");

            //谓词

            //第一个查询结果
            var firstSpanNode = doc.XPathSelectElement("//span[1]");
            Console.WriteLine(firstSpanNode.Value);

            //最后一个查询结果
            var lastDivNode = doc.XPathSelectElement("//span[last()]");
            Console.WriteLine(lastDivNode.Attribute("class").Value);

            //选择带有属性class的节点
            var nodeWithClassAttribute = doc.XPathSelectElements("//div[@class]");
            foreach (var item in nodeWithClassAttribute)
            {
                Console.WriteLine("====================");
                Console.WriteLine(item);
            }

            //选择带有属性class，且值为item-title的div节点
            var nodeWithClassValueAttribute = doc.XPathSelectElements("//div[@class='item-title']");
            foreach (var item in nodeWithClassValueAttribute)
            {
                Console.WriteLine(item);
            }

            //简单的选择计算

            //如获取倒数第二个div节点
            var nextToLastDivNode = doc.XPathSelectElement("//div[last()-1]");
            Console.WriteLine(nextToLastDivNode);

            //获取值大于10的span节点
            var greaterThanTenSpan = doc.XPathSelectElement("//span[text()>10]");
            Console.WriteLine(greaterThanTenSpan);

            //获取值小于10的span节点
            var lessThanTenSpan = doc.XPathSelectElement("//span[text()<10]");
            Console.WriteLine(lessThanTenSpan);

            //获取值加10等于22的span节点
            var plusTenSpan = doc.XPathSelectElements("//span[text()+10=22]");
            foreach (var item in plusTenSpan)
            {
                Console.WriteLine(item.Value);
            }

            //组合查询
            //如查询值等于12或等于2的span节点
            var combineQuery = doc.XPathSelectElements("//span[text()=2]|//span[text()=12]");
            foreach (var item in combineQuery)
            {
                Console.WriteLine(item.Value);
            }
        }
    }
}
