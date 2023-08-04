using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitychanController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1.0f;   //ユニティーちゃんの移動速度
    [SerializeField] float _jumpPower = 3.0f;   //ユニティーちゃんのジャンプの強さ
    [SerializeField] float _dashSpeed = 2.0f;   //shiftを押した際の加速度
    [SerializeField] float _attackInterval = 1.0f;    //攻撃のクールタイム
    [SerializeField] float _gravityDrag = .8f; //ジャンプボタンを離した際の落下速度の上昇率
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
            // 上昇中にジャンプボタンを離したら上昇を減速する
            velocity.y *= _gravityDrag;
        }

        _rb.velocity = velocity;
    }

    void FixedUpdate()
    {
        if (!Input.GetButton("Fire3"))
        {
            _rb.AddForce(Vector2.right * _h * _moveSpeed);
            Debug.Log("通常速度で動作");
        }
        else
        {
            _rb.AddForce(Vector2.right * _h * _moveSpeed * _dashSpeed);
            Debug.Log("ダッシュで動作");
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
        // キャラクターの左右の向きを制御する
        if (_h != 0)
        {
            _sprite.flipX = (_h < 0);
        }

        // アニメーションを制御する
        if (_anim)
        {
            _anim.SetFloat("SpeedX", Mathf.Abs(_rb.velocity.x));
            _anim.SetFloat("SpeedY", _rb.velocity.y);
            _anim.SetBool("IsGrounded", _isGrounded);
        }
    }
}
