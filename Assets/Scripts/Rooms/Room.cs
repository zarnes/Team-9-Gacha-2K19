using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Animator _animator;
    private bool _started;
    private bool _ended;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
