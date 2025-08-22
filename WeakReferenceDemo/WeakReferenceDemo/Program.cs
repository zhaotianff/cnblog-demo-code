using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        //假设缓存数量为50
        int cacheSize = 50;

        Random r = new Random();
        Cache c = new Cache(cacheSize);

        string DataName = "";
        GC.Collect(0);

        // 随机从缓存中取数据
        for (int i = 0; i < c.Count; i++)
        {
            int index = r.Next(c.Count);

            // 访问对象的属性值
            DataName = c[index].Name;
        }
        
        //输出结果
        double regenPercent = c.RegenerationCount / (double)c.Count;   //对象重新创建的比例
        Console.WriteLine("缓存数量: {0}, 重新创建的比例: {1:P2}", c.Count, regenPercent);
    }
}

/// <summary>
/// 缓存类
/// </summary>
public class Cache
{
    // 缓存字典
    static Dictionary<int, WeakReference> cache;

    // 重新创建的个数
    int regenCount = 0;

    public Cache(int count)
    {
        cache = new Dictionary<int, WeakReference>();

        //创建指定数量的弱引用
        for (int i = 0; i < count; i++)
        {
            cache.Add(i, new WeakReference(new Data(i), false));
        }
    }

    /// <summary>
    /// 缓存数量
    /// </summary>
    public int Count
    {
        get { return cache.Count; }
    }

    /// <summary>
    /// 需要重新创建的对象个数
    /// </summary>
    public int RegenerationCount
    {
        get { return regenCount; }
    }

    /// <summary>
    /// 从缓存获取数据
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Data this[int index]
    {
        get
        {
            Data d = cache[index].Target as Data;
            if (d == null)
            {
                // 如果对象已经被回收了，重新创建一个
                Console.WriteLine("索引 {0}: 的数据需要重新创建", index);
                d = new Data(index);
                cache[index].Target = d;
                regenCount++;
            }
            else
            {
                // 通过弱引用获取对象，不需要重新创建
                Console.WriteLine("索引 {0}: 的数据不需要重新创建", index);
            }

            return d;
        }
    }
}

/// <summary>
/// 字节数据类
/// </summary>
public class Data
{
    private byte[] data;   //字节数据  这个字段实际没有用到，只是为了模拟大量数据
    private string name;   //名称 返回size

    public Data(int size)
    {
        data = new byte[size * 1024];
        name = size.ToString();
    }

    public string Name
    {
        get { return name; }
    }
}

/*
示例输出：
Regenerate object at 15: Yes
Regenerate object at 8: No
Regenerate object at 7: Yes
Regenerate object at 48: No
Regenerate object at 20: Yes
Regenerate object at 41: Yes
Regenerate object at 25: Yes
Regenerate object at 0: No
Regenerate object at 6: No
Regenerate object at 46: Yes
Regenerate object at 3: Yes
Regenerate object at 21: Yes
Regenerate object at 22: Yes
Regenerate object at 14: No
Regenerate object at 2: No
Regenerate object at 29: No
Regenerate object at 37: Yes
Regenerate object at 32: No
Regenerate object at 6: No
Regenerate object at 13: Yes
Regenerate object at 29: No
Regenerate object at 27: Yes
Regenerate object at 4: Yes
Regenerate object at 46: No
Regenerate object at 37: No
Cache size: 50, Regenerated: 66.00%
 */