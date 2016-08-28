using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public StructureDetector detector;
    public Message messageComponent;

    [Header("Showing Structure")]
    public float m_showingStructureTime = 3.0f;
    public FocusObject showingStructureCamera;
    public UnityEvent onStartShowingStructure;

    [Header("Playing")]
    public Transform structureUndonePrefab;
    public Transform structureUndonePosition;
    public Transform detectorPosition;
    public UnityEvent onStartPlaying;

    [Header("ShowingAward")]
    public float m_showingAwardTime = 5.0f;

    [Header("GameOver")]
    public UnityEvent onGameOver;

    public enum GameState
    {
        SHOWING_STRUCTURE,
        PLAYING,
        SHOWING_AWARD,
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
        messageComponent.SetMessage("Try to build this structure", m_showingStructureTime);
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
            case GameState.SHOWING_AWARD:
                ShowingAwardUpdate();
                break;
            case GameState.GAME_OVER:
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                break;
        }
	}

    private void ShowingAwardUpdate()
    {
        m_acumTime += Time.deltaTime;
        if (m_acumTime > m_showingAwardTime)
        {
            m_gameState = GameState.GAME_OVER;
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
                m_gameState = GameState.SHOWING_AWARD;
                showingStructureCamera.target = structureUndonePosition;
                showingStructureCamera.transform.position = m_mainCamera.transform.position;
                showingStructureCamera.StartFocusMove();
                messageComponent.SetMessage("Congratulation!", m_showingAwardTime);
                m_mainCamera.enabled = false;
                m_acumTime = 0;
            }
            else
            {
                messageComponent.SetMessage("Sorry, The structure is a fucking shit!", m_showingAwardTime);
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

    public void ResetGame()
    {
        Vector3 pos = structureUndonePosition.position;
        Destroy(structureUndonePosition.gameObject);
        structureUndonePosition = Instantiate(structureUndonePrefab);
        structureUndonePosition.position = pos;
        
    }
}
