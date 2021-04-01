using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // objetivo
    Transform goal;
    // Velocidade
    float speed= 5.0f;
    // offset dos pontos
    float accuracy= 1.0f;
    // velocidade rotacao
    float rotSpeed= 2.0f;
    // ref do wpManager
    public GameObject wpManager;
    // points mapa
    GameObject[] wps; 
    GameObject currentNode;

    int currentWP= 0;

    Graph g;
    
    void Start()
    {

        // pegando referencia
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        // comeca no zero
        currentNode = wps[0];
    }


    private void LateUpdate()
    {
        // retorna qnd zero
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;
        
        currentNode = g.getPathPoint(currentWP);
        //checa a distance do tank para o ponto para ir pro next
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }
        // Muda o proximo point
        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;

            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            this.transform.position = Vector3.MoveTowards(transform.position, goal.position, Time.deltaTime * speed);
        }
    }


    // Faz o tank ir ate o Heliporto
    public void GoToHeli() 
    { 
        currentWP = 0;
        g.AStar(currentNode, wps[13]); 
    }

    //Faz o tank ir ate o meio
    public void GoToMeio() 
    { 
        currentWP = 0;
        g.AStar(currentNode, wps[10]); 
    }

    // Faz o tank ir ate o vila
    public void GoToVila()
    {
        currentWP = 0;
        g.AStar(currentNode, wps[7]);
    }

}
