using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Route Route;
   public List<Node> nodeList = new List<Node>();


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
    void Update()
    {
        
    }
}
