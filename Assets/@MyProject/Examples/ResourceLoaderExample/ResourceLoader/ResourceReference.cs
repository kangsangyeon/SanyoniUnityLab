using System;

namespace MyProject.Examples.ResourceLoaderExample
{
    internal sealed class ResourceReference : IDisposable
    {
        private readonly IResource _resource;
        private readonly object _target;
        private bool _disposed;

        public object Target => _target;

        internal ResourceReference(IResource resource, object target)
        {
            _resource = resource;
            _target = target;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            _resource.RemoveReference(this);
        }
    }
}