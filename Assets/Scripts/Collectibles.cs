using UnityEngine;

public class Collectibles : MonoBehaviour
{
    CubeCollector cubeCollector;
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected)
        {
            
            if (other.CompareTag("Player"))
            {
                var cubeCollector = other.gameObject.transform.parent.GetComponent<CubeCollector>();
                var newCubePos = new Vector3(cubeCollector.Cubes[0].transform.position.x, cubeCollector.Cubes.Count + .01f, cubeCollector.Cubes[0].transform.position.z);

                var newFigurePos = new Vector3(cubeCollector.Cubes[0].transform.position.x, cubeCollector.Cubes.Count + 1, cubeCollector.Cubes[0].transform.position.z);

                
                cubeCollector.figure.transform.position = newFigurePos;
                cubeCollector.Cubes.Add(Instantiate(cubeCollector.prefab, newCubePos, Quaternion.identity, other.gameObject.transform.parent));
                Destroy(gameObject);  
                isCollected = true;
                
            }
            
        }       
    }
}
