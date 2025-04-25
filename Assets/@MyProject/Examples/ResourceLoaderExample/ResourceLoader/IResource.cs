using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Examples.ResourceLoaderExample
{
    public interface IResource
    {
        string Key { get; }
        Type ResourceType { get; }
        UnityEngine.Object Asset { get; }

        void AddReference(GameObject binder);
        void AddReference(Component binder);
        void AddReference(ICollection<IDisposable> disposables);

        internal void RemoveReference(ResourceReference reference);
        void RemoveReference(GameObject binder);
        void RemoveReference(Component binder);
        void RemoveReference(ICollection<IDisposable> disposables);
    }
}