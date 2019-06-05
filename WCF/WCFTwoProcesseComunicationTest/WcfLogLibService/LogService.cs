using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;

namespace WcfLogLibService
{
    [ServiceBehavior(
       Name = "LogService",
       InstanceContextMode = InstanceContextMode.Single)]
    class LogService : ILogService
    {
        Dictionary<string, ILogCallback> _subscribers = null;

        IServerControl _serverCtrl = null;

        public LogService(IServerControl serverCtrl)
        {
            _serverCtrl = serverCtrl;

            _subscribers =
                new Dictionary<string, ILogCallback>();
        }
        public LogService()
        { }
        public void SendMessage(string user, string msg)
        {
            OperationContext ctx = OperationContext.Current;

            foreach (var subscriber in _subscribers)
            {
                try
                {
                    if (ctx.SessionId == subscriber.Key)
                        continue;

                    if (null != subscriber.Value)
                    {
                        Thread thread = new Thread(delegate ()
                        {
                            subscriber.Value.OnNewMessage(user, msg);
                        });

                        thread.Start();
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            _serverCtrl.LogMessage("Message from " + user + ": " + msg);
        }

        public bool Subscribe()
        {
            try
            {
                OperationContext ctx = OperationContext.Current;

                ILogCallback callback =
                    ctx.GetCallbackChannel<ILogCallback>();

                if (!_subscribers.ContainsKey(ctx.SessionId))
                {
                    _subscribers.Add(ctx.SessionId, callback);
                    _serverCtrl.LogMessage("New user connected: " + ctx.SessionId);
                }

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Unsubscribe()
        {
            try
            {
                OperationContext ctx = OperationContext.Current;

                if (!_subscribers.ContainsKey(ctx.SessionId))
                    return false;

                _subscribers.Remove(ctx.SessionId);

                _serverCtrl.LogMessage("User disconnected. Id:" + ctx.SessionId);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
