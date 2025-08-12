using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Monitoring;

public class ConnectedUsersStore
{
    private readonly HashSet<Guid> _connectedUsers = new();
    private readonly object _lockObj = new();

    public void Add(Guid userId)
    {
        lock (_lockObj)
        {
            _connectedUsers.Add(userId);
        }
    }

    public void Remove(Guid userId)
    {
        lock (_lockObj)
        {
            _connectedUsers.Remove(userId);
        }
    }

    public List<Guid> GetAll()
    {
        lock (_lockObj)
        {
            return _connectedUsers.ToList();
        }
    }
}
