using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int NodeID;
    public Text NumberText;
    public Node ConnectedNode; //for ladder or snake connections

    List<Token> tokenList = new List<Token>();

    public void SetNodeId(int nodeId)
    {
        NodeID = nodeId;
        if(NumberText != null) NumberText.text = NodeID.ToString();
        
    }

    private void OnDrawGizmos()
    {
        if(ConnectedNode != null)
        {
            Color col = Color.white;

            col = (ConnectedNode.NodeID > NodeID) ? Color.blue : Color.red;

            Debug.DrawLine(transform.position, ConnectedNode.transform.position, col);
        }
    }

    public void AddToken(Token token)
    {
        tokenList.Add(token);
        //rearrange tokens to all fit on one node
        RearrangeTokens();
    }

    public void RemoveToken(Token token)
    {
        tokenList.Remove(token);
        //rearrange tokens to all fit on one node
        RearrangeTokens();
    }

    void RearrangeTokens()
    {
        if(tokenList.Count > 1)
        {
            int squareSize = Mathf.CeilToInt(Mathf.Sqrt(tokenList.Count));
            int token = -1;
            for (int x = 0; x < squareSize; x++)
            {
                for (int z = 0; z < squareSize; z++)
                {
                    token++;
                    if (token > tokenList.Count - 1) break;

                    Vector3 newPos = transform.position + new Vector3(-0.25f + x * 0.5f, 0, -0.25f + z * 0.5f);
                    tokenList[token].transform.position = newPos;
                }
            }
        }
        else if(tokenList.Count == 1)
        {
            tokenList[0].transform.position = transform.position;
        }
    }
}
