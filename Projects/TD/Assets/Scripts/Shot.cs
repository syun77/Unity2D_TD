using UnityEngine;
using System.Collections;

/// ショット
public class Shot : Token
{
  // ショットオブジェクト管理
  public static TokenMgr<Shot> parent;
  // ショットを撃つ
  //public static Shot Add(float px, float py, float direction, float speed)
  public static Shot Add(float px, float py, float direction, float speed, int power)
  {
    Shot s = parent.Add(px, py, direction, speed);
    if(s == null)
    {
      return null;
    }
    s.Init(power);
    return s;
  }

  /// ショットの威力 (※追加)
  int _power;
  public int Power
  {
    get { return _power; }
  }

  /// 初期化 (※追加)
  public void Init(int power)
  {
    _power = power;
  }

  void Update()
  {
    if (IsOutside())
    {
      // 画面外に出たので消滅
      Vanish();
    }
  }

  /// 消滅 (①)
  public override void Vanish()
  {
    // パーティクル生成
    for (int i = 0; i < 4; i++)
    {
      int timer = Random.Range(20, 40);
      // 反対方向に飛ばす (②)
      float dir = Direction - 180 + Random.Range(-60, 60);
      float spd = Random.Range(1.0f, 1.5f);
      Particle p = Particle.Add(Particle.eType.Ball, timer, X, Y, dir, spd);
      if (p)
      {
        // 小さくする
        p.Scale = 0.6f;
        // 赤色にする
        p.SetColor(1, 0.0f, 0.0f);
      }
    }
    // 親クラスの消滅処理を呼び出す
    base.Vanish();
  }
}