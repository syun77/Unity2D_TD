using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// 敵の生成
public class EnemyGenerator
{
  /// ①メンバ変数を定義
  // 敵の移動経路
  List<Vec2D> _pathList;

  // 敵生成のインターバル
  float _interval;
  // 敵生成のインターバルタイマー
  float _tInterval;
  // 同一Wave内で敵を生成する数
  int _number;
  public int Number
  {
    get { return _number; }
  }

  /// ②コンストラクタ
  public EnemyGenerator(List<Vec2D> pathList)
  {
    _pathList = pathList;
  }

  // ③開始
  public void Start(int nWave)
  {
    // 出現間隔
    _interval = EnemyParam.GenerationInterval();
    _tInterval = 0;
    // 出現数
    _number = EnemyParam.GenerationNumber();
  }

  /// ④更新
  public void Update()
  {
    if(_number <= 0)
    {
      // ⑧すべての敵が出現したので何もしない
      return;
    }

    // ⑤経過時間を足し込む
    _tInterval += Time.deltaTime;
    // ⑥経過時間をチェック
    if(_tInterval >= _interval)
    {
      // インターバルを超えたので敵出現
      _tInterval -= _interval;
      // 敵を生成
      Enemy.Add(_pathList);
      // ⑦敵生成カウンタを減らす
      _number--;
    }
  }
}
