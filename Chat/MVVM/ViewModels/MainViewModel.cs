using Chat.Core;
using Chat.MVVM.Core;
using Chat.MVVM.Models.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigationService NavigationService { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            SessionStateChecker.CheckState();
        }
    }
}
