using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
  public class HUDLoading : MonoBehaviour
  {
    [SerializeField] private Image progressImage;
    [SerializeField] private float speed;

    private float endValue;
    private Action callback;
    private AsyncOperation waitNextScene;

    private void Update()
    {
      endValue = waitNextScene.progress;

      UpdateProgress(Mathf.MoveTowards(progressImage.fillAmount, endValue, speed * Time.deltaTime));

      if (progressImage.fillAmount >= 1)
          OnFinishLoad();
    }

    public void StartLoad(AsyncOperation waitNextScene, Action callback)
    {
      this.callback = callback;
      this.waitNextScene = waitNextScene;
      
      progressImage.fillAmount = 0;
      gameObject.SetActive(true);
    }

    private void UpdateProgress(float progress) => 
      progressImage.fillAmount = progress;

    private void OnFinishLoad()
    {
      callback?.Invoke();
      gameObject.SetActive(false);
    }
  }
}