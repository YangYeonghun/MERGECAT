using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx.Operators;
using UniRx;
using System;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    IntReactiveProperty score;
    public IntReactiveProperty Score { get { return score; } set { score = value; } }

    int order;
    int minStep = 1;
    int maxStep = 1;
    public List<int> stepPointList;

    [SerializeField] public int MaxStep { get { return maxStep; } }

    public Ball ballPref;
    public Ball cutterPref;
    public Transform startPos;
    Ball curBall;

    Camera cam;

    public List<Sprite> ballSprites;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        score = new IntReactiveProperty();
    }

    private void Start()
    {
        cam = Camera.main;
        order = 1;

        minStep = 1;
        maxStep = 1;

        Spawn();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 nextPos = cam.ScreenToWorldPoint(Input.mousePosition);
            nextPos.y = startPos.position.y;
            curBall?.TouchControl(nextPos);
        }
        else if (Input.GetMouseButtonUp(0) && curBall != null)
        {
            curBall?.Drop();
            curBall = null;
            Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe((x) => Spawn());
        }
    }

    void Spawn()
    {
        Ball ball = Instantiate<Ball>(ballPref);
        ball.transform.position = startPos.transform.position;
        ball.Order = order++;
        ball.SetFruit(UnityEngine.Random.Range(minStep, maxStep + 1));
        curBall = ball;
    }

    public void OnClickCutterButton()
    {
        SpawnCutter();
    }
    void SpawnCutter()
    {
        curBall.PushToPool();


        Ball ball = Instantiate<Ball>(cutterPref);
        ball.transform.position = startPos.transform.position;
        ball.Order = order++;
        curBall = ball;
    }

    public void SetSprite(List<Sprite> sprites)
    {
        ballSprites = sprites;
    }

    public Sprite GetSprite(int index)
    {
        return ballSprites[index];
    }


    public void AddScore(int step)
    {
        score.Value += step;

        if (stepPointList.Count > maxStep && score.Value >= stepPointList[maxStep])
            maxStep++;
    }
}
