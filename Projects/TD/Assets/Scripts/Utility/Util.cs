using UnityEngine;
using System.Collections;

/// 様々なユーティリティ.
public class Util {
  /// Mathf.Cosの角度指定版.
	public static float CosEx(float Deg) {
		return Mathf.Cos(Mathf.Deg2Rad * Deg);
	}
  /// Mathf.Sinの角度指定版.
	public static float SinEx(float Deg) {
		return Mathf.Sin(Mathf.Deg2Rad * Deg);
	}

  /// 指定した2つのTokenの距離を取得する
  public static float DistanceBetween(Token t1, Token t2) {
    var dx = t2.X - t1.X;
    var dy = t2.Y - t1.Y;
    return Mathf.Sqrt(dx*dx + dy*dy);
  }

  /// t1からみたt2がいる方向を求める（単位は度）
  public static float AngleBetween(Token t1, Token t2) {
    var dx = t2.X - t1.X;
    var dy = t2.Y - t1.Y;
    return Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
  }


  /// 入力方向を取得する.
	public static Vector2 GetInputVector() {
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		return new Vector2(x, y).normalized;
	}

  /// トークンを動的生成する.
	public static Token CreateToken(float x, float y, string SpriteFile, string SpriteName, string objName="Token") {
		GameObject obj = new GameObject(objName);
		obj.AddComponent<SpriteRenderer>();
		obj.AddComponent<Rigidbody2D>();
		obj.AddComponent<Token>();

		Token t = obj.GetComponent<Token>();
    // スプライト設定.
		t.SetSprite(GetSprite(SpriteFile, SpriteName));
    // 座標を設定.
		t.X = x;
		t.Y = y;
    // 重力を無効にする.
		t.GravityScale = 0.0f;

		return t;
	}

  /// スプライトをリソースから取得する.
  /// ※スプライトは「Resources/Sprites」以下に配置していなければなりません.
  /// ※fileNameに空文字（""）を指定するとシングルスプライトから取得します.
	public static Sprite GetSprite(string fileName, string spriteName) {
		if(spriteName == "") {
			// シングルスプライト
			return Resources.Load<Sprite>(fileName);
		}
		else {
			// マルチスプライト
			Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
			return System.Array.Find<Sprite>(sprites, (sprite) =>  sprite.name.Equals(spriteName));
		}
	}

  /// プレハブ取得.
  /// プレハブは必ず"Resources/Prefabs/"に配置すること.
  public static GameObject GetPrefab (GameObject prefab, string name)
  {
    return prefab ?? (prefab = Resources.Load ("Prefabs/" + name) as GameObject);
  }

	private static Rect _guiRect = new Rect();
	static Rect GetGUIRect() {
		return _guiRect;
	} 
	private static GUIStyle _guiStyle = null;
	static GUIStyle GetGUIStyle() {
		return _guiStyle ?? (_guiStyle = new GUIStyle());
	}
  /// フォントサイズを設定.
	public static void SetFontSize(int size) {
		GetGUIStyle().fontSize = size;
	}
  /// フォントカラーを設定.
	public static void SetFontColor(Color color) {
		GetGUIStyle().normal.textColor = color;
	}
  /// フォント位置設定
  public static void SetFontAlignment(TextAnchor align)
  {
    GetGUIStyle().alignment = align;
  }
  /// ラベルの描画.
	public static void GUILabel(float x, float y, float w, float h, string text) {
		Rect rect = GetGUIRect();
		rect.x = x;
		rect.y = y;
		rect.width = w;
		rect.height = h;

		GUI.Label(rect, text, GetGUIStyle());
	}
  /// ボタンの配置.
  public static bool GUIButton(float x, float y, float w, float h, string text) {
    Rect rect = GetGUIRect();
    rect.x = x;
    rect.y = y;
    rect.width = w;
    rect.height = h;

    return GUI.Button(rect, text, GetGUIStyle());
  }
}
