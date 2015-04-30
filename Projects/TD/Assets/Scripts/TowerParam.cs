using UnityEngine;
using System.Collections;

/// タワーのパラメータ計算クラス
public class TowerParam
{
  /// 射程範囲
  public static float Range(int lv)
  {
    // チップサイズ + (0.5 * チップサイズ * lv)
    float size = Field.GetChipSize();
    return size + (0.5f * size * lv);
  }

  /// 連射速度
  public static float Firerate(int lv)
  {
    // 2sec * (0.9f ^ (lv - 1))
    return 2.0f * (Mathf.Pow(0.9f, (lv - 1)));
  }

  /// 攻撃威力
  public static int Power(int lv)
  {
    // 1 * lv
    return 1 * lv;
  }
}
