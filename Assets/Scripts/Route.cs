using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] nodes;
   public  List<Transform> nodeList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        FIllNodes();
    }

    void FIllNodes()
    {
        nodeList.Clear();
        nodes = GetComponentsInChildren<Transform>();


        int num = -1;
        foreach(Transform t in nodes) 
        {
            Node nod = t.GetComponent<Node>();
            if(t != this.transform && nod != null)
            {
                num++;
                nodeList.Add(t);
                t.gameObject.name = "field " + num;

                nod.SetNodeId(num);
            }
        }
    }

    private void OnDrawGizmos()
    {
        FIllNodes();

        for (int i = 0; i < nodeList.Count; i++)
        {
           Vector3 start = nodeList[i].position;
            if (i > 0)
            {
                Vector3 prev = nodeList[i - 1].position;
                Debug.DrawLine(prev, start, Color.green);
            }
        }
    }
}
