using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;

    public GameFactory(IAssetProvider assetsProvider ) => 
      _assets = assetsProvider;

    public void Cleanup()
    {
    }

    public GameObject Instantiate(string prefabPath) => 
      _assets.Instantiate(prefabPath);

    public GameObject Instantiate (string prefabPath, Vector3 position) => 
      _assets.Instantiate(prefabPath, position);
  }
}