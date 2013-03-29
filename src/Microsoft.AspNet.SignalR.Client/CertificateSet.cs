using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AspNet.SignalR.Client
{
    class CertificateSet : ICollection<X509Certificate>
    {
        private HashSet<X509Certificate> _certHashSet;
        private readonly IConnection _connection;

        public CertificateSet(IConnection connection)
        {
            _connection = connection;
            _certHashSet = new HashSet<X509Certificate>();
        }

        public void Add(X509Certificate item)
        {
            EnsureConnectionDisconnected();
            _certHashSet.Add(item);
        }

        public void Clear()
        {
            EnsureConnectionDisconnected();
            _certHashSet.Clear();
        }

        public bool Contains(X509Certificate item)
        {
            return _certHashSet.Contains(item);
        }

        public void CopyTo(X509Certificate[] array, int arrayIndex)
        {
            _certHashSet.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _certHashSet.Count; }
        }

        public bool IsReadOnly
        {
            get { return _connection.State != ConnectionState.Disconnected; }
        }

        public bool Remove(X509Certificate item)
        {
            EnsureConnectionDisconnected();
            return _certHashSet.Remove(item);
        }

        public IEnumerator<X509Certificate> GetEnumerator()
        {
            return _certHashSet.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _certHashSet.GetEnumerator();
        }

        private void EnsureConnectionDisconnected()
        {
            if (_connection.State != ConnectionState.Disconnected)
            {
                throw new InvalidOperationException(Resources.Error_CertsCanOnlyBeAddedWhenDisconnected);
            }
        }
    }
}
