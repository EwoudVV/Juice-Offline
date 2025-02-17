using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[RequireComponent(typeof(VideoPlayer))]
public class VideoLooper : MonoBehaviour
{
    public VideoClip videoClip;
    public RawImage rawImage;

    private VideoPlayer videoPlayer;
    private RenderTexture renderTexture;
    private bool textureCreated;

    void OnEnable()
    {
        if (videoClip == null || rawImage == null) return;

        if (renderTexture == null)
        {
            renderTexture = new RenderTexture((int)videoClip.width, (int)videoClip.height, 24);
            renderTexture.Create();
            textureCreated = true;
        }

        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = renderTexture;
        videoPlayer.isLooping = true;

        rawImage.texture = renderTexture;
        rawImage.uvRect = new Rect(1f / videoClip.width, 1f / videoClip.height, 
                                1f - (2f / videoClip.width), 
                                1f - (2f / videoClip.height));

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += Prepared;
    }

    void OnDisable()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.prepareCompleted -= Prepared;
        }

        if (textureCreated && renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
            renderTexture = null;
        }
    }

    void Prepared(VideoPlayer vp)
    {
        vp.Play();
    }

    void OnDestroy()
    {
        if (textureCreated && renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
        }
    }
}