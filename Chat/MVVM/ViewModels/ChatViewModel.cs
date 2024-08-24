using Chat.Core;
using Chat.MVVM.Core;
using Chat.MVVM.Models.Handlers;
using Chat.MVVM.Models.Instances.Configs;
using CommonLibrary.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private List<DialogueCard> _dialoguesCards = new List<DialogueCard>();
        public List<DialogueCard> DialoguesCards
        {
            get => _dialoguesCards;
            set => SetField(ref _dialoguesCards, value);
        }

        public override IConfig? Config => throw new NotImplementedException();

        public ChatViewModel() 
        {
            DialoguesCardsProvider.RequestDialogues();
        }

        public override void ResetData()
        {
            throw new NotImplementedException();
        }
    }
}
