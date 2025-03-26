using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrawOnRendererTexture : MonoBehaviour
{
    public float moveDownSpeed;

    public int hitTextureSize = 64;
    public Texture2D hitTexture;
    public Texture2D dotsTexture;

    public MeshRenderer meshRenderer;

    public LayerMask layerMask;

    private RenderTexture _newRT;

    private float _widthScale;
    private Material _newMaterial;


    private void Start()
    {
        _widthScale = 1f / transform.localScale.x;

        _newRT = new RenderTexture(512, 512, 32, RenderTextureFormat.ARGB32);
        _newRT.Create();

        _newMaterial = new Material(meshRenderer.material);
        meshRenderer.material = _newMaterial;

        _newMaterial.SetTexture("_RenderTex", _newRT);
        ClearRenderTexture(_newRT);
    }


    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = (other.transform.position - Camera.main.transform.position).normalized;
        if (Physics.Raycast(Camera.main.transform.position, direction, out RaycastHit hit, 100f, layerMask))
        {
            StartCoroutine(HandleHitEffect(new Vector2(hit.textureCoord.x, 1 - hit.textureCoord.y)));
        }
    }

    private IEnumerator HandleHitEffect(Vector2 uv)
    {
        // Draw the initial texture at the hit point
        DrawTexture(uv);

        // Wait for the specified delay before moving the texture down
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

        float speed = 0f;
        float speedLerp = 0;

        float maxSpeedLerp = Random.Range(0.5f, 1.5f);

        while (uv.y < 0.99f)
        {
            speed = Mathf.Lerp(speed, moveDownSpeed, speedLerp / maxSpeedLerp);
            speedLerp += Time.deltaTime;

            uv.y -= speed * Time.deltaTime;
            DrawDots(uv);
            yield return new WaitForSeconds(0.01f);
        }

        yield return null; // Continue updating until you stop the coroutine
    }

    void DrawTexture(Vector2 uv)
    {
        RenderTexture.active = _newRT;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, _newRT.width, _newRT.height, 0);

        float posX = uv.x * _newRT.width - (hitTextureSize * _widthScale) / 2;
        float posY = (1 - uv.y) * _newRT.height - hitTextureSize / 2;

        Graphics.DrawTexture(new Rect(posX, posY, hitTextureSize * _widthScale, hitTextureSize), hitTexture);

        GL.PopMatrix();
        RenderTexture.active = null;
    }

    void DrawDots(Vector2 uv)
    {
        RenderTexture.active = _newRT;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, _newRT.width, _newRT.height, 0);

        float posX = uv.x * _newRT.width - (hitTextureSize * _widthScale) / 2;
        float posY = (1 - uv.y) * _newRT.height - hitTextureSize / 2;

        Graphics.DrawTexture(new Rect(posX, posY, hitTextureSize * _widthScale, hitTextureSize), dotsTexture);

        GL.PopMatrix();
        RenderTexture.active = null;
    }

    void ClearRenderTexture(RenderTexture rt)
    {
        RenderTexture.active = rt;
        GL.Clear(true, true, new Color(0, 0, 0, 0));
        RenderTexture.active = null;
    }
}