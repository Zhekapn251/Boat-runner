using System.Collections;
using UnityEngine;

namespace Infrastructure
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup Curtain;
    private float _curtainAlphaDecrement = 0.03f;
    private float _timeDelay = 0.01f;

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= _curtainAlphaDecrement;
        yield return new WaitForSeconds(_timeDelay);
      }
      
      gameObject.SetActive(false);
    }
  }
}