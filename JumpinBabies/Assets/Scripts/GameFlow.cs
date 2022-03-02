using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow:MonoBehaviour
{
     #region scene links
     public Image TransitionIMG;

     public GameObject PlayerGO;
     public GameObject FlameGO;
     public GameObject AmbulanceGO;
     public GameObject Window1;
     public GameObject Window2;
     public GameObject Window3;

     public Transform[] WalkingPoints;

     public GameObject UIButtons;
     public Text ScoreText;
     public GameObject PauseScreen;
     public GameObject PauseButton;
     public GameObject LivesContainer;
     #endregion

     [HideInInspector]
     /// <summary>
     /// variants: 0,1,2 or -1 when moving
     /// </summary>
     public int curPlayerPos = -1;
     float playerMovingSpeed = 0.1f;
     int highscore = 0;
     int score = 0;
     int lives = 5;
     bool paused = false;
     public int flyingBabyCount = 0;
     public bool SoundOn;
     public bool VibrationOn;

     private void Awake()
     {
          SoundOn = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
          VibrationOn = PlayerPrefs.GetInt("Vibration") == 1 ? true : false;

          StartCoroutine(RevealGameEffect());
     }

     void Start()
     {
          CleanScene();
     }

     //------scene ref voids------

     public void OnStartGameClick()
     {
          StartCoroutine(StartGameC());
     }
     public void OnExitBtnClick()
     {
          UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
     }
     public void OnTogglePauseClick()
     {
          if(!paused)
          {
               paused = true;
               Time.timeScale = 0;
               PauseScreen.SetActive(true);
               PauseButton.transform.GetChild(0).GetComponent<Text>().text = "Continue";
          }
          else
          {
               paused = false;
               Time.timeScale = 1;
               PauseScreen.SetActive(false);
               PauseButton.transform.GetChild(0).GetComponent<Text>().text = "Pause";
          }
     }

     //------ui------
     void CleanScene()
     {
          FlameGO.SetActive(false);
          FlameGO.transform.position += Vector3.left * 5.0f;
          PlayerGO.SetActive(false);
          PlayerGO.transform.position += Vector3.right * 20.0f;
          AmbulanceGO.SetActive(false);
          AmbulanceGO.transform.position += Vector3.right * 4.0f;
     }
     IEnumerator RevealGameEffect()
     {
          Color _alpha1 = new Color(0, 0, 0, 1);
          Color _alpha0 = new Color(0, 0, 0, 0);

          highscore = PlayerPrefs.GetInt("HighScore");
          ScoreText.text = "Highscore : " + highscore;

          for(float i = 0; i < 1.0f; i += Time.deltaTime / 2.0f/*secs*/)
          {
               TransitionIMG.color = Color.Lerp(_alpha1, _alpha0, i);
               yield return null;
          }
          TransitionIMG.raycastTarget = false;
     }
     IEnumerator StartGameC()
     {
          //---prepare GOs---
          LivesContainer.SetActive(true);
          UIButtons.SetActive(false);

          ScoreText.text = "Score : 0";

          if(SoundOn)
               this.gameObject.GetComponent<AudioSource>().Play();

          Vector3 _flameSrcPos = FlameGO.transform.position;
          Vector3 _flameDstPos = _flameSrcPos + Vector3.right * 5.0f;
          Vector3 _playerSrcPos = PlayerGO.transform.position;
          Vector3 _playerDstPos = _playerSrcPos + Vector3.left * 20.0f;
          Vector3 _ambSrcPos = AmbulanceGO.transform.position;
          Vector3 _ambDstPos = _ambSrcPos + Vector3.left * 4.0f;

          yield return new WaitForSeconds(4.8f);

          FlameGO.SetActive(true);
          PlayerGO.SetActive(true);
          AmbulanceGO.SetActive(true);

          if(SoundOn)
               FlameGO.GetComponent<AudioSource>().Play();

          for(float i = 0; i < 1.0f; i += Time.deltaTime)
          {
               FlameGO.transform.position = Vector3.Lerp(_flameSrcPos, _flameDstPos, i);
               PlayerGO.transform.position = Vector3.Lerp(_playerSrcPos, _playerDstPos, i);
               AmbulanceGO.transform.position = Vector3.Lerp(_ambSrcPos, _ambDstPos, i);
               yield return null;
          }
          PlayerGO.transform.position = _playerDstPos;

          //---init---
          curPlayerPos = 0;
          PlayerGO.AddComponent<PlayerInput>().gameFlowRef = this;
          //_pi.gameFlowRef = this;
          Window1.AddComponent<BabyThrower>().gameFlowRef = this;
          //Window2.AddComponent<BabyThrower>().gameFlowRef = this;
          Window3.AddComponent<BabyThrower>().gameFlowRef = this;
          PauseButton.SetActive(true);
          ScoreText.gameObject.SetActive(true);
     }

     //------game------

     public void MovePlayerLeft()
     {
          if(curPlayerPos != -1)
               StartCoroutine(MovePlayerLeftC());
     }
     public void MovePlayerRight()
     {
          if(curPlayerPos != -1)
               StartCoroutine(MovePlayerRightC());
     }
     IEnumerator MovePlayerRightC()
     {
          int _oldPos = curPlayerPos;
          int _newPos = curPlayerPos != 2 ? ++curPlayerPos : 0;//(curPlayerPos + 1) % 3;
          curPlayerPos = -1;

          for(float i = 0; i < 1.0f; i += Time.deltaTime / playerMovingSpeed)
          {
               PlayerGO.transform.position = Vector3.Lerp(WalkingPoints[_oldPos].position, WalkingPoints[_newPos].position, i);
               yield return null;
          }
          PlayerGO.transform.position = WalkingPoints[_newPos].position;

          curPlayerPos = _newPos;
     }
     IEnumerator MovePlayerLeftC()
     {
          int _oldPos = curPlayerPos;
          int _newPos = curPlayerPos != 0 ? --curPlayerPos : 2;//(curPlayerPos - 1);
          curPlayerPos = -1;

          for(float i = 0; i < 1.0f; i += Time.deltaTime / playerMovingSpeed)
          {
               PlayerGO.transform.position = Vector3.Lerp(WalkingPoints[_oldPos].position, WalkingPoints[_newPos].position, i);
               yield return null;
          }
          PlayerGO.transform.position = WalkingPoints[_newPos].position;

          curPlayerPos = _newPos;
     }
     public void AddPointToPlayer()
     {
          score += 1;
          ScoreText.text = "Score : " + score.ToString();

          //hook
          if(lives < 6 && score % 3 == 0)
               IncrementLives();
     }
     void IncrementLives()
     {
          lives += 1;
          Instantiate(LivesContainer.transform.GetChild(0).gameObject, LivesContainer.transform).SetActive(true);
     }
     public void DecrementLives()
     {
          if(VibrationOn)
               Handheld.Vibrate();

          if(lives == 0)
          {
               //game over
               if(score > highscore)
                    PlayerPrefs.SetInt("HighScore", score);

               UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
          }
          else
          {
               lives -= 1;
               Destroy(LivesContainer.transform.GetChild(lives + 1).gameObject);
          }

     }
}