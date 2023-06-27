using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    public Route Route;
   public List<Node> nodeList = new List<Node>();

    int routePosition;

    int playerId;
    float speed = 8f;

    int stepsToMove;
    int doneSteps;

    bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in Route.nodeList)
        {
            Node n = t.GetComponentInChildren<Node>();
            if(n!= null)
            {
                nodeList.Add(n);
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //spacebar roll dice
    //    if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
    //    {
    //        stepsToMove = Random.Range(1, 7);
    //        print("dice rolled " + stepsToMove);

    //        if(doneSteps + stepsToMove < Route.nodeList.Count)
    //        {
    //            StartCoroutine(Move());
    //        }
    //        else
    //        {
    //            print("Number too high");
    //        }
    //    }
    //}

    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }

        isMoving = true;

        while(stepsToMove > 0)
        {
            routePosition++;
            Vector3 nextPos = Route.nodeList[routePosition].transform.position;

            while(MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            stepsToMove--;
            doneSteps++;
        }

        yield return new WaitForSeconds(0.1f);
        //snake/ladder check
        if (nodeList[routePosition].ConnectedNode != null)
        {
            int connectedNodeId = nodeList[routePosition].ConnectedNode.NodeID;
            Vector3 nextPos = nodeList[routePosition].ConnectedNode.transform.position;

            while (MoveToNextNode(nextPos))
            {
                yield return null;
                doneSteps = connectedNodeId;
                routePosition = connectedNodeId;
            }
        }
        //check win condition

        //update game manager
        GameManager.Instance.State = GameManager.States.SwitchPlayer;

        isMoving = false;
    }

    bool MoveToNextNode(Vector3 nextPos)
    {
        return nextPos != (transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime));
    }

    public void Turn(int diceNumber)
    {
        stepsToMove = diceNumber;
        if (doneSteps + stepsToMove < Route.nodeList.Count)
        {
            StartCoroutine(Move());
        }
        else
        {
            //print("Number too high");
            GameManager.Instance.State = GameManager.States.SwitchPlayer;
        }
    }
}
