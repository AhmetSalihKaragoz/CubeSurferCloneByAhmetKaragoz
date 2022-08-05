using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardStage : MonoBehaviour
{
    [SerializeField] CubeCollector cubeCollector;
    [SerializeField] CubeMovement cubeMovement;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {      
            cubeCollector.Cubes.Remove(other.gameObject);
            other.gameObject.transform.parent = null;
        }

        if (cubeCollector.Cubes.Count == 0)
        {
            cubeMovement.isOnCurve = true;
            cubeCollector.gameObject.transform.DOMove(cubeCollector.gameObject.transform.position + transform.forward*1.5f, 0.8f);
        }
    }
}
