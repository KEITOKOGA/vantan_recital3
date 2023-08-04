using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitychanController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1.0f;   //���j�e�B�[�����̈ړ����x
    [SerializeField] float _jumpPower = 3.0f;   //���j�e�B�[�����̃W�����v�̋���
    [SerializeField] float _dashSpeed = 2.0f;   //shift���������ۂ̉����x
    [SerializeField] float _attackInterval = 1.0f;    //�U���̃N�[���^�C��
    [SerializeField] float _gravityDrag = .8f; //�W�����v�{�^���𗣂����ۂ̗������x�̏㏸��
    Rigidbody2D _rb = default;
    bool _isGrounded = false;
    Animator _anim = default;
    SpriteRenderer _sprite = default;
    float _attackTimer;
    float _h;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _attackTimer = _attackInterval;
    }

    void Update()
    {
        _attackTimer += Time.deltaTime;
        _h = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = _rb.velocity;
        if (Input.GetButtonDown("Fire1"))
        {

        }
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isGrounded = false;
            velocity.y = _jumpPower;
        }
        else if (!Input.GetButton("Jump") && velocity.y > 0)
        {
            // �㏸���ɃW�����v�{�^���𗣂�����㏸����������
            velocity.y *= _gravityDrag;
        }

        _rb.velocity = velocity;
    }

    void FixedUpdate()
    {
        if (!Input.GetButton("Fire3"))
        {
            _rb.AddForce(Vector2.right * _h * _moveSpeed);
            Debug.Log("�ʏ푬�x�œ���");
        }
        else
        {
            _rb.AddForce(Vector2.right * _h * _moveSpeed * _dashSpeed);
            Debug.Log("�_�b�V���œ���");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }
    private void LateUpdate()
    {
        // �L�����N�^�[�̍��E�̌����𐧌䂷��
        if (_h != 0)
        {
            _sprite.flipX = (_h < 0);
        }

        // �A�j���[�V�����𐧌䂷��
        if (_anim)
        {
            _anim.SetFloat("SpeedX", Mathf.Abs(_rb.velocity.x));
            _anim.SetFloat("SpeedY", _rb.velocity.y);
            _anim.SetBool("IsGrounded", _isGrounded);
        }
    }
}
