using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class Canvas_InGame : MonoBehaviour
{
    [SerializeField] Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.Score.Subscribe((x) => scoreText.text = $"Score: {x}").AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
