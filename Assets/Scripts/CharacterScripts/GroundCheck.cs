using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private bool _grounded = false;
    private List<string> _groundTags = new List<string>();

    void OnTriggerEnter2D(Collider2D other)
    {
        _grounded = true;
        _groundTags.Add(other.tag);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _groundTags.Remove(other.tag);
        if (_groundTags.Count == 0)
            _grounded = false;
    }

    public bool StandingOn(string coliderTag)
    {
        return (_grounded == true && _groundTags.Contains(coliderTag));
    }



}
