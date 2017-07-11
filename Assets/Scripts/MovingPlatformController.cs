using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour {

    public Transform[] Points;
    public int nextDestinationIndex = 0;
    public float speed = 0.5f;

    private float journeyLength;
    private int currentPointIndex;
    private float distanceCovered;
    private Dictionary<Transform,Transform> collider_exParent = new Dictionary<Transform, Transform>();
   
    // Use this for initialization
    void Start () {

    }

    void FixedUpdate()
    {
        if (!AreThereMultiplePoints(Points))
            return;

        if (transform.position == Points[nextDestinationIndex].position)
        {
            currentPointIndex = nextDestinationIndex;
            nextDestinationIndex = nextDestinationIndex + 1 >= Points.Length ? 0 : nextDestinationIndex + 1;
            journeyLength = Vector3.Distance(Points[currentPointIndex].position, Points[nextDestinationIndex].position);
            distanceCovered = 0;
        }
        else
        {
            distanceCovered += Time.deltaTime * speed;
            float journeyCompletion = distanceCovered / journeyLength;
            
            transform.position = Vector3.Lerp(
                Points[currentPointIndex].position, 
                Points[nextDestinationIndex].position,
                journeyCompletion);
        }
    }

    private bool AreThereMultiplePoints(Transform[] points)
    {
        return (points != null && points.Length > 1);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        List<string> possibleChildrenTags = new List<string> { Tags.PLAYER, Tags.ENEMY };
        if (possibleChildrenTags.Contains(coll.gameObject.tag))
        {
            collider_exParent.Add(coll.gameObject.transform, coll.gameObject.transform.parent);
            coll.gameObject.transform.parent = transform;

        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        List<string> possibleChildrenTags = new List<string> { Tags.PLAYER, Tags.ENEMY };
        if (possibleChildrenTags.Contains(coll.gameObject.tag))
        {
            Transform temp;
            collider_exParent.TryGetValue(coll.gameObject.transform, out temp);
            coll.gameObject.transform.parent = temp == null? null : temp.transform;
            collider_exParent.Remove(coll.gameObject.transform);
        }
    }

}
