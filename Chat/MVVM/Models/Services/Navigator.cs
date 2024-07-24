using Chat.Core;
using Chat.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Models.Services
{
    public static class Navigator
    {
        public static void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            INavigationService navService = ServiceProvider.GetRequiredService<INavigationService>();
            navService.NavigateTo<TViewModel>();
        }
    }
}
