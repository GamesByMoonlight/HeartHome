using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiver : MonoBehaviour {
    public float magnitude = 0.1f;
    [SerializeField]
    bool shivering = false;
    public bool Shivering { get { return shivering; } set { SetShivering(value);} }

    private void Start()
    {
        SetShivering(shivering);
    }

    void SetShivering(bool toShiver)
    {
        shivering = toShiver;
        if(shivering && Application.isPlaying)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        //Vector2 start = transform.position;
        while(shivering)
        {
            transform.localPosition = (new Vector2(0f, 0f) + new Vector2(Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude)));
            yield return new WaitForSeconds(.05f);
        }
        transform.position = new Vector2(0f, 0f);
    }
}
