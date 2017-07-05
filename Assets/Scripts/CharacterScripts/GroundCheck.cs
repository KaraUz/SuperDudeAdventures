using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private bool _grounded = false;
    private List<int> _groundLayers = new List<int>();
    private List<string> _groundTags = new List<string>();

    void OnTriggerEnter2D(Collider2D other)
    {
        _grounded = true;
        _groundLayers.Add(other.gameObject.layer);
        _groundTags.Add(other.tag);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _groundLayers.Remove(other.gameObject.layer);
        _groundTags.Remove(other.tag);
        if (_groundLayers.Count == 0 && _groundTags.Count == 0)
            _grounded = false;
    }

    public bool OnLayer(string coliderLayer)
    {
        return (_grounded == true && _groundLayers.Contains(LayerMask.NameToLayer(coliderLayer)));
    }

    public bool OnTag(string coliderTag)
    {
        return (_grounded == true && _groundTags.Contains(coliderTag));
    }


}
