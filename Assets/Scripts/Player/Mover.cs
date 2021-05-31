using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody),typeof(Player))]
public class Mover : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trail;
    [SerializeField] private float _speed;
    [SerializeField] private float _energyCost;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speedDesh;
    [SerializeField] private float _timeDesh;
    [SerializeField] private float _mouseSensitivity;

    private Rigidbody _rigidbody;
    private float _angleMouse;
    private bool _isGround;
    private Player _player;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Update()
    {
        Jump();
        Desh();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(transform.forward * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(-transform.forward * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(transform.right * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(-transform.right * _speed);
        }
    }

    private void Rotate()
    {
        _angleMouse += Input.GetAxis("Mouse X") * _mouseSensitivity;

        transform.rotation = Quaternion.AngleAxis(_angleMouse, Vector3.up);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }
    }

    private void Desh()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _player.CurrentEnergi >= _energyCost)
        {
            StartCoroutine(StartDash());
            _player.SpendEnergy(_energyCost);
        }
    }

    private IEnumerator StartDash()
    {
        _trail.Play();
        float normalSpeed = _speed;
        _speed = _speedDesh;
        yield return new WaitForSeconds(_timeDesh);
        _trail.Stop();
        _speed = normalSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            _isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            _isGround = false;
        }
    }
}
