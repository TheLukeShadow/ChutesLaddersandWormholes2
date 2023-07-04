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

    float cTime = 0;
    float amplitude = 0.5f;


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

        //remove token from current node
        nodeList[routePosition].RemoveToken(this);


        while (stepsToMove > 0)
        {
            routePosition++;
            Vector3 nextPos = Route.nodeList[routePosition].transform.position;

            //line movement
            //while(MoveToNextNode(nextPos)) { yield return null; }

            //arc movement
            Vector3 startPos = Route.nodeList[routePosition -1].transform.position;
            while (MoveInArcToNextNode(startPos, nextPos, 4f)) { yield return null; }

            yield return new WaitForSeconds(0.1f);

            cTime = 0;

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
        //add token to new node
        nodeList[routePosition].AddToken(this);

        //check win condition
        if(doneSteps == nodeList.Count - 1)
        {
            GameManager.Instance.ReportWinner();
            yield break;
        }

        //update game manager
        GameManager.Instance.State = GameManager.States.SwitchPlayer;

        isMoving = false;
    }

    bool MoveToNextNode(Vector3 nextPos)
    {
        return nextPos != (transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime));
    }

    bool MoveInArcToNextNode(Vector3 startPos, Vector3 nextPos, float arcSpeed) 
    {
        cTime += arcSpeed * Time.deltaTime;
        Vector3 myPosition = Vector3.Lerp(startPos, nextPos, cTime);
        myPosition.y += amplitude * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);

        return nextPos !=(transform.position = Vector3.Lerp(transform.position, myPosition, cTime));
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
