using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour {

    public Transform[] points;
    public float speed = 0.5f;

    private int currentPointIndex = 0;
    private int nextDestinationIndex = 1;
    private float journeyLength;
    private float distanceCovered;
    //private Dictionary<Transform,Transform> collider_exParent = new Dictionary<Transform, Transform>();
    Dictionary<Transform,Transform> groundCheck_topLevelParent = new Dictionary<Transform, Transform>();

    // Use this for initialization
    void Start () {
    }

    void FixedUpdate()
    {
        if (!AreThereMultiplePoints(points))
            return;

        if (transform.position == points[nextDestinationIndex].position)
        {
            currentPointIndex = nextDestinationIndex;
            nextDestinationIndex = nextDestinationIndex + 1 >= points.Length ? 0 : nextDestinationIndex + 1;
            journeyLength = Vector3.Distance(points[currentPointIndex].position, points[nextDestinationIndex].position);
            distanceCovered = 0;

        }
        else
        {
            distanceCovered += Time.deltaTime * speed;
            float journeyCompletion = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(
                points[currentPointIndex].position,
                points[nextDestinationIndex].position,
                journeyCompletion);
        }
    }

    private bool AreThereMultiplePoints(Transform[] points)
    {
        return (points != null && points.Length > 1);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        List<string> possibleChildrenTags = new List<string> { Tags.GROUND_CHECK };

        if (possibleChildrenTags.Contains(coll.gameObject.tag) && !alreadyCollidingGameobject(coll.transform))
        {
            var theLastColliderParent = findLastParent(coll.transform);
            theLastColliderParent.parent = transform;
            groundCheck_topLevelParent.Add(coll.transform,theLastColliderParent);
            //collider_exParent.Add(coll.transform, coll.transform.parent);
            //coll.transform.parent = transform;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(alreadyCollidingGameobject(coll.transform))
        {
            Transform theLastColliderParent;
            groundCheck_topLevelParent.TryGetValue(coll.transform, out theLastColliderParent);
            theLastColliderParent.parent = null;
            groundCheck_topLevelParent.Remove(coll.transform);
        }
    }

    private bool alreadyCollidingGameobject(Transform colliderTransform)
    {
        foreach (var recordedTransform in groundCheck_topLevelParent.Keys)
        {
            if (colliderTransform.Equals(recordedTransform))
            {
                return true;
            }
        }
        return false;
    }

    private Transform findLastParent(Transform transform)
    {
        if (transform.parent == null)
        {
            return null;
        }

        Transform lastParent = transform.parent;
        for (int i = 0; i < 100; i++)
        {
            if (lastParent.parent == null)
                return lastParent;
            else
                lastParent = lastParent.parent;
        }
        return null;
    }
}
