using System.Collections;
using UnityEditor.Build.Content;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    Transform player1;
    Transform player2;

    public Vector3 offset;
    public float smoothSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartCoroutine(CameraStartDelay());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CREATE CAUGHT FLAG AND ISCAUGHT FUNCTION
        /*if (player1.isCaught())
         * {
         *    Vector3 desiredPosition = player1.position + offset;
         *    Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
         *    transform.position = smoothPosition;
        }*/
        //if player 2 is caught
        /*if (player2.isCaught()) 
         * {
         *      Vector3 desiredPosition = player2.position + offset;
         *      Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothPosition * Time.deltaTime);
         *      transform.position = smootPosition;
         * } 
        */
        //if no player is caught
        //else 
        //{
        Vector3 desiredPosition = FindCentroid() + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
        //}
    }

    IEnumerator CameraStartDelay()
    {
        yield return new WaitForSeconds(0.01f);
        player1 = PlayerInputManager.player1.transform;
        player2 = PlayerInputManager.player2.transform;
        transform.position = player1.position + offset;
    }

    Vector3 FindCentroid()
    {
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;

        totalX += player1.position.x + player2.position.x;
        totalY += player1.position.y + player2.position.y;
        totalZ += player1.position.z + player2.position.z;

        float centerX = totalX / 2;
        float centerY = totalY / 2;
        float centerZ = totalZ / 2;

        return new Vector3(centerX, centerY, centerZ);

    }
}
