using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CubeMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float sideSpeed;
    [SerializeField] Transform figure;
    Transform endLocation;
    float sideInput;
    public LayerMask groundLayer;

    [HideInInspector] public bool isOnCurve = false;

    private void Update()
    {
        sideInput = Input.GetAxis("Horizontal");
        Move();
    }

    public void MoveForward()
    {
        transform.Translate(moveSpeed * Time.deltaTime * transform.forward, Space.World);        
    }
    public void MoveSideward()
    {
        if (sideInput!= 0)
        {
            transform.Translate(sideInput * sideSpeed * Time.deltaTime * transform.right, Space.World);
        }
    }
    public void Move()
    {
        if (!isOnCurve)
        {
            MoveForward();
            Clamp();
            MoveSideward();
        }   
    }
    private void Clamp()
    {
        RaycastHit hit1;
        Debug.DrawRay(figure.position+figure.right*1.15f+Vector3.up, Vector3.down*500);

        if(!Physics.Raycast(figure.position+figure.right*1.15f+Vector3.up, Vector3.down, out hit1,1000,groundLayer))
        {
          sideInput = sideInput > 0 ? 0 : sideInput;
        }

        RaycastHit hit2;

        if (!Physics.Raycast(figure.position + figure.right * -1.15f + Vector3.up, Vector3.down, out hit1, 1000, groundLayer))
        {
          sideInput = sideInput < 0 ? 0 : sideInput;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            isOnCurve = true;
            endLocation = other.gameObject.transform.GetChild(0);
            StartCoroutine(CurveLerpCoroutine());

        }
    }


    public IEnumerator CurveLerpCoroutine()
    {
        var timeElapsed = 0f;
        var lerpDuration =.5f;
        var startPos = gameObject.transform.position;
        var startRot = gameObject.transform.rotation;
        while (timeElapsed < lerpDuration)
        {
            gameObject.transform.position = Vector3.Lerp(startPos, endLocation.position, timeElapsed / lerpDuration);
            gameObject.transform.rotation = Quaternion.Lerp(startRot, endLocation.rotation, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.position = endLocation.position;
        gameObject.transform.rotation = endLocation.rotation;
        isOnCurve = false;
        
    }




}
