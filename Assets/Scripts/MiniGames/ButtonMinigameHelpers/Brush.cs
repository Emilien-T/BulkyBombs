using AudioSystem;
using Enums;
using System;
using System.Collections;
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
    public GameObject button;
    public AnimationCurve buttonPressCurve;

    private bool isBuffing;
    private Vector2 moveDir;
    private Coroutine buttonPressCo;
    public Animator animator;

    public bool polishing = false;
    private SoundBuilder polishingSound;
    void Start()
    {
        InputController.Instance.button1 += OnButtonDown;
        InputController.Instance.directionalControls += DirectionalControls;
        // Initialize to white so multiply starts at 1
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = paintTexture;
        GL.Clear(false, true, Color.white);
        RenderTexture.active = prev;
        targetObject.GetComponent<Renderer>().material.SetTexture("_BuffMask", paintTexture);
        targetObject.GetComponent<Renderer>().material.SetFloat("_AlphaMult", 1);

    }
    private void OnButtonDown(bool isDown)
    {
        if (isBuffing == isDown) return;
        if (!isBuffing) 
        {
            animator.SetTrigger("Idle");
            polishing = true;
            polishingSound = AudioLibrary.Instance.Polish();
        }
        else
        {
            animator.SetTrigger("Bob");
            if (polishing == true) polishingSound?.Stop();
            polishing = false;
        }
            isBuffing = isDown;
    }
    private void DirectionalControls(Vector2 dir)
    {
        moveDir = dir;
    }
    void Update()
    {
        if (minigameManager.bombController.currentMinigame != MinigameType.Button || minigameManager.completed || minigameManager.lost)
        {
            if (polishing == true) polishingSound?.Stop();
            polishing = false;
            return;
        }
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
            if (buttonPressCo == null) 
            {
                buttonPressCo = StartCoroutine(lowerButtonRoutine());
            }
            minigameManager.LoseGame();
        }
    }
    private IEnumerator lowerButtonRoutine() 
    {
        float timer = 0;
        Vector3 finalPos = button.transform.localPosition + new Vector3(0,-0.2f,0);
        Vector3 startPos = button.transform.localPosition;
        while (timer < 0.1f) 
        {
            timer += Time.deltaTime;
            float t = buttonPressCurve.Evaluate(timer/0.4f);
            button.transform.localPosition = Vector3.LerpUnclamped(startPos, finalPos, t);
            yield return null;
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

        InputController.Instance.button1 -= OnButtonDown;
        InputController.Instance.directionalControls -= DirectionalControls;
    }

    Coroutine cleanupRoutine;
    public void AutoCleanup()
    {
        if (cleanupRoutine != null)
            StopCoroutine(cleanupRoutine);

        cleanupRoutine = StartCoroutine(AutoCleanupRoutine());
    }
    IEnumerator AutoCleanupRoutine()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            targetObject.GetComponent<Renderer>().material.SetFloat("_AlphaMult", Mathf.Lerp(1,0,timer/0.5f));
            yield return null;
        }

        cleanupRoutine = null;
    }
}