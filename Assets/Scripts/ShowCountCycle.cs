using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 加減算カウンタークラス
/// 機能
///   比率カウント
///   直接カウント
///   敷き詰め、敷き詰める長さ
///   何秒に一度カウンタを回すか
/// </summary>
public class ShowCountCycle : MonoBehaviour {

    //敷き詰めタイプ
    public enum FillType
    {
        None            = 1,//何もしない
        Zero            = 2,//0で敷き詰める
        HalfWidthSpace  = 4//半角スペース
    };

    public FillType fillType = FillType.None;
    public int length = 5;                                  //文字の敷き詰める長さ
    public int directCount = 1;                             //直接加算する数値
    [Range(0.0f, 1.0f)] public float percent = 0.05f;       //比率で加算する
    [Range(0.0f, 1.0f)] public float invokeTime = 0.03f;    //何秒に一回呼ぶか

    private Text text;          //表示するテキスト
    private int show;           //表示する数値
    private int delta;          //差分
    private int next;           //数値の移動位置
    private bool isCounting;    //カウント中かどうか

    /// <summary>
    /// 初期化
    /// </summary>
    void Awake ()
    {
        isCounting = false;
        show = 0;
        next = 0;
        text = GetComponent<Text>();
        Show ();
    }

    /// <summary>
    /// 次の値が現在表示している方向から +方向か -方向かによって処理を分ける
    /// </summary>
    /// <value>The cycle.</value>
    private int cycle
    {
        get{
            if (delta > 0) {
                return (int)((float)delta * percent) + directCount;
            } else {
                return (int)((float)delta * percent) - directCount;
            }
        }
    }

    /// <summary>
    /// 目標値と同じ、または超えていたらtrueを返す
    /// </summary>
    /// <returns><c>true</c> if this instance is over; otherwise, <c>false</c>.</returns>
    private bool IsOver()
    {
        if (delta > 0) {
            return show >= next;
        } else {
            return show <= next;
        }
    }

    /// <summary>
    /// 敷き詰めタイプによって表示分けを行う
    /// </summary>
    private void Show()
    {
        switch (fillType) {
        case FillType.None:
            text.text = show.ToString ();
            break;
        case FillType.HalfWidthSpace:
            text.text = show.ToString().PadLeft(length);
            break;
        case FillType.Zero:
            text.text = show.ToString().PadLeft(length, '0');
            break;
        }
    }

    /// <summary>
    /// 一定時間置きに計算処理を行い表示する。
    /// </summary>
    private void ShowCoinCounter()
    {
        show += cycle;
        if (IsOver())
        {
            show = next;
            Show ();
            isCounting = false;
            return;
        }

        Show ();
        Invoke ("ShowCoinCounter", invokeTime);
    }

    /// <summary>
    /// 次に移動する数値を得てカウントを開始する
    /// </summary>
    /// <param name="next">Next.</param>
    public void GetNext (int next)
    {
        this.next = next;
        delta = next - show;
        if (isCounting || delta == 0)
        {
            return;
        }

        isCounting = true;
        ShowCoinCounter ();
    }
}