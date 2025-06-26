using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTreeViewHierarchical
{
    public class Level1 : ViewModelBase
    {
        private string level1Item = "";

        public string Level1Item
        {
            get => level1Item;
            set
            {
                level1Item = value;
                RaiseChange("Level1Item");
            }
        }

        private List<Level2> level1ChildList = new List<Level2>();

        public List<Level2> Level1ChildList
        {
            get => level1ChildList;
            set
            {
                level1ChildList = value;
                RaiseChange("Level1ChildList");
            }
        }

        public Level1(string item)
        {
            //构建测试数据
            level1Item = item;
            for(int i = 0;i<3;i++)
            {
                level1ChildList.Add(new Level2($"层级2项目@{i}"));
            }
        }
    }

    public class Level2 : ViewModelBase
    {
        private string level2Item = "";

        public string Level2Item
        {
            get => level2Item;
            set
            {
                level2Item = value;
                RaiseChange("Level2Item");
            }
        }

        private List<Level3> level2ChildList = new List<Level3>();

        public List<Level3> Level2ChildList
        {
            get => level2ChildList;
            set
            {
                level2ChildList = value;
                RaiseChange("Level2ChildList");
            }
        }

        public Level2(string item)
        {
            //构建测试数据
            level2Item = item;
            for (int i = 0; i < 3; i++)
            {
                level2ChildList.Add(new Level3($"层级3项目@{i}"));
            }
        }
    }

    public class Level3 : ViewModelBase
    {
        private string level3Item = "";

        public string Level3Item
        {
            get => level3Item;
            set
            {
                level3Item = value;
                RaiseChange("Level3Item");
            }
        }

        public Level3(string item)
        {
            //构建测试数据
            level3Item = item;      
        }
    }
}
