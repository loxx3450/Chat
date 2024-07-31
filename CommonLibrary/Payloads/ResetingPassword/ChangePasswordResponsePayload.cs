using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.ResetingPassword
{
    public enum ChangePasswordResponseType
    {
        Success,
        SmthWentWrong
    }

    public class ChangePasswordResponsePayload : JSONPayload
    {
        public ChangePasswordResponseType ResponseType { get; set; }

        public ChangePasswordResponsePayload(ChangePasswordResponseType responseType)
        {
            ResponseType = responseType;
        }
    }
}
