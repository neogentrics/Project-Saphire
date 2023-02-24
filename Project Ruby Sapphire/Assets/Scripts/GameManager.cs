using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game State Events
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnpause;

    private enum State
    {
        WaitingToStart,
        GamePlaying,
        Gameover
    }

    private State state;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;

        state = State.WaitingToStart;

        if (GameManager.Instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextmanager.gameObject);
            Destroy(hud);
            Destroy(options);
            Destroy(controls);
            return;
        }

        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {

        //Zmatias
        //Moved new XPSystem method from update to start.
        //When games fires up it creates one Class of type XPsystem
        //Any changes that are happening durin runtime will be save on class "level"
        //Previously, you were creating for each frame new level so the data from previous frame were lost.
        level = new XPSystem(StartIngLevel, Player.Instance.OnLevelUp);

        //Zmatias
        //When gameplay start at the begining UI should be set based on Level
        //XPSystem_Startup();

        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

    }

    private void Update()
    {        

        switch (state)
        {
            case State.WaitingToStart:

                state = State.GamePlaying;
                break;            
            case State.GamePlaying:

                goldHUDText.text = Instance.gold.ToString() + " :Gold Coins";
                rubyHUDText.text = Instance.ruby.ToString() + " :Ruby Coins";
                levelText.text = "LEVEL " + level.playerLevel.ToString();
                hitpointText.text = "Health " + Player.Instance.hitpoints.ToString() + " / " + player.maxHitpoint.ToString();

                break;
            case State.Gameover:
                deathMenu.SetActive(true);

                Time.timeScale = 1.0f;                
                player.Respawn();
                break;
        }


        //Zmatias
        //XP System is used for Updating UI
        XPSystem();

        // Options Menu 
        /*
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (OptionsMenu.GameIsPaused)
            {
                optionsMenu.Resume();
            }
            else
            {
                optionsMenu.Paused();
            }
        } */
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            //state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsGameOver()
    {
        return state == State.Gameover;
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpause?.Invoke(this, EventArgs.Empty);
        }
    }


    // Resources
    [Header("Zmatias New Variables")]
    [Tooltip("Starting Level of Player")]
    public int StartIngLevel = 1;

    [Header("Rest Variables")]
    public XPSystem level;
    public TextMeshProUGUI hitpointText, levelText, goldHUDText, rubyHUDText, xpHUDText;

    // Not necessary anymore
    /*
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices; 
    public Weapon weapon;

     */


    // References
    public Player player;
    public FloatingTextManager floatingTextmanager;
    //public OptionsMenu optionsMenu;
    public RectTransform hitpointBar, xpHUDBar;
    public GameObject hud, options, deathMenu, controls;

    // Logic
    public int gold;
    public int ruby;
    public int experience;


    // Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextmanager.Show(msg, fontSize, color, position, motion, duration);
    }


    // Hitpoint Bar

    public void OnHitPointChange()
    {
        float ratio = (float)player.hitpoints / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
        //getHitSoundEffect.Play();
    }

    // Experience System       


    private void XPSystem()
    {
        //Zmatias method
        //Added new variables in XPSystem Class
        //The aim is to store values that are represented on the menu each time the change on XPClass.
        //More info on XPSystem Class scipt

        int diff = level.XPNeed4LevelUp;
        int XP_4_LevelUP = level.XPGainedDuringLevel;

        if (level.XPGainedDuringLevel >= level.XPNeed4LevelUp)
        {
            xpHUDText.text = ("LEVEL " + level.currentLevel.ToString() + ":    " + diff.ToString() + " Max XP Level"); // Display total XP 
            xpHUDBar.localScale = Vector3.one;
        }
        else
        {

            float completionRatio = (float)XP_4_LevelUP / (float)diff;
            xpHUDBar.localScale = new Vector3(completionRatio, 1, 1);
            xpHUDText.text = ("LEVEL " + level.currentLevel.ToString() + ":    " + XP_4_LevelUP.ToString() + " xp / " + diff + " xp");
        }

    }

    // On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;

    }


    // Save State "Save Game Data"
    /*
     * INT preferredSkin
     * INT gold
     * INT experience
     * INT weaponLevel
     */

    // Old Save and Load System --> Needs Refactoring

    public void SaveState()
    {

        string s = "";

        s += "0" + "|";
        s += gold.ToString() + "|";
        s += experience.ToString() + "|";
       // s += weapon.weaponLevel.ToString();



        PlayerPrefs.SetString("SaveState", s);

    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change Player Skin
        gold = int.Parse(data[1]);

        // Experience
        experience = int.Parse(data[2]);
        if (level.currentLevel != 1)
            player.SetLevel(level.currentLevel);

        /* Change weapon level;
        weapon.SetWeaponLevel(int.Parse(data[3])); */

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;


    }
}