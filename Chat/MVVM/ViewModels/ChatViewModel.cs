using Chat.Core;
using Chat.MVVM.Core;
using Chat.MVVM.Models.Instances.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        public override IConfig? Config => throw new NotImplementedException();

        public override void ResetData()
        {
            throw new NotImplementedException();
        }
    }
}
