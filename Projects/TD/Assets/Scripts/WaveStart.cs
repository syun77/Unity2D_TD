using UnityEngine;
using System.Collections;

/// ①Wave開始演出クラス
public class WaveStart : TextObj
{

  // ②状態定数
  enum eState
  {
    Appear,   // 出現
    Wait,     // 停止
    Disapper, // 退出
    End,      // おしまい
  }

  /// ③座標の定数
  // 中心座標(X)
  const float CENTER_X = 338;
  // 中心からのオフセット座標(X)
  const float OFFSET_X = 600;

  /// ④状態とタイマー
  // 状態
  eState _state = eState.End;
  // タイマー
  float _timer = 0;

  // ⑤初期化
  void Start()
  {
    // 画面外に出しておく
    X = CENTER_X + OFFSET_X;
    // 非表示にしておく
    Visible = false;
  }

  // ⑥演出開始
  public void Begin(int nWave)
  {
    // Wave数をテキストに設定
    Label = "Wave " + nWave;
    // 開始演出スタート
    _timer = OFFSET_X;
    _state = eState.Appear;
    // 表示する
    Visible = true;
  }

  // 更新
  void FixedUpdate()
  {
    switch (_state)
    {
      case eState.Appear:
        // ⑦出現中
        _timer *= 0.9f;
        X = CENTER_X - _timer;
        if (_timer < 1)
        {
          // 40フレーム停止する
          _timer = 40;
          _state = eState.Wait;
        }
        break;

      case eState.Wait:
        // ⑧停止中
        _timer -= 1;
        if (_timer < 1)
        {
          _timer = OFFSET_X;
          _state = eState.Disapper;
        }
        break;

      case eState.Disapper:
        // ⑨退出中
        _timer *= 0.9f;
        X = CENTER_X + (OFFSET_X - _timer);
        if (_timer < 1)
        {
          // おしまい
          _state = eState.End;
          // 非表示にしておく
          Visible = false;
        }
        break;

      case eState.End:
        // おしまい
        break;
    }
  }
}
