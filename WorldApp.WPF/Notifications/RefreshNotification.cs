using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldApp.WPF.Notifications
{
    public class RefreshNotification(bool force) : ValueChangedMessage<bool>(force) { }
}