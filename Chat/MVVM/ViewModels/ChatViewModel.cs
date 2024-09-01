using Chat.Core;
using Chat.MVVM.Core;
using Chat.MVVM.Models.Handlers;
using Chat.MVVM.Models.Instances.Configs;
using CommonLibrary.Models.Custom;
using CommonLibrary.Models.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        // ============= Properties for Binding =============

        private List<DialogueCard> _dialoguesCards = new List<DialogueCard>();
        public List<DialogueCard> DialoguesCards
        {
            get => _dialoguesCards;
            set => SetField(ref _dialoguesCards, value);
        }


        private DialogueCard _selectedDialogue;
        public DialogueCard SelectedDialogue 
        {
            get => _selectedDialogue;
            set => SetField(ref _selectedDialogue, value);
        }


        private List<MessageDto> _messages = new List<MessageDto>();
        public List<MessageDto> Messages
        {
            get => _messages;
            set => SetField(ref _messages, value);
        }


        public override IConfig? Config => throw new NotImplementedException();


        // ============= Commands =============
        public RelayCommand UploadMessagesCommand { get; set; }


        public ChatViewModel() 
        {
            DialoguesCardsProvider.RequestDialogues();

            UploadMessagesCommand = new RelayCommand(UploadMessages);
        }


        // ============= private methods =============
        private void UploadMessages(object obj)
        {
            MessagesProvider.RequestMessages(SelectedDialogue.DialogueId);
        }


        // ============= default methods =============
        public override void ResetData()
        {
            throw new NotImplementedException();
        }
    }
}
