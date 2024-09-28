using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Connects a UI slider control to a video player, allowing users to scrub to a particular time in th video.
    /// </summary>
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoControllerScript : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("If checked, the slider will fade off after a few seconds. If unchecked, the slider will remain on.")]

        bool m_VideoIsPlaying;
        VideoPlayer m_VideoPlayer;

        void Start()
        {
            m_VideoPlayer = GetComponent<VideoPlayer>();
            if (!m_VideoPlayer.playOnAwake)
            {
                m_VideoPlayer.playOnAwake = true; // Set play on awake for next enable.
                m_VideoPlayer.Play(); // Play video to load first frame.
                VideoStop(); // Stop the video to set correct state and pause frame.
            }
            else
            {
                VideoPlay(); // Play to ensure correct state.
            }

        }


        public void PlayOrPauseVideo()
        {
            if (m_VideoIsPlaying)
            {
                VideoStop();
            }
            else
            {
                VideoPlay();
            }
        }


        void VideoStop()
        {
            m_VideoIsPlaying = false;
            m_VideoPlayer.Pause();
            // m_ButtonPlayOrPause.SetActive(true);
        }

        void VideoPlay()
        {
            m_VideoIsPlaying = true;
            m_VideoPlayer.Play();
            // m_ButtonPlayOrPause.SetActive(false);
        }

    }
}
