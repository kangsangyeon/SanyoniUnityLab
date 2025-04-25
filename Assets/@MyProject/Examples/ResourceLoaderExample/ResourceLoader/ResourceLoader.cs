using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace MyProject.Examples.ResourceLoaderExample
{
    public class ResourceLoader
    {
        private readonly IResourceProvider _provider;

        // 타입별 캐시: Dictionary<(Type, key), asset>
        private readonly Dictionary<(Type, string), UnityEngine.Object> _cache = new();
        private readonly Dictionary<(Type, string), UniTask<UnityEngine.Object>> _loading = new();

        public ResourceLoader(IResourceProvider provider)
        {
            _provider = provider;
        }

        public Resource<T> Load<T>(string key) where T : UnityEngine.Object
        {
            var tKey = (typeof(T), key);

            if (_cache.TryGetValue(tKey, out var cached))
                return (Resource<T>)cached;

            var asset = _provider.Load<T>(key);
            var res = new Resource<T>(key, asset);
            _cache[tKey] = res;
            return res;
        }

        public async UniTask<Resource<T>> LoadAsync<T>(string key) where T : UnityEngine.Object
        {
            var tKey = (typeof(T), key);

            if (_cache.TryGetValue(tKey, out var cached))
                return (Resource<T>)cached;

            if (_loading.TryGetValue(tKey, out var task))
                return (Resource<T>)await task;

            var loadTask = _provider.LoadAsync<T>(key).ContinueWith(asset =>
            {
                var r = new Resource<T>(key, asset);
                _cache[tKey] = r;
                _loading.Remove(tKey);
                return (IResource)r;
            });

            _loading[tKey] = loadTask;
            return (Resource<T>)await loadTask;
        }

        public void Unload<T>(string key) where T : UnityEngine.Object
        {
            var tKey = (typeof(T), key);

            if (_cache.TryGetValue(tKey, out var asset))
            {
                _provider.Unload(asset);
                _cache.Remove(tKey);
            }

            _loading.Remove(tKey);
        }

        public void UnloadAll()
        {
            foreach (var pair in _cache)
            {
                _provider.Unload(pair.Value);
            }

            _cache.Clear();
            _loading.Clear();
        }
    }
}