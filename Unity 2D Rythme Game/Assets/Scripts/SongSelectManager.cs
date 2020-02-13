using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SongSelectManager : MonoBehaviour
{
    public Image musicImageUI;
    public Text musicTitleUI;
    public Text bpmUI;

    private int musicIndex;
    private int musicCount = 3; // 현재 곡의 갯수

    private void UpdateSong(int musicIndex)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        //리소스에서 불러오기
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicIndex.ToString());
        StringReader stringReader = new StringReader(textAsset.text);
        //첫 번째 줄에 적힌 곡 이름을 읽어 UI 업데이트
        musicTitleUI.text = stringReader.ReadLine();
        //두 번째 줄은 읽기만하고 아무 처리 안함
        stringReader.ReadLine();
        //세 번째 줄에 적힌 BPM을 읽어 UI 업데이트
        bpmUI.text = "BPM: " + stringReader.ReadLine().Split(' ')[0];
        //리소스에서 불러오기
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicIndex.ToString());
        audioSource.clip = audioClip;
        audioSource.Play();
        //리소스에서 불러오기
        musicImageUI.sprite = Resources.Load<Sprite>("Beats/" + musicIndex.ToString());
    }

    public void Right()
    {
        musicIndex = musicIndex + 1;
        if (musicIndex > musicCount) musicIndex = 1;
        UpdateSong(musicIndex);
    }

    public void Left()
    {
        musicIndex = musicIndex - 1;
        if (musicIndex < 1) musicIndex = musicCount;
        UpdateSong(musicIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        musicIndex = 1;
        UpdateSong(musicIndex);
    }

    public void GameStart()
    {
        PlayerInformation.selectedMusic = musicIndex.ToString();
        SceneManager.LoadScene("GameScene");
    }
}
