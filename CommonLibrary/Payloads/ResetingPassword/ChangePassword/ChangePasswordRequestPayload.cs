using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.ResetingPassword.ChangePassword
{
    public class ChangePasswordRequestPayload : JSONPayload
    {
        public string NewPassword { get; set; }
        public int AssociatedUserId { get; set; }

        public ChangePasswordRequestPayload(string newPassword, int associatedUserId)
        {
            NewPassword = newPassword;
            AssociatedUserId = associatedUserId;
        }
    }
}
