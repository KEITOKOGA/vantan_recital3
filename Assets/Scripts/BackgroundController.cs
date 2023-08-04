using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    //スクロールさせたい背景
    [SerializeField] SpriteRenderer _backgroundSprite = null;
    //横方向への背景のスクロール速度
    [SerializeField] float _scrollSpeedX = -1f;
    //背景のクローンの格納変数
    SpriteRenderer _backgroundSpriteClone;
    //背景の座標の初期値
    float _initialPositionX;

    void Start()
    {
        _initialPositionX = _backgroundSprite.transform.position.x;     //背景の座標の初期値を記憶しておく
        _backgroundSpriteClone = Instantiate(_backgroundSprite);
        _backgroundSpriteClone.transform.Translate(_backgroundSprite.bounds.size.x, 0f, 0f);

    }
    void Update()
    {
        _backgroundSprite.transform.Translate(_scrollSpeedX * Time.deltaTime, 0f, 0f);
        _backgroundSpriteClone.transform.Translate(_scrollSpeedX * Time.deltaTime, 0f, 0f);

        if (_backgroundSprite.transform.position.x < _initialPositionX - _backgroundSprite.bounds.size.x)    //背景がある程度左に行ったら右に戻す
        {
            _backgroundSprite.transform.Translate(_backgroundSprite.bounds.size.x * 2, 0f, 0f);
        }

        if (_backgroundSpriteClone.transform.position.x < _initialPositionX - _backgroundSpriteClone.bounds.size.x)     //クローンにも同様の処理をする
        {
            _backgroundSpriteClone.transform.Translate(_backgroundSpriteClone.bounds.size.x * 2, 0f, 0f);
        }
    }
}