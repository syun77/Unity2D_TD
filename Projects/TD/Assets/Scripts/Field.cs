using UnityEngine;
using System.Collections;
using System.Collections.Generic; // ←※①ここを追加する

/// フィールド情報の管理
public class Field : Token
{
  /// 何もない
  public const int CHIP_NONE = 0;
  /// 開始地点
  public const int CHIP_PATH_START = 26;

  /// パス（座標リスト）
  List<Vec2D> _path;
  public List<Vec2D> Path
  {
    get { return _path; }
  }

  /// コリジョンレイヤー
  Layer2D _lCollision;
  public Layer2D lCollision
  {
    get { return _lCollision; }
  }

  /// チップ1マスのサイズを取得する
  public static float GetChipSize()
  {
    var spr = GetChipSprite();
    return spr.bounds.size.x;
  }

  /// チップサイズの基準となるスプライトを取得する
  static Sprite GetChipSprite()
  {
    return Util.GetSprite("Levels/tileset", "tileset_0");
  }

  /// チップ座標をワールドのX座標を取得する.
  public static float ToWorldX(int i)
  {
    // カメラビューの左下の座標を取得
    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    var spr = GetChipSprite();
    var sprW = spr.bounds.size.x;

    return min.x + (sprW * i) + sprW / 2;
  }

  /// チップ座標をワールドのY座標を取得する.
  public static float ToWorldY(int j)
  {
    // カメラビューの右上の座標を取得する
    Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    var spr = GetChipSprite();
    var sprH = spr.bounds.size.y;

    // Unityでは上下逆になるので、逆さにして変換
    return max.y - (sprH * j) - sprH / 2;
  }

  /// ワールド座標をチップ座標系のX座標に変換する.
  public static int ToChipX(float x)
  {
    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    var spr = GetChipSprite();
    var sprW = spr.bounds.size.x;

    return (int)((x - min.x) / sprW);
  }

  /// ワールド座標をチップ座標系のX座標に変換する.
  public static int ToChipY(float y)
  {
    Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    var spr = GetChipSprite();
    var sprH = spr.bounds.size.y;

    return (int)((y - max.y) / -sprH);
  }

  public void Load()
  {
    // マップ読み込み
    TMXLoader tmx = new TMXLoader();
    tmx.Load("Levels/map");

    // 経路レイヤーを取得
    Layer2D lPath = tmx.GetLayer("path");
    // 開始地点を検索
    Vec2D pos = lPath.Search(CHIP_PATH_START);
    // 座標リストを作成
    _path = new List<Vec2D>();
    // 開始座標を座標リストに登録
    _path.Add(new Vec2D(pos.X, pos.Y));
    // 通路をふさぐ
    lPath.Set(pos.X, pos.Y, CHIP_NONE);
    // 座標リスト作成
    CreatePath(lPath, pos.X, pos.Y, _path);

    // コリジョンレイヤーを取得する
    _lCollision = tmx.GetLayer("collision");
  }

  /// パスを作る
  void CreatePath(Layer2D layer, int x, int y, List<Vec2D> path)
  {
    // 左・上・右・下を調べる
    int[] xTbl = { -1, 0, 1, 0 };
    int[] yTbl = { 0, -1, 0, 1 };
    for (var i = 0; i < xTbl.Length; i++)
    {
      int x2 = x + xTbl[i];
      int y2 = y + yTbl[i];
      int val = layer.Get(x2, y2);
      if (val > CHIP_NONE)
      {
        // 経路を発見
        // 経路をふさぐ
        layer.Set(x2, y2, CHIP_NONE);
        // 座標を追加
        path.Add(new Vec2D(x2, y2));
        // パス生成を再帰呼び出し
        CreatePath(layer, x2, y2, path);
      }
    }
  }
}
