using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Instantiate(string address) =>
      Resources.Load<GameObject>(address);
  }
}