using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObstacleCubes : MonoBehaviour
{
    private bool isUsed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isUsed)
        {
            return;
            
        }
        if (other.CompareTag("Player"))
        {
            isUsed = true;
            var cubeCollector = other.gameObject.transform.parent.GetComponent<CubeCollector>();
            cubeCollector.Cubes.Remove(other.gameObject);
            other.gameObject.transform.parent = null;
            other.enabled = false;
            Debug.Log(cubeCollector.Cubes.Count);
            if (cubeCollector.Cubes.Count == 0)
            {
                Destroy(cubeCollector.figure);
            }
        }
    }

}
