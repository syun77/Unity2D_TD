using UnityEngine;
using System.Collections;

/// 射程範囲カーソル
public class CursorRange : Token
{
  /// 射程範囲の表示設定
  public void SetVisible(bool b, int lvRange)
  {
    // レベルから射程範囲を取得
    float range = TowerParam.Range(lvRange);

    // 表示上のサイズを設定
    Scale = range / (1.5f * Field.GetChipSize()) * 5f;

    // 表示フラグを設定
    Visible = b;
  }
}