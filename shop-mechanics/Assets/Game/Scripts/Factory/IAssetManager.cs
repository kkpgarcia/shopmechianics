public interface IAssetManager
{
    void Initialize();
    T Load<T>(string path) where T : UnityEngine.Object;
    void Unload(UnityEngine.Object obj);
}