using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //オブジェクト、コンポーネント
    public Text textScore;
    public Text textMessage;
    public GameObject imageWin;
    public GameObject imageHukidashi;
    public Text textGirlTalk;
    //勝敗カットイン関連
    public GameObject imageCutin;
    public GameObject imagePlayerGu;
    public GameObject imagePlayerCyoki;
    public GameObject imagePlayerPa;
    public GameObject imageComGu;
    public GameObject imageComCyoki;
    public GameObject imageComPa;
    public GameObject textMessageWait;

    //変数宣言
    int playerHand;
    int computerHand;
    int winNum;             //勝ち数
    int loseNum;            //負け数
    //フラグ
    bool isFadeImageWin = false;
    bool isCutinWait = false;

	// Use this for initialization
	void Start () {
        playerHand = 0;
        computerHand = 0;
        winNum = 0;
        loseNum = 0;
        //オブジェクトの初期状態
        imageCutin.SetActive(false);
        imageWin.SetActive(false);
        textMessageWait.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //結果カットイン表示時のクリック待ち処理
        if (isCutinWait) {
            if (Input.GetMouseButtonDown(0)) {  //マウス左ボタン押下
                isCutinWait = false;
                imageCutin.SetActive(false);
                textMessageWait.SetActive(false);
                textMessage.text = "じゃん…けん…";
            } else {
                //クリック待ちカーソル表示
                textMessageWait.SetActive(true);
                float a = textMessageWait.GetComponent<Text>().color.a;
                a -= 0.01f;
                if (a < 0.5f) a = 1.0f;
                textMessageWait.GetComponent<Text>().color = new Color(1.0f, 1.0f, 1.0f, a);
            }
        }

        //勝利画像のフェード処理
        if (isFadeImageWin) {
            float a = imageWin.GetComponent<Image>().color.a;
            a -= 0.02f;
            imageWin.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, a);
            if (a <= 0) {
                isFadeImageWin = false;
                imageWin.SetActive(false);
            }
        }
    }

    //グーボタン押下処理
    public void PushGuButon() {
        playerHand = 1;
        SetPlayerHand(playerHand);
        computerHand = Random.Range(1, 4);  //1～3
        SetComHand(computerHand);
        switch (computerHand) {
            case 1:     //グー
                PlayerDraw();
                break;
            case 2:     //チョキ
                PlayerWin();
                break;
            case 3:     //パー
                PlayerLose();
                break;
            default:
                ComputerBerserk();
                break;
        }
    }

    //チョキボタン押下処理
    public void PushCyokiButton() {
        playerHand = 2;
        SetPlayerHand(playerHand);
        computerHand = Random.Range(1, 4);  //1～3
        SetComHand(computerHand);
        switch (computerHand) {
            case 1:     //グー
                PlayerLose();
                break;
            case 2:     //チョキ
                PlayerDraw();
                break;
            case 3:     //パー
                PlayerWin();
                break;
            default:
                ComputerBerserk();
                break;
        }
    }

    //パーボタン押下処理
    public void PushPaButton() {
        playerHand = 3;
        SetPlayerHand(playerHand);
        computerHand = Random.Range(1, 4);  //1～3
        SetComHand(computerHand);
        switch (computerHand) {
            case 1:     //グー
                PlayerWin();
                break;
            case 2:     //チョキ
                PlayerLose();
                break;
            case 3:     //パー
                PlayerDraw();
                break;
            default:
                ComputerBerserk();
                break;
        }
    }

    //勝ち処理
    void PlayerWin() {
        textMessage.text = "コンピューターは" + GetHandStr(computerHand) + "でした。\nあなたの勝ちです！";
        winNum++;
        UpdateScore();
        DispimageCutin();
        DispImageWin();
        CheckGirlTalk();
    }

    //負け処理
    void PlayerLose() {
        textMessage.text = "コンピューターは" + GetHandStr(computerHand) + "でした。\nあなたの負けです…";
        loseNum++;
        UpdateScore();
        DispimageCutin();
    }

    //引き分け処理
    void PlayerDraw() {
        textMessage.text = "コンピューターは" + GetHandStr(computerHand) + "でした。\n引き分けです。";
        DispimageCutin();
    }

    //成績更新
    void UpdateScore() {
        textScore.text = "プレイヤー：" + winNum + "勝" + loseNum + "敗";
    }

    //手IDの文字列を取得
    string GetHandStr(int handNum) {
        string handStr;
        switch(handNum) {
            case 1:
                handStr = "グー";
                break;
            case 2:
                handStr = "チョキ";
                break;
            case 3:
                handStr = "パー";
                break;
            default:
                handStr = "エラー";
                break;
        }
        return handStr;
    }

    //結果カットインの表示
    void DispimageCutin() {
        imageCutin.SetActive(true);
        isCutinWait = true;
    }

    //勝利画像の表示
    void DispImageWin() {
        imageWin.SetActive(true);
        imageWin.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        isFadeImageWin = true;
    }

    //結果カットインのプレイヤーの手
    void SetPlayerHand(int handId) {
        switch (handId) {
            case 1:     //グー
                imagePlayerGu.SetActive(true);
                imagePlayerCyoki.SetActive(false);
                imagePlayerPa.SetActive(false);
                break;
            case 2:     //チョキ
                imagePlayerGu.SetActive(false);
                imagePlayerCyoki.SetActive(true);
                imagePlayerPa.SetActive(false);
                break;
            case 3:     //パー
                imagePlayerGu.SetActive(false);
                imagePlayerCyoki.SetActive(false);
                imagePlayerPa.SetActive(true);
                break;
        }
    }

    //結果カットインのプレイヤーの手
    void SetComHand(int handId) {
        switch (handId) {
            case 1:     //グー
                imageComGu.SetActive(true);
                imageComCyoki.SetActive(false);
                imageComPa.SetActive(false);
                break;
            case 2:     //チョキ
                imageComGu.SetActive(false);
                imageComCyoki.SetActive(true);
                imageComPa.SetActive(false);
                break;
            case 3:     //パー
                imageComGu.SetActive(false);
                imageComCyoki.SetActive(false);
                imageComPa.SetActive(true);
                break;
        }
    }

    //女の子のセリフ
    void CheckGirlTalk() {
        imageHukidashi.SetActive(false);
        if (winNum == 10) {
            textGirlTalk.text = "10勝ね…";
            imageHukidashi.SetActive(true);
        }
        if (winNum == 20) {
            textGirlTalk.text = "ふーん…\n20勝したんだ";
            imageHukidashi.SetActive(true);
        }
        if (winNum == 30) {
            textGirlTalk.text = "へー…\n30勝ねー";
            imageHukidashi.SetActive(true);
        }
        if (winNum == 40) {
            textGirlTalk.text = "40勝かー\nやるじゃん！";
            imageHukidashi.SetActive(true);
        }
        if (winNum == 50) {
            textGirlTalk.text = "ちょw\n50勝とか！";
            imageHukidashi.SetActive(true);
        }
        if (winNum == 60) {
            textGirlTalk.text = "60勝て!?w\nすごくない!?";
            imageHukidashi.SetActive(true);
        }
        if (winNum == 70) {
            textGirlTalk.text = "70勝なんて…\n好きかも…";
            imageHukidashi.SetActive(true);
        }
        if (winNum == 80) {
            textGirlTalk.text = "80勝おめ！\nつきあおうよ！";
            imageHukidashi.SetActive(true);
        }
        if (winNum == 90) {
            textGirlTalk.text = "90勝だね！\nいつ結婚する？";
            imageHukidashi.SetActive(true);
        }
        if (winNum >= 100 && winNum <= 119) {
            textGirlTalk.text = "100勝ぉぉ…\nもう放さないよ…";
            imageHukidashi.SetActive(true);
        }
        if (winNum >= 120 && winNum <= 139) {
            textGirlTalk.text = "ねえ…\nお腹さわってみて";
            imageHukidashi.SetActive(true);
        }
        if (winNum >= 140 && winNum <= 159) {
            textGirlTalk.text = "がんばれ☆\nがんばれ☆";
            imageHukidashi.SetActive(true);
        }
        if (winNum >= 160 && winNum <= 179) {
            textGirlTalk.text = "全部私が\n面倒みるからね…";
            imageHukidashi.SetActive(true);
        }
        if (winNum >= 180 && winNum <= 199) {
            textGirlTalk.text = "そう…ずっと…\n押し続けて…";
            imageHukidashi.SetActive(true);
        }
        if (winNum >= 200) {
            textGirlTalk.text = "フフ…これでもう\n永遠に一緒だね…";
            imageHukidashi.SetActive(true);
        }
    }

    //コンピューター暴走
    void ComputerBerserk() {
        textMessage.text = "コンピューターは暴走しました。";
    }

}
