using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyNewMessage(Message message);

        [OperationContract(IsOneWay = true)]
        void NotifyStatusChange(User user, Status newStatus);
    }
}
