using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTreeViewHierarchical
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<Level1> hierarchicalTestList = new List<Level1>();

        public List<Level1> HierarchicalTestList
        {
            get => hierarchicalTestList;
            set
            {
                hierarchicalTestList = value;
                RaiseChange("HierarchicalTestList");
            }
        }

        public MainWindowViewModel()
        {
            for (int i = 0; i < 3; i++)
            {
                hierarchicalTestList.Add(new Level1($"层级1项目@{i}"));
            }
        }
    }
}
