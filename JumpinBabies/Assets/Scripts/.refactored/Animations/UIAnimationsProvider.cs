using System;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UIAnimations.DelegatePlayerAnimations
{
     public class ScreenAnimations
     {
          //can create canvas with image or create image on existing canvas
          private void DimScreen(float duration)
          {
               Image solidBlackImage = null;
               solidBlackImage.CrossFadeAlpha(1.0f, duration, true);
          }
          //or bind to existing image
          private void DimScreen1(float duration)
          {
               Image solidBlackImage = null;
               solidBlackImage.CrossFadeAlpha(1.0f, duration, true);
          }
          //can only bind to existing image
          private void BrightenScreen(Transform canvas)
          {
               var scr = new GameObject();
               var f = scr.AddComponent<Image>();
               f.sprite = Sprite.Create(Texture2D.blackTexture, new Rect(Vector2.zero, Vector2.one), Vector2.zero);
               scr.transform.SetParent(canvas);

               f.CrossFadeAlpha(0, 5, true);
          }
     }
}

sealed class UIAnimationsProvider
{
     GameObject StartBtn;
     GameObject OptionsBtn;
     GameObject ExitBtn;
     GameObject ButtonSet1;
     GameObject ButtonSet2;
     Image TransitionImg1;
     Image TransitionImg2;
     Color _alpha0;
     Color _alpha1;
     Toggle SoundToggle;
     Toggle VibrationToggle;

     //void RandomizeBtnsAnimOffset()
     //{
     //     StartCoroutine(RandomizeBtnsAnimOffsetC());
     //}
     //void RevelMenuEffect()
     //{
     //     StartCoroutine(RevelMenuEffectC());
     //}

     IEnumerator RandomizeBtnsAnimOffsetC()
     {
          StartBtn.GetComponent<Animator>().enabled = true;
          yield return new WaitForSeconds(0.5f);
          OptionsBtn.GetComponent<Animator>().enabled = true;
          yield return new WaitForSeconds(0.1f);
          ExitBtn.GetComponent<Animator>().enabled = true;
     }

     IEnumerator RevelMenuEffectC()
     {
          Color _alpha1 = new Color(0, 0, 0, 1);
          Color _alpha0 = new Color(0, 0, 0, 0);

          //hide 1st block
          for(float i = 0; i < 1.0f; i += Time.deltaTime / 2.0f/*secs*/)
          {
               TransitionImg1.color = Color.Lerp(_alpha1, _alpha0, i);
               yield return null;
          }

          yield return new WaitForSeconds(0.7f);

          //hide 2nd block
          for(float i = 0; i < 1.0f; i += Time.deltaTime / 1.0f)
          {
               TransitionImg2.color = Color.Lerp(_alpha1, _alpha0, i);
               yield return null;
          }

          TransitionImg2.raycastTarget = false;
     }
     IEnumerator EnterOptionsC()
     {
          Color _alpha0 = new Color(0, 0, 0, 0);
          Color _alpha1 = new Color(0, 0, 0, 1);

          TransitionImg2.raycastTarget = true;

          //reveal "hider"
          for(float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
          {
               TransitionImg2.color = Color.Lerp(_alpha0, _alpha1, i);
               yield return null;
          }

          SoundToggle.isOn = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
          VibrationToggle.isOn = PlayerPrefs.GetInt("Vibration") == 1 ? true : false;

          ButtonSet1.SetActive(false);
          ButtonSet2.SetActive(true);

          //hide "hider"
          for(float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
          {
               TransitionImg2.color = Color.Lerp(_alpha1, _alpha0, i);
               yield return null;
          }

          TransitionImg2.raycastTarget = false;
     }
     IEnumerator ExitOptionsC()
     {
          Color _alpha0 = new Color(0, 0, 0, 0);
          Color _alpha1 = new Color(0, 0, 0, 1);

          TransitionImg2.raycastTarget = true;
          //reveal "hider"
          for(float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
          {
               TransitionImg2.color = Color.Lerp(_alpha0, _alpha1, i);
               yield return null;
          }

          ButtonSet1.SetActive(true);
          ButtonSet2.SetActive(false);

          //hide "hider"
          for(float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
          {
               TransitionImg2.color = Color.Lerp(_alpha1, _alpha0, i);
               yield return null;
          }

          TransitionImg2.raycastTarget = false;
     }
     IEnumerator StartPlayingC()
     {
          Color _alpha0 = new Color(0, 0, 0, 0);
          Color _alpha1 = new Color(0, 0, 0, 1);

          TransitionImg1.raycastTarget = true;
          //reveal "hider"
          for(float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
          {
               TransitionImg1.color = Color.Lerp(_alpha0, _alpha1, i);
               yield return null;
          }
          UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
     }
}
