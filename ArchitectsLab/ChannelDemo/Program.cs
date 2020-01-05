using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 *
 * https://devblogs.microsoft.com/dotnet/an-introduction-to-system-threading-channels/
 *
 */

namespace ChannelDemo
{
    public sealed class Channel<T>
    {
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);
        public void Write(T value)
        {
            _queue.Enqueue(value); // store the data
            _semaphore.Release(); // notify any consumers that more data is available
        }

        public async Task<T> ReadAsync(CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false); // wait
            bool gotOne = _queue.TryDequeue(out T item); // retrieve the data
            Debug.Assert(gotOne);
            return item;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
