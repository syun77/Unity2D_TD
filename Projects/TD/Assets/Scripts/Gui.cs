using UnityEngine;
using System.Collections;

public class Gui
{
  // Wave数テキスト
  TextObj _txtWave;
  // 所持金テキスト
  TextObj _txtMoney;
  // コストテキスト
  TextObj _txtCost;
  // 購入ボタン
  ButtonObj _btnBuy;
  // タワー情報テキスト
  TextObj _txtTowerInfo;
  /// アップグレードボタン
  // 射程範囲
  ButtonObj _btnRange;
  // 連射速度
  ButtonObj _btnFirerate;
  // 攻撃威力
  ButtonObj _btnPower;

  /// コンストラクタ
  public Gui()
  {
    // Wave数
    _txtWave = MyCanvas.Find<TextObj>("TextWave");
    // 所持金テキスト
    _txtMoney = MyCanvas.Find<TextObj>("TextMoney");
    // コストテキスト
    _txtCost = MyCanvas.Find<TextObj>("TextCost");
    _txtCost.Label = "";
    // 購入ボタン
    _btnBuy = MyCanvas.Find<ButtonObj>("ButtonBuy");
    // タワー情報を取得する
    _txtTowerInfo = MyCanvas.Find<TextObj>("TextTowerInfo");
    // 射程範囲ボタン
    _btnRange = MyCanvas.Find<ButtonObj>("ButtonRange");
    // 連射速度ボタン
    _btnFirerate = MyCanvas.Find<ButtonObj>("ButtonFirerate");
    // 攻撃威力ボタン
    _btnPower = MyCanvas.Find<ButtonObj>("ButtonPower");
  }

  /// 更新
  public void Update(GameMgr.eSelMode selMode, Tower tower)
  {
    // Wave数を更新
    _txtWave.SetLabelFormat("Wave: {0}", Global.Wave);
    _txtMoney.SetLabelFormat("Money: ${0}", Global.Money);
    // 生産コストを取得する
    int cost = Cost.TowerProduction();
    _txtCost.Label = "";
    if (selMode == GameMgr.eSelMode.Buy)
    {
      // 購入モードのみテキストを設定する
      _txtCost.SetLabelFormat("(cost ${0})", cost);
    }
    // 購入ボタンを押せるかどうかチェック
    _btnBuy.Enabled = (Global.Money >= cost);
    // 購入コストを表示する
    _btnBuy.FormatLabel("Buy (${0})", cost);
    // ライフ表示
    for (int i = 0; i < Global.LIFE_MAX; i++)
    {
      bool b = (Global.Life > i);
      MyCanvas.SetActive("ImageLife" + i, b);
    }

    if (selMode == GameMgr.eSelMode.Upgrade)
    {
      // 選択しているタワーの情報を表示する
      _txtTowerInfo.SetLabelFormat(
        "<<Tower Info>>\n  Range: Lv{0}\n  Firerate: Lv{1}\n  Power: Lv{2}",
        tower.LvRange,
        tower.LvFirerate,
        tower.LvPower
      );

      // アップグレードボタン更新
      int money = Global.Money;
      // 射程範囲
      _btnRange.Enabled = (money >= tower.CostRange);
      _btnRange.FormatLabel("Range (${0})", tower.CostRange);
      // 連射速度
      _btnFirerate.Enabled = (money >= tower.CostFirerate);
      _btnFirerate.FormatLabel("Firerate (${0})", tower.CostFirerate);
      // 攻撃威力
      _btnPower.Enabled = (money >= tower.CostPower);
      _btnPower.FormatLabel("Power (${0})", tower.CostPower);
    }
  }
}
