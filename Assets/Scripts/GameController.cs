using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject agentPrefab;
    public Transform ai;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var go = GameObject.FindWithTag("Agent");
            Destroy(go);
            Instantiate(agentPrefab, ai);
        }
    }

}
