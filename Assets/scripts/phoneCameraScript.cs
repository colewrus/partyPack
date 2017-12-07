using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class phoneCameraScript : MonoBehaviour {

    bool camAvailable;
    WebCamTexture frontCam;
    Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;
    public RawImage inImage;
    public Image zone;

    public GameObject captured;
    Vector3 screenWorld;


    public bool b_FrontCam;

   
    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        b_FrontCam = true;

        if(devices.Length == 0)
        {
            Debug.Log("No Camera detected");
            camAvailable = false;
            return;
        }

        for(int i = 0; i < devices.Length; i++)
        {
            /*
            if (b_FrontCam)
            {
                if (devices[i].isFrontFacing)
                {
                    frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                }
            }else if (!b_FrontCam)
            {
                if (!devices[i].isFrontFacing)
                {
                    frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                }
            }else{
                frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }    
            */

            frontCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
        }

        if(frontCam == null)
        {
            Debug.Log("can't find front camera");
            return;
        }

        frontCam.Play();

        background.texture = frontCam;

        camAvailable = true;
        zone.rectTransform.pivot = new Vector2(0, 0);
        screenWorld = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 4.5f, Screen.height / 2.2f, 0));

        //Setup the box to show the photo capture area
        zone.rectTransform.position = new Vector3(Screen.width / 2.5f, Screen.height / 2, 0);
        zone.rectTransform.InverseTransformPoint(screenWorld);
        zone.rectTransform.sizeDelta = new Vector2(Screen.width / 4.5f, Screen.height / 2.2f);

    }

    public void ToggleCam()
    {
        if (b_FrontCam)
        {
            b_FrontCam = false;
            
        }else if (!b_FrontCam)
        {
            b_FrontCam = true;
        }
        Debug.Log(b_FrontCam);
    }

    public void MenuReturn()
    {
        frontCam.Stop();
        SceneManager.LoadScene("menu");
    }

    private void Update()
    {
        if (!camAvailable)
            return;
        float ratio = (float)frontCam.width / (float)frontCam.height;
        fit.aspectRatio = ratio;
        

        float scaleY = frontCam.videoVerticallyMirrored ? -1f: 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -frontCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        if (Input.GetKeyDown(KeyCode.Space))
        {            
            StartCoroutine(ScreenCap());
        }


        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Debug.Log(screenWorld);
        }
        
    }

    public IEnumerator ScreenCap()
    {
        zone.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        string imageName = "photo.png";
     
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);

        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        byte[] imageBytes = screenImage.EncodeToPNG();

        //System.IO.File.WriteAllBytes(Application.dataPath + "/test.png", imageBytes);

        //string file = Application.persistentDataPath + "/test.png";
        //File.WriteAllBytes(file, imageBytes);
        //This could be a preview
        //inImage.texture = screenImage;

        Sprite tempSprite = Sprite.Create(screenImage, new Rect(screenImage.width/2.5f, screenImage.height/2f, screenImage.width/4.5f, screenImage.height/2.2f), new Vector2(0, 0));
        
        captured.GetComponent<SpriteRenderer>().sprite = tempSprite;
        zone.gameObject.SetActive(true);

    }


}
