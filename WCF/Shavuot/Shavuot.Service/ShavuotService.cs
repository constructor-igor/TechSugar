using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Shavuot.Contract;

namespace Shavuot.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ShavuotService: IShavuotService
    {
        readonly Dictionary<string, IShavuotServiceCallback> m_subscribers;

        public ShavuotService()
        {
            m_subscribers = new Dictionary<string, IShavuotServiceCallback>();
        }

        #region IShavuotService
        public void Greeting(Message message)
        {
            Console.WriteLine($"[ShavuotService.Greeting({GetHashCode()})] {message}");

            OperationContext ctx = OperationContext.Current;

            foreach (var subscriber in m_subscribers)
            {
                try
                {
                    if (ctx.SessionId == subscriber.Key)
                        continue;

                    if (null != subscriber.Value)
                    {
                        Task.Factory.StartNew(() => { subscriber.Value.OnNewMessage(message); });
////                        Thread thread = new Thread(delegate ()
////                        {
////                            subscriber.Value.OnNewMessage(message);
////                        });
//
//                        thread.Start();
                    }
                }
                catch
                {
                }
            }
        }

        public bool Subscribe()
        {
            try
            {
                OperationContext ctx = OperationContext.Current;
                IShavuotServiceCallback callback = ctx.GetCallbackChannel<IShavuotServiceCallback>();
                if (!m_subscribers.ContainsKey(ctx.SessionId))
                {
                    m_subscribers.Add(ctx.SessionId, callback);
                }
                Console.WriteLine($"[ShavuotService.Subscribe({GetHashCode()})] {ctx.SessionId}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Unsubscribe()
        {
            try
            {
                OperationContext ctx = OperationContext.Current;
                if (!m_subscribers.ContainsKey(ctx.SessionId))
                    return false;
                m_subscribers.Remove(ctx.SessionId);
                Console.WriteLine($"[ShavuotService.Unsubscribe({GetHashCode()})] {ctx.SessionId}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
