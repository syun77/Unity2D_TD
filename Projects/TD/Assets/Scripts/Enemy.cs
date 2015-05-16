using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// 敵クラス
public class Enemy : Token
{
  /// 管理オブジェクト
  public static TokenMgr<Enemy> parent = null;

  // プレハブから敵を至誠
  public static Enemy Add(List<Vec2D> path)
  {
    Enemy e = parent.Add(0, 0);
    if(e == null)
    {
      return null;
    }
    e.Init(path);
    return e;
  }

	// アニメーション用のスプライト
	public Sprite spr0;
	public Sprite spr1;

  // HP
  int _hp;

  // 所持金
  int _money;

	// アニメーションタイマー
	int _tAnim = 0;

  // 速度パラメータ
  float _speed = 0; // 速度
  float _tSpeed = 0; // 補完値(0.0〜100.0)
  // 経路座標のリスト
  List<Vec2D> _path;
  // 経路の現在の番号
  int _pathIdx;
  // チップ座標
  Vec2D _prev; // 1つ前
  Vec2D _next; // 1つ先

  /// 画像の角度を更新
  void UpdateAngle()
  {
    float dx = _next.x - _prev.x;
    float dy = _next.y - _prev.y;
    Angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
  }

  /// 初期化
  public void Init(List<Vec2D> path)
  {
    // 経路をコピー
    _path = path;
    _pathIdx = 0;
    // 移動速度
    _speed = EnemyParam.Speed();
    _tSpeed = 0;

    // 移動先を更新
    MoveNext();
    // _prevに反映する
    _prev.Copy(_next);
    // 一つ左にずらす
    _prev.x -= Field.GetChipSize();
    // 一度座標を更新しておく
    FixedUpdate();

    // HPを設定する
    _hp = EnemyParam.Hp();

    // 所持金を設定
    _money = EnemyParam.Money();
  }

	// アニメーション更新
	void FixedUpdate()
	{
		_tAnim++;
		if(_tAnim%32 < 16)
		{
			SetSprite(spr0);
		}
		else
		{
			SetSprite(spr1);
		}

    // 速度タイマー更新
    _tSpeed += _speed;
    if(_tSpeed >= 100.0f) {
      // 移動先を次に進める
      _tSpeed -= 100.0f;
      MoveNext();
    }

    // ⑤速度タイマーに対応する位置に線形補間で移動する
    X = Mathf.Lerp(_prev.x, _next.x, _tSpeed / 100.0f);
    Y = Mathf.Lerp(_prev.y, _next.y, _tSpeed / 100.0f);

    // 画像の角度を更新
    UpdateAngle();
	}

  /// 次の移動先に進める
  void MoveNext()
  {
    if(_pathIdx >= _path.Count)
    {
      // ゴールにたどりついた
      _tSpeed = 100.0f;
      // ダメージを与える
      Global.Damage();
      // 自爆する
      Vanish();

      return;
    }
    // ⑧移動先を移動元にコピーする
    _prev.Copy(_next);

    // ⑦チップ座標を取り出す
    Vec2D v = _path[_pathIdx];
    _next.x = Field.ToWorldX(v.X);
    _next.y = Field.ToWorldY(v.Y);
    // パス番号を進める
    _pathIdx++;
  }

  /// 衝突判定
  void OnTriggerEnter2D(Collider2D other)
  {
    // レイヤー名を取得する
    string name = LayerMask.LayerToName(other.gameObject.layer);
    if(name == "Shot")
    {
      // ショットと衝突
      Shot s = other.gameObject.GetComponent<Shot>();
      // ショット消滅
      s.Vanish();

      // ダメージ処理をする
      Damage(s.Power);

      if (Exists == false)
      {
        // 所持金を増やす
        Global.AddMoney(_money);
      }
    }
  }

  /// ダメージを受けた
  void Damage(int val)
  {
    // HPを減らす
    _hp -= val;
    if(_hp <= 0)
    {
      // HPがなくなったので死亡
      Vanish();
    }
  }

  /// 消滅
  public override void Vanish()
  {
    // パーティクル生成
    // リングエフェクト生成
    {
      // 生存時間は30フレーム。移動はしない
      Particle p = Particle.Add(Particle.eType.Ring, 30, X, Y, 0, 0);
      if(p)
      {
        // 明るい緑
        p.SetColor(0.7f, 1, 0.7f);
      }
    }

    // ボールエフェクト生成
    float dir = Random.Range(35, 55);
    for(int i = 0; i < 8; i++)
    {
      // 消滅フレーム数
      int timer = Random.Range(20, 40);
      // 移動速度
      float spd = Random.Range(0.5f, 2.5f);
      Particle p = Particle.Add(Particle.eType.Ball, timer, X, Y, dir, spd);
      // 移動方向
      dir += Random.Range(35, 55);
      if(p)
      {
        // 緑色を設定
        p.SetColor(0.0f, 1, 0.0f);
        // 大きさを設定
        p.Scale = 0.8f;
      }
    }

    // 親の消滅処理を呼び出す
    base.Vanish();
  }

}
