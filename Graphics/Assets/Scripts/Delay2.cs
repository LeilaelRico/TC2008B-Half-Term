using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Delay2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(15, GotoNextScene);
    }

    // Update is called once per frame
    void GotoNextScene()
    {
        SceneManager.LoadScene("Attack 3");
        
    }
}
