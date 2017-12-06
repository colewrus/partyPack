using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fling : MonoBehaviour {

    bool isPressed = false;
    public Rigidbody2D rb;
    public Rigidbody2D hook;

    public float maxDragDist = 3f;

    public float delay = 0.15f;

    public GameObject nextBall;

    private void Awake()
    {
        Debug.Log(gameObject.name);
    }

    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, hook.position) > maxDragDist)
            {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDist;                
            }
            else
                rb.position = mousePos;
   
           
        }
    }

    private void OnMouseDown()
    {
        
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(FlingTimer());
    }

    IEnumerator FlingTimer()
    {
        yield return new WaitForSeconds(delay);

        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
        rb.freezeRotation = false;

        yield return new WaitForSeconds(3f);

        if(nextBall != null)
        {
            nextBall.SetActive(true);
            Camera.main.GetComponent<CameraFollow>().target = nextBall.transform;
        }else
        {
            Camera.main.GetComponent<CameraFollow>().Lose_Restart();
            enemyScript.EnemiesAlive = 0;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

}
