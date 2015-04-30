using UnityEngine;
using System.Collections;

/// ①敵のパラメータに関する情報を計算するクラス
public class EnemyParam
{

  /// ②敵のHPを取得する
  public static int Hp()
  {
    // 1 + (Wave数 / 3)
    return 1 + (Global.Wave / 3);
  }

  /// ③敵の速度を取得する
  public static float Speed()
  {
    // 3 + (0.1 * Wave数)
    return 3 + (0.1f * Global.Wave);
  }

  /// ④敵の所持金を取得する
  public static int Money()
  {
    if (Global.Wave < 5)
    {
      // Wave4以下は2
      return 2;
    }

    // 5以上は1
    return 1;
  }

  /// ⑤敵の出現数を取得する
  public static int GenerationNumber()
  {
    return 5 + Global.Wave;
  }

  /// ⑥敵の出現間隔を取得する
  public static float GenerationInterval()
  {
    // 1.5sec.
    return 1.5f;
  }
}
