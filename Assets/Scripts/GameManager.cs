using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) return null;

            return instance;
        }
    }

    public Player player { get; private set; }
    public TalkManager talkManager { get; private set; }
    public LineManager lineManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public ShakeEffect shakeEffect { get; private set; }

    public List<GameObject> effects = new List<GameObject>();

    public GameObject coinEffect;
    public GameObject coin;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        player = FindObjectOfType<Player>();
        talkManager = FindObjectOfType<TalkManager>();
        lineManager = FindObjectOfType<LineManager>();
        uiManager = FindObjectOfType<UIManager>();
        shakeEffect = FindObjectOfType<ShakeEffect>();
    }

    public void InstantiateHitEffect(Transform other)
    {
        Instantiate(effects[Random.Range(0, effects.Count)], other.position, Quaternion.identity);
    }

    public void InstantiateCoinEffect(Transform other)
    {
        Instantiate(coinEffect, other.position, Quaternion.identity);
    }

    public void DropCoin(Transform other,int EA)
    {
        if (coin == null) return;

        for (int i = 0; i < EA; i++)
        {
            GameObject obj = Instantiate(coin, other.position, Quaternion.identity);

            if (obj.GetComponent<Rigidbody2D>() == null) return;

            float x = Random.Range(-2.0f, 2.0f);
            float y = Random.Range(5.0f, 7.0f);

            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y);
        }
    }

    public void ShakeCamera(float amount, float duration)
    {
        shakeEffect.StartCoroutine(shakeEffect.Shake(amount, duration));
    }
}
