using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace MyProject.Examples.ResourceLoaderExample
{
    public class Resource<T> : IResource where T : UnityEngine.Object
    {
        private readonly List<ResourceReference> _references = new();

        public string Key { get; }
        public Type ResourceType { get; }
        public T AssetTyped { get; }
        public UnityEngine.Object Asset => AssetTyped;

        internal bool HasNoReferences => _references.Count == 0;

        public Resource(string key, T asset)
        {
            Key = key;
            ResourceType = typeof(T);
            AssetTyped = asset;
        }

        public void AddReference(GameObject binder)
        {
            if (!binder) return;

            var reference = new ResourceReference(this, binder);
            reference.AddTo(binder);
            _references.Add(reference);
        }

        public void AddReference(Component binder)
        {
            if (!binder) return;
            AddReference(binder.gameObject);
        }

        public void AddReference(ICollection<IDisposable> disposables)
        {
            if (disposables == null) return;

            var reference = new ResourceReference(this, disposables);
            reference.AddTo(disposables);
            _references.Add(reference);
        }

        void IResource.RemoveReference(ResourceReference reference)
        {
            _references.Remove(reference);
        }

        public void RemoveReference(GameObject binder)
        {
            if (binder == null) return;
            foreach (var r in _references.Where(r => r.Target.Equals(binder)))
                r.Dispose();
        }

        public void RemoveReference(Component binder)
        {
            if (binder == null) return;
            RemoveReference(binder.gameObject);
        }

        public void RemoveReference(ICollection<IDisposable> disposables)
        {
            if (disposables == null) return;
            foreach (var r in _references.Where(r => r.Target.Equals(disposables)))
            {
                r.Dispose();
                disposables.Remove(r);
            }
        }
    }
}