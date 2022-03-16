using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    #region scene links
    public Image TransitionImg1;
    public Image TransitionImg2;
    public GameObject StartBtn;
    public GameObject OptionsBtn;
    public GameObject ExitBtn;
    public Toggle SoundToggle;
    public Toggle VibrationToggle;
    public GameObject ButtonSet1;
    public GameObject ButtonSet2;
    #endregion region

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);
        if (!PlayerPrefs.HasKey("Vibration"))
            PlayerPrefs.SetInt("Vibration", 1);
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);

        RevelMenuEffect();
    }

    void Start()
    {
        RandomizeBtnsAnimOffset();
    }

    //------------scene ref voids------------

    public void OnSoundToggleClick()
    {
        PlayerPrefs.SetInt("Sound", SoundToggle.isOn ? 1 : 0);
    }

    public void OnVibrationToggleClick()
    {
        PlayerPrefs.SetInt("Vibration", VibrationToggle.isOn ? 1 : 0);
    }

    public void OnPlayBtnClick()
    {
        StartCoroutine(StartPlayingC());
    }

    public void OnExitBtnClick()
    {
        Application.Quit(0);
    }

    #region UI Effects
    void RandomizeBtnsAnimOffset()
    {
        StartCoroutine(RandomizeBtnsAnimOffsetC());
    }
    void RevelMenuEffect()
    {
        StartCoroutine(RevelMenuEffectC());
    }
    //scene ref void
    public void OnEnterOptionsClick()
    {

        StartCoroutine(EnterOptionsC());
    }
    //scene ref void
    public void OnExitOptionsClick()
    {
        StartCoroutine(ExitOptionsC());
    }

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
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 2.0f/*secs*/)
        {
            TransitionImg1.color = Color.Lerp(_alpha1, _alpha0, i);
            yield return null;
        }

        yield return new WaitForSeconds(0.7f);

        //hide 2nd block
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 1.0f)
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
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
        {
            TransitionImg2.color = Color.Lerp(_alpha0, _alpha1, i);
            yield return null;
        }

        SoundToggle.isOn = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
        VibrationToggle.isOn = PlayerPrefs.GetInt("Vibration") == 1 ? true : false;

        ButtonSet1.SetActive(false);
        ButtonSet2.SetActive(true);

        //hide "hider"
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
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
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
        {
            TransitionImg2.color = Color.Lerp(_alpha0, _alpha1, i);
            yield return null;
        }

        ButtonSet1.SetActive(true);
        ButtonSet2.SetActive(false);

        //hide "hider"
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
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
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 0.5f)
        {
            TransitionImg1.color = Color.Lerp(_alpha0, _alpha1, i);
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    #endregion
}
