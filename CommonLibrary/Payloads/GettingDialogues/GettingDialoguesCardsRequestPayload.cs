using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.GettingDialogues
{
    public class GettingDialoguesCardsRequestPayload : JSONPayload
    {
        public int UserId { get; set; }

        public GettingDialoguesCardsRequestPayload(int userId)
        {
            UserId = userId;
        }
    }
}
