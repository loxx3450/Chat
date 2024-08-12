using CommonLibrary.Payloads.PayloadTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Payloads.SessionStateCheck
{
    public enum SessionStateCheckResponseType
    {
        UserIsLoggedIn,
        UserIsLoggedOut
    }

    public class SessionStateCheckResponsePayload: JSONPayload
    {
        public SessionStateCheckResponseType ResponseType { get; set; }
        public int AssociatedUserId { get; set; }

        public SessionStateCheckResponsePayload(SessionStateCheckResponseType responseType, int associatedUserId)
        {
            ResponseType = responseType;
            AssociatedUserId = associatedUserId;
        }
    }
}
