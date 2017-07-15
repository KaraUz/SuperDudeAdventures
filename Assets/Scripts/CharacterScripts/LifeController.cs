using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {
    public int startingHealth = 1;
    public float immuneTime = 1;

    public int Life { get; private set; }
    private bool _immune = false;

    // Use this for initialization
    void Start () {
        Life = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HandleCollision(Collider2D coll)
    {
        List<string> acceptedTags = new List<string> { Tags.ENEMY, Tags.HAZARD };

        if (acceptedTags.Contains(coll.tag) && !_immune)
        {
            if (Life > 0)
            {
                Life--;
                print("Remaining life: " + Life);
                _immune = true;
                StartCoroutine(WaitAndDo(immuneTime, () => _immune = false));
            }
        }
    }

    public IEnumerator WaitAndDo(float time, Action doSomething)
    {
        yield return new WaitForSeconds(time); // waits 3 seconds
        doSomething();
    }

    void OnTriggerExit2D(Collider2D coll)
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        HandleCollision(coll);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        HandleCollision(coll.collider);
    }
}
