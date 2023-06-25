using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int NodeID;
    public Text NumberText;
    public Node ConnectedNode; //for ladder or snake connections

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
}
