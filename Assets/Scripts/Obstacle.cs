using UnityEngine;

public class Obstacle : MonoBehaviour {

    public GameObject player;
    public bool isMoving;
    Level levelManager;
    
    void Start()
    {

        levelManager = GameObject.Find("LevelManager").GetComponent<Level>();

        if (isMoving)
        {
            iTween.MoveBy(gameObject, iTween.Hash("z", 8, "time", 4, "delay", 1, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!levelManager.isWinner)
        {
            Level.Fail();
        }        
    }

}
