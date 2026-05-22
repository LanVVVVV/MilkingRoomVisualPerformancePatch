using MBMScripts;
using MilkingRoomVisualPerformancePatch.AssetBundles;
using UnityEngine;

namespace MilkingRoomVisualPerformancePatch.UpdaterShader;

public class UpdaterShaderTank : Updater
{
    [SerializeField]
    private SpriteRenderer m_sr = null!;

    [SerializeField]
    private MaterialPropertyBlock m_block = null!;

    [SerializeField]
    private Reference m_tankReference = null!;

    [SerializeField]
    private Shader m_tankShader = ABLoader.ShaderTankLiquid;

    [SerializeField]
    private Sprite m_waveSprite = null!;

    [SerializeField]
    private float m_waveHeight = 0.06f;

    [SerializeField]
    private float m_waveSpeed = 1f;

    [SerializeField]
    private float m_waveBaseSpeed = 1f;

    [SerializeField]
    private float m_breakerSpeed = 10f;

    [SerializeField]
    private float m_displayFill;

    [SerializeField]
    private float m_targetFill;

    [SerializeField]
    private float m_fillTime = 1.5f;

    private float waveOffset = 0;

    public float WaveHeight
    {
        get => m_waveHeight;
        set
        {
            m_waveHeight = value;

            if (m_block is null)
                return;
            m_block.SetFloat("_WaveHeight", value);
            m_sr.SetPropertyBlock(m_block);
        }
    }

    public float WaveSpeed { get => m_waveBaseSpeed; set => m_waveBaseSpeed = value; }

    public float BreakerSpeed { get => m_breakerSpeed; set => m_breakerSpeed = value; }

    public float FillTime { get => m_fillTime; set => m_fillTime = value; }

    protected override void Awake()
    {
        base.Awake();
        foreach (Reference reference in ReferenceArray)
        {
            if (reference.ReferenceType == EReferenceType.Float)
            {
                m_tankReference = reference;
            }
        }
        m_sr = GetComponent<SpriteRenderer>();
        m_sr.material = new Material(m_tankShader);
        m_sr.color = Color.white;

        m_block = new MaterialPropertyBlock();
        m_block.SetTexture("_MainTex", m_sr.sprite.texture);
        m_block.SetTexture("_WaveTex", m_waveSprite.texture);
        m_block.SetFloat("_WaveHeight", m_waveHeight);
        m_block.SetFloat("_Fill", 0);
        m_block.SetFloat("_WaveOffset", 0);
        m_block.SetColor("_Color", Color.white);

        m_sr.SetPropertyBlock(m_block);
    }

    public void Init(Shader shader, Sprite seamlessSprite)
    {
        m_tankShader = shader;
        m_waveSprite = seamlessSprite;
        if (m_block is null)
            return;
        m_sr.material = new Material(shader);
        m_block.SetTexture("_WaveTex", seamlessSprite.texture);
        m_sr.SetPropertyBlock(m_block);
    }

    protected override void Display()
    {
        float targetFill = m_tankReference.GetFloat();
        if (targetFill == m_targetFill)
            return;
        m_targetFill = targetFill;
    }

    public void SetFill(float displayfill) { m_displayFill = displayfill; }

    public void Update()
    {
        float diff = Mathf.Abs(m_displayFill - m_targetFill);

        if (diff < 0.001f)
        {
            if (diff != 0f) m_displayFill = m_targetFill;
            else if (m_displayFill == 0f) return;

            if (m_waveSpeed != m_waveBaseSpeed) m_waveSpeed = m_waveBaseSpeed;
        }
        else
        {
            m_displayFill = Mathf.Lerp(
                m_displayFill,
                m_targetFill,
                Time.unscaledDeltaTime / m_fillTime
                );

            float targetSpeed = Mathf.Lerp(m_waveBaseSpeed, m_breakerSpeed, 1f - Mathf.Pow(1f - diff, 10f));
            m_waveSpeed = Mathf.Lerp(m_waveSpeed, targetSpeed, 1f - Mathf.Exp(-10f * Time.unscaledDeltaTime));
        }

        waveOffset += m_waveSpeed * Time.unscaledDeltaTime * 0.5f;
        waveOffset -= Mathf.Floor(waveOffset);

        m_block.SetFloat("_Fill", m_displayFill);
        m_block.SetFloat("_WaveOffset", -waveOffset);
        m_sr.SetPropertyBlock(m_block);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        float targetFill = m_tankReference.GetFloat();
        SetFill(targetFill);
    }
}