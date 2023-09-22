using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float height;
    
    public void Open()
    {
        StartCoroutine("OpenDoor");
    }

    IEnumerator OpenDoor()
    {
        for (float dist = 0; dist < height; dist+=0.1f)
        {
            transform.Translate(0, -0.1f, 0);
            yield return null;
        }
    }

    public void Close()
    {
        StartCoroutine("CloseDoor");
    }

    IEnumerator CloseDoor()
    {
        for (float dist = 0; dist < height; dist+=0.1f)
        {
            transform.Translate(0, 0.1f, 0);
            yield return null;
        }
    }
}
