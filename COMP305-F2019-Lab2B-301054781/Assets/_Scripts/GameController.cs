/*
 * By: Siying Li
 * Student Number: 301054781
 * Date: 2019-11-09
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;

public class GameController : MonoBehaviour
{
    [Header("Scene Game Objects")]
    public GameObject cloud;
    public GameObject island;
    public int numberOfClouds;
    public List<GameObject> clouds;


    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;

    

    //public HighScoreSO highScoreSO;

    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;

    [Header("Game Settings")]
    public ScoreBoard scoreBoard;

    [Header("Scene Settings")]
    public SceneSettings activeSceneSettings;
    public List<SceneSettings> sceneSettings;
   

    // public properties
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            scoreBoard.lives = _lives;
            if(_lives < 1)
            {
                
                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }
           
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            scoreBoard.score = _score;

            if (scoreBoard.highScore < _score)
            //if (highScoreSO.score < _score)
            {
                scoreBoard.highScore = _score;
                //highScoreSO.score = _score;
            }
            scoreLabel.text = "Score: " + _score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
    }

    private void GameObjectInitialization()
    {
        //scoreBoard = GameObject.Find("ScoreBoard");

        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");

        //scoreBoard = Resources.FindObjectsOfTypeAll<ScoreBoard>()[0] as ScoreBoard;
        
    }


    private void SceneConfiguration()
    {
        //selects the current scene
        Scene SceneToComparable = (Scene)Enum.Parse(typeof(Scene),
                    value: SceneManager.GetActiveScene().name.ToUpper());
        //compares the settings list with the current scene
        var query = from settings in sceneSettings
                    where settings.scene == SceneToComparable
                    select settings;

        //sets the appropriate settings for the loaded scene
        activeSceneSettings = query.ToList().First();
        {
            //checks if Main scene is active and sets up initial lives and score
            if (activeSceneSettings.scene == Scene.MAIN)
            {
                Lives = 5;
                Score = 0;
            }

            //applies all scne settings from the scriptable object from scene settings
            activeSoundClip = activeSceneSettings.activeSoundClip;
            scoreLabel.enabled = activeSceneSettings.scoreLabelEnabled;
            livesLabel.enabled = activeSceneSettings.livesLabelEnabled;
            highScoreLabel.enabled = activeSceneSettings.highScoreLabelEnabled;
            startLabel.SetActive(activeSceneSettings.startLabelActive);
            endLabel.SetActive(activeSceneSettings.endLabelActive);
            startButton.SetActive(activeSceneSettings.startButtonActive);
            restartButton.SetActive(activeSceneSettings.restartButtonActive);

            //Assigns test values to labels from the scoreboard Scriptable object
            highScoreLabel.text = "High Score: " + scoreBoard.highScore;
            livesLabel.text = "Lives: " + scoreBoard.lives;
            highScoreLabel.text = "Score: " + scoreBoard.highScore;

        }

        //switch (SceneManager.GetActiveScene().name)
        //{
        //    case "Start":
        //        scoreLabel.enabled = false;
        //        livesLabel.enabled = false;
        //        highScoreLabel.enabled = false;
        //        endLabel.SetActive(false);
        //        restartButton.SetActive(false);
        //        activeSoundClip = SoundClip.NONE;
        //        break;
        //    case "Main":
        //        highScoreLabel.enabled = false;
        //        startLabel.SetActive(false);
        //        startButton.SetActive(false);
        //        endLabel.SetActive(false);
        //        restartButton.SetActive(false);
        //        activeSoundClip = SoundClip.ENGINE;
        //        break;
        //    case "End":
        //        scoreLabel.enabled = false;
        //        livesLabel.enabled = false;
        //        startLabel.SetActive(false);
        //        startButton.SetActive(false);
        //        activeSoundClip = SoundClip.NONE;
        //        highScoreLabel.text = "High Score: " + scoreBoard.highScore;
        //        break;
        //}




        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }



        // creates an empty container (list) of type GameObject
        clouds = new List<GameObject>();

        for (int cloudNum = 0; cloudNum < numberOfClouds; cloudNum++)
        {
            clouds.Add(Instantiate(cloud));
        }

        Instantiate(island);
    }


    // Event Handlers
    public void OnStartButtonClick()
    {
        
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
