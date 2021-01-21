using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFliper : MonoBehaviour
{
    private Quaternion playerRotation;

    void Awake()
    {
        playerRotation = transform.rotation;
    }

    public void WallsFliperActionAfterPlayerAction()
    {
        StartCoroutine(delay());
    }

    public void RestartPosition()
    {
        transform.rotation = playerRotation;
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.15f);
        iTween.RotateTo(this.gameObject, iTween.Hash(
                          "rotation", new Vector3(0, 0, transform.rotation.eulerAngles.z - 90),
                          "time", 0.2f,
                          "easetype", iTween.EaseType.easeInBack
                ));
    }
}
