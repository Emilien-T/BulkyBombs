using System;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public RenderTexture paintTexture;
    public Material brushMaterial;
    public GameObject targetObject;  // drag the paintable object here
    public LayerMask gunkLayer;
    public LayerMask buttonLayer;
    public ButtonMinigame minigameManager;
    public event Action<int> pixelsLeft;
    public float MoveSensitivity;
    public float brushSize;

    private bool isBuffing;
    private Vector2 moveDir;

    void Start()
    {
        InputController.Instance.button0 += OnButtonDown;
        InputController.Instance.directionalControls += DirectionalControls;
        // Initialize to white so multiply starts at 1
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = paintTexture;
        GL.Clear(false, true, Color.white);
        RenderTexture.active = prev;
        targetObject.GetComponent<Renderer>().material.SetTexture("_BuffMask", paintTexture);

    }
    private void OnButtonDown(bool isDown)
    {
        isBuffing = isDown;
    }
    private void DirectionalControls(Vector2 dir) 
    {
        moveDir = dir;
    }
    void Update()
    {
        if(!minigameManager.isFocused || minigameManager.completed) return;
        transform.localPosition += new Vector3(moveDir.x, 0, moveDir.y) * MoveSensitivity * Time.deltaTime;
        if (!isBuffing) return;

        Vector3 direction = transform.position - Camera.main.transform.position;
        Ray ray = new Ray(Camera.main.transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gunkLayer))
        {
            if (hit.collider.gameObject == targetObject)
            {
                PaintAt(hit.textureCoord);
            }
        }

        if (Physics.Raycast(ray, Mathf.Infinity, buttonLayer)) 
        {
            minigameManager.LoseGame();
        }
    }

    void PaintAt(Vector2 uv)
    {
        brushMaterial.SetVector("_BrushUV", new Vector4(uv.x, uv.y, 0, 0));
        brushMaterial.SetFloat("_BrushSize", brushSize);

        RenderTexture temp = RenderTexture.GetTemporary(paintTexture.descriptor);
        Graphics.Blit(paintTexture, temp);
        Graphics.Blit(temp, paintTexture, brushMaterial);
        RenderTexture.ReleaseTemporary(temp);

        // Count remaining undrawn pixels
        RenderTexture.active = paintTexture;
        Texture2D readback = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        readback.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        readback.Apply();
        RenderTexture.active = null;

        Color[] pixels = readback.GetPixels();
        int remaining = 0;
        foreach (var p in pixels)
            if (p.a > 0.1f) remaining++;

        pixelsLeft?.Invoke(remaining);
        Debug.Log("pixels left: " + remaining);

        Destroy(readback);
    }
    private void OnDisable()
    {

        InputController.Instance.button0 -= OnButtonDown;
        InputController.Instance.directionalControls -= DirectionalControls;
    }
}