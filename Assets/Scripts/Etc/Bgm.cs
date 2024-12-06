using UnityEngine;
using DarkTonic.MasterAudio;

public class Bgm : MonoBehaviour
{
    void Start()
    {
        MasterAudio.StartPlaylist("Bgm");
    }

}
