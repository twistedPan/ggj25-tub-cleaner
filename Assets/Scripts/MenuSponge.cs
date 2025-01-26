using System;
using UnityEngine;

public class MenuSponge : MonoBehaviour
{
    public bool SpongeGoesBrrr = false;
    public float speed = 10;
    public int startdelayTime = 2;
    private Action callback;
    private float timer = 0;

    public void GoGoSpongeBoy(Action value)
    {
        callback = value;
        SpongeGoesBrrr = true;
    }

    private void Update()
    {
        if (SpongeGoesBrrr)
        {
            // move transform in forward direction
            transform.position += (-transform.forward) * Time.deltaTime * speed;


            timer += Time.deltaTime;
            if (timer >= startdelayTime)
            {
                SpongeGoesBrrr = false;
                callback?.Invoke();
            }

        }
    }
}
