using UnityEngine;
using System.Collections;

/// グローバル情報
public class Global
{
  /// 初期化
  public static void Init()
  {
    _wave = 1;
    _money = MONEY_INIT;
    // ライフ初期化
    _life = LIFE_INIT;
  }

  /// 所持金
  // 初期値
  const int MONEY_INIT = 30;
  static int _money;
  public static int Money
  {
    get { return _money; }
  }
  // 所持金を増やす
  public static void AddMoney(int v)
  {
    _money += v;
  }
  // お金を使う
  public static void UseMoney(int v)
  {
    _money -= v;
    if (_money < 0)
    {
      _money = 0;
    }
  }

  /// ライフ
  // 初期値
  const int LIFE_INIT = 3;
  // 最大値
  public const int LIFE_MAX = 3;
  static int _life;
  public static int Life
  {
    get { return _life; }
  }
  public static void Damage()
  {
    // ライフを1つ減らす
    _life--;
    if (_life < 0)
    {
      _life = 0;
    }
  }

  /// Wave数
  static int _wave = 1;
  public static int Wave
  {
    get { return _wave; }
  }
  /// Wave数を次に進める
  public static void NextWave()
  {
    _wave++;
  }
}
