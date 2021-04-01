using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Link
{
    // ida e volta
    public enum direction { UNI, BI }

    // inicio
    public GameObject node1;

    // final
    public GameObject node2;

    // direcao
    public direction dir;
}


public class WPManager : MonoBehaviour
{
    // pontos
    public GameObject[] waypoints;

    // links
    public Link[] links;
    public Graph graph = new Graph();
    
    void Start()
    {
        // add um ponto para cada no mapa
        if(waypoints.Length > 0){ 
            foreach (GameObject wp in waypoints) 
            { 
                graph.AddNode(wp);
            } 
            foreach (Link l in links) 
            { 
                graph.AddEdge(l.node1, l.node2);

                if (l.dir == Link.direction.BI) 
                    graph.AddEdge(l.node2, l.node1); 
            } 
        }

    }

    
    void Update()
    {        

        graph.debugDraw();
    }
}
