using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public StructureDetector detector;

    [Header("Showing Structure")]
    public float m_showingStructureTime = 3.0f;
    public FocusObject showingStructureCamera;
    public UnityEvent onStartShowingStructure;

    [Header("Playing")]
    public Transform detectorPosition;
    public UnityEvent onStartPlaying;

    [Header("GameOver")]
    public UnityEvent onGameOver;

    public enum GameState
    {
        SHOWING_STRUCTURE,
        PLAYING,
        GAME_OVER,
    }
    GameState m_gameState;
    float m_acumTime = 0;
    Camera m_mainCamera;

	// Use this for initialization
	void Start () 
    {
        onStartShowingStructure.Invoke();
        m_mainCamera = Camera.main;
        m_mainCamera.enabled = false;
        m_gameState = GameState.SHOWING_STRUCTURE;
        showingStructureCamera.StartFocusMove();
        detector.Scan();
        m_acumTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        switch (m_gameState)
        {
            case GameState.SHOWING_STRUCTURE:
                ShowingStructureUpdate();
                break;
            case GameState.PLAYING:
                PlayingUpdate();
                break;
            case GameState.GAME_OVER:
                break;
            default:
                break;
        }
	}

    public void GameOver()
    {
        if (m_gameState == GameState.PLAYING)
        {   
            detector.Scan();
            if (detector.Compare())
            {
                onGameOver.Invoke();
                m_gameState = GameState.GAME_OVER;
                SceneManager.LoadScene("MainMenu");
            }
        }

        
    }

    private void PlayingUpdate()
    {
        
    }

    private void ShowingStructureUpdate()
    {
        m_acumTime += Time.deltaTime;
        if (m_acumTime > m_showingStructureTime)
        {
            showingStructureCamera.EndFocusMove();
            m_mainCamera.enabled = true;
            m_gameState = GameState.PLAYING;
            onStartPlaying.Invoke();
            detector.transform.position = detectorPosition.position;
        }
    }

    public void StartGame()
    {

    }
}
