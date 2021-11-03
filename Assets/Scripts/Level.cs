using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DiasGames.ThirdPersonSystem;
using DiasGames.ThirdPersonSystem.ClimbingSystem;


public class Level : MonoBehaviour
{
    //  Public variables 
    public GameObject player;
    public Player hero;
    public Transform lightTransform;

    [HideInInspector]
    public bool isWinner = false;

    // Private variables
    bool isGameOver = false;
    bool isStarted = false;
    List<ThirdPersonAbility> abilities;
    ClimbingAbility climb;
    ClimbJump jump;
    GameObject firstLedge = null;
    GameObject targetLedge = null;
    CanvasGroup UI_GameOver;
    CanvasGroup UI_Winner;

    // Events
    static UnityEvent failEvent;
    static UnityEvent winEvent;


    private void Start()
    {
        // Find the panels that we'll show after win or game over.
        UI_GameOver = GameObject.Find("UI_GameOver").GetComponent<CanvasGroup>();
        UI_Winner = GameObject.Find("UI_Winner").GetComponent<CanvasGroup>();


        // Create and attach events to behaviour
        failEvent = new UnityEvent();
        winEvent = new UnityEvent();

        failEvent.AddListener(GameOver);
        winEvent.AddListener(Victory);

        // Setup Scene
        SetupScene();
    }

    /// <Summary>
    /// Restarts the level
    /// </Summary>
    /// 
    public void Restart()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ChangeLevel()
    {
        Debug.Log("Next Level");
    }

    /// <Summary>
    /// This method hides all panels.
    /// </Summary>
    /// 
    void HidePanels() {
        UI_GameOver.alpha = 0;
        UI_GameOver.interactable = false;

        UI_Winner.alpha = 0;
        UI_Winner.interactable = false;
    }


    /// <Summary>
    /// This method decide the panel show or hide.
    /// </Summary>
    /// <param name="panelName">Target panel  name</param>
    void ShowPanel(string panelName)
    {
        if(panelName == "UI_GameOver")
        {
            UI_Winner.gameObject.SetActive(false);

            UI_GameOver.alpha = .8f;
            UI_GameOver.interactable = true;            
        }

        if (panelName == "UI_Winner")
        {
            UI_GameOver.gameObject.SetActive(false);

            UI_Winner.alpha = .8f;
            UI_Winner.interactable = true;            
        }
    }

    /// <Summary>
    /// The method actually runs in Start void, it separated for to be organized.
    /// </Summary>
    void SetupScene()
    {
        // Hide panels
        HidePanels();

        // apply skin to body from scriptableObject player
        GameObject body = GameObject.Find("Player/Body");
        body.GetComponent<SkinnedMeshRenderer>().material = hero.mat;

        // get abilities and for first jump to first ledge
        abilities = player.GetComponent<ThirdPersonSystem>().CharacterAbilities;
        climb = abilities[10] as ClimbingAbility;
        jump = abilities[11] as ClimbJump;

        // find first ledge
        GameObject[] ledges = GameObject.FindGameObjectsWithTag("Ledge");

        // Check firsr and target ledges
        foreach(GameObject go in ledges)
        {
            Ledge currentLedge = go.GetComponent<Ledge>();

            if (currentLedge.isFirst)
            {
                firstLedge = go;
            }

            if (currentLedge.isTarget)
            {
                targetLedge = go;
            }            
        }

        // Check the Ledges have isFirst and isTarget else raise an exception
        if (targetLedge == null)
        {
            throw new System.Exception("Please select the target ledge as isTarget = true");
        }
        

        if (firstLedge)
        {
            player.transform.position = new Vector3(firstLedge.transform.position.x, 0, player.transform.position.z);
            jump.StartClimbJump(ClimbJumpType.Up, firstLedge.transform.position, climb.GrabPosition, 0, true);

            isStarted = true;
        }
        else
        {
            throw new System.Exception("Please select the first ledge as isFirst = true");
        }
        

        lightTransform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }

    /// <Summary>
    /// The method runs by an onGround event which is attached to the player, and the methods checks player on the ground and game already started?
    /// </Summary>
    public void CheckFallDown()
    {
        if (isStarted)
        {
            GameOver();
        }        
    }


    /// <Summary>
    /// This is a basic method to follow player.
    /// </Summary>    
    public void RepositionCamera()
    {
        if (!isGameOver)
        {
            float x = Camera.main.transform.position.x;
            float z = Camera.main.transform.position.z;
            float y = player.transform.position.y + 0.5f;

            Vector3 cameraPosition = new Vector3(x, y, z);

            float camY = Camera.main.transform.position.y;
            float playerY = player.transform.position.y;

            if (playerY > camY)
            {
                Camera.main.transform.position = cameraPosition;
            }
        }              
    }


    void GameOver()
    {
        isGameOver = true;

        Health h = (Health) player.GetComponent<ModifierManager>().GetModifier<Health>();        

        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 1f, Camera.main.transform.position.z);

        h.Die();

        ShowPanel("UI_GameOver");
    }

    void Victory()
    {
        isWinner = true;

        ShowPanel("UI_Winner");
    }

    public static void Win()
    {
        winEvent.Invoke();        
    }

    public static void Fail()
    {        
        failEvent.Invoke();
    }

}
