using System;
using System.Security.Cryptography;
using System.Text;

namespace GameScores.GamesCollector.Worker.WorkingSet;

internal record WorkingSetItem(Uri Url)
{
    private int? _hashCode;
    
    public override int GetHashCode()
    {
        if (_hashCode == null)
        {
            byte[] hash = MD5.HashData(Encoding.UTF8.GetBytes(Url.ToString()));

            _hashCode = BitConverter.ToInt32(hash, 0) & 0x7FFFFFFF;
        }

        return _hashCode.Value;
    }

    public virtual bool Equals(WorkingSetItem? other) => other != null && Url.Equals(other.Url);
}