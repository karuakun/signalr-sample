using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;

namespace signalsample.Hubs
{
    public class ProcessHub: Hub
    {
        public ChannelReader<ProcessInfo[]> Counter(int count, int delay)
        {
            var channel = Channel.CreateUnbounded<ProcessInfo[]>();

            _ = WriteItems(channel.Writer, count, delay);

            return channel.Reader;
        }

        private async Task WriteItems(ChannelWriter<ProcessInfo[]> writer, int count, int delay)
        {
            for (var i = 0; i < count; i++)
            {
                var p1 = Process.GetProcesses();
                var p2 = p1.OrderByDescending(_ => _.WorkingSet64);
                var p3 = p2.Select(_ => new ProcessInfo
                {
                    ProcessName = _.ProcessName,
                    WorkingSet = _.WorkingSet64,
//                    TotalProcessorTime = _.TotalProcessorTime
                });
                var p4 = p3.ToArray();
                //var processes = Process.GetProcesses()
                //    .OrderByDescending(p => p.TotalProcessorTime)
                //    .Select(p => new ProcessInfo
                //    {
                //        ProcessName = p.ProcessName,
                //        WorkingSet = p.WorkingSet64,
                //        TotalProcessorTime = p.TotalProcessorTime
                //    }).ToArray();

                await writer.WriteAsync(p4);
                await Task.Delay(delay);
            }
            writer.TryComplete();
        }
    }

    public class ProcessInfo
    {
        public string ProcessName { get; set; }
        public long WorkingSet { get; set; }
    }
}
