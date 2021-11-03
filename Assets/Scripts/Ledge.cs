using System.Collections.Generic;
using UnityEngine;
using DiasGames.ThirdPersonSystem;
using DiasGames.ThirdPersonSystem.ClimbingSystem;

public class Ledge : MonoBehaviour
{
    public GameObject player;
    public bool isTarget;
    public bool isFirst;

    List<ThirdPersonAbility> abilities;
    ClimbingAbility climb;
    ClimbJump jump;

    private void Start()
    {
        abilities = player.GetComponent<ThirdPersonSystem>().CharacterAbilities;
        climb = abilities[10] as ClimbingAbility;
        jump  = abilities[11] as ClimbJump;
    }

    void Update()
    {

        //Check the player reached out the target
        if(isTarget && climb.CurrentLedgeTransform == transform)
        {
            Level.Win();
        }

        //Check for mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    if (raycastHit.transform.gameObject.tag == "Ledge") {
                        Vector3 rayDirection = (raycastHit.transform.position - climb.CurrentLedgeTransform.position).normalized;

                        bool IsUp = rayDirection.y < 0;
                        bool IsRight;
                        Vector3 targetDirection;
                        ClimbJumpType jumpType;

                        if (!IsUp) {
                            IsRight = rayDirection.x > 0;
                            targetDirection = IsRight ? transform.right : -transform.right;
                            jumpType = IsRight ? ClimbJumpType.Right : ClimbJumpType.Left;
                        }
                        else
                        {
                            targetDirection = transform.up;
                            jumpType = ClimbJumpType.Up;
                        }
                                             
                        jump.StartClimbJump(jumpType, targetDirection, climb.GrabPosition, 0, true);

                        Destroy(climb.CurrentLedgeTransform.gameObject);

                    }
                }
            }
        }
    }
}
