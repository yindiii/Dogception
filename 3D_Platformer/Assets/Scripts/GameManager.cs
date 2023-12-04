using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject startingScreenCanvas;
    public GameObject winningScreenCanvas;
    public GameObject losingScreenCanvas;
    public GameObject Player;
    public GameObject EnemySpawner;
    public GameObject InGameUI;
    public Camera UICamera; 
    public Camera playerCamera;

    private AudioSource audioSource;
    public AudioClip die;
    public AudioClip win;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

        UICamera.gameObject.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //startingScreenCanvas = GameObject.Find("StartingScreenCanvas");
        //winningScreenCanvas = GameObject.Find("WinningScreenCanvas");
        //losingScreenCanvas = GameObject.Find("LosingScreenCanvas");
        //Player = GameObject.Find("Player");
        //EnemySpawner = GameObject.Find("EnemySpawner");
        //InGameUI = GameObject.Find("InGameUI");
        //UICamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        //playerCamera = GameObject.FindWithTag("Player").GetComponentInChildren<Camera>();

        ShowStartingScreen();
        Player.SetActive(false);
        EnemySpawner.SetActive(false);
        InGameUI.SetActive(false);
    }

    public void ShowStartingScreen()
    {
        startingScreenCanvas.SetActive(true);
        winningScreenCanvas.SetActive(false);
        losingScreenCanvas.SetActive(false);
    }

    public void ShowWinningScreen()
    {
        winningScreenCanvas.SetActive(true);
        startingScreenCanvas.SetActive(false);
        losingScreenCanvas.SetActive(false);
        Player.SetActive(false);
        EnemySpawner.SetActive(false);
        InGameUI.SetActive(false);
        UICamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);
        EnemySpawner.GetComponent<EnemySpawner>().StopSpawning();
        //Cursor.lockState = CursorLockMode.Confined;
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.clip = win;
            audioSource.Play();
        }
    }

    public void ShowLosingScreen()
    {
        losingScreenCanvas.SetActive(true);
        startingScreenCanvas.SetActive(false);
        winningScreenCanvas.SetActive(false);
        Player.SetActive(false);
        EnemySpawner.SetActive(false);
        InGameUI.SetActive(false);
        UICamera.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(false);
        EnemySpawner.GetComponent<EnemySpawner>().StopSpawning();
        //Cursor.lockState = CursorLockMode.Confined;
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.clip = die;
            audioSource.Play();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        startingScreenCanvas.SetActive(false);
        winningScreenCanvas.SetActive(false);
        losingScreenCanvas.SetActive(false);
        Player.SetActive(true);
        EnemySpawner.SetActive(true);
        InGameUI.SetActive(true);
        UICamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
    }
}
