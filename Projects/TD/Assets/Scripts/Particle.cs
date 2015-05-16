using UnityEngine;
using System.Collections;

/// パーティクル
public class Particle : Token
{

  // 使用するスプライト(①)
  public Sprite spr0; // 塗りつぶしの円
  public Sprite spr1; // リング

  // パーティクル種別(②)
  public enum eType {
    Ball, // 塗りつぶしの円
    Ring, // リング
    Ellipse, // 楕円
  }

  // パーティクル管理(③)
  public static TokenMgr<Particle> parent;
  public static Particle Add(eType type, int timer, float px, float py, float direction, float speed)
  {
    Particle p = parent.Add(px, py, direction, speed);
    if(p == null)
    {
      return null;
    }

    // 初期化
    p.Init(type, timer);

    return p;
  }

  // メンバ変数定義(④)
  // 種別
  eType _type;

  // 消滅タイマー
  int _tDestroy;
  // 拡大タイマー
  const float SCALE_MAX = 4;
  float _tScale;

  /// 初期化
  void Init(eType type, int timer)
  {
    // スプライト設定(①)
    switch(type)
    {
    case eType.Ball:
      // 塗りつぶしの円
      SetSprite(spr0);
      break;

    case eType.Ring:
    case eType.Ellipse:
      // リング
      SetSprite(spr1);
      _tScale = SCALE_MAX;
      break;
    }
    _type = type;

    // タイマー設定(②)
    _tDestroy = timer;

    // 初期化(③)
    // スケール値を初期化
    Scale = 1.0f;
    // アルファ値を戻す
    Alpha = 1.0f;
  }

  /// 更新
  void Update ()
  {
    // 各種別ごとの処理(①)
    switch(_type)
    {
    case eType.Ball:
      // 速度を減衰する
      MulVelocity(0.9f);
      // 小さくする
      MulScale(0.93f);
      break;

    case eType.Ring:
      // スケール値を設定
      _tScale *= 0.9f;
      Scale = (SCALE_MAX - _tScale);
      // アルファ値を小さくする
      Alpha -= 0.05f;
      break;

    case eType.Ellipse:
      // スケール値を設定
      _tScale *= 0.9f;
      ScaleX = (SCALE_MAX - _tScale) * 2;
      ScaleY = (SCALE_MAX - _tScale);
      // アルファ値を小さくする
      Alpha -= 0.05f;
      break;
    }

    // 消滅チェック(②)
    _tDestroy--;
    if(_tDestroy < 1)
    {
      // 消滅
      Vanish();
    }
  }

}

