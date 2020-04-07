using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Gamebase.Application.Utilities
{
    public static class AddressableUtility
    {
        public static bool FindAssetBundlePath(object key, out IList<string> results)
        {
            results = null;
            
            IList<IResourceLocation> locations;
            foreach (var locator in Addressables.ResourceLocators)
            {
                if (locator.Locate(key, typeof(UnityEngine.Object), out locations))
                {
                    results = new List<string>();
                    foreach (var location in locations)
                    {
                        if (Addressables.ResourceManager.GetResourceProvider(typeof(AssetBundleProvider), location) == null)
                        {
                            // AssetDatabase Provider 
                            results.Add(location.InternalId);
                        }
                        else
                        {
                            // AssetBundle Provider
                            foreach (var dependency in location.Dependencies)
                            {
                                results.Add(dependency.InternalId);
                            }
                        }
                    }
                    return true;
                }
            }
            
            return false;
        }
    }
}