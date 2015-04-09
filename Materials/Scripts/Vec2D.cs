using UnityEngine;
using System.Collections;

/// 2次元ベクトル
public struct Vec2D {
  public float x;
  public float y;
  public int X {
    get { return (int)x; }
  }
  public int Y {
    get { return (int)y; }
  }
  public Vec2D(float px, float py) {
    x = px;
    y = py;
  }
  public void Set(float px, float py) {
    x = px;
    y = py;
  }
  public void Copy(Vec2D v) {
    Set(v.x, v.y);
  }
  public void Dump() {
    Debug.LogFormat("Vec2D (x,y)=({0},{1})", x, y);
  }
}
