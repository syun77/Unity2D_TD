using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// uGUI Button操作モジュール
/// </summary>
public class ButtonObj : MonoBehaviour {

  /// uGUI Button
  Button _button = null;

  /// uGUI Text
  // ※有効にするための条件
  // ・Textはボタンの直下の階層にあること
  // ・オブジェクト名に"Text"という文字が含まれていること
  Text _text = null;
  public string Label
  {
    get { return _text.text; }
    set { _text.text = value; }
  }
  /// 書式付きでテキストを設定する
  public void FormatLabel(string format, params object[] arg0)
  {
    Label = string.Format(format, arg0);
  }

  /// 表示フラグ
  public bool Visible
  {
    get { return enabled; }
    set { enabled = value; }
  }

  /// 有効フラグ
  public bool Enabled
  {
    get { return _button.interactable; }
    set { _button.interactable = value; }
  }

	void Start () {
    // ボタンを取得
    _button = GetComponent<Button>();

    // 下の階層にあるTextを取得する
    foreach(Transform child in transform) {
      if(child.name.Contains("Text")) {
        // 対象のオブジェクトが見つかった
        _text = child.GetComponent<Text>();
        break;
      }
    }
	}
}
