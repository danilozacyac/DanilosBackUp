using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DanilosBackUp.FileSystemApi
{
    public class ItemStyleSelector : StyleSelector
    {
        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is Drive)
                return this.DriveStyle;
            else if (item is Directory)
                return this.DirectoryStyle;
            else if (item is File)
                return this.FileStyle;
            else if (item is NetworkPcs)
                return this.NetworkStyle;
            return base.SelectStyle(item, container);
        }

        public Style DirectoryStyle
        {
            get;
            set;
        }
        public Style FileStyle
        {
            get;
            set;
        }
        public Style DriveStyle
        {
            get;
            set;
        }

        public Style NetworkStyle
        {
            get;
            set;
        }
    }
}
