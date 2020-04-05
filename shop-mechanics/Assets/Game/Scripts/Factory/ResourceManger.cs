using UnityEngine;
using Common.Singleton;

/**

This resource manager only implements UnityEngine Resources API.
Any changes to the API or improvements are done here.

**/
public class ResourceManager : MonoSingleton<ResourceManager>, IAssetManager {

    //We don't initialize resources folder. This is just an implementation for
    //other types of asset management such as asset bundles.
    public void Initialize() {}

    //Simple implementation of resource load. We can implement resource async load
    //in the future. For a minimal version of this, this should suffice.
    public T Load<T>(string path) where T : Object {
        return Resources.Load<T>(path);
    }

    public void Unload(Object obj) {
        Resources.UnloadAsset(obj);
    }
}