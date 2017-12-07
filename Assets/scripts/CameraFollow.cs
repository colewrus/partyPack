using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public bool levelEnd = false;

    public float win_Delay;
    public Text EndLevelText;


    private void Start()
    {     



        EndLevelText.gameObject.SetActive(false);
    }

   

    private void FixedUpdate()
    {
        Vector3 desirePosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desirePosition, smoothSpeed);
        transform.position = smoothedPosition;

        if (levelEnd)
            StartCoroutine(Level_End());

        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    IEnumerator Level_End()
    {
        
        EndLevelText.text = "You Win";
        EndLevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(win_Delay);
        SceneManager.LoadScene(0);
        levelEnd = false;
    }

    public void Lose_Restart()
    {
        EndLevelText.text = "Try Again";
        EndLevelText.gameObject.SetActive(true);

    }

}
