using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Delay3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(45, GotoNextScene);
    }

    // Update is called once per frame
    void GotoNextScene()
    {
        SceneManager.LoadScene("Attack 1");
        
    }
}
