﻿using System.Collections;
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

        Texture2D texLoad;
        texLoad = new Texture2D(2, 2);
        texLoad.LoadImage(File.ReadAllBytes(Application.dataPath + "/test.png"));

  

        levelEnd = false;
        EndLevelText.gameObject.SetActive(false);
        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        objs[1].gameObject.SetActive(false);
        objs[2].gameObject.SetActive(false);
        Sprite tempSprite = Sprite.Create(texLoad, new Rect(texLoad.width/4.5f, texLoad.height/2f, texLoad.width/2.5f, texLoad.height/3f), new Vector2(0.5f,0.5f), 1000);
        Debug.Log(texLoad.texelSize + " " + objs[0].GetComponent<SpriteRenderer>().size);

       

        objs[0].gameObject.GetComponent<SpriteRenderer>().sprite = tempSprite;
       
       

        for (int i=0; i < objs.Length; i++)
        {
           
            //objs[i].gameObject.GetComponent<SpriteRenderer>().sprite 
        }
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
