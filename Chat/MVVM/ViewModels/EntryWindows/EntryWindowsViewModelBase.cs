using Chat.MVVM.Core;
using Chat.MVVM.Models.Instances.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels.EntryWindows
{
    public abstract class EntryWindowsViewModelBase : ViewModelBase
    {
        public override EntryWindowsConfig? Config => new EntryWindowsConfig();
    }
}
