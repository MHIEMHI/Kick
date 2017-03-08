using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalieAnimation : MonoBehaviour {

    private Vector3 fp;   //First finger position
    private Vector3 lp;   //Last finger position
    private bool isKicked;  //flag to indicate if the ball has been kicked
    public SwipeControl swipe;   //reference to SwipeControl script
    private float dragDistance;  //Distance needed for a swipe to register
    private Animation anim;
                                 // Use this for initialization
    void Start () {
        dragDistance = Screen.width * 5 / 100;  //set dragdistance to 5% of screen width
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update () {
        isKicked = swipe.isKickedPlayer;  //check if the ball is kicked
        if (swipe.turn == 1)     //check if players turn to keep the goal
            playerGoalie();
        else if (swipe.turn == 0)    //check if opponent's turn to keep
            opponentGoalie();
    }

    void opponentGoalie()
    {
        //check if the ball is kicked so that we can play the animation
        if (isKicked)
        {
            int num = (int)Random.Range(1f, 3f); //generate a random number within 1 to 3
                                                 //play a particular animation for a particular number generated
            if (num == 1)
            {
                anim.Play("RightSave");
            }
            else if (num == 2)
            {
                anim.Play("LeftSave");
            }
            else if (num == 3)
            {
                anim.Play("StandSave");
            }
            swipe.isKickedPlayer = false;  //once the animation is played set the isKicked flag to false
        }
    }

    void playerGoalie()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }

            if ((touch.phase == TouchPhase.Ended) && swipe.isKickedOpponent)
            {

                lp = touch.position;

                //First check if it's actually a drag
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {   //It's a drag

                    //Now check what direction the drag was
                    //First check which axis
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right move
                            anim.Play("RightSave");

                        }
                        else if ((lp.x < fp.x))
                        {   //Left move
                            anim.Play("LeftSave");

                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if ((lp.y > fp.y))  //If the movement was up
                        {   //Up move
                            anim.Play("StandSave");


                        }
                    }
                }
            }

        }

    }
}
