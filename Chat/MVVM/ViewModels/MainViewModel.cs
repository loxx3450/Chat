using Chat.Core;
using Chat.MVVM.Core;
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

            //TODO: should depend on the state of user
            NavigationService.NavigateTo<LoginViewModel>();
        }
    }
}
