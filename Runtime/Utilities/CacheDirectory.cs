using System.IO;
using UniRx.Async;

namespace Gamebase.Utilities
{
    public sealed class CacheDirectory
    {
        private readonly string Root;

        public CacheDirectory(string path)
        {
            Root = Path.Combine(
                UnityEngine.Application.temporaryCachePath,
                path);

            if (!Directory.Exists(Root))
            {
                Directory.CreateDirectory(Root);
            }
        }

        public string FullPath(string path)
        {
            return Path.Combine(Root, path);
        }
        
        public bool Exists(string fileName)
        {
            return File.Exists(FullPath(fileName));
        }
        
        public void Save(string fileName, byte[] bytes)
        {
            File.WriteAllBytes(FullPath(fileName), bytes);
        }
        
        public async UniTask SaveAsync(string fileName, byte[] bytes)
        {
            await UniTask.Run(() => Save(fileName, bytes));
        }
        
        public void Clear()
        {
            Directory.Delete(Root, true);
        }
    }
}