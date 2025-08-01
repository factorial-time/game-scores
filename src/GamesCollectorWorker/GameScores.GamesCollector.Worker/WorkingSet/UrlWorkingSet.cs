using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameScores.GamesCollector.Worker.WorkingSet;

internal sealed class UrlWorkingSet
{
    private readonly ConcurrentBag<WorkingSetItem> _workingSet = new();
    
    public event EventHandler<EventArgs>? WorkingSetChanged;

    private static readonly Lazy<UrlWorkingSet> _instance = new(() => new UrlWorkingSet());
    
    public static UrlWorkingSet Instance => _instance.Value;

    private UrlWorkingSet()
    {
    }
    
    public void Refresh(IEnumerable<Uri> urls)
    {
        _workingSet.Clear();
        foreach (Uri url in urls)
        {
            _workingSet.Add(new(url));
        }

        EventHandler<EventArgs>? handler = Volatile.Read(ref WorkingSetChanged);
        if (handler != null)
        {
            handler(this, EventArgs.Empty);
        }
    }

    public IEnumerable<Uri> GetAssignedUrls(int idx, int total) => _workingSet
        .Where(setItem => setItem.GetHashCode() % total == idx)
        .Select(setItem => setItem.Url);
}