using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIcontrol : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button btn;
    void Start()
    {
        btn.onClick.AddListener(() =>{
         SceneManager.LoadScene("playGround");
        });


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
