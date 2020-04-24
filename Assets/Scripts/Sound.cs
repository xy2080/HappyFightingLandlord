using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoSingleton<Sound> {

    AudioSource m_Bg;
    AudioSource m_Effect;
    public string ResourceDir = "";
    protected override void Awake()
    {
        base.Awake();
        m_Bg = GetComponent<AudioSource>();
        m_Bg.loop = true;
        m_Bg.playOnAwake = false;
        m_Bg.volume = 0.5f;
        m_Effect = GetComponent<AudioSource>();
    }

    public void PlayBG(string name)
    {
        string oldName = "";
        if(m_Bg.clip==null)
        {
            oldName = "";
        }
        else
        {
            oldName = m_Bg.clip.name;
        }
        if(name!=oldName)
        {
            AudioClip clip = Resources.Load<AudioClip>(ResourceDir+"/"+name);
            if(clip!=null)
            {
                m_Bg.clip = clip;
                m_Bg.Play();
            }
            else
            {
                Debug.Log("没有当前音效"+ ResourceDir + "/" + name);
            }
        }
    }

    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(ResourceDir + "/" + name);
        if(clip!=null)
        m_Effect.PlayOneShot(clip);

        else
        Debug.Log("播放音效");
    }
}
