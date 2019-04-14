using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Animator _animator;
    private bool _started;
    private bool _ended;
    private RoomGenerator _rg;

    // Start is called before the first frame update
    void Start()
    {
        _rg = GameObject.Find("ProceduralGenerator").GetComponent<RoomGenerator>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && _started && !_ended)
        {
            EndRoom();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_started)
        {
            _animator.SetTrigger("StartRoom");
            _started = true;
        }
    }

    public void EndRoom()
    {
        if (_started && !_ended)
        {
            _ended = true;
            _animator.SetTrigger("EndRoom");
            _rg.Generate(transform.position + Vector3.forward * 20);
        }
    }

    public void CloseRoom()
    {
        StartCoroutine(ICloseRoom());
    }

    private IEnumerator ICloseRoom()
    {
        _animator.SetTrigger("CloseRoom");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
