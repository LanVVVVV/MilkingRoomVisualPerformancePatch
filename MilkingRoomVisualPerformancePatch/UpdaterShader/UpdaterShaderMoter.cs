using DG.Tweening;
using MBMScripts;
using MilkingRoomVisualPerformancePatch.AssetBundles;
using UnityEngine;

namespace MilkingRoomVisualPerformancePatch.UpdaterShader;

public class UpdaterShaderMoter : Updater
{
    [SerializeField]
    private SpriteRenderer m_sr = null!;

    [SerializeField]
    private MaterialPropertyBlock m_block = null!;

    [SerializeField]
    private Reference m_moterReference = null!;

    [SerializeField]
    private Shader m_moterShader = ABLoader.ShaderMoterLiquid;

    [SerializeField]
    private float m_displayFill;

    [SerializeField]
    private float m_targetFill;

    [SerializeField]
    private float m_fillTime = 0.5f;

    public float FillTime
    {
        get => m_fillTime;
        set => m_fillTime = value;
    }

    protected override void Awake()
    {
        base.Awake();
        foreach (Reference reference in ReferenceArray)
        {
            if (reference.ReferenceType == EReferenceType.Float)
            {
                m_moterReference = reference;
            }
        }
        m_sr = GetComponent<SpriteRenderer>();
        m_sr.material = new Material(m_moterShader);
        m_sr.color = Color.white;

        m_block = new MaterialPropertyBlock();
        m_block.SetTexture("_MainTex", m_sr.sprite.texture);
        m_block.SetFloat("_Fill", 0);
        m_block.SetColor("_Color", Color.white);

        m_sr.SetPropertyBlock(m_block);
    }

    public void Init(Shader moterShader)
    {
        m_moterShader = moterShader;
        if (m_block is null) return;
        m_sr.material = new Material(moterShader);
        m_sr.SetPropertyBlock(m_block);
    }

    protected override void Display()
    {
        float targetFill = m_moterReference.GetFloat();
        if (targetFill == m_targetFill) return;
        m_targetFill = targetFill;
    }

    public void Update()
    {
        float diff = Mathf.Abs(m_displayFill - m_targetFill);

        if (diff < 0.001f)
        {
            if (diff != 0f) m_displayFill = m_targetFill;
            else if (m_displayFill == 0f) return;
        }
        else
        {
            m_displayFill = Mathf.Lerp(
                m_displayFill,
                m_targetFill,
                Time.unscaledDeltaTime / m_fillTime
                );
        }

        SetFill(m_displayFill);
    }

    public void SetFill(float displayfill)
    {
        m_displayFill = displayfill;
        m_block.SetFloat("_Fill", displayfill);
        m_sr.SetPropertyBlock(m_block);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        float targetFill = m_moterReference.GetFloat();
        SetFill(targetFill);
    }
}