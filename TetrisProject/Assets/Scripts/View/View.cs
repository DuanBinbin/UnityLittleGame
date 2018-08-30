using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour {

    private Ctrl ctrl;

    private RectTransform logoName;
    private RectTransform menuUI;
    private RectTransform gameUI;
    private GameObject restartButton;
    private GameObject gameOverUI;
    private GameObject settingUI;
    private GameObject rankUI;

    private Text score;
    private Text highScore;
    private Text gameOverScore;

    private GameObject mute;

    private Text rankScore;
    private Text rankHighScore;
    private Text rankNumbersGame;

	// Use this for initialization
	void Awake ()
    {
        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();

        logoName = transform.Find("Canvas/LogoName") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        restartButton = transform.Find("Canvas/MenuUI/RestartButton").gameObject;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;

        score = transform.Find("Canvas/GameUI/ScoreLabel/Text").GetComponent<Text>();
        highScore = transform.Find("Canvas/GameUI/HighScoreLabel/Text").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/GameOverUI/Text").GetComponent<Text>();

        mute = transform.Find("Canvas/SettingUI/AudioButton/Mute").gameObject;

        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/Text").GetComponent<Text>();
        rankHighScore = transform.Find("Canvas/RankUI/HighScoreLabel/Text").GetComponent<Text>();
        rankNumbersGame = transform.Find("Canvas/RankUI/NumbersGameLabel/Text").GetComponent<Text>();
    }
	
	public void ShowMenu()
    {
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(-160.3f, 0.5f);
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(66.64f, 0.5f);
    }
    public void HideMenu()
    {
        logoName.DOAnchorPosY(160.4f, 0.5f)
            .OnComplete(delegate { logoName.gameObject.SetActive(false); });
        menuUI.DOAnchorPosY(-66.64f, 0.5f)
            .OnComplete(delegate { menuUI.gameObject.SetActive(false); });
    }
    public void UpdateGameUI(int score , int highScore )
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
    }
    public void ShowGameUI(int score=0,int highScore = 0)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-160.3f, 0.5f);
    }
    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(160.4f, 0.5f)
            .OnComplete(delegate { gameUI.gameObject.SetActive(false); });
    }
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }
    public void ShowGameOverUI(int score = 0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }
    public void OnHomeButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnSettingButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(true);
    }
    public void SetMuteActive(bool isActive)
    {
        mute.SetActive(isActive);
    }
    public void OnSettingUIClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(false);
    }
    //public void OnRankButtonClick()
    //{
    //    ctrl.audioManager.PlayCursor();
    //    rankUI.SetActive(true);
    //}
    public void ShowRankUI(int score,int highScore,int numbersGame)
    {
        this.rankScore.text = score.ToString();
        this.rankHighScore.text = highScore.ToString();
        this.rankNumbersGame.text = numbersGame.ToString();
        rankUI.SetActive(true);
    }
    public void OnRankUIClick()
    {
        rankUI.SetActive(false);
    }
}
